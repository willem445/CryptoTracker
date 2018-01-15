using System;
using System.Windows.Forms;
using System.Timers;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

//Crypto Images
//https://github.com/cjdowner/cryptocurrency-icons

namespace CryptoTracker
{
    public partial class MainAppForm : MetroFramework.Forms.MetroForm
    {
        ToolTip toolTip = new ToolTip();
        PriceManager priceManager;
        System.Timers.Timer updatePrices;

        List<string> coinNamesList = new List<string>(); //Stores the names of each coin added
        public List<string> coinTradeName = new List<string>();

        //UI Lists
        List<Label> priceLabelList = new List<Label>(); //List of labels to iterate through when updating prices
        List<MetroFramework.Controls.MetroTextBox[]> textBoxArrayList = new List<MetroFramework.Controls.MetroTextBox[]>(); //Array of textboxes for each coin, stored in a list

        //Program variables
        int flowControlRowCount = 0; //Tracks coins added to row in flow control
        bool updatingUiFlag = false; //Tracks if UI is currently being updated, prevents thread interference
        int coinCount = 0; //Count of coins added to project

        public MainAppForm()
        {
            InitializeComponent();
            this.Text = "Crypto Tracker";

            priceManager = new PriceManager();

            updatePrices = new System.Timers.Timer();
            updatePrices.Interval = 30000; //30 seconds
            updatePrices.Elapsed += new ElapsedEventHandler(UpdatePrices);
            updatePrices.Start();

            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 15000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;

            AddNewLine();     
        }

        public void UpdatePrices(object sender, ElapsedEventArgs e)
        {
            priceManager.UpdatePriceData();
            if (!updatingUiFlag)
            {
                UpdateUI();
            }  
        }

        private void UpdateUI()
        {
            updatingUiFlag = true;

            for (int i = 0; i < coinCount; i++)
            {
                if (priceManager.coinPriceList[i].HasValue)
                {
                    this.Invoke((MethodInvoker)delegate {
                        priceLabelList[i].Text = "$" + priceManager.coinPriceList[i].Value.ToString("0.00"); // runs on UI thread

                        //Update textboxes
                        for (int j = 0; j < 5; j++)
                        {
                            if (j == 0)
                            {
                                textBoxArrayList[i][j].Text = priceManager.valueArrayList[i][j].Value.ToString("0.000000"); // runs on UI thread
                            }
                            else if (j >= 1 && j <= 3)
                            {
                                textBoxArrayList[i][j].Text = "$" + priceManager.valueArrayList[i][j].Value.ToString("0.00"); // runs on UI thread
                            }
                            else if (j == 4)
                            {
                                textBoxArrayList[i][j].Text = priceManager.valueArrayList[i][j].Value.ToString("0.00") + "%"; // runs on UI thread
                            }

                            if (j == 3)
                            {
                                if (priceManager.valueArrayList[i][j] <= 0)
                                {
                                    textBoxArrayList[i][j].ForeColor = Color.Red; // runs on UI thread
                                }
                                else
                                {
                                    textBoxArrayList[i][j].ForeColor = Color.Green; // runs on UI thread
                                }
                            }
                        }

                        //Update tooltip
                        if (priceManager.toolTipValues.Count == coinCount)
                        {
                            string comma = String.Format("{0:#,###0.#}", Convert.ToDouble(priceManager.toolTipValues[i][1]));
                            toolTip.SetToolTip(priceLabelList[i], "Rank: " + priceManager.toolTipValues[i][0] + "\n" + "Market Cap: $" + comma + "\n" + "% Change 1h: " + priceManager.toolTipValues[i][2] + "%\n" + "% Change 24h: " + priceManager.toolTipValues[i][3] + "%\n" + "% Change 7d: " + priceManager.toolTipValues[i][4] + "%");
                        }
                        
                        totalProfitLabel.Text = "$" + priceManager.totalProfit.ToString("0.00"); // runs on UI thread
                        totalInvestedLabel.Text = "$" + priceManager.totalInvestment.ToString("0.00");
                        totalValueLabel.Text = "$" + priceManager.totalValue.ToString("0.00");
                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate {
                        foreach (var item in textBoxArrayList[i])
                        {
                            item.Text = "Error";
                        }
                    });
                }
            }

            updatingUiFlag = false;
        }

        //Open saved file
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ParseSavedData(openFileDialog1.FileName);
            }

            priceManager.UpdatePriceData();
            UpdateUI();
        }

        private void addBuyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCoinForm addCoin = new AddCoinForm(); //Instantiate form

