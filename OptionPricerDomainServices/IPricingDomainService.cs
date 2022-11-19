using OptionPricerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricerDomainServices
{
    public interface IPricingDomainService
    {
        double Price(Option option);
    }
}
