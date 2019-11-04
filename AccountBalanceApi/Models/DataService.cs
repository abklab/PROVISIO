using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AccountBalanceApi.Models
{
    public class DataService
    {
         string connectionString = ConfigurationManager.ConnectionStrings["PROVISIO_ConnectionString"].ToString();

        private SqlConnection connection;



        #region Data Access Operations
        public DataTable GetData(SqlCommand cmd)
        {
            DataTable data = new DataTable();
            string msg = "";
            try
            {
                connection = new SqlConnection(connectionString);
                using (connection)
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(data);

                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            
            return data;
        }

        public string Save(SqlCommand cmd)
        {
            connection = new SqlConnection(connectionString);
            string msg = "";
            try
            {
                using (connection)
                {

                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    msg = i > 0 ? "Success" : "Error";

                }
                return msg;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                connection.Close();
              
                cmd.Dispose();
            }
            return msg;
        }
        #endregion

        public List<CustomerAccount> GetCustomerAccounts()
        {
            using (var command = new SqlCommand())
            {
                var accounts = new List<CustomerAccount>();

                command.CommandText = "[usp_Get_All_Accounts]";

                var dt = GetData(command);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        var acc = new CustomerAccount
                        {
                            Id = (int)r["Id"],

                            ReferenceNumber = r["ReferenceNumber"].ToString(),

                            CustomerBalance = Convert.ToDecimal(r["CustomerBalance"]),

                            B_AccountNumber = r["B_AccountNumber"].ToString(),

                            B_AccountName = r["B_AccountName"].ToString(),

                            B_Name = r["B_Name"].ToString(),

                            MomoNumber = r["MomoNumber"].ToString(),

                            MomoProvider = r["MomoProvider"].ToString(),

                            TelephoneNumber = r["TelephoneNumber"].ToString(),

                            LastAccessed =Convert.ToDateTime( r["LastAccessed"]),

                            Notes = r["Notes"].ToString()
                        };
                        accounts.Add(acc);
                    }
                }
                return accounts;

            }
        }

        public CustomerAccount GetCustomerAccountsByAccountMomoNumber(string number)
        {
            using (var command = new SqlCommand())
            {
               
                command.CommandText = "usp_Get_By_AccountNumber";

                command.Parameters.AddWithValue("@accountNumber", number);

                var dt = GetData(command);

                if (dt != null && dt.Rows.Count > 0)
                {
                    var r = dt.Rows[0];
                      var acc = new CustomerAccount
                        {
                            Id = (int)r["Id"],

                            ReferenceNumber = r["ReferenceNumber"].ToString(),

                            CustomerBalance = Convert.ToDecimal(r["CustomerBalance"]),

                            B_AccountNumber = r["B_AccountNumber"].ToString(),

                            B_AccountName = r["B_AccountName"].ToString(),

                            B_Name = r["B_Name"].ToString(),

                            MomoNumber = r["MomoNumber"].ToString(),

                            MomoProvider = r["MomoProvider"].ToString(),

                            TelephoneNumber = r["TelephoneNumber"].ToString(),

                            LastAccessed = Convert.ToDateTime(r["LastAccessed"]),

                            Notes = r["Notes"].ToString()
                        };
                  return acc;
                }
                return null;
            }
        }

        public string CreateTransaction(CustomerAccount account)
        {
            var last_accessed = DateTime.Now;
            using (var command = new SqlCommand())
            {
              
                command.CommandText = "[usp_Create_Account]";

                command.Parameters.AddWithValue("@referenceNumber", account.ReferenceNumber);
                command.Parameters.AddWithValue("@CustomerBalance", account.CustomerBalance);
                command.Parameters.AddWithValue("@B_AccountNumber", account.B_AccountNumber);
                command.Parameters.AddWithValue("@B_AccountName", account.B_AccountName);
                command.Parameters.AddWithValue("@B_Name", account.B_Name);
                command.Parameters.AddWithValue("@MomoNumber", account.MomoNumber);
                command.Parameters.AddWithValue("@MomoProvider", account.MomoProvider);
                command.Parameters.AddWithValue("@TelephoneNumber", account.TelephoneNumber);
                command.Parameters.AddWithValue("@Notes", account.Notes);
                command.Parameters.AddWithValue("@LastAccessed", last_accessed);

                var result = Save(command);
                var response = result == "Success" ? "S-100" : "E-500";
                return response;
            }
        }

        public string UpdateAccount(CustomerAccount account)
        {
            using (var command = new SqlCommand())
            {              
                command.CommandText = "[usp_Update_AccountBalance]";

                command.Parameters.AddWithValue("@referenceNumber", account.ReferenceNumber);
                command.Parameters.AddWithValue("@CustomerBalance", account.CustomerBalance);
                command.Parameters.AddWithValue("@B_AccountNumber", account.B_AccountNumber);
                command.Parameters.AddWithValue("@MomoNumber", account.MomoNumber);
                command.Parameters.AddWithValue("@Notes", account.Notes);

                var result = Save(command);
                var response = result == "Success" ? "US-100" : "EU-500";
                return response;
            }
        }    
    }
}