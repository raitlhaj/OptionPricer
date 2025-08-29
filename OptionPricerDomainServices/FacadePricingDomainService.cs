using OptionPricerDomain;

namespace OptionPricerDomainServices
{
    public interface IFacadePricingDomainService
    {
       void EnrichOptionWithPrice(Option option);
    }
    public class FacadePricingDomainService : IFacadePricingDomainService
    {
        private readonly IPricingDomainService bSDomainService;
        private readonly IPricingDomainService cRRDomainService;
        private readonly IPricingDomainService mCDomainService;
        public FacadePricingDomainService()
        {
            this.bSDomainService = new BSDomainService();
            this.cRRDomainService = new CRRDomainService();
            this.mCDomainService = new MCDomainService();
        }

        public void EnrichOptionWithPrice(Option option)
        {
            switch(option.PricingModel)
            {
                case PricingModel.CRR: option.OptionPrice.Price = cRRDomainService.Price(option); break;
                case PricingModel.BS: option.OptionPrice.Price = bSDomainService.Price(option); break;
                case PricingModel.MC: option.OptionPrice.Price = mCDomainService.Price(option); break;
            }
        }
    }
}
