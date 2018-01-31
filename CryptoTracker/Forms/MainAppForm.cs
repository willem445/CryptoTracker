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
        public enum rowNames
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
        DataTable tableBindToDataGridView = new DataTable(); //Contains both unsaved and saved data which is bound to data grid view for viewing
        DataTable unsavedTradesDataTable = new DataTable(); //Contains unsaved data that has not yet been written to file, used to avoid adding duplicate trades to price tracking totals

        //UI Lists
        List<Label> priceLabelList = new List<Label>(); //List of labels to iterate through when updating prices
        List<MetroFramework.Controls.MetroTextBox[]> textBoxArrayList = new List<MetroFramework.Controls.MetroTextBox[]>(); //Array of textboxes for each coin, stored in a list

        //MainApp fields
        int flowControlRowCount = 0; //Tracks coins added to row in flow control
        int coinCount = 0; //Count of coins added to project

        //Constructor***************************************************************************
        public MainAppForm()
        {
            InitializeComponent();

            priceManager = new PriceManager();

            ApplicationDataInitialize();
            ApplicationFormInitialize();
            
        }

        private void ToolTip_Popup(object sender, PopupEventArgs e)
        {
            //TODO- Tooltip not workign correctly
            string text = "";
            foreach(var item in priceManager.coinModelList)
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

        private void ApplicationFormInitialize()
        {
            //Create handle created event
            HandleCreated += MainAppForm_HandleCreated;

            //Set datasource for dataGridView trades table
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
            foreach (var item in priceManager.coinModelList)
            {
                selectCoin_CB.Items.Add(item.Name);
            }

            saveImportButton.Enabled = false;
            saveImportButton.Visible = false;

            importSelect_CB.Items.Add("Binance");
            importSelect_CB.Items.Add("Coinbase");

            //Add controls for each parsed coin to form
            foreach (var item in priceManager.coinModelList)
            {
                AddNewCoinToFlowControl(item);
            }
        }

        private void ApplicationDataInitialize()
        {
            //Parse data in documents folder
            FileIO file = new FileIO();
            priceManager.coinModelList = file.ParseSavedData();

            //Update prices based on parsed data
            priceManager.UpdatePriceData();

            //Update trades data grid view from xml file
            DataTable temp = file.ParseTradesFile();
            if (temp != null)
            {
                tableBindToDataGridView.Merge(temp);
            }
        }

        //Methods*******************************************************************************
        /// <summary>
        /// Update the UI when the form handle is created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainAppForm_HandleCreated(object sender, EventArgs e)
        {
            //UpdateUI();
        }

        /// <summary>
        /// Called by timer every 30 seconds to update prices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdatePrices(object sender, ElapsedEventArgs e)
        {
            //TODOHP - Causes system reflection exception error when updating, does not update binding

            //Get data from API
            priceManager.UpdatePriceData();  
        }

        /// <summary>
        /// Button to manually update data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshButton_Click(object sender, MouseEventArgs e)
        {
            priceManager.UpdatePriceData();
        }

        /// <summary>
        /// Creates labels for a new row in flow panel
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

            flowLayoutPanel1.Controls.Add(newFlowPanel);
        }

        /// <summary>
        /// Adds new coin to crypto tracker
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
            coinPrice.DataBindings.Add(new Binding("Text", addCoin, "PriceToString", false, DataSourceUpdateMode.OnPropertyChanged));

            toolTip.SetToolTip(coinPrice, "Test");


            MetroFramework.Controls.MetroTextBox coinQuantity = new MetroFramework.Controls.MetroTextBox();
            coinQuantity.Name = addCoin.Name + "Quantity_TB";
            coinQuantity.DataBindings.Add("Text", addCoin, "QuantityToString", false, DataSourceUpdateMode.OnPropertyChanged);

            MetroFramework.Controls.MetroTextBox coinInvested = new MetroFramework.Controls.MetroTextBox();
            coinInvested.Name = addCoin.Name + "Invested_TB";
            coinInvested.DataBindings.Add("Text", addCoin, "NetCostToString", false, DataSourceUpdateMode.OnPropertyChanged);

            MetroFramework.Controls.MetroTextBox coinValue = new MetroFramework.Controls.MetroTextBox();
            coinValue.Name = addCoin.Name + "Value_TB";
            coinValue.DataBindings.Add("Text", addCoin, "ValueToString", false, DataSourceUpdateMode.OnPropertyChanged);

            MetroFramework.Controls.MetroTextBox coinProfit = new MetroFramework.Controls.MetroTextBox();
            coinProfit.Name = addCoin.Name + "Profit_TB";
            coinProfit.DataBindings.Add("Text", addCoin, "ProfitToString", false, DataSourceUpdateMode.OnPropertyChanged);
            coinProfit.DataBindings.Add("Forecolor", addCoin, "Color", false, DataSourceUpdateMode.OnPropertyChanged);

            MetroFramework.Controls.MetroTextBox coinProfitPercent = new MetroFramework.Controls.MetroTextBox();
            coinProfitPercent.Name = addCoin.Name + "ProfitPercent_TB";
            coinProfitPercent.DataBindings.Add("Text", addCoin, "ProfitPercentToString", false, DataSourceUpdateMode.OnPropertyChanged);

            //Add controls to coin specifc flow panel
            newFlowPanel.Controls.Add(coinName);
            newFlowPanel.Controls.Add(coinPrice);
            newFlowPanel.Controls.Add(coinQuantity);
            newFlowPanel.Controls.Add(coinInvested);
            newFlowPanel.Controls.Add(coinValue);
            newFlowPanel.Controls.Add(coinProfit);
            newFlowPanel.Controls.Add(coinProfitPercent);

            //Add new flow panel to base flow panel
            flowLayoutPanel1.Controls.Add(newFlowPanel);

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

                infoFlowPanel.Controls.Add(tile);
            }
            catch
            {

            }

            //Update counts
            flowControlRowCount++; //Update row count
            coinCount++; //Update coin count
        }

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

                }
            }
        }

        /// <summary>
        /// Call save function to save data to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Save data to text file
        /// </summary>
        private void Save()
        {
            //TODO - Move this to FileIO

            string path = System.IO.Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDoc‌​uments), "CrytoTracker");

            string[] textFileArray = new string[priceManager.CoinModelList.Count];
            bool readError = false;

            //Loop through each coin and put data in string array, if there is an error, do not write
            for (int i = 0; i < priceManager.CoinModelList.Count; i++)
            {
                try
                {
                    textFileArray[i] = priceManager.coinModelList[i].Name + ", " + priceManager.CoinModelList[i].QuantityToString + ", " + priceManager.CoinModelList[i].NetCost.ToString() + ", " + priceManager.coinModelList[i].APILink;
                }
                catch
                {
                    MessageBox.Show("Error reading data");
                    readError = true;
                }
            }

            if (!readError)
            {
                if (!Directory.Exists(Path.Combine(path, "CoinData.txt")))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(Path.Combine(path, "CoinData.txt")))
                {
                    for (int i = 0; i < priceManager.CoinModelList.Count; i++)
                    {
                        //Print name, quantity, net cost, and api link to text file
                        file.WriteLine(textFileArray[i]);
                    }
                }
            }
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
            EditCoinForm editCoin = new EditCoinForm(priceManager.coinModelList);

            if (editCoin.ShowDialog() == DialogResult.OK) //Show form
            {
                CoinModel coinModel = editCoin.Coin; //Create new coin model and add data from form

                //Find index of coin to edit
                int index;
                for (index = 0; index < coinCount; index++)
                {
                    if (priceManager.coinModelList[index].Name ==  coinModel.Name)
                    {
                        break;
                    }
                }

                //Update lists with new values
                priceManager.coinModelList[index].Quantity = coinModel.Quantity;
                priceManager.coinModelList[index].NetCost = coinModel.NetCost;

                textBoxArrayList[index][0].Text = coinModel.Quantity.ToString();
                textBoxArrayList[index][1].Text ="$" + coinModel.NetCost.ToString();

                //Save data if enabled
                if (editCoin.SaveEnabled == true)
                {
                    Save();
                }
            }
        }

        /// <summary>
        /// 
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
        /// 
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
        /// Manually add a trade to the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            AddTradeForm importTrade = new AddTradeForm();

            if (importTrade.ShowDialog() == DialogResult.OK)
            {
                //Add data from add trade window to data grid view
                tableBindToDataGridView.Merge(importTrade.table);
                unsavedTradesDataTable.Merge(importTrade.table);
            }
        }

        /// <summary>
        /// Import trades from an exchange report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void importButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\Users\Willem\Desktop";
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                string exchange = importSelect_CB.Text;

                DataTable temp = new DataTable();

                metroProgressSpinner1.EnsureVisible = true;
                importButton.Enabled = false;

                //Start new thread and wait until complete
                temp = await Task.Factory.StartNew(() => ImportDataThread(exchange, file));

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


                dataGridView2.Refresh();


                metroProgressSpinner1.EnsureVisible = false;
                importButton.Enabled = true;

                //Enable save button if an import was successfull
                saveImportButton.Enabled = true;
                saveImportButton.Visible = true;
            }
        }

        /// <summary>
        /// Save data from imports to an xml file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveImportButton_Click(object sender, EventArgs e)
        {
            FileIO file = new FileIO();
            
            //Create list of coins currently being tracked to compare to trades being added 
            List<string> trackedCoins = new List<string>();
            foreach (var item in priceManager.CoinModelList)
            {
                trackedCoins.Add(item.Symbol);
            }

            //Check to see if coin is currently being tracked, if not, ask user if they want to 
            for (int i = 0; i < unsavedTradesDataTable.Rows.Count; i++)
            {
                if (!trackedCoins.Contains(unsavedTradesDataTable.Rows[i][2].ToString().Split('/')[0]))
                {
                    MessageBoxForm message = new MessageBoxForm(unsavedTradesDataTable.Rows[i][2].ToString().Split('/')[0] + " is not currently being tracked. Would you like to track it?");

                    if (message.ShowDialog() == DialogResult.OK)
                    {
                        CoinModel addCoin = new CoinModel();

                        int index = 0;
                        foreach (var item in priceManager.AllCoinNames)
                        {
                            if (unsavedTradesDataTable.Rows[i][2].ToString().Split('/')[0] == item.Symbol)
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

                        trackedCoins.Add(addCoin.Symbol);

                    }
                }
            }

            priceManager.UpdatePriceDataFromTrades(unsavedTradesDataTable);

            //Clear unsaved data table once data has been saved to file
            unsavedTradesDataTable.Clear();

            //Save data to file
            Save();
            file.SaveToXML(dataGridView2);
        }

        //Threads********************************************************************************
        public DataTable ImportDataThread(string exchange, string fileName)
        {

                GeneralImport import = new GeneralImport();
                DataTable test = import.ImportFromExchange(exchange, fileName);

                Console.WriteLine("Done");

            return test;

        }
    }
}
