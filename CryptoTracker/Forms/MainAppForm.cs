#define DEBUG

using System;
using System.Windows.Forms;
using System.Timers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Linq;
using LiveCharts; //Core of the library
using LiveCharts.Wpf; //The WPF controls
using LiveCharts.WinForms; //the WinForm wrappers
using System.Data;
using ExcelDataReader;
using System.Threading;
using System.Threading.Tasks;



//Crypto Images
//https://github.com/cjdowner/cryptocurrency-icons

//Good stock computer icons
//https://www.shareicon.net/motherboard-device-hardware-chip-graphic-card-smps-112175

//Crypto history api
//https://www.cryptocompare.com/api/#-api-data-pricehistorical-

namespace CryptoTracker
{
    public partial class MainAppForm : MetroFramework.Forms.MetroForm
    {
        //Enumerations*************************************************************************
        /// <summary>
        /// Enumeration for each row in textbox array
        /// </summary>
        public enum TextBoxRowNames
        {
            Quantity,
            TotalInvested,
            Value,
            Profit,
            ProfitPercent
        }

        //Fields********************************************************************************
        ToolTip toolTip = new ToolTip();
        PriceManager priceManager;
        System.Timers.Timer updatePrices;

        //UI Lists and Tables
        List<Label> priceLabelList = new List<Label>(); //List of labels to iterate through when updating prices
        List<MetroFramework.Controls.MetroTextBox[]> textBoxArrayList = new List<MetroFramework.Controls.MetroTextBox[]>(); //Array of textboxes for each coin, stored in a list
        DataTable tableBindToDataGridView = new DataTable(); //Contains both unsaved and saved data which is bound to data grid view for viewing
        DataTable unsavedTradesDataTable = new DataTable(); //Contains unsaved data that has not yet been written to file, used to avoid adding duplicate trades to price tracking totals

        //MainApp fields
        UInt16 flowControlRowCount = 0; //Tracks coins added to row in flow control

        //Constructor***************************************************************************
        public MainAppForm()
        {
            InitializeComponent();

            metroTabControl1.SelectedIndex = 0;

            //Start thread that initializes data and form on startup
            InitThread();
        }

        //Methods*******************************************************************************

        //General Methods
        /// <summary>
        /// Initializes form and data
        /// </summary>
        private void Init(IProgress<int> progress)
        {
            startUpStatusLabel.Invoke(new Action(() => startUpStatusLabel.Text = "Updating prices..."));
            priceManager = new PriceManager(progress);
            ApplicationFormInitialize(progress);
            progress.Report(100);

            startUpStatusLabel.Invoke(new Action(() => startUpStatusLabel.Visible = false));
            startUpStatusLabel.Invoke(new Action(() => startUpStatusLabel.Enabled = false));
            loadBar.Invoke(new Action(() => loadBar.Enabled = false));
            loadBar.Invoke(new Action(() => loadBar.Visible= false));
        }

        /// <summary>
        /// Common method for updating tracking data and UI
        /// </summary>
        private void UpdatePriceAndUI()
        {
            //Get data from API
            priceManager.UpdatePriceData();

            //Invoke UI update
            UpdateUI();
        }

