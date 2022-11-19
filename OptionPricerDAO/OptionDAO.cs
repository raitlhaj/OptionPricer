using System.Data;
using System.Data.SqlClient;

namespace OptionPricerDAO
{
    public interface IOptionDAO
    {
        List<OptionDTO> GetOptions();
        void SetOption(OptionDTO optionDTO);
        int FindId(OptionDTO optionDTO); 
        void DeleteOption(OptionDTO optionDTO);
        void UpdateOption(OptionDTO oldOptionDTO, OptionDTO optionDTO);
        void CleanOptiontbl();

    }
    public class OptionDAO : IDisposable,IOptionDAO
    {
        private readonly string connectionString;

        public OptionDAO()
        {
            // server, DB, Credentials
            connectionString = @"Data Source=DESKTOP-EG3O2MT\SQLEXPRESS;Initial Catalog=OptionPricing.Db;Integrated Security=True";
        }

        public void TestSqlReq()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var sql = "select * from tblUnderlyingType";
                var command = new SqlCommand(sql, sqlConnection);
                
                command.ExecuteNonQuery();
                var dataReader = command.ExecuteReader(); // return an object "tab"

                while(dataReader.Read())
                {
                    //0 = id, 1=second column 
                    System.Console.WriteLine($"Id={dataReader[0]}  and ULType= {dataReader.GetString(1)} ");
                  
                }

