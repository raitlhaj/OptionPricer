using Infrastructure;
using OptionPricerDomain;
using OptionPricerDomainServices;
using OptionPricerRepository;

namespace OptionPricerService
{
    public interface IOptionService
    {
         Option DeserializeOption(string str);
         void PersistOption(Option option);
         string SerializeOption(Option option);
        void EnrichOptionWithPrice(Option option);
        public void UpdateOption(Option oldOption, Option newOption);
    }

    public class OptionService:IOptionService
    {
        private readonly ISerialization serializer;
        private readonly IOptionRepository optionRepository;
        private readonly IFacadePricingDomainService pricer;
        //request response

        public OptionService(ISerialization serializer, IOptionRepository optionRepository, IFacadePricingDomainService pricer)
        {
            this.serializer = serializer;
            this.optionRepository = optionRepository;
            this.pricer = pricer;
        }

        //De-serialization method
        public Option DeserializeOption(string str)
        {
            return serializer.Deserialize<Option>(str);
        }

        //Data persistence

        public void PersistOption(Option option)
        {
            optionRepository.InsertOption(option);
        }
        //Pricing
        public void EnrichOptionWithPrice(Option option)
        {
            pricer.EnrichOptionWithPrice(option);
        }

        //Pricing persisting
        /*
          the same method - persistOption has to be called at this step  
        */

        public void UpdateOption(Option oldOption,Option newOption)
        {
            optionRepository.UpdateOption(oldOption, newOption);
        }


        //Option price serialization
        public string SerializeOption(Option option)
        {
            return serializer.Serialize(option);
        }

        //send a response

        // next time
    }
}