using Infrastructure;
using OptionPricerDAO;
using OptionPricerDomainServices;
using OptionPricerRepository;
using OptionPricerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricerNetMQService
{
    public class Starter:IDisposable
    {
        private readonly IDependancyInjectionManager dependancyInjectionManager;
        public Starter()
        {
            this.dependancyInjectionManager = new DependancyInjectionManager();
        }

        public void RegisterInstancies()
        {
            dependancyInjectionManager.Register<IOptionDAO, OptionDAO>();
            dependancyInjectionManager.Register<IOptionRepository, OptionRepository>();
            dependancyInjectionManager.Register<IFacadePricingDomainService, FacadePricingDomainService>();
            dependancyInjectionManager.Register<IOptionService, OptionService>();
            dependancyInjectionManager.Register<ISerialization, Serialization>();

        }

        public T Resolve<T>()
        {
            return dependancyInjectionManager.Resolve<T>();
        }

        public void Dispose()
        {
            dependancyInjectionManager.Dispose();
        }

    }
}