        //UI Update Methods
        /// <summary>
        /// Initializes all controls on the form at startup
        /// </summary>
        private void ApplicationFormInitialize(IProgress<int> progress)
        {
            startUpStatusLabel.Invoke(new Action(() => startUpStatusLabel.Text = "Reading saved trades..."));

            //Set datasource for dataGridView trades table
            FileIO file = new FileIO();
            DataTable temp = file.XmlToDatatable();
            if (temp != null)
            {
                tableBindToDataGridView.Merge(temp);
            }

            progress.Report(85);
            startUpStatusLabel.Invoke(new Action(() => startUpStatusLabel.Text = "Configuring app..."));

            this.Invoke((MethodInvoker)delegate {

                BindingSource source = new BindingSource();
                source.DataSource = tableBindToDataGridView;
                dataGridView2.DataSource = source;

                //Configure the autoupdate timer
                updatePrices = new System.Timers.Timer();
                updatePrices.Interval = 30000; //30 seconds
                updatePrices.Elapsed += new ElapsedEventHandler(UpdatePrices);
                updatePrices.Start();

                //Configure the tooltip
                toolTip.AutoPopDelay = 15000;
                toolTip.InitialDelay = 1000;
                toolTip.ReshowDelay = 500;
                toolTip.ShowAlways = true;
                toolTip.Popup += ToolTip_Popup;

                //Configure portfolio filter
                filter_CB.Items.Add("Greater Than");
                filter_CB.Items.Add("Less Than");

                //Initialize the new line labels
                AddNewLine();

                //Configure import trades tab
                foreach (var item in priceManager.AllCoinNames)
                {
                    selectCoin_CB.Items.Add(item.Name);
                }
                selectCoin_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                selectCoin_CB.AutoCompleteSource = AutoCompleteSource.ListItems;
                selectCoin_CB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                saveImportButton.Enabled = false;
                saveImportButton.Visible = false;

                importSelect_CB.Items.Add("Binance");
                importSelect_CB.Items.Add("Coinbase");
                importSelect_CB.Items.Add("Kucoin");

                //Add controls for each parsed coin to form
                foreach (var item in priceManager.TrackedCoinList)
                {
                    AddNewCoinToFlowControl(item);
                }
            });

            progress.Report(95);
            startUpStatusLabel.Invoke(new Action(() => startUpStatusLabel.Text = "Updating ui..."));

            UpdateUI();

            startUpStatusLabel.Invoke(new Action(() => startUpStatusLabel.Text = "Done"));
        }

        /// <summary>
        /// Invokes UI updates from a thread
        /// </summary>
        private void UpdateUI()
        {
            //Update UI for each individual coin being tracked
            int i = 0;
            foreach (var item in priceManager.TrackedCoinList)
            {
                priceLabelList[i].Invoke(new Action(() => priceLabelList[i].Text = item.PriceToString));
                textBoxArrayList[i][0].Invoke(new Action(() => textBoxArrayList[i][0].Text = item.QuantityToString));
                textBoxArrayList[i][1].Invoke(new Action(() => textBoxArrayList[i][1].Text = item.NetCostToString));
                textBoxArrayList[i][2].Invoke(new Action(() => textBoxArrayList[i][2].Text = item.ValueToString));
                textBoxArrayList[i][3].Invoke(new Action(() => textBoxArrayList[i][3].Text = item.ProfitToString));
                textBoxArrayList[i][4].Invoke(new Action(() => textBoxArrayList[i][4].Text = item.ProfitPercentToString));

                //Update text color for profit/loss
                if (item.Profit.Value < 0)
                {
                    textBoxArrayList[i][3].Invoke(new Action(() => textBoxArrayList[i][3].ForeColor = Color.Red));
                }
                else
                {
                    textBoxArrayList[i][3].Invoke(new Action(() => textBoxArrayList[i][3].ForeColor = Color.Green));
                }

                i++;
            }

            //Update UI totals 
            totalProfitLabel.Invoke(new Action(() => totalProfitLabel.Text = priceManager.TotalProfit.FloatToMonetary()));
            totalInvestedLabel.Invoke(new Action(() => totalInvestedLabel.Text = priceManager.TotalInvestment.FloatToMonetary()));
            totalValueLabel.Invoke(new Action(() => totalValueLabel.Text = priceManager.TotalValue.FloatToMonetary()));
            //fiatLabel.Invoke(new Action(() => fiatLabel.Text = priceManager.TotalFiatCost.FloatToMonetary()));
        }

