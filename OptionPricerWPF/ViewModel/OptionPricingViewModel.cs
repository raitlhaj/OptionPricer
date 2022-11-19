using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System;
using System.Windows.Input;
using OptionPricerDAO;
using OptionPricerRepository;
using Infrastructure;
using OptionPricerDomainServices;
using OptionPricerService;
using OptionPricerDomain;
using NetMQ.Sockets;
using NetMQ;
using OptionPricerNetMQService;
using System.Threading;

namespace OptionPricerWPF.ViewModel
{
    public interface IOptionPricingViewModel
    {
        
    }
    public class OptionPricingViewModel: ViewModelBase, IOptionPricingViewModel
    {
        private double _price;
        private double _stockPrice;
        private double _strikePrice;
        private double _riskFreeRate;
        private double _volatility;
        private DateTime _maturity;
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


        public DateTime Maturity
        {
            get
            {
                return _maturity;
            }
            set
            {
                SetProperty(ref _maturity, value);
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
            using (var requestSocket = new RequestSocket(">tcp://localhost:5555"))
            {
                Console.WriteLine("Application is sending a request :\n");
                var optionPricerDAO = new OptionDAO();
                OptionRepository optionRepo = new OptionRepository(optionPricerDAO);
                var serializer = new Serialization();
                var facadePricing = new FacadePricingDomainService();
                var optionService = new OptionService(serializer, optionRepo, facadePricing);

                Underlying underlyingValue = new Underlying(UnderlyingName, (UnderlyingType)UnderlyingType, StockPrice, Volatility, RiskFreeInterestRate);
                Maturity maturityDate = new Maturity(Convert.ToDateTime(Maturity));
                OptionType optionTypeValue = (OptionType)OptionType;
                PricingModel pricingModelValue = (PricingModel)PricingModel;
                SubOption subOptionValue = SubOption.CALL;
                Trader traderValue = new Trader("RACHRACH", 122);
                Strike strikeValue = new Strike(StrikePrice);
                OptionPrice optionPriceValue = new OptionPrice(1e-8);

                var option = new Option(underlyingValue, maturityDate, strikeValue, optionPriceValue, optionTypeValue, pricingModelValue, subOptionValue, underlyingValue.UnderlyingType, traderValue);
                string OptionJSON = optionService.SerializeOption(option);
               
                requestSocket.SendFrame(OptionJSON);
                
                var message = requestSocket.ReceiveFrameString();
                Console.WriteLine(message);
                OptionPrice = optionService.DeserializeOption(message).OptionPrice.Price;
            }
        }
        private void OnClickResetButton()
        {
            StrikePrice = 40;
            StockPrice = 36.23;
            RiskFreeInterestRate = 0.05;
            Volatility = 0.01;
            Maturity = DateTime.Now.AddYears(2);
            UnderlyingName = "AXA";
            OptionPrice = 1e-8;

            //EUNMs
            OptionType = EnumOptionType.AMERICAN;
            PricingModel = EnumPricingModel.BS;
            OptionSubType = EnumOptionSubType.CALL;
            UnderlyingType = EnumUnderlyingType.EQUITY;
            Currency=EnumCurrency.EUR;
        }
        private void OnClickDeleteButton()
        {
            
        }
        private void OnClickSearchButton()
        {
            
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
            EUROPEAN,
            AMERICAN,
            ASIAN
        }
  
        public enum EnumOptionSubType
        {
            CALL,
            PUT
        }
        public enum EnumUnderlyingType
        {          
            EQUITY,
            RATE,
            BOND,
            COMMODITY,
            FIC,
            UNKOWN
        }
        public enum EnumPricingModel
        {
            BS,
            CRR,
            MC
        }
    }
}
