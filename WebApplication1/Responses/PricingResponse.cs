using OptionPricerWebApp.Requests;

namespace OptionPricerWebApp.Responses
{
    public class PricingResponse:PricingRequest
    {
        public double Price { get; set; }
    }
}
