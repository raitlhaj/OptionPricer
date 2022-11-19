// See https://aka.ms/new-console-template for more information
using Infrastructure;
using OptionPricerDAO;
using OptionPricerDomain;
using OptionPricerDomainServices;
using OptionPricerRepository;
using OptionPricerService;

var optionPricerDAO = new OptionDAO();
var optionDTO=new OptionDTO();
var updatedOptionDTO = new OptionDTO();

/// <summary>
/// INIT THE OPTION (A) WE WANT TO INSERT
/// </summary>

/*optionDTO.UnderlyingName = "SG-AC";
optionDTO.Maturity = Convert.ToDateTime("01/02/2023");
optionDTO.Strike = 155;
optionDTO.OptionType = "EUROPEAN";
optionDTO.SubType = "PUT";
optionDTO.Price = 152.3;
optionDTO.TraderName = "WILL.YHU";
optionDTO.Volatilty = 0.023;
optionDTO.Rate = 0.05;
optionDTO.StockPrice = 35;
optionDTO.PricingModel = "CRR";
optionDTO.UnderlyingType = "EQUITY";

updatedOptionDTO.UnderlyingName = "SG";
updatedOptionDTO.Maturity = Convert.ToDateTime("01/02/2023");
updatedOptionDTO.Strike = 155;
updatedOptionDTO.OptionType = "EUROPEAN";
updatedOptionDTO.SubType = "PUT";
updatedOptionDTO.Price = 152.3;
updatedOptionDTO.TraderName = "DIJA.ULTI";
updatedOptionDTO.Volatilty = 0.023;
updatedOptionDTO.Rate = 0.05;
updatedOptionDTO.StockPrice = 35;
updatedOptionDTO.PricingModel = "BS";
updatedOptionDTO.UnderlyingType = "EQUITY"; */

//GET ALL OPTIONS
/*var result=optionPricerDAO.GetOptions();
foreach (var option in result)
{
    System.Console.WriteLine(option);
}
Console.ReadKey(); */

//INSERT THE OPTION (A) 
//System.Console.WriteLine(optionDTO.ToString());
//optionPricerDAO.SetOption(optionDTO);
//optionPricerDAO.DeleteOption(optionDTO);
//optionPricerDAO.UpdateOption(optionDTO, updatedOptionDTO);

//System.Console.WriteLine(optionDTO.ToString());

/*OptionRepository optionRepo = new OptionRepository(optionPricerDAO);

UnderlyingType ulType = UnderlyingType.BOND;
Underlying underlyingValue = new Underlying("AXA", ulType, 56, 0.07, 0.05);
Maturity maturityDate = new Maturity(Convert.ToDateTime("01/02/2023"));
Strike strikeValue = new Strike(50);
Strike strikeValue2 = new Strike(60);
OptionPrice optionPriceValue = new OptionPrice(10.2);
OptionType optionTypeValue = OptionType.EUROPEAN;
PricingModel pricingModelValue = PricingModel.CRR;
SubOption subOptionValue = SubOption.CALL;
UnderlyingType underlyingTypeValue = UnderlyingType.COMMODITY;
Trader traderValue = new Trader( "RACHRACH", 166);

var option1 = new Option(underlyingValue, maturityDate, strikeValue, optionPriceValue, optionTypeValue, pricingModelValue, subOptionValue, underlyingTypeValue, traderValue);
var option2 = new Option(underlyingValue, maturityDate, strikeValue2, optionPriceValue, optionTypeValue, pricingModelValue, subOptionValue, underlyingTypeValue, traderValue); */

//optionRepo.InsertOption(option1);
//optionRepo.UpdateOption(option1,option2);


//Test DomainServices

/*var bSDomainService=new BSDomainService();
var crrDomainService = new CRRDomainService();
var mcDomainService = new MCDomainService();*/


/*Console.WriteLine("Black scholes model: "+bSDomainService.Price(option1));
Console.WriteLine("COX ROX Robinestein: "+crrDomainService.Price(option1));
Console.WriteLine("MonteCarlo: "+mcDomainService.Price(option1)); */

//Console.ReadKey();


//Test Seer +Des

OptionRepository optionRepo = new OptionRepository(optionPricerDAO);
var serializer = new Serialization();
var facadePricing = new FacadePricingDomainService();
var optionService = new OptionService(serializer, optionRepo, facadePricing);
int i = 0; 

UnderlyingType ulType = UnderlyingType.COMMODITY;
Underlying underlyingValue = new Underlying("AXA", ulType, 56, 0.07, 0.05);
Maturity maturityDate = new Maturity(Convert.ToDateTime("01/02/2023"));
OptionPrice optionPriceValue = new OptionPrice(10.2);
OptionType optionTypeValue = OptionType.EUROPEAN;
PricingModel pricingModelValue = PricingModel.CRR;
SubOption subOptionValue = SubOption.CALL;
UnderlyingType underlyingTypeValue = UnderlyingType.COMMODITY;
Trader traderValue = new Trader("RACHRACH", 166);
Strike strikeValue = new Strike(50 + i++);

var option = new Option(underlyingValue, maturityDate, strikeValue, optionPriceValue, optionTypeValue, pricingModelValue, subOptionValue, underlyingTypeValue, traderValue);
string OptionJSON = optionService.SerializeOption(option); 

Console.ReadLine();

