using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OptionPricerDomain
{

    public class Underlying
    {
        public string Name { get; set; }

        //[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public UnderlyingType UnderlyingType { get; set; }
        public double SpotPrice { get; set; }
        public double Volatility { get; set; }
        public double Rate { get; set; }
        
        // faire les aautres parametes comme classes et checker leurs validités 
        // faire les tests pour ces classes
        
        public Underlying(string name, UnderlyingType underlyingType, double spotPrice,double volatility,double rate)
        {

            if (string.IsNullOrEmpty(name)) throw new Exception("The Underlying name can't be empty!");
            if(underlyingType == UnderlyingType.UNKOWN) throw new Exception("You've to specify a correct Ul Type!");
            if(spotPrice<0) throw new Exception("You've to specify a correct spotvalue!");
            if (volatility < 0) throw new Exception("You've to specify a correct volatility!");

            this.Name = name;
            this.UnderlyingType = underlyingType;
            this.SpotPrice= spotPrice;
            this.Volatility = volatility;
            this.Rate = rate;

        }
    }
}
