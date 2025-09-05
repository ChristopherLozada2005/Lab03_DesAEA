using Microsoft.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string connectionString =
            "Data Source=LAB1502-05\\SQLEXPRESS;" +
            "Initial Catalog=Tecsup2023DB; User Id=userTecsup; Pwd=123456;" +
            "TrustServerCertificate=True";

        public MainWindow()
        {
            InitializeComponent();
            LoadStudents(); // Cargar todos al inicio
        }

        private void LoadStudents(string filter = "")
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT StudentId, FirstName, LastName FROM Students";

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    query += " WHERE FirstName LIKE @filter OR LastName LIKE @filter";
                }

                SqlCommand cmd = new SqlCommand(query, conn);

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    cmd.Parameters.AddWithValue("@filter", "%" + filter + "%");
                }

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        StudentId = (int)reader["StudentId"],
                        FirstName = reader["FirstName"].ToString() ?? string.Empty,
                        LastName = reader["LastName"].ToString() ?? string.Empty
                    });

                }
            }

            dgStudents.ItemsSource = students;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadStudents(txtSearch.Text.Trim());
        }
    }
}
