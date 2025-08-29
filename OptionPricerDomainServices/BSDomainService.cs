using OptionPricerDomain;

namespace OptionPricerDomainServices
{


    public class BSDomainService : IPricingDomainService 
    {
        //private readonly IOptionRepository optionRepository;
        public BSDomainService()
        {

        }
       public double Price(Option option)
        {
            int way;

            double theta = (option.Maturity.Date - DateTime.Now).TotalDays/252, sig = option.Underlying.Volatility;
            double dl = (Math.Log(option.Underlying.SpotPrice/option.Strike.Value) + (option.Underlying.Rate + sig * sig * 0.5) * theta) / sig * Math.Sqrt(theta);
            double d2 = dl - sig;

            if (option.SubOption == SubOption.CALL) way = 1;
            else way = -1;

            return way * option.Underlying.SpotPrice* Phi(way * dl) - way * option.Strike.Value * Math.Exp(-option.Underlying.Rate * theta) *Phi(way * d2);
        }

        public double Phi(double z)
        {
            // Approximation de la fonction de distribution cumulative normale (par des séries de Taylor)
            // Fonction reprise du document de Bernt Arne Ødegaard
            if (z > 6.0)
                return 1.0; // éviter les valeurs illicites
            if (z < -6.0)
                return 0.0;

            double b1 = 0.31938153;
            double b2 = -0.356563782;
            double b3 = 1.781477937;
            double b4 = -1.821255978;
            double b5 = 1.330274429;
            double p = 0.2316419;
            double c2 = 0.3989423;
            double a = Math.Abs(z);
            double t = 1.0 / (1.0 + a * p);
            double b = c2 * Math.Exp((-z) * (z / 2.0));
            double n = ((((b5 * t + b4) * t + b3) * t + b2) * t + b1) * t;
            n = 1.0 - b * n;

            if (z < 0.0)
                n = 1.0 - n;
            return n;
        }
    }
}