using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricerDomain
{
    public class Strike
    {
        public double Value;
        public Strike(double strike)
        {
            if (strike < 0) throw new Exception("The strike value shouldn't be négative!");
            this.Value = strike;
        }
    }
}