        /// <summary>
        /// Creates descriptive labels for each row in the price tracking tab, called when a new line is created
        /// </summary>
        private void AddNewLine()
        {
            FlowLayoutPanel newFlowPanel = new FlowLayoutPanel();
            newFlowPanel.WrapContents = true;
            newFlowPanel.FlowDirection = FlowDirection.BottomUp;
            newFlowPanel.Height = 185;
            newFlowPanel.Width = 90;

            MetroFramework.Controls.MetroLabel quantity = new MetroFramework.Controls.MetroLabel();
            quantity.Margin = new Padding(2, 2, 2, 2);
            quantity.Text = "Quantity:";
            quantity.FontSize = MetroFramework.MetroLabelSize.Small;
            MetroFramework.Controls.MetroLabel invested = new MetroFramework.Controls.MetroLabel();
            invested.Margin = new Padding(2, 2, 2, 2);
            invested.Text = "Total Invested:";
            invested.FontSize = MetroFramework.MetroLabelSize.Small;
            MetroFramework.Controls.MetroLabel value = new MetroFramework.Controls.MetroLabel();
            value.Margin = new Padding(2, 2, 2, 2);
            value.Text = "Value:";
            value.FontSize = MetroFramework.MetroLabelSize.Small;
            MetroFramework.Controls.MetroLabel profit = new MetroFramework.Controls.MetroLabel();
            profit.Margin = new Padding(2, 2, 2, 2);
            profit.Text = "Profit:";
            profit.FontSize = MetroFramework.MetroLabelSize.Small;
            MetroFramework.Controls.MetroLabel profitPercent = new MetroFramework.Controls.MetroLabel();
            profitPercent.Margin = new Padding(2, 2, 2, 2);
            profitPercent.Text = "Profit %:";
            profitPercent.FontSize = MetroFramework.MetroLabelSize.Small;

            newFlowPanel.Controls.Add(profitPercent);
            newFlowPanel.Controls.Add(profit);
            newFlowPanel.Controls.Add(value);
            newFlowPanel.Controls.Add(invested);
            newFlowPanel.Controls.Add(quantity);

            //flowLayoutPanel1.Controls.Add(newFlowPanel);
            flowLayoutPanel1.Invoke(new Action(() => flowLayoutPanel1.Controls.Add(newFlowPanel)));
        }

