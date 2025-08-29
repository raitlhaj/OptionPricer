using MersenneTwister;
using OptionPricerDomain;
using System.Collections.Concurrent;
using System.Net.WebSockets;

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

        public double  S(Option option)
        {
            int T = (int)(option.Maturity.Date - DateTime.Now).TotalDays / 252;
            return option.Underlying.SpotPrice * Math.Exp((option.Underlying.Rate - Math.Pow(option.Underlying.Volatility, 2) / 2) * T + option.Underlying.Volatility * BoxmullerMC()[1]);
        }
        public double Price(Option option)
        {
            int N = 1000000;
            double premium = 0, T = (int)(option.Maturity.Date - DateTime.Now).TotalDays / 252, theta = T, e = 0;
            
            
            if (option.SubOption == SubOption.CALL) e = 1;
            if (option.SubOption == SubOption.PUT) e = -1;

            int numThreads = Environment.ProcessorCount; // use all available cores
            double[] results = new double[numThreads];

     
            Thread[] threads = new Thread[numThreads];
            for (int i = 0; i < numThreads; i++)
            {
                int threadIndex = i;
                threads[i] = new Thread(() =>
                {
                var Str = S(option);
                results[threadIndex] = Math.Exp(-option.Underlying.Rate * theta) * ((Str - option.Strike.Value) * e > 0 ? (S(option) - option.Strike.Value) * e : 0);
                });
                threads[i].Start();
            }

            // Wait for all threads to complete
            foreach (var thread in threads)
            {
                thread.Join();
            }

            // Calculate the final result by averaging the individual thread results
            double sum = 0;
            foreach (var result in results)
            {
                sum += result;
            }

       
            return sum / numThreads;
        }

    }


}