                sqlConnection.Close();

            }
        }

        public void RunProc()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var sqlStr = "exec pr1 'STOCK' ";
                var sqlcommand = new SqlCommand(sqlStr, sqlConnection);

                sqlcommand.ExecuteNonQuery();
                var dataReader = sqlcommand.ExecuteReader(); // return an object "tab"

                while (dataReader.Read())
                {
                    //0 = id, 1=second column 
                    System.Console.WriteLine($"Id={dataReader[0]}  and ULType= {dataReader.GetString(1)} ");

                }

                sqlConnection.Close();

            }
        }



        public List<OptionDTO> GetOptions()
        {
            var optionDTOs = new List<OptionDTO>();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var sqlcommand = new SqlCommand("UspGetOptions", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                sqlcommand.ExecuteNonQuery();
                var dataReader = sqlcommand.ExecuteReader(); // return an object "tab"

                while (dataReader.Read())
                {
                    var optionDTO = new OptionDTO();
                    optionDTO.Strike = Convert.ToDouble(dataReader[0]);
                    optionDTO.Maturity = Convert.ToDateTime(dataReader[1]);
                    optionDTO.Price = Convert.ToDouble(dataReader[2]);
                    optionDTO.SubType = dataReader[3].ToString().Trim();
                    optionDTO.OptionType = dataReader[4].ToString().Trim();
                    optionDTO.TraderName = dataReader[5].ToString().Trim();
                    optionDTO.UnderlyingName = dataReader[6].ToString().Trim();
                    optionDTO.PricingModel = dataReader[7].ToString().Trim();
                    optionDTO.UnderlyingType = dataReader[8].ToString().Trim();
                    optionDTO.Volatilty = Convert.ToDouble(dataReader[9]);
                    optionDTO.Rate = Convert.ToDouble(dataReader[10]);

                    optionDTOs.Add(optionDTO);

                }
                sqlConnection.Close();
            }
            return optionDTOs;
        }


        public void SetOption(OptionDTO optionDTO)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                //string attributes = $"{optionDTO.UnderlyingName}','{optionDTO.Maturity}','{optionDTO.Strike}','{optionDTO.OptionType}','{optionDTO.SubType}','{optionDTO.Price}','{optionDTO.TraderName}
                //','{optionDTO.Volatilty}','{optionDTO.Rate}','{optionDTO.StockPrice}','{optionDTO.UnderlyingType}','{optionDTO.PricingModel}";
                //var sqlcommand = new SqlCommand($" UspInsertOption {attributes}", sqlConnection)
               var sqlcommand = new SqlCommand("UspInsertOption", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                sqlcommand.Parameters.AddWithValue("@underlying", optionDTO.UnderlyingName);
                sqlcommand.Parameters.AddWithValue("@maturity",optionDTO.Maturity);
                sqlcommand.Parameters.AddWithValue("@strike", optionDTO.Strike);
                sqlcommand.Parameters.AddWithValue("@optionType", optionDTO.OptionType);
                sqlcommand.Parameters.AddWithValue("@optionSubType", optionDTO.SubType);
                sqlcommand.Parameters.AddWithValue("@price", optionDTO.Price);
                sqlcommand.Parameters.AddWithValue("@trader", optionDTO.TraderName);
                sqlcommand.Parameters.AddWithValue("@volatility", optionDTO.Volatilty);
                sqlcommand.Parameters.AddWithValue("@freeRate", optionDTO.Rate);
                sqlcommand.Parameters.AddWithValue("@stockPrice", optionDTO.StockPrice);
                sqlcommand.Parameters.AddWithValue("@ulType", optionDTO.UnderlyingType);
                sqlcommand.Parameters.AddWithValue("@pricingModel", optionDTO.PricingModel);

                sqlcommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void DeleteOption(OptionDTO optionDTO)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
       
                var sqlcommand = new SqlCommand("UspDeleteOption", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                sqlcommand.Parameters.AddWithValue("@optionId", FindId(optionDTO));

                sqlcommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void UpdateOption(OptionDTO oldOptionDTO, OptionDTO optionDTO)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var sqlcommand = new SqlCommand("UspUpdateOption", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                //find option
                sqlcommand.Parameters.AddWithValue("@optionId", FindId(oldOptionDTO));
                sqlcommand.Parameters.AddWithValue("@newunderlying", optionDTO.UnderlyingName);
                sqlcommand.Parameters.AddWithValue("@newmaturity", optionDTO.Maturity);
                sqlcommand.Parameters.AddWithValue("@newstrike", optionDTO.Strike);
                sqlcommand.Parameters.AddWithValue("@newoptionType", optionDTO.OptionType);
                sqlcommand.Parameters.AddWithValue("@newoptionSubType", optionDTO.SubType);
                sqlcommand.Parameters.AddWithValue("@newprice", optionDTO.Price);
                sqlcommand.Parameters.AddWithValue("@newtrader", optionDTO.TraderName);
                sqlcommand.Parameters.AddWithValue("@newvolatility", optionDTO.Volatilty);
                sqlcommand.Parameters.AddWithValue("@newfreeRate", optionDTO.Rate);
                sqlcommand.Parameters.AddWithValue("@newstockPrice", optionDTO.StockPrice);
                sqlcommand.Parameters.AddWithValue("@newulType", optionDTO.UnderlyingType);
                sqlcommand.Parameters.AddWithValue("@newpricingModel", optionDTO.PricingModel);

                sqlcommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public int FindId(OptionDTO optionDTO)
        {
            int ID = -1;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var sqlcommand = new SqlCommand("UspFindOption", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlcommand.Parameters.AddWithValue("@underlying", optionDTO.UnderlyingName);
                sqlcommand.Parameters.AddWithValue("@maturity", optionDTO.Maturity);
                sqlcommand.Parameters.AddWithValue("@strike", optionDTO.Strike);
                sqlcommand.Parameters.AddWithValue("@optionType", optionDTO.OptionType);
                sqlcommand.Parameters.AddWithValue("@optionSubType", optionDTO.SubType);
                sqlcommand.Parameters.AddWithValue("@price", optionDTO.Price);
                sqlcommand.Parameters.AddWithValue("@trader", optionDTO.TraderName);
                sqlcommand.Parameters.AddWithValue("@volatility", optionDTO.Volatilty);
                sqlcommand.Parameters.AddWithValue("@freeRate", optionDTO.Rate);
                sqlcommand.Parameters.AddWithValue("@stockPrice", optionDTO.StockPrice);
                sqlcommand.Parameters.AddWithValue("@ulType", optionDTO.UnderlyingType);
                sqlcommand.Parameters.AddWithValue("@pricingModel", optionDTO.PricingModel);

                SqlParameter retVal = new SqlParameter("@returnId", SqlDbType.Int);
                retVal.Direction = ParameterDirection.ReturnValue;
                sqlcommand.Parameters.Add(retVal);
                var myReader = sqlcommand.ExecuteReader();
                myReader.Close(); 
                sqlcommand.ExecuteNonQuery();
                ID = (int)sqlcommand.Parameters["@returnId"].Value;
         
                sqlConnection.Close();
            }
         return ID;
        }

        public void CleanOptiontbl()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                var sqlcommand = new SqlCommand("UspCleanOptiontbl", sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };


                sqlcommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
        public void Dispose()
        {
           // Nothing to do : throw new NotImplementedException();
        }

    }


    
}