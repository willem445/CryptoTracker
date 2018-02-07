using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    public class CoinModel : INotifyPropertyChanged
    {

        private string name;
        private string symbol;

        private float? price;
        private string priceToString;

        private string rank;
        private string marketcap;
        private string percent_changed_1h;
        private string percent_changed_24h;
        private string percent_changed_7d;

        private float quantity;
        private string quantityToString;

        private float netCost;
        private string netCostToString;

        private float? value;
        private string valueToString;

        private float? profit;
        private string profitToString;

        private float? profitPercent;
        private string profitPercentToString;

        private string apiLink;

        public struct CoinNameStruct
        {
            public string Id;
            public string Name;
            public string Symbol;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                symbol = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Symbol)));
            }
        }

        //PRICE**************************
        public float? Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                PriceToString = price.Value.FloatToMonetary();
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Price)));
            }
        }

        public string PriceToString
        {
            get
            {
                return priceToString;
            }
            set
            {
                priceToString = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(PriceToString)));
            }
        }

        //RANK***************************
        public string Rank
        {
            get
            {
                return rank;
            }
            set
            {
                rank = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Rank)));
            }
        }

        public string MarketCap
        {
            get
            {
                return marketcap;
            }
            set
            {
                marketcap = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(MarketCap)));
            }
        }

        public string Percent_Change_1h
        {
            get
            {
                return percent_changed_1h;
            }
            set
            {
                percent_changed_1h = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Percent_Change_1h)));
            }
        }

        public string Percent_Change_24h
        {
            get
            {
                return percent_changed_24h;
            }
            set
            {
                percent_changed_24h = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Percent_Change_24h)));
            }
        }

        public string Percent_Change_7d
        {
            get
            {
                return percent_changed_7d;
            }
            set
            {
                percent_changed_7d = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Percent_Change_7d)));
            }
        }

        //Quantity***************************
        public float Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                QuantityToString = quantity.ToString();
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Quantity)));
            }

        }

        public string QuantityToString
        {
            get
            {
                return quantityToString;
            }
            set
            {
                quantityToString = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(QuantityToString)));
            }
        }

        //Net Cost*************************
        public float NetCost
        {
            get
            {
                return netCost;
            }
            set
            {
                netCost = value;
                NetCostToString = netCost.FloatToMonetary();
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(NetCost)));
            }
        }

        public string NetCostToString
        {
            get
            {
                return netCostToString;
            }
            set
            {
                netCostToString = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(NetCostToString)));
            }
        }

        //Value*************************
        public float? Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                ValueToString = this.value.Value.FloatToMonetary();
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public string ValueToString
        {
            get
            {
                return valueToString;
            }
            set
            {
                valueToString = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(ValueToString)));
            }
        }

        //Profit*************************
        public float? Profit
        {
            get
            {
                return profit;
            }
            set
            {
                profit = value;
                ProfitToString = profit.Value.FloatToMonetary();
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Profit)));
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Color)));
            }
        }

        public Color Color
        {
            get
            {
                return (Profit > 0) ? Color.Green : Color.Red;
            }
        }


        public string ProfitToString
        {
            get
            {
                return profitToString;
            }
            set
            {
                profitToString = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(ProfitToString)));
            }
        }

        //Profit Percent********************
        public float? ProfitPercent
        {
            get
            {
                return profitPercent;
            }
            set
            {
                profitPercent = value;
                ProfitPercentToString = profitPercent.Value.FloatToPercent();
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(ProfitPercent)));
            }
        }

        public string ProfitPercentToString
        {
            get
            {
                return profitPercentToString;
            }
            set
            {
                profitPercentToString = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(ProfitPercentToString)));
            }
        }

        public string APILink
        {
            get
            {
                return apiLink;
            }
            set
            {
                apiLink = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(APILink)));
            }
        }


        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

        #endregion
    }
}
