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
        static string connectionString =
            "Data Source=LAB1502-05\\SQLEXPRESS;" +
            "Initial Catalog=Tecsup2023DB; User Id=userTecsup; Pwd=123456;" +
            "TrustServerCertificate=True";

        static void Main(string[] args)
        {
            Console.WriteLine("=== Forma Desconectada (DataTable) ===");
            var list1 = GetStudentsByDataTable();
            foreach (var s in list1)
                Console.WriteLine($"{s.StudentId} - {s.FirstName} {s.LastName}");

            Console.WriteLine("\n=== Forma Conectada (DataReader) ===");
            var list2 = GetStudentsByList();
            foreach (var s in list2)
                Console.WriteLine($"{s.StudentId} - {s.FirstName} {s.LastName}");
        }

        //Desconectado
        static List<Student> GetStudentsByDataTable()
        {
            List<Student> students = new List<Student>();
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT StudentId, FirstName, LastName FROM Students", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows)
            {
                students.Add(new Student
                {
                    StudentId = Convert.ToInt32(row["StudentId"]),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString()
                });
            }
            return students;
        }

        // Conectado
        static List<Student> GetStudentsByList()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT StudentId, FirstName, LastName FROM Students", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        StudentId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2)
                    });
                }
            }
            return students;
        }
    }

}
