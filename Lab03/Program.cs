using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection =
                new SqlConnection("Data Source=LAB1502-05\\SQLEXPRESS;" + "Initial Catalog=Lab03BDB; User Id=userTecsup; Pwd=123456;"+
                "TrustServerCertificate=True");

            try
            {
                sqlConnection.Open();
                Console.WriteLine("Conexion Exitosa");
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());

                throw;
            }
        }
    }
}