        /// <summary>
        /// Adds new UI controls to the form when a new coin is being tracked
        /// </summary>
        /// <param name="addCoin"></param>
        public void AddNewCoinToFlowControl(CoinModel addCoin)
        {
            //Track if we need to add new row labels or not
            if (flowControlRowCount == 7)
            {
                AddNewLine();
                flowControlRowCount = 0;
            }

            //Create flow panel to add coin specific controls
            FlowLayoutPanel newFlowPanel = new FlowLayoutPanel();
            newFlowPanel.WrapContents = true;
            newFlowPanel.FlowDirection = FlowDirection.TopDown;
            newFlowPanel.Height = 185;
            newFlowPanel.Width = 90;

            //Create controls
            MetroFramework.Controls.MetroLabel coinName = new MetroFramework.Controls.MetroLabel();
            coinName.Text = addCoin.Name;

            MetroFramework.Controls.MetroLabel coinPrice = new MetroFramework.Controls.MetroLabel();
            coinPrice.Name = addCoin.Name + "Label";


            toolTip.SetToolTip(coinPrice, "Test");


            MetroFramework.Controls.MetroTextBox coinQuantity = new MetroFramework.Controls.MetroTextBox();
            coinQuantity.Name = addCoin.Name + "Quantity_TB";


            MetroFramework.Controls.MetroTextBox coinInvested = new MetroFramework.Controls.MetroTextBox();
            coinInvested.Name = addCoin.Name + "Invested_TB";


            MetroFramework.Controls.MetroTextBox coinValue = new MetroFramework.Controls.MetroTextBox();
            coinValue.Name = addCoin.Name + "Value_TB";


            MetroFramework.Controls.MetroTextBox coinProfit = new MetroFramework.Controls.MetroTextBox();
            coinProfit.Name = addCoin.Name + "Profit_TB";


            MetroFramework.Controls.MetroTextBox coinProfitPercent = new MetroFramework.Controls.MetroTextBox();
            coinProfitPercent.Name = addCoin.Name + "ProfitPercent_TB";

            //Add controls to coin specifc flow panel
            newFlowPanel.Controls.Add(coinName);
            newFlowPanel.Controls.Add(coinPrice);
            newFlowPanel.Controls.Add(coinQuantity);
            newFlowPanel.Controls.Add(coinInvested);
            newFlowPanel.Controls.Add(coinValue);
            newFlowPanel.Controls.Add(coinProfit);
            newFlowPanel.Controls.Add(coinProfitPercent);

            //Add new flow panel to base flow panel
            flowLayoutPanel1.Invoke(new Action(() => flowLayoutPanel1.Controls.Add(newFlowPanel)));

            //Update Lists
            priceLabelList.Add(coinPrice);

            //Create new textbox array and add to textbox array list
            MetroFramework.Controls.MetroTextBox[] newArray = new MetroFramework.Controls.MetroTextBox[5];
            newArray[0] = coinQuantity;
            newArray[1] = coinInvested;
            newArray[2] = coinValue;
            newArray[3] = coinProfit;
            newArray[4] = coinProfitPercent;
            textBoxArrayList.Add(newArray);

            //Update textbox properties
            foreach (var item in newArray)
            {
                item.Size = new Size(85, 20);
                item.Padding = new Padding(0, 4, 0, 4);
                item.CustomBackground = false;
                item.CustomForeColor = true;
            }

            //Update tiles with picture, name, and url link
            try
            {
                //Create metro tile
                MetroFramework.Controls.MetroTile tile = new MetroFramework.Controls.MetroTile();

                //Connect to API to get trade name
                var cli = new System.Net.WebClient();
                string prices = cli.DownloadString(addCoin.APILink);
                dynamic results = JsonConvert.DeserializeObject<dynamic>(prices);

                //Update path to correct image
                string result = results[0].symbol;
                result = result.ToLower();
                string path = @"../../Resources\" + result + "@2x.png";

                //If there is an image for the coin, update tile color and add image to tile
                if (File.Exists(path))
                {
                    //Sample pixel color
                    int x4 = 16;
                    int y = 16;

                    Bitmap b = new Bitmap(path);
                    Color x = b.GetPixel(x4, y);

                    //Add image to tile 
                    tile.TileImage = Image.FromFile(path);
                    tile.UseTileImage = true;
                    tile.TileImageAlign = ContentAlignment.MiddleCenter;
                    tile.BackColor = ControlPaint.Light(x);
                }
                else //If there is no image for the coin, select random color and add default image
                {
                    tile.UseTileImage = true;
                    Random rnd = new Random();
                    Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    tile.BackColor = ControlPaint.Light(randomColor);

                    tile.TileImage = Image.FromFile(@"../../Resources\default_tile.png");
                    tile.UseTileImage = true;
                    tile.TileImageAlign = ContentAlignment.MiddleCenter;
                }

                //Update tile name and other parameters         
                tile.Size = new Size(120, 120);
                tile.Visible = true;
                tile.Enabled = true;
                tile.CustomBackground = true;
                tile.Text = addCoin.Name;
                tile.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
                tile.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
                tile.Click += Tile_Click;
                tile.Name = addCoin.APILink;

                infoFlowPanel.Invoke(new Action(() => infoFlowPanel.Controls.Add(tile)));
            }
            catch
            {
#if DEBUG
                Console.WriteLine("Error adding tile to form");
#endif
            }

            //Update counts
            flowControlRowCount++; //Update row count
        }

        //UI Event Handlers

        //Price Tracking Tab*******************************************************
        /// <summary>
        /// Updates price tracking data and UI every 30 seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdatePrices(object sender, ElapsedEventArgs e)
        {
            //TODO - Make update time value a setting
            UpdatePriceAndUI();
        }

        /// <summary>
        /// Mainform button pressed handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainAppForm_KeyUp(object sender, KeyEventArgs e)
        {
            //Handle refresh button pressed
            if (e.KeyCode == Keys.F5)
            {
                //statusLabel.Text = "Refreshing";
                UpdatePriceAndUI();
                //statusLabel.Text = "";

                #if DEBUG
                Console.WriteLine("Refreshed");
                #endif
            }
        }

