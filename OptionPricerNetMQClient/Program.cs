// See https://aka.ms/new-console-template for more information
using NetMQ;
using NetMQ.Sockets;
using OptionPricerDomain;
using OptionPricerRepository;
using OptionPricerDAO;
using Infrastructure;
using OptionPricerDomainServices;
using OptionPricerService;

using (var requestSocket = new RequestSocket(">tcp://localhost:5555"))
{
    int i = 0;
    while(true)
    {
        //Test du workflow
        Console.WriteLine("Client is sending Hello!");
        var optionPricerDAO = new OptionDAO();
        OptionRepository optionRepo = new OptionRepository(optionPricerDAO);
        var serializer = new Serialization();
        var facadePricing = new FacadePricingDomainService();
        var optionService = new OptionService(serializer, optionRepo, facadePricing);
  
        UnderlyingType ulType = UnderlyingType.COMMODITY;
        Underlying underlyingValue = new Underlying("AXA", ulType, 56+i/100, 0.07 + i/100, 0.05 + i/100);
        Maturity maturityDate = new Maturity(Convert.ToDateTime("01/02/2023"));
        OptionPrice optionPriceValue = new OptionPrice(10.2);
        OptionType optionTypeValue = OptionType.EUROPEAN;
        PricingModel pricingModelValue = PricingModel.CRR;
        SubOption subOptionValue = SubOption.CALL;
        UnderlyingType underlyingTypeValue = UnderlyingType.COMMODITY;
        Trader traderValue = new Trader("RachidA", 166+i);
        Strike strikeValue = new Strike(50 + i);
 
        var option = new Option(underlyingValue, maturityDate, strikeValue, optionPriceValue, optionTypeValue, pricingModelValue, subOptionValue, underlyingTypeValue, traderValue);
        string OptionJSON =optionService.SerializeOption(option);
        requestSocket.SendFrame(OptionJSON);
        var message = requestSocket.ReceiveFrameString();
        Console.WriteLine(message);
        Thread.Sleep(2000);
        i++;
    }

}