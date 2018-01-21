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
        private string rank;
        private string marketcap;
        private string percent_changed_1h;
        private string percent_changed_24h;
        private string percent_changed_7d;
        private float quantity;
        private float netCost;
        private float? value;
        private float? profit;
        private float? profitPercent;
        private string apiLink;

        //TODO - Add convertsion properties to convert float to string to bind to textboxes
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

        public float? Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Price)));
            }
        }

        public string PriceToString
        {
            get
            {
                if (price.HasValue)
                {
                    return "$" + price.Value.ToString("0.00");
                }
                return null;
            }
        }

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

        public string Percent_Change_24hr
        {
            get
            {
                return percent_changed_24h;
            }
            set
            {
                percent_changed_24h = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Percent_Change_24hr)));
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

        public float Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Quantity)));
            }

        }

        public string QuantityToString
        {
            get
            {
                return quantity.ToString();
            }
        }

        public float NetCost
        {
            get
            {
                return netCost;
            }
            set
            {
                netCost = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(NetCost)));
            }
        }

        public string NetCostToString
        {
            get
            {
                return "$" + netCost.ToString("0.00");
            }
        }

        public float? Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public string ValueToString
        {
            get
            {
                if (value.HasValue)
                {
                    return "$" + value.Value.ToString("0.00");
                }
                return null;
            }
        }

        public float? Profit
        {
            get
            {
                return profit;
            }
            set
            {
                profit = value;
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
                if (profit.HasValue)
                {
                    return "$" + profit.Value.ToString("0.00");
                }
                return null;
            }
        }

        public float? ProfitPercent
        {
            get
            {
                return profitPercent;
            }
            set
            {
                profitPercent = value;
                InvokePropertyChanged(new PropertyChangedEventArgs(nameof(ProfitPercent)));
            }
        }

        public string ProfitPercentToString
        {
            get
            {
                if (profitPercent.HasValue)
                {
                    return profitPercent.Value.ToString("0.0") + "%";
                }
                return null;
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
