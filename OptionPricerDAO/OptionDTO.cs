using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricerDAO
{
    public class OptionDTO
    {      
        //Op.id, Op.strike, Op.maturity, Pr.priceValue, So.subType, ot.optionType,Trd.TraderName,Ul.name, PricingModel, UL type
        public double Strike { get; set; }
        public DateTime Maturity { get; set; }
        public double Price { get; set; }
        public double StockPrice { get; set; }
        public double Volatilty { get; set; }
        public double Rate { get; set; }
        public string? SubType { get; set; }
        public string? TraderName { get; set; }
        public string? OptionType { get; set; }
        public string? UnderlyingName { get; set; }
        public string? PricingModel { get; set; }
        public string? UnderlyingType { get; set; }


        public override string ToString()
        {
            return $"==============================\nOption: {OptionType}, {SubType} \nwith Strike: {Strike} \nMaturity: {Maturity} \nPrice:  {Price} \non UnderlyingName:  {UnderlyingName},\nVolatilty: {Volatilty} \nRate: {Rate} \nUnderlyingType: {UnderlyingType}    \nTraderName: {TraderName}  \nUnder:  {PricingModel} Model \n==============================";
        }
        public override bool Equals(Object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;
            var optionDTO = obj as OptionDTO;
            if (optionDTO == null) return false;
            if(UnderlyingName == optionDTO.UnderlyingName && Strike == optionDTO.Strike && UnderlyingType == optionDTO.UnderlyingType)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
