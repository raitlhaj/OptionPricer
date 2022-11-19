using OptionPricerDAO;
using OptionPricerDomain;

namespace OptionPricerRepository
{
    public interface IOptionRepository
    {
        void InsertOption(Option option);
        void UpdateOption(Option option1, Option option2);
        List<Option> GetOptions();
        void DeleteOption(Option option);


    }

    public class OptionRepository : IOptionRepository
    {
        private readonly IOptionDAO optionDAO;
        //public OptionRepository() { }
        public OptionRepository(IOptionDAO optionDAO)
        {
            this.optionDAO = optionDAO;
        }
        public void InsertOption(Option option)
        {
            var optionDTO = FromDomainToDTO(option);
            optionDAO.SetOption(optionDTO);
        }


        public void UpdateOption(Option oldoption, Option newoption)
        {
            var oldoptionDTO = FromDomainToDTO(oldoption);
            var newoptionDTO =  FromDomainToDTO(newoption);
            optionDAO.UpdateOption(oldoptionDTO, newoptionDTO);
        }

        private OptionDTO FromDomainToDTO(Option option)
        {
            var optionDTO = new OptionDTO();

            optionDTO.Strike = option.Strike.StrikeValue;
            optionDTO.Maturity = option.Maturity.Date;
            optionDTO.TraderName = option.Trader.Name;
            optionDTO.Price = option.OptionPrice.Price;
            optionDTO.StockPrice = option.Underlying.SpotPrice;
            optionDTO.Volatilty = option.Underlying.Volatility;
            optionDTO.Rate = option.Underlying.Rate;
            optionDTO.UnderlyingName = option.Underlying.Name;
            optionDTO.Rate = option.Underlying.Rate;
            optionDTO.OptionType = option.OptionType.ToString();
            optionDTO.SubType = option.SubOption.ToString();
            optionDTO.PricingModel = option.PricingModel.ToString();
            optionDTO.UnderlyingType = option.UnderlyingType.ToString();

            return optionDTO;
        }

        private Option FromDTOTODomain(OptionDTO optionDTO)
        {
            Strike strike=new Strike(optionDTO.Strike);
            Maturity maturity = new Maturity(optionDTO.Maturity);
            UnderlyingType ulType = (UnderlyingType)Enum.Parse(typeof(UnderlyingType),optionDTO.UnderlyingType);
            Underlying underlyingValue = new Underlying(optionDTO.UnderlyingName, ulType,optionDTO.StockPrice, optionDTO.Volatilty, optionDTO.Rate);
            Strike strikeValue = new Strike(optionDTO.Strike);
            OptionPrice optionPriceValue = new OptionPrice(optionDTO.Price);
            OptionType optionTypeValue = (OptionType)Enum.Parse(typeof(OptionType), optionDTO.OptionType);
            PricingModel pricingModelValue = (PricingModel)Enum.Parse(typeof(PricingModel), optionDTO.PricingModel);
            SubOption subOptionTypeValue = (SubOption)Enum.Parse(typeof(SubOption), optionDTO.SubType);
            Trader traderValue = new Trader(optionDTO.TraderName);

            return new Option(underlyingValue, maturity, strikeValue, optionPriceValue, optionTypeValue, pricingModelValue, subOptionTypeValue, ulType, traderValue);
        }
        public List<Option> GetOptions()
        {
            var optionDTOs = new List<OptionDTO>();
            var options = new List<Option>();

            optionDTOs = optionDAO.GetOptions();

            foreach( var optionDTO in optionDTOs)
            {
                options.Add(FromDTOTODomain(optionDTO));
            }

            return options;
        }

        public void DeleteOption(Option option)
        {
            var optionDTO=new OptionDTO();
            optionDTO=FromDomainToDTO(option);
            optionDAO.DeleteOption(optionDTO);
         }


    }
}