        /// <summary>
        /// Update tool tip display data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolTip_Popup(object sender, PopupEventArgs e)
        {
            //TODO- Tooltip not workign correctly
            string text = "";
            foreach(var item in priceManager.TrackedCoinList)
            {
                if (e.AssociatedControl.Name.Contains(item.Name))
                {
                    text = item.MarketCap;
                }
            }

            //toolTip.SetToolTip(, text);

            //e.AssociatedControl.Name
            //throw new NotImplementedException();
        }

        //Menu Strip****************************************************************
        /// <summary>
        /// Call save function to save tracked coins data to file, only need to save if price tracking tab was updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileIO save = new FileIO();
            save.SavePriceTrackingToFile(priceManager.TrackedCoinList);
        }

        /// <summary>
        /// Adds new coin to the form. User enters relevant data and UI is updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addBuyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCoinForm addCoin = new AddCoinForm(priceManager.AllCoinNames); //Instantiate form

            if (addCoin.ShowDialog() == DialogResult.OK) //Show form
            {
                CoinModel coinModel = addCoin.Coin; //Create new coin model and add data from form

                //Add new value array to price manager
                priceManager.AddNewCoin(coinModel);
                AddNewCoinToFlowControl(coinModel); //Add coin to form
                UpdatePriceAndUI();
            }
        }

        /// <summary>
        /// Open the edit coin window
        /// Used to change the quantity and net cost if user makes any trades since last save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditCoinForm editCoin = new EditCoinForm(priceManager.TrackedCoinList);

            if (editCoin.ShowDialog() == DialogResult.OK) //Show form
            {
                CoinModel coinModel = editCoin.Coin; //Create new coin model and add data from form

                //Find index of coin to edit
                int index;
                for (index = 0; index < priceManager.TrackedCoinList.Count; index++)
                {
                    if (priceManager.TrackedCoinList[index].Name ==  coinModel.Name)
                    {
                        break;
                    }
                }

                //Update lists with new values
                priceManager.TrackedCoinList[index].Quantity = coinModel.Quantity;
                priceManager.TrackedCoinList[index].NetCost = coinModel.NetCost;

                textBoxArrayList[index][0].Text = coinModel.Quantity.ToString();
                textBoxArrayList[index][1].Text ="$" + coinModel.NetCost.ToString();

                //Save data if enabled
                if (editCoin.SaveEnabled == true)
                {
                    FileIO save = new FileIO();
                    save.SavePriceTrackingToFile(priceManager.TrackedCoinList);
                }
            }
        }

        /// <summary>
        /// Open the donate form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DonateForm donate = new DonateForm();
            donate.ShowDialog();
            donate.Dispose();
        }

        //Coin Info Tab************************************************************
        /// <summary>
        /// Parse and build url to navigate to coin page on coinmarketcap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tile_Click(object sender, EventArgs e)
        {
            //Need to build link to https://coinmarketcap.com/currencies/coin/ and navigate to website

            //The api url was saved as name, parse the coin name from the url since the coin name in url
            //can differ from the official name
            string name = ((MetroFramework.Controls.MetroTile)sender).Name.ToString().Split('/')[5];

            //Check if the parsed name matches the official name, some protection if the parsing fails
            if (name.Contains(((MetroFramework.Controls.MetroTile)sender).Text.ToLower()))
            {
                try
                {
                    //Open the url in web browser for the user
                    ProcessStartInfo sInfo = new ProcessStartInfo("https://coinmarketcap.com/currencies/" + name + "/");
                    Process.Start(sInfo);
                }
                catch
                {
                    MessageBoxForm errorMessage = new MessageBoxForm("Error connecting to coin market cap.");
                }
            }
        }

        //Portfolio Tab*************************************************************
        /// <summary>
        /// Load portfolio data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroTabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == metroTabPage2)
            {
                PortfolioSelected();
            }
        }

        /// <summary>
        /// Loads and formats data for each coin being tracked into pie chart and list view
        /// </summary>
        private void PortfolioSelected()
        {
            //Func<ChartPoint, string> labelPoint = chartPoint =>
            //    string.Format("({0:P})", chartPoint.Participation);

            //pieChart1.Series.Clear();
            //listView1.Items.Clear();

            //for (int i = 0; i < coinCount; i++)
            //{
            //    double percent = (double)priceManager.valueArrayList[i][(int)PriceManager.rowNames.Value] / (double)priceManager.totalValue;

            //    if (filterTextBox.Text == "" || filter_CB.SelectedIndex == -1)
            //    {
            //        pieChart1.Series.Add(new PieSeries
            //        {
            //            Title = coinNamesList[i],
            //            Values = new ChartValues<double> { (double)priceManager.valueArrayList[i][(int)PriceManager.rowNames.Value] / (double)priceManager.totalValue },
            //            DataLabels = true,
            //            LabelPoint = labelPoint,
            //            //LabelPosition = PieLabelPosition.OutsideSlice,
            //        });

            //        string[] values = {priceManager.valueArrayList[i][(int)PriceManager.rowNames.Quantity].ToString(),
            //            "$" + priceManager.valueArrayList[i][(int)PriceManager.rowNames.Value].Value.ToString("0.00"), (percent*100).ToString("0.00") + "%" };

            //        listView1.Items.Add(coinNamesList[i]).SubItems.AddRange(values);
            //    }
            //    else if (filter_CB.SelectedIndex == 1)
            //    {
            //        if (percent*100 < Convert.ToDouble(filterTextBox.Text)) //TODO - If entering two periods, get error
            //        {
            //            pieChart1.Series.Add(new PieSeries
            //            {
            //                Title = coinNamesList[i],
            //                Values = new ChartValues<double> { (double)priceManager.valueArrayList[i][(int)PriceManager.rowNames.Value] / (double)priceManager.totalValue },
            //                DataLabels = true,
            //                LabelPoint = labelPoint,
            //                //LabelPosition = PieLabelPosition.OutsideSlice,
            //            });

            //            string[] values = {priceManager.valueArrayList[i][(int)PriceManager.rowNames.Quantity].ToString(),
            //            "$" + priceManager.valueArrayList[i][(int)PriceManager.rowNames.Value].Value.ToString("0.00"), (percent*100).ToString("0.00") + "%" };

            //            listView1.Items.Add(coinNamesList[i]).SubItems.AddRange(values);
            //        }
            //    }
            //    else if (filter_CB.SelectedIndex == 0)
            //    {
            //        if (percent * 100 > Convert.ToDouble(filterTextBox.Text))
            //        {
            //            pieChart1.Series.Add(new PieSeries
            //            {
            //                Title = coinNamesList[i],
            //                Values = new ChartValues<double> { (double)priceManager.valueArrayList[i][(int)PriceManager.rowNames.Value] / (double)priceManager.totalValue },
            //                DataLabels = true,
            //                LabelPoint = labelPoint,
            //                //LabelPosition = PieLabelPosition.OutsideSlice,
            //            });

            //            string[] values = {priceManager.valueArrayList[i][(int)PriceManager.rowNames.Quantity].ToString(),
            //            "$" + priceManager.valueArrayList[i][(int)PriceManager.rowNames.Value].Value.ToString("0.00"), (percent*100).ToString("0.00") + "%" };

            //            listView1.Items.Add(coinNamesList[i]).SubItems.AddRange(values);
            //        }
            //    }
            //}

            //pieChart1.LegendLocation = LegendLocation.Right;
            //pieChart1.HoverPushOut = 10;
            //pieChart1.ForeColor = Color.Black;
            //pieChart1.DataTooltip = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            if (filterTextBox.Text.IsNumeric() && filterTextBox.Text != "." && filterTextBox.Text != "" && filter_CB.SelectedIndex != -1)
            {
                PortfolioSelected();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filter_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filterTextBox.Text.IsNumeric() && filterTextBox.Text != "." && filterTextBox.Text != "" && filter_CB.SelectedIndex != -1)
            {
                PortfolioSelected();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="chartPoint"></param>
        private void pieChart1_DataHover(object sender, ChartPoint chartPoint)
        {
            foreach (ListViewItem lvw in listView1.Items)
            {
                lvw.BackColor = Color.White;

                string value = lvw.SubItems[0].Text;
                if (lvw.SubItems[0].Text == chartPoint.SeriesView.Title)
                {
                    lvw.BackColor = Color.LightGreen;
                }
            }

        }

        /// <summary>
        /// Cancel tab switch if the tabs are not enabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroTabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
        }



        //Trades Tab***************************************************************************
        /// <summary>
        /// Manually add a trade to the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            if (selectCoin_CB.SelectedIndex > -1)
            {
                AddTradeForm importTrade = new AddTradeForm(priceManager.AllCoinNames, selectCoin_CB.SelectedIndex);

                if (importTrade.ShowDialog() == DialogResult.OK)
                {
                    if (importTrade.AddTradeTable != null)
                    {
                        //TODO - Fix data conflicting types here

                        //Add data from add trade window to data grid view
                        tableBindToDataGridView.Merge(importTrade.AddTradeTable, true, MissingSchemaAction.Ignore);
                        unsavedTradesDataTable.Merge(importTrade.AddTradeTable);

                        //Enable save button if an import was successfull
                        saveImportButton.Enabled = true;
                        saveImportButton.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Import trades from an exchange report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void importButton_Click(object sender, EventArgs e)
        {
            if (importSelect_CB.SelectedIndex >= 0)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = @"C:\Users\Willem\Desktop";
                openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Multiselect = true;
                openFileDialog1.Title = "Import trades from " + importSelect_CB.Text;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    foreach (var item in openFileDialog1.FileNames)
                    {
                        string file = item;
                        string exchange = importSelect_CB.Text;

                        DataTable temp = new DataTable();

                        importButton.Enabled = false;
                        saveImportButton.Enabled = false;
                        addButton.Enabled = false;

                        var progress = new Progress<int>(progressPercent => pBar.Value = progressPercent);

                        //Start new thread and wait until complete
                        temp = await Task.Run(() => ImportDataThread(exchange, file, progress));

                        if (temp != null)
                        {
                            if (dataGridView2.RowCount > 0)
                            {
                                tableBindToDataGridView.Merge(temp, true, MissingSchemaAction.Ignore);
                                unsavedTradesDataTable.Merge(temp);
                            }
                            else
                            {
                                tableBindToDataGridView.Merge(temp);
                                unsavedTradesDataTable.Merge(temp);
                            }

                            //Enable save button if an import was successfull
                            saveImportButton.Enabled = true;
                            saveImportButton.Visible = true;
                            dataGridView2.Refresh();
                        }
                        else
                        {
                            MessageBoxForm error = new MessageBoxForm("Excel document incorrect format.", false);
                            error.ShowDialog();
                            error.Dispose();
                        }
                    }

                    importButton.Enabled = true;
                    addButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Save data from imports to an xml file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void saveImportButton_Click(object sender, EventArgs e)
        {
            FileIO file = new FileIO();
            saveImportButton.Enabled = false;
            addButton.Enabled = false;
            importButton.Enabled = false;

            //Create list of coins currently being tracked to compare to trades being added 
            List<string> trackedCoins = new List<string>();
            foreach (var item in priceManager.TrackedCoinList)
            {
                trackedCoins.Add(item.Symbol);
            }

            //Find list of coins that are not being tracked
            List<string> unTrackedCoins = new List<string>();
            for (int i = 0; i < unsavedTradesDataTable.Rows.Count; i++)
            {
                //TODO - Refactor how this peice of code functions
                string coin = unsavedTradesDataTable.Rows[i][2].ToString().Split('/')[0];
                string coinBase = unsavedTradesDataTable.Rows[i][2].ToString().Split('/')[1];
                if (!trackedCoins.Contains(coin) || (!trackedCoins.Contains(coinBase) && coinBase != "USD"))
                {
                    if (!unTrackedCoins.Contains(coin))
                    {
                        unTrackedCoins.Add(coin);
                    }

                    if (!unTrackedCoins.Contains(coinBase) && coinBase != "USD" && !trackedCoins.Contains(coinBase))
                    {
                        unTrackedCoins.Add(coinBase);
                    }
                }
            }

            if (unTrackedCoins.Count != 0)
            {
                //Ask user which coins to track
                TrackNewCoinsForm trackCoins = new TrackNewCoinsForm(unTrackedCoins);
                if (trackCoins.ShowDialog() == DialogResult.OK)
                {
                    int j = 0;
                    foreach (TabPage tab in metroTabControl1.TabPages)
                    {
                        if (j != 2)
                            tab.Enabled = false;
                        j++;
                    }

                    var progress = new Progress<int>(progressPercent => pBar.Value = progressPercent);

                    //Add new coins to tracking in update thread
                    await Task.Run(() => TrackNewCoinThread(trackCoins.TrackNewCoin, progress));               
                }
            }

            int k = 0;
            foreach (TabPage tab in metroTabControl1.TabPages)
            {
                if (k != 2)
                    tab.Enabled = true;
                k++;
            }

            //Update data when done with thread
            priceManager.UpdatePriceDataFromTrades(unsavedTradesDataTable);
            UpdatePriceAndUI();

            //Clear unsaved data table once data has been saved to file
            unsavedTradesDataTable.Clear();
            unTrackedCoins.Clear();

            //Save data to file
            file.SavePriceTrackingToFile(priceManager.TrackedCoinList);
            file.DataGridViewToXML(dataGridView2);

            importButton.Enabled = true;
            saveImportButton.Enabled = false;
            saveImportButton.Visible = false;
            addButton.Enabled = true;

            updatePrices.Start();
        }

        //Threads********************************************************************************
        /// <summary>
        /// Thread for updating new coins to track, updates form UI and price manager data
        /// </summary>
        /// <param name="coinsToTrack"></param>
        /// <param name="progress"></param>
        public void TrackNewCoinThread(List<string> coinsToTrack, IProgress<int> progress)
        {
            updatePrices.Stop();
 
            int i = 0;
            foreach (string coin in coinsToTrack)
            {
                CoinModel addCoin = new CoinModel();

                int index = 0;
                foreach (var item in priceManager.AllCoinNames)
                {
                    if (coin == item.Symbol)
                    {
                        break;
                    }
                    index++;
                }

                //TODO - If a coin is not in the list (ie IOTA in binance report, MIOTA in CMC) we get index out of bounds error
                addCoin.Quantity = 0;
                addCoin.NetCost = 0;
                addCoin.Name = priceManager.AllCoinNames[index].Name;
                addCoin.APILink = "https://api.coinmarketcap.com/v1/ticker/" + priceManager.AllCoinNames[index].Id;

                //Add new value array to price manager
                priceManager.AddNewCoin(addCoin);

                AddNewCoinToFlowControl(addCoin);

                priceManager.UpdatePriceData();

                progress.Report((int)(((float)i / coinsToTrack.Count) * 100.0F));
                i++;
            }
            progress.Report(100);
#if DEBUG
            Console.WriteLine("Track New Coins Done");
#endif
        }

        /// <summary>
        /// Thread for importing excel spreadsheet into trades table
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="fileName"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public DataTable ImportDataThread(string exchange, string fileName, IProgress<int> progress)
        {

                GeneralImport import = new GeneralImport();
                DataTable test = import.ImportFromExchange(exchange, fileName, progress);

#if DEBUG
            Console.WriteLine("Import Done");
#endif

            return test;

        }

        /// <summary>
        /// Thread for initializing the form and data on startup, avoids perceived hangup when loading
        /// </summary>
        public async void InitThread()
        {
            var progress = new Progress<int>(progressPercent => loadBar.Value = progressPercent);
            await Task.Run(() => Init(progress));
        }
    }
}
