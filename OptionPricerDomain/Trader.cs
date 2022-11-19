namespace OptionPricerDomain
{
    public class Trader
    {
        public int IdTrader { get; set; }
        public string Name { get; set; }
        public Trader(string name, int idTrader=0) 
        {
            if (idTrader < 0) throw new Exception("IdTrader should be greater than 0 !");
            if (string.IsNullOrEmpty(name)) throw new Exception("TarderName is empty !");
            this.IdTrader=idTrader;
            this.Name=name;
        }
    }


}