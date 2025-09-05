using System;
using System.Collections.Generic;
using System.Data;
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
            //Desconectada
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(
                    "Data Source=LAB1502-05\\SQLEXPRESS;" +
                    "Initial Catalog=Tecsup2023DB; User Id=userTecsup; Pwd=123456;" +
                    "TrustServerCertificate=True"))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand(
                        "SELECT StudentId, FirstName, LastName FROM Students", sqlConnection);

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);

                    sqlConnection.Close();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine($"{row["StudentId"]}, {row["FirstName"]}, {row["LastName"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
