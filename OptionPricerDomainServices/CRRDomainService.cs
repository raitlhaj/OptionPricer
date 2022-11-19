using OptionPricerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricerDomainServices
{
    public class CRRDomainService:IPricingDomainService
    {
        Option option;
        public CRRDomainService()
        {
        }
        public CRRDomainService(Option option)
        {
            this.option = option;
        }
       
        public void ShowMe(double [,] spot,int T)
        {
            for (int i = 0; i < 2 * T + 1; i++)
            {
                for (int j = 0; j < T + 1; j++)
                {
                    Console.Write(" " + spot[i, j]);
                }
                    Console.WriteLine();
            }
                    Console.WriteLine();
        }
          
public double Price(Option option)
        {
            int T = (int)(option.Maturity.Date - DateTime.Now).TotalDays / 252;
            double S0 = option.Underlying.SpotPrice;
            double u = option.Underlying.Rate + option.Underlying.Volatility;
            double d = option.Underlying.Rate - option.Underlying.Volatility;
            double r = option.Underlying.Rate;
            double strike = option.Strike.StrikeValue;
            
            var tree = new double[2*T + 1, T+1];
            var spot = new double[2*T + 1, T+1];
            var intrinsec = new double[2*T+1, T+1];

            //Init 
            for (int i = 0; i < 2*T+1; i++)
            for (int j = 0; j < T+1; j++)
            {
                spot[i, j] = 0;
                tree[i, j] = 0;
                intrinsec[i, j] = 0;
            }

            spot[T, 0] = S0;

            //Tree Construction
          


                for (int i = 0; i < 2*T+1; i++)
                for (int j = 1; j < T + 1; j++)
                {
                    if (i < T)
                        spot[i, j] = spot[i + 1, j - 1] * (1 + u);
                    if (i >= T)
                        spot[i, j] = spot[i - 1, j - 1] * (1 + d);
                }


            //Probablilty q
            double q = (u - r) / (u - d);

            //intrinsec values
                for (int i = 0; i < 2 * T + 1; i++)
                for (int j = 1; j < T+1; j++)
                {
                    if (spot[i,j]!=0)
                        intrinsec[i,j] = Math.Max(((option.SubOption == SubOption.CALL) ? 1 : -1) * (spot[i,j] - strike), 0.0);
                }


                if (option.OptionType==OptionType.EUROPEAN )
                {  //payoff calculation :
                    for (int i = 0; i < 2*T+1; i++)
                        if (spot[i,T ]!=0)
                            tree[i,T ] = Math.Max(((option.SubOption == SubOption.CALL) ? 1 : -1) * (spot[i,T] - strike), 0.0);

                    for (int j = T; j >= 1; j--)
                    {
                        for (int i = 0; i < 2 * T + 1; i++)
                        {
                          if (spot[i, j - 1] != 0)
                                tree[i, j -1] = (1 / (1 + r)) * (q * tree[i - 1, j] + (1 - q) * tree[i + 1, j]);
                        }
                    }
                 }

                if (option.OptionType == OptionType.AMERICAN)
                {
                    for (int j = T ; j >= 0; j--)
                        for (int i = 1; i < 2*T+1 - 2; i++)
                        {
                            if (spot[i,j]!=0)
                                tree[i,j] = (1 / (1 + r)) * Math.Max(q * tree[i - 1,j] + (1 - q) * tree[i + 1,j], intrinsec[i,j - 1]);
                        }
                }

            return tree[T, 0];
        }

    }



}
