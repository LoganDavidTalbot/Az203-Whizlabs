using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Insert_Data(3, "James", "james@go.com");
            //Update_Data(3, "New Name");
            //Delete_Data(3);
            Read_Data();


            Console.ReadKey();
        }

        private static void Delete_Data(int p_ID)
        {
            SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
            conn.DataSource = "whizlabs-server.database.windows.net";
            conn.UserID = "logan";
            conn.Password = "***";
            conn.InitialCatalog = "whizlabsdb";

            using (SqlConnection connection = new SqlConnection(conn.ConnectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE Customer WHERE ID=");
                sb.Append(p_ID);

                string sql = sb.ToString();

                SqlCommand command = new SqlCommand(sql, connection);
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();

                Console.WriteLine("Record Deleted");
            }
        }

        private static void Update_Data(int p_ID, string p_Name)
        {
            SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
            conn.DataSource = "whizlabs-server.database.windows.net";
            conn.UserID = "logan";
            conn.Password = "m4U2jtjB5a3e";
            conn.InitialCatalog = "whizlabsdb";

            using (SqlConnection connection = new SqlConnection(conn.ConnectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE Customer SET Name='");
                sb.Append(p_Name);
                sb.Append("' WHERE ID=");
                sb.Append(p_ID);

                string sql = sb.ToString();

                SqlCommand command = new SqlCommand(sql, connection);
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();

                Console.WriteLine("Record Update");
            }
        }

        private static void Insert_Data(int p_ID, string p_Name, string p_Email)
        {
            SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
            conn.DataSource = "whizlabs-server.database.windows.net";
            conn.UserID = "logan";
            conn.Password = "m4U2jtjB5a3e";
            conn.InitialCatalog = "whizlabsdb";

            using (SqlConnection connection = new SqlConnection(conn.ConnectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO Customer(ID,Name,Email) VALUES(");
                sb.Append(p_ID);
                sb.Append(",'");
                sb.Append(p_Name);
                sb.Append("','");
                sb.Append(p_Email);
                sb.Append("')");

                string sql = sb.ToString();

                SqlCommand command = new SqlCommand(sql, connection);
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();

                Console.WriteLine("Record Inserted");
            }
        }

        private static void Read_Data()
        {
            SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
            conn.DataSource = "whizlabs-server.database.windows.net";
            conn.UserID = "logan";
            conn.Password = "m4U2jtjB5a3e";
            conn.InitialCatalog = "whizlabsdb";

            using (SqlConnection connection = new SqlConnection(conn.ConnectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT ID,Name,Email FROM Customer");
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        }
                    }
                }
            }
        }
    }
}
