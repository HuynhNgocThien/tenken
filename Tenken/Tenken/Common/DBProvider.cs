using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Security.Cryptography;

namespace TenkenWeb.Common
{
  public class DBProvider
  {
        static SqlConnection dbConnection = null;
        public static SqlConnection getDbConnection()
        {
            if (dbConnection == null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
                try {
                    dbConnection = new SqlConnection(connectionString);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            return dbConnection;
        }
        public static string EncodeSHA1(string pass)

        {

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] input = System.Text.Encoding.UTF8.GetBytes(pass);

            input = sha1.ComputeHash(input);

            System.Text.StringBuilder output = new System.Text.StringBuilder();

            foreach (byte b in input)

            {
                output.Append(b.ToString("x1").ToLower());

            }

            pass = output.ToString();

            return pass;

        }
    }
}
