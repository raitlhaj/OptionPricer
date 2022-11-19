using Microsoft.AspNetCore.Mvc;
using WebApplication1.Responses;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PricingController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<PricingController> _logger;

        public PricingController(ILogger<PricingController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetOptions")]
        public IEnumerable<PricingResponse> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new PricingResponse() { PRICE=0.23}).ToArray();
        }


        [HttpGet(Name = "GetPrice")]
        public double GetPrice()
        {
            return 1.23564;
        }
    }
}