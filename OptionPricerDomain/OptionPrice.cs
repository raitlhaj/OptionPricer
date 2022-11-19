using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricerDomain
{
    public class OptionPrice
    {
        public double Price { get; set; }
        public OptionPrice(double price)
        {
            if (price < 0) throw new Exception("Option price should not be negative!");
            this.Price = price;
        }
    }
}
