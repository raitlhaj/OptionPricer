using OptionPricerDomain;
using MersenneTwister;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricerDomainServices
{
    public class MCDomainService : IPricingDomainService
    {
        public double Uniform()
        {
            //generation pesudo aleatoire number using MersenneTwister
            return Randoms.Next(10);
        }
        public double[] BoxmullerMC()
        {
            var tab = new double[2];
            tab[0] = Math.Sqrt(-2 * Math.Log(Uniform())) * Math.Cos(8 * Math.Atan(1) * Uniform());
            tab[0] = Math.Sqrt(-2 * Math.Log(Uniform())) * Math.Sin(8 * Math.Atan(1) * Uniform());

            return tab;
        }

        public double S(Option option)
        {
            int T = (int)(option.Maturity.Date - DateTime.Now).TotalDays / 252;
            return option.Underlying.SpotPrice * Math.Exp((option.Underlying.Rate - Math.Pow(option.Underlying.Volatility, 2) / 2) * T + option.Underlying.Volatility * BoxmullerMC()[1]);
        }
        public double Price(Option option)
        {
            double premium = 0, T = (int)(option.Maturity.Date - DateTime.Now).TotalDays / 252, theta = T, St, europeanVal, intrinsecVal, e = 0;
            double N = 10000;

            if (option.SubOption == SubOption.CALL) e = 1;
            if (option.SubOption == SubOption.PUT) e = -1;

            
            for (int i = 0; i < N; i++)
            {
                double ST = S(option);
                europeanVal = Math.Exp(-option.Underlying.Rate * theta) * ((ST - option.Strike.StrikeValue) * e > 0 ? (ST - option.Strike.StrikeValue) * e : 0);

                for (int k = 0; k < T; k++)
                {
                    //intrinsec value
                    St = S(option);
                    intrinsecVal = Math.Exp(-option.Underlying.Rate * theta) * ((St - option.Strike.StrikeValue) * e > 0 ? (St - option.Strike.StrikeValue) * e : 0);
                    // if(europeanVal<intrinsecVal ) { std::cout<<std::endl<< "Sec:"<<i<<" Exec possible at time="<<k<< "premium="<<intrinsecVal<<" vs. "<<europeanVal;}
                }

                premium += europeanVal;
            }

            return premium / N;
        }

    }


}

