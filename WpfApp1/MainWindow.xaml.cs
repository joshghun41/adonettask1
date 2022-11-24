using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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


    public partial class MainWindow : Window
    {
        public class Author
        {
            public object Id;
            public object Name;
            public object SurName;
        }
        public Author author11 { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //using (var conn = new SqlConnection())
            //{
            //    conn.ConnectionString = @"Data Source=STHQ0127-07;Initial Catalog=Library;User ID=admin;Password=admin;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //    SqlDataReader reader = null;
            //    conn.Open();
            //    string query = "SELECT * FROM Authors";
            //    using (SqlCommand command = new SqlCommand(query, conn))
            //    {
            //        reader = command.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            Author author = new Author();

            //        }
            //    }
            //}


        }

        private void Button_Click(object sender, RoutedEventArgs e)//showall
        {
            this.DataContext = this;
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = @"Data Source=STHQ0127-06;Initial Catalog=Library;User ID=admin;Password=admin;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlDataReader reader = null;
                conn.Open();
                string query = "SELECT * FROM Authors";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                      author11 = new Author()
                      {
                          Id=reader[0],
                          Name = reader[1],
                          SurName=reader[2]
                      };
                        listview1.Items.Add($"{author11.Id}-{author11.Name}-{author11.SurName}");

                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            listview1.Items.Clear ();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = $@"Data Source=STHQ0127-06;Initial Catalog=Library;User ID=admin;Password=admin;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                conn.Open();
                string query = @"INSERT INTO Authors(Id,FirstName,LastName)
                VALUES(@id,@firstName,@lastName)";

                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = System.Data.SqlDbType.Int;
                paramId.Value = int.Parse(textbox1.Text);


                var paramFirstName = new SqlParameter();
                paramFirstName.ParameterName = "@firstName";
                paramFirstName.SqlDbType = System.Data.SqlDbType.NVarChar;
                paramFirstName.Value = (textbox2.Text);


                var paramLastName = new SqlParameter();
                paramLastName.ParameterName = "@lastName";
                paramLastName.SqlDbType = System.Data.SqlDbType.NVarChar;
                paramLastName.Value = (textbox3.Text);


                using (SqlCommand command = new SqlCommand(query, conn))
                {

                    command.Parameters.Add(paramId);
                    command.Parameters.Add(paramFirstName);
                    command.Parameters.Add(paramLastName);

                    var result = command.ExecuteNonQuery();
                    listview1.Items.Add($@"{paramId.Value}-{paramFirstName.Value},{paramLastName.Value}");
                }

            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = $@"Data Source=STHQ0127-06;Initial Catalog=Library;User ID=admin;Password=admin;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                conn.Open();
                string query = @"delete Authors 
                                where Id=@id";

                var paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                paramId.SqlDbType = System.Data.SqlDbType.Int;
                paramId.Value = int.Parse(textbox1.Text);


                using (SqlCommand command = new SqlCommand(query, conn))
                {

                    command.Parameters.Add(paramId);
                    var result=command.ExecuteNonQuery();
                   

                }

            }


        }
    }
}
