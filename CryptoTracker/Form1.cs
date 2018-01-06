using System;
using System.Windows.Forms;
using System.Timers;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;

namespace CryptoTracker
{
    public partial class Form1 : Form
    {
        ToolTip toolTip = new ToolTip();

        PriceManager priceManager;
        System.Timers.Timer updatePrices;

        List<Label> priceLabelList = new List<Label>();
        List<TextBox[]> textBoxArrayList = new List<TextBox[]>();

        int flowControlCoinCount = 0;

        public Form1()
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
            UpdateUI();
        }

        //Refresh data
        private void button1_Click(object sender, EventArgs e)
        {
            priceManager.UpdatePriceData();
            UpdateUI();
        }

        private void UpdateUI()
        {
            int i = 0;
            foreach (var item in priceLabelList)
            {
                this.Invoke((MethodInvoker)delegate {
                    //if (priceManager.coinPrice[i] < Convert.ToDouble(item.Text.Replace('$', ' ')))
                    //{
                    //    item.ForeColor = Color.Red;
                    //}
                    //else if (priceManager.coinPrice[i] > Convert.ToDouble(item.Text.Replace('$', ' ')))
                    //{
                    //    item.ForeColor = Color.Green;
                    //}
                    //else
                    //{
                    //    item.ForeColor = Color.Black;
                    //}

                    item.Text = "$" + priceManager.coinPrice[i].ToString("0.00"); // runs on UI thread
                });

                for (int j = 0; j < 5; j++)
                {
                    if (j == 0)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            textBoxArrayList[i][j].Text = priceManager.valueArrayList[i][j].ToString("0.000000"); // runs on UI thread
                        });
                    }
                        
                    else if (j >= 1 && j <= 3)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            textBoxArrayList[i][j].Text = "$" + priceManager.valueArrayList[i][j].ToString("0.00"); // runs on UI thread
                        });
                    }
                        
                    else if (j == 4)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            textBoxArrayList[i][j].Text = priceManager.valueArrayList[i][j].ToString("0.00") + "%"; // runs on UI thread
                        });
                    }

                    if (j == 3)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            if (priceManager.valueArrayList[i][j] <= 0)
                            {
                                textBoxArrayList[i][j].ForeColor = Color.Red; // runs on UI thread
                            }
                            else
                            {
                                textBoxArrayList[i][j].ForeColor = Color.Green; // runs on UI thread
                            }
                        });
                    }
                        
                }

                this.Invoke((MethodInvoker)delegate {
                    toolTip.SetToolTip(priceLabelList[i], "Rank: " + priceManager.toolTipValues[i][0] + "\n" + "Market Cap: $" + priceManager.toolTipValues[i][1] + "\n" + "% Change 1h: " + priceManager.toolTipValues[i][2] + "%\n" + "% Change 24h: " + priceManager.toolTipValues[i][3] + "%\n" + "% Change 7d: " + priceManager.toolTipValues[i][4] + "%");
                });

                
                i++;             
            }

            this.Invoke((MethodInvoker)delegate {
                totalProfitLabel.Text = "$" + priceManager.totalProfit.ToString("0.00"); // runs on UI thread
                totalInvestedLabel.Text = "$" + priceManager.totalInvestment.ToString("0.00");
                totalValueLabel.Text = "$" + priceManager.totalValue.ToString("0.00");
            });

            
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
            AddNewCoin addCoin = new AddNewCoin();

            if (addCoin.ShowDialog() == DialogResult.OK)
            {
                CoinModel coinModel = addCoin.Coin;

                AddNewCoinToFlowControl(coinModel);
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
                newCoin.Quantity = data[1];
                newCoin.NetCost = data[2];
                newCoin.APILink = data[3];

                AddNewCoinToFlowControl(newCoin);
            }
        }

        private void AddNewLine()
        {
            FlowLayoutPanel newFlowPanel = new FlowLayoutPanel();
            newFlowPanel.WrapContents = true;
            newFlowPanel.FlowDirection = FlowDirection.BottomUp;
            newFlowPanel.Height = 185;
            newFlowPanel.Width = 90;

            Label quantity = new Label();
            quantity.Margin = new Padding(2, 2, 2, 2);
            quantity.Text = "Quantity:";
            Label invested = new Label();
            invested.Margin = new Padding(2, 2, 2, 2);
            invested.Text = "Total Invested:";
            Label value = new Label();
            value.Margin = new Padding(2, 2, 2, 2);
            value.Text = "Value:";
            Label profit = new Label();
            profit.Margin = new Padding(2, 2, 2, 2);
            profit.Text = "Profit:";
            Label profitPercent = new Label();
            profitPercent.Margin = new Padding(2, 2, 2, 2);
            profitPercent.Text = "Profit %:";

            newFlowPanel.Controls.Add(profitPercent);
            newFlowPanel.Controls.Add(profit);
            newFlowPanel.Controls.Add(value);
            newFlowPanel.Controls.Add(invested);
            newFlowPanel.Controls.Add(quantity);

            flowLayoutPanel1.Controls.Add(newFlowPanel);
        }

        public void AddNewCoinToFlowControl(CoinModel addCoin)
        {
            if (flowControlCoinCount == 7)
            {
                AddNewLine();
                flowControlCoinCount = 0;
            }

            FlowLayoutPanel newFlowPanel = new FlowLayoutPanel();
            newFlowPanel.WrapContents = true;
            newFlowPanel.FlowDirection = FlowDirection.TopDown;
            newFlowPanel.Height = 185;
            newFlowPanel.Width = 90;

            

            Label coinName = new Label();
            coinName.Text = addCoin.CoinName;
            

            Label coinPrice = new Label();
            coinPrice.Name = addCoin.CoinName + "Label";
            coinPrice.Text = "$100,000";

            TextBox coinQuantity = new TextBox();
            coinQuantity.Name = addCoin.CoinName + "Quantity_TB";
            coinQuantity.Text = addCoin.Quantity;

            TextBox coinInvested = new TextBox();
            coinInvested.Name = addCoin.CoinName + "Invested_TB";
            coinInvested.Text = addCoin.NetCost;

            TextBox coinValue = new TextBox();
            coinValue.Name = addCoin.CoinName + "Value_TB";

            TextBox coinProfit = new TextBox();
            coinProfit.Name = addCoin.CoinName + "Profit_TB";

            TextBox coinProfitPercent = new TextBox();
            coinProfitPercent.Name = addCoin.CoinName + "ProfitPercent_TB";

            newFlowPanel.Controls.Add(coinName);
            newFlowPanel.Controls.Add(coinPrice);
            newFlowPanel.Controls.Add(coinQuantity);
            newFlowPanel.Controls.Add(coinInvested);
            newFlowPanel.Controls.Add(coinValue);
            newFlowPanel.Controls.Add(coinProfit);
            newFlowPanel.Controls.Add(coinProfitPercent);

            flowLayoutPanel1.Controls.Add(newFlowPanel);

            //Update Lists
            priceLabelList.Add(coinPrice);

            TextBox[] newArray = new TextBox[5];
            newArray[0] = coinQuantity;
            newArray[1] = coinInvested;
            newArray[2] = coinValue;
            newArray[3] = coinProfit;
            newArray[4] = coinProfitPercent;

            textBoxArrayList.Add(newArray);

            priceManager.coinApiUrlList.Add(addCoin.APILink);

            float[] coinValues = new float[5];
            coinValues[(int)PriceManager.rowNames.Quantity] = (float)Convert.ToDouble(addCoin.Quantity);
            coinValues[(int)PriceManager.rowNames.TotalInvested] = (float)Convert.ToDouble(addCoin.NetCost);
            priceManager.valueArrayList.Add(coinValues);

            flowControlCoinCount++;
        }
    }
}