            if (addCoin.ShowDialog() == DialogResult.OK) //Show form
            {
                CoinModel coinModel = addCoin.Coin; //Create new coin model and add data from form

                AddNewCoinToFlowControl(coinModel); //Add coin to form
            }
        }

        public void ParseSavedData(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (string line in lines)
            {
                CoinModel newCoin = new CoinModel();

                string[] data = line.Split(',');
                newCoin.CoinName = data[0];
                newCoin.Quantity = (float)(Convert.ToDouble(data[1]));
                newCoin.NetCost = (float)(Convert.ToDouble(data[2]));
                newCoin.APILink = data[3];

                AddNewCoinToFlowControl(newCoin);
            }
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
            coinName.Text = addCoin.CoinName;

            MetroFramework.Controls.MetroLabel coinPrice = new MetroFramework.Controls.MetroLabel();
            coinPrice.Name = addCoin.CoinName + "Label";
            coinPrice.Text = "$100,000";

            MetroFramework.Controls.MetroTextBox coinQuantity = new MetroFramework.Controls.MetroTextBox();
            coinQuantity.Name = addCoin.CoinName + "Quantity_TB";
            coinQuantity.Text = addCoin.Quantity.ToString();

            MetroFramework.Controls.MetroTextBox coinInvested = new MetroFramework.Controls.MetroTextBox();
            coinInvested.Name = addCoin.CoinName + "Invested_TB";
            coinInvested.Text = addCoin.NetCost.ToString();

            MetroFramework.Controls.MetroTextBox coinValue = new MetroFramework.Controls.MetroTextBox();
            coinValue.Name = addCoin.CoinName + "Value_TB";

            MetroFramework.Controls.MetroTextBox coinProfit = new MetroFramework.Controls.MetroTextBox();
            coinProfit.Name = addCoin.CoinName + "Profit_TB";

            MetroFramework.Controls.MetroTextBox coinProfitPercent = new MetroFramework.Controls.MetroTextBox();
            coinProfitPercent.Name = addCoin.CoinName + "ProfitPercent_TB";

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
            coinNamesList.Add(addCoin.CoinName);

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


            
            //Get trade name
            try
            {
                var cli = new System.Net.WebClient();
                string prices = cli.DownloadString(addCoin.APILink);

                dynamic results = JsonConvert.DeserializeObject<dynamic>(prices);

                //coinTradeName.Add();

                int x4 = 16;
                int y = 16;

                string result = results[0].symbol;
                result = result.ToLower();
                string path = @"../../Resources\" + result + "@2x.png";

                if (File.Exists(path))
                {
                    Bitmap b = new Bitmap(path);
                    Color x = b.GetPixel(x4, y);

                    //Add tile to info panel
                    MetroFramework.Controls.MetroTile tile = new MetroFramework.Controls.MetroTile();
                    tile.Size = new Size(120, 120);
                    tile.Visible = true;
                    tile.Enabled = true;

                    tile.CustomBackground = true;
                    tile.BackColor = ControlPaint.Light(x);
                    tile.TileImage = Image.FromFile(path);
                    tile.UseTileImage = true;
                    tile.TileImageAlign = ContentAlignment.MiddleCenter;
                    tile.Text = addCoin.CoinName;
                    tile.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
                    tile.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;

                    infoFlowPanel.Controls.Add(tile);
                }                              
            }
            catch
            {

            }




            //Add new value array to price manager
            priceManager.AddNewCoin(addCoin);        

            //Update counts
            flowControlRowCount++; //Update row count
            coinCount++; //Update coin count
        }

        /// <summary>
        /// Write coin data to text file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            //TODO - Option for user to select save location
            //TODO - Fix so if no coins are added, cannot save
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            string[] textFileArray = new string[coinCount];
            bool readError = false;

            //Loop through each coin and put data in string array, if there is an error, do not write
            for (int i = 0; i < coinCount; i++)
            {
                try
                {
                    textFileArray[i] = coinNamesList[i] + ", " + textBoxArrayList[i][0].Text + ", " + textBoxArrayList[i][1].Text.TrimStart('$') + ", " + priceManager.coinApiUrlList[i];
                }
                catch
                {
                    MessageBox.Show("Error reading data");
                    readError = true;
                }
            }

            if (!readError)
            {
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"C:\Users\Willem\Desktop\CoinPrices.txt"))
                {
                    for (int i = 0; i < coinCount; i++)
                    {
                        //Print name, quantity, net cost, and api link to text file
                        file.WriteLine(textFileArray[i]);
                    }
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditCoinForm editCoin = new EditCoinForm(coinNamesList);

            if (editCoin.ShowDialog() == DialogResult.OK) //Show form
            {
                CoinModel coinModel = editCoin.Coin; //Create new coin model and add data from form

                //Find index of coin to edit
                int index;
                for (index = 0; index < coinCount; index++)
                {
                    if (coinNamesList[index] ==  coinModel.CoinName)
                    {
                        break;
                    }
                }

                //Update lists with new values
                priceManager.valueArrayList[index][0] = coinModel.Quantity;
                priceManager.valueArrayList[index][1] = coinModel.TotalInvested;

                textBoxArrayList[index][0].Text = coinModel.Quantity.ToString();
                textBoxArrayList[index][1].Text ="$" + coinModel.TotalInvested.ToString();

                //Save data if enabled
                if (editCoin.SaveEnabled == true)
                {
                    Save();
                }
            }
        }

        private void button1_Click(object sender, MouseEventArgs e)
        {
            priceManager.UpdatePriceData();
            if (!updatingUiFlag)
            {
                UpdateUI();
            }
        }
    }
}
