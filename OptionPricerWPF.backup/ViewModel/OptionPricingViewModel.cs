
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;


namespace OptionPricerWPF.ViewModel
{
    public class OptionPricingViewModel: ViewModelBase
    {
        private double _price;
        private double _stockPrice;
        private double _strikePrice;
        private double _riskFreeRate;
        private double _volatility;
        private double _timeToMaturity;
        private string _underlyingName;

        private EnumOptionType _optionType;
        private EnumPricingModel _pricingModel;
        private EnumUnderlyingType _underlyingType;
        private EnumOptionSubType _optionSubType;
        private EnumCurrency _currency;
        //Domain objects creattion
    

        //

        public double OptionPrice
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
        public double StockPrice
        {
            get{
                return _stockPrice;
            }
            set
            {
                SetProperty(ref _stockPrice, value);
            }
        }


        public double StrikePrice
        {
            get
            {
                return _strikePrice;
            }
            set
            {
                SetProperty(ref _strikePrice, value);
            }
        }


        public double RiskFreeInterestRate
        {
            get
            {
                return _riskFreeRate;
            }
            set
            {
                SetProperty(ref _riskFreeRate, value);
            }
        }

        public double Volatility
        {
            get
            {
                return _volatility;
            }
            set
            {
                SetProperty(ref _volatility, value);
            }
        }


        public double TimeToMaturity
        {
            get
            {
                return _timeToMaturity;
            }
            set
            {
                SetProperty(ref _timeToMaturity, value);
            }
        }

        public EnumCurrency Currency
        {
            get { return _currency; }
            set { SetProperty(ref _currency, value); }
        }
        public EnumOptionType OptionType
        {
            get { return _optionType; }
            set {  SetProperty(ref _optionType, value); }
        }
        public EnumOptionSubType OptionSubType
        {
            get { return _optionSubType; }
            set { SetProperty(ref _optionSubType, value); }
        }
        public EnumUnderlyingType UnderlyingType
        {
            get { return _underlyingType; }
            set { SetProperty(ref _underlyingType, value); }
        }
        public EnumPricingModel PricingModel
        {
            get { return _pricingModel; }
            set { SetProperty(ref _pricingModel, value); }
        }

        public string UnderlyingName
        {
            get { return _underlyingName; }
            set { SetProperty(ref _underlyingName, value); }
        }

    


        //butoon and actions
        public ICommand PriceButton { get; set; }
        public ICommand ResetButton { get; set; }
        public ICommand DeleteButton { get; set; }
        public ICommand SearchButton { get; set; }


        public OptionPricingViewModel()
        {
            PriceButton = new RelayCommand(OnClickPriceButton);
            ResetButton = new RelayCommand(OnClickResetButton);
            DeleteButton = new RelayCommand(OnClickDeleteButton);
            SearchButton = new RelayCommand(OnClickSearchButton);

        }
        private void OnClickPriceButton()
        {
            StockPrice = 20;
            
        }
        private void OnClickResetButton()
        {
            //
            StrikePrice = 100;
            StockPrice = 100;
            RiskFreeInterestRate = 0.05;
            Volatility = 0.01;
            TimeToMaturity = 1;
            UnderlyingName = null;
            UnderlyingName = "X";
            OptionPrice = 0;

            //EUNMs
            OptionType = EnumOptionType.AMER;
            PricingModel = EnumPricingModel.BS;
            OptionSubType = EnumOptionSubType.CALL;
            UnderlyingType = EnumUnderlyingType.EQUITY;
            Currency=EnumCurrency.EUR;
        }
        private void OnClickDeleteButton()
        {
            RiskFreeInterestRate = 50;
        }
        private void OnClickSearchButton()
        {
            StrikePrice = 33;
        }



        //Enums
        public enum EnumCurrency
        {
            EUR,
            USD,
            GBP,
            MAD,
            YEN,
            RUR,
            RUB
        }
        public enum EnumOptionType
        {
            EUR,
            AMER,
            ASIA
        }
  
        public enum EnumOptionSubType
        {
            CALL,
            PUT
        }
        public enum EnumUnderlyingType
        {
            EQUITY,
            FX,
            RATE,
            BOND
        }
        public enum EnumPricingModel
        {
            BS,
            CRR,
            MC
        }
    }
}
