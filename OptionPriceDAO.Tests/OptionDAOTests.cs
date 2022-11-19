using NUnit.Framework;
using OptionPricerDAO;
using System;

namespace OptionPriceDAO.Tests
{
    public class OptionDAOTests
    {
        private readonly IOptionDAO optionDAO;

        public OptionDAOTests()
        {
            optionDAO = new OptionDAO();
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void InsertAndGetOptionDTOs()
        {
            optionDAO.CleanOptiontbl();
       
            //Arrange
            var option1 = Create("AXA", "CALL", 50,"01/06/2056" , "EUROPEAN",13, "Will.WAL", 0.07, 0.05, 26, "BS", "EQUITY");
            var option2 = Create("SG", "CALL", 50, "01/06/2056", "AMERICAN", 12, "Will.WAL", 0.07, 0.05, 25, "BS", "EQUITY");

            //Action
            optionDAO.SetOption(option1);
            optionDAO.SetOption(option2);
            var optionDTOs = optionDAO.GetOptions();

            //Assert
            AssertOptions(option1, optionDTOs[0]);
            AssertOptions(option2, optionDTOs[1]);
        }
        [Test]
        public void UpdateAndGetOptionDTOs()
        {
            optionDAO.CleanOptiontbl();

            //Arrange
            var oldOptionDTO = Create("AXA", "CALL", 50, "01/06/2056", "EUROPEAN", 13, "Will.WAL", 0.07, 0.05, 26, "BS", "EQUITY");
            var newoptionDTO = Create("SG", "CALL", 50.5, "01/06/2056", "AMERICAN", 12, "Will.WAL", 0.07, 0.05, 25, "BS", "EQUITY");

            //Action
            optionDAO.SetOption(oldOptionDTO);
            optionDAO.UpdateOption( oldOptionDTO,  newoptionDTO);

            var optionDTOs = optionDAO.GetOptions();

            //Assert
            AssertOptions(newoptionDTO, optionDTOs[0]);
            
        }
        [Test]
        public void DeleteAndGetOptionDTOs()
        {
            optionDAO.CleanOptiontbl();

            //Arrange
            var oldOptionDTO = Create("AXA", "CALL", 50, "01/06/2056", "EUROPEAN", 13, "Will.WAL", 0.07, 0.05, 26, "BS", "EQUITY");

            //Action
            optionDAO.SetOption(oldOptionDTO);
            optionDAO.DeleteOption(oldOptionDTO);

            var optionDTOs = optionDAO.GetOptions();

            //Assert
            Assert.IsEmpty( optionDTOs);

        }

        [Test]
        public void FindAndGetOptionDTOs()
        {
            //optionDAO.CleanOptiontbl();

            //Arrange
            var optionDTO = Create("AXA", "CALL", 45, "01/06/2056", "EUROPEAN", 13, "Will.WAL", 0.07, 0.05, 26, "BS", "EQUITY");

            //Action
            optionDAO.SetOption(optionDTO);
            int id= optionDAO.FindId(optionDTO);

            //var optionDTOs = optionDAO.GetOptions();

            //Assert
            Assert.IsTrue(id==2);

        }

        private OptionDTO Create(string UlName, string SType, double Strike,string Maturity,string OpType, double Price, string Trader, double Vol, double R, double S, string Mdl,string Eqty)
        {
            var optionDTO = new OptionDTO( );

            optionDTO.UnderlyingName = UlName;
            optionDTO.Maturity = Convert.ToDateTime(Maturity);
            optionDTO.Strike = Strike;
            optionDTO.OptionType = OpType;
            optionDTO.SubType = SType;
            optionDTO.Price = Price;
            optionDTO.TraderName = Trader;
            optionDTO.Volatilty =Vol;
            optionDTO.Rate =R;
            optionDTO.StockPrice = S;
            optionDTO.PricingModel = Mdl;
            optionDTO.UnderlyingType = Eqty;

            return optionDTO;
        }
        private void AssertOptions(OptionDTO expected, OptionDTO reel)
        {
            Assert.True(expected.Equals(reel));
        }



    }
}