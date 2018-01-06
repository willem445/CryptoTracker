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
        PriceManager priceManager;
        List<Label> labelPriceList = new List<Label>();
        private TextBox[,] textboxes;
        System.Timers.Timer updatePrices;

        int flowControlCoinCount = 0;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Crypto Tracker";

            priceManager = new PriceManager();

            labelPriceList.Add(bitcoinLabel);
            labelPriceList.Add(etherLabel);
            labelPriceList.Add(litecoinLabel);
            labelPriceList.Add(stellarLabel);
            labelPriceList.Add(rippleLabel);
            labelPriceList.Add(iotaLabel);
            labelPriceList.Add(waltonLabel);
            labelPriceList.Add(reqLabel);

            labelPriceList.Add(iconLabel);
            labelPriceList.Add(vechainLabel);
            labelPriceList.Add(binanceLabel);
            labelPriceList.Add(perlLabel);
            labelPriceList.Add(bntyLabel);
            labelPriceList.Add(dentLabel);
            labelPriceList.Add(linkLabel);

            textboxes = new TextBox[,] 
            {
                {bitcoinQuantityTB, bitcoinInvestedTB, bitcoinValueTB, bitcoinProfitTB, bitcoinProfitPercentTB},
                {etherQuantityTB, etherInvestedTB, etherValueTB, etherProfitTB, etherProfitPercentTB},
                {litecoinQuantityTB, litecoinInvestedTB, litecoinValueTB, litecoinProfitTB, litecoinProfitPercentTB},
                {stellarQuantityTB, stellarInvestedTB, stellarValueTB, stellarProfitTB, stellarProfitPercentTB},
                {rippleQuantityTB, rippleInvestedTB, rippleValueTB, rippleProfitTB, rippleProfitPercentTB},
                {iotaQuantityTB, iotaInvestedTB, iotaValueTB, iotaProfitTB, iotaProfitPercentTB},
                {waltonQuantityTB, waltonInvestedTB, waltonValueTB, waltonProfitTB, waltonProfitPercentTB},
                {reqQuantityTB, reqInvestedTB, reqValueTB, reqProfitTB, reqProfitPercentTB},

                {iconQuantityTB, iconInvestedTB, iconValueTB, iconProfitTB, iconProfitPercentTB},
                {vechainQuantityTB, vechainInvestedTB, vechainValueTB, vechainProfitTB, vechainProfitPercentTB},
                {binanceQuantityTB, binanceInvestedTB, binanceValueTB, binanceProfitTB, binanceProfitPercentTB},
                {perlQuantityTB, perlInvestedTB, perlValueTB, perlProfitTB, perlProfitPercentTB},
                {bntyQuantityTB, bntyInvestedTB, bntyValueTB, bntyProfitTB, bntyProfitPercentTB},
                {dentQuantityTB, dentInvestedTB, dentValueTB, dentProfitTB, dentProfitPercentTB},
                {linkQuantityTB, linkInvestedTB, linkValueTB, linkProfitTB, linkProfitPercentTB}
            };

            updatePrices = new System.Timers.Timer();
            updatePrices.Interval = 30000; //30 seconds
            updatePrices.Elapsed += new ElapsedEventHandler(UpdatePrices);
            updatePrices.Start();

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
            foreach (var item in labelPriceList)
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
                            textboxes[i, j].Text = priceManager.valueArray[i, j].ToString("0.000000"); // runs on UI thread
                        });
                    }
                        
                    else if (j >= 1 && j <= 3)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            textboxes[i, j].Text = "$" + priceManager.valueArray[i, j].ToString("0.00"); // runs on UI thread
                        });
                    }
                        
                    else if (j == 4)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            textboxes[i, j].Text = priceManager.valueArray[i, j].ToString("0.00") + "%"; // runs on UI thread
                        });
                    }

                    if (j == 3)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            if (priceManager.valueArray[i, j] <= 0)
                            {
                                textboxes[i, j].ForeColor = Color.Red; // runs on UI thread
                            }
                            else
                            {
                                textboxes[i, j].ForeColor = Color.Green; // runs on UI thread
                            }
                        });
                    }
                        
                }

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
                priceManager.ParseSavedData(openFileDialog1.FileName);
            }

            priceManager.UpdatePriceData();
            UpdateUI();
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

        private void addBuyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewCoin addCoin = new AddNewCoin();

            if (addCoin.ShowDialog() == DialogResult.OK)
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
                coinPrice.Text = "$100,000";

                TextBox coinQuantity = new TextBox();
                coinQuantity.Text = addCoin.Quantity;

                TextBox coinInvested = new TextBox();
                coinInvested.Text = addCoin.NetCost;

                TextBox coinValue = new TextBox();
                TextBox coinProfit = new TextBox();
                TextBox coinProfitPercent = new TextBox();

                newFlowPanel.Controls.Add(coinName);
                newFlowPanel.Controls.Add(coinPrice);
                newFlowPanel.Controls.Add(coinQuantity);
                newFlowPanel.Controls.Add(coinInvested);
                newFlowPanel.Controls.Add(coinValue);
                newFlowPanel.Controls.Add(coinProfit);
                newFlowPanel.Controls.Add(coinProfitPercent);

                flowLayoutPanel1.Controls.Add(newFlowPanel);

                flowControlCoinCount++;
            }
        }
    }
}
