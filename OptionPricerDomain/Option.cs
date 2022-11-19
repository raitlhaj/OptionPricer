using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricerDomain
{

    public class Option
    {
        public Underlying Underlying { get; set; }
        public Maturity Maturity { get; set; }
        public Strike Strike { get; set; }
        public OptionPrice OptionPrice { get; set; }
        public Trader Trader { get; set; }
        public OptionType OptionType { get; set; }
        public PricingModel PricingModel { get; set; }
        public SubOption SubOption{ get; set; }
        public UnderlyingType UnderlyingType { get; set; }

        public Option(Underlying underlyingValue,
                    Maturity maturityDate, 
                    Strike strikeValue, 
                    OptionPrice optionPriceValue ,
                    OptionType optionTypeValue, 
                    PricingModel pricingModelValue,
                    SubOption subOptionValue, 
                    UnderlyingType underlyingTypeValue,
                    Trader traderValue)
        {
            this.Trader = traderValue;
            this.Underlying = underlyingValue;
            this.UnderlyingType = underlyingTypeValue;
            this.SubOption=   subOptionValue;
            this.OptionPrice= optionPriceValue;
            this.Maturity = maturityDate;
            this.Strike = strikeValue;
            this.OptionType= optionTypeValue;
            this.PricingModel = pricingModelValue;
        }




    }
}
