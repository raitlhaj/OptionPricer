using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricerDomain
{
    public class Maturity
    {
        public DateTime Date { get; set; }

        public Maturity(DateTime date)
        {
            if (date < DateTime.Today) throw new Exception("You entred a previous date for the maturity! ");   
            // check bankholiday ->using table Calendar
            this.Date = date;
        }
    }
}
