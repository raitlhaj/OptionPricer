using NSubstitute;
using NUnit.Framework;
using OptionPricerDAO;
using OptionPricerDomain;
using OptionPricerRepository;
using System;
using System.Collections.Generic;

namespace OptionRepository.Tests
{
    public class OptionRepositoryTests
    {
        private readonly IOptionDAO optionDAO;
        private readonly IOptionRepository optionRepository;
        
        public OptionRepositoryTests()
        {
          // var optionPricerDAO = new OptionDAO();
         //  var optionRepository = new  OptionRepository(optionPricerDAO);
          this.optionDAO=Substitute.For<IOptionDAO>();   // creer une fake couche DAO
          this.optionRepository = new OptionPricerRepository.OptionRepository(this.optionDAO);
        }

        [SetUp]
        public void Setup()
        {
         
        }
        /*
        [Test]
       public void InsertOptionDomainTest()
        {
          

            var optionA = CreateOptionDomain(133);
            var optionB = CreateOptionDomain(156);

            var optionsExpected = new List<Option>();
            optionsExpected.Add(optionA);
            optionsExpected.Add(optionB);

            optionRepository.InsertOption(optionA);
            optionRepository.InsertOption(optionA);
            this.optionDAO.SetOption(optionDTO).Returns();
            var options = this.optionRepository.


            Assert.IsTrue(AssertGetOptionDomain(options, optionDTO));

        } 
        */
        [Test]
        public void GetOptionsRepositoryTest()
        {
            //Arrange
            var optionA = CreateOptionDTO("AXAR", "CRR", "EQUITY");
            var optionB = CreateOptionDTO("AXAR", "BS", "EQUITY");

            List<OptionDTO> optionDTO=new List<OptionDTO>();
            optionDTO.Add(optionA);
            optionDTO.Add(optionB);
            //action
            this.optionDAO.GetOptions().Returns(optionDTO);
            List<Option> options= optionRepository.GetOptions();
;            //Asssert
            Assert.IsTrue(AssertGetOptionDomain(options, optionDTO));
        }

/*
        [Test]
        public void UpdateOptionsTest()
        {
            //Arrange
            var optionOld = CreateOptionDomain(133);
            var optionNew = CreateOptionDomain(156);

            //Action
            List<OptionDTO> optionDTO = new List<OptionDTO>();
            optionDTO.Add(optionNew);
            

            this.optionDAO.FindId(      ).Returns(1);

            optionRepository.InsertOption(optionOld);
            optionRepository.UpdateOption(optionOld, optionNew);
           
            var options = this.optionRepository.GetOptions();

            //Asssert
            Assert.True(options[0].Equals(optionNew));
        } */
        [Test]
        public void DeleteOptionDomainTest()
        {
            //Arrange
            var optionOld = CreateOptionDomain(133);
            var optionA = CreateOptionDTO("AXAR", "CRR", "EQUITY");

            //Action
            this.optionDAO.SetOption(optionA);
            this.optionDAO.DeleteOption(optionA);
            optionRepository.DeleteOption(optionOld);

            var options = this.optionRepository.GetOptions();

            //Assert
            Assert.IsEmpty(options);

        }

        public bool AssertGetOptionDomain(List<Option> options, List<OptionDTO> optionDTO)
        {
            for( int i =0;i<options.Count;i++)
            {
                if ( options[i].Trader.Name != optionDTO[i].TraderName ||
                     options[i].Underlying.Name != optionDTO[i].UnderlyingName ||
                     options[i].UnderlyingType.ToString() != optionDTO[i].UnderlyingType ||
                     options[i].SubOption.ToString() != optionDTO[i].SubType ||
                     options[i].OptionPrice.Price != optionDTO[i].Price ||
                     options[i].Maturity.Date != optionDTO[i].Maturity ||
                     options[i].Strike.StrikeValue != optionDTO[i].Strike ||
                     options[i].OptionType.ToString() != optionDTO[i].OptionType ||
                     options[i].PricingModel.ToString() != optionDTO[i].PricingModel) return false;
            }
            return true; 
    }

        public Option CreateOptionDomain(double myStrike)
        {
            UnderlyingType ulType = UnderlyingType.BOND;
            Underlying underlyingValue = new Underlying("AXA", ulType, 56, 0.07, 0.05);
            Maturity maturityDate = new Maturity(Convert.ToDateTime("01/02/2023"));
            Strike strikeValue = new Strike(myStrike);
            OptionPrice optionPriceValue = new OptionPrice(10.2);
            OptionType optionTypeValue = OptionType.EUROPEAN;
            PricingModel pricingModelValue = PricingModel.CRR;
            SubOption subOptionValue = SubOption.PUT;
            UnderlyingType underlyingTypeValue = UnderlyingType.COMMODITY;
            Trader traderValue = new Trader("RACHRACH", 166);

            return new Option(underlyingValue, maturityDate, strikeValue, optionPriceValue, optionTypeValue, pricingModelValue, subOptionValue, underlyingTypeValue, traderValue);
        }


        private OptionDTO CreateOptionDTO(string UlName, string Mdl, string Eqty)
        {
            var optionDTO = new OptionDTO();

            optionDTO.UnderlyingName = UlName;
            optionDTO.Maturity = Convert.ToDateTime("01/02/2023");
            optionDTO.Strike = 155;
            optionDTO.OptionType = "EUROPEAN";
            optionDTO.SubType = "PUT";
            optionDTO.Price = 152.3;
            optionDTO.TraderName = "WILL.YHU";
            optionDTO.Volatilty = 0.023;
            optionDTO.Rate = 0.05;
            optionDTO.StockPrice = 35;
            optionDTO.PricingModel = Mdl;
            optionDTO.UnderlyingType = Eqty;

            return optionDTO;
        }

    }
}