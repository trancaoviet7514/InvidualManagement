using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    class DataProvider
    {
        private static DataProvider instance;

        internal static DataProvider Instance
        {
            get { if (DataProvider.instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }
        private DataProvider() { }

        private string connectionSRT = "Data Source=.\\SQLEXPRESS;Initial Catalog=InvidualManager;Integrated Security=True";
       
        public DataTable ExecuteQuery(string query, object[] parameter = null) {

            DataTable datatable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSRT)){
                
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null) {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
            

                SqlDataAdapter adapter = new SqlDataAdapter(command);
            
                adapter.Fill(datatable);
                connection.Close();
            }

            return datatable;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data;

            using (SqlConnection connection = new SqlConnection(connectionSRT))
            {

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }


                data = command.ExecuteNonQuery();

            
                connection.Close();
            }

            return data;
        }

        public object ExcuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSRT))
            {

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }


                data = command.ExecuteScalar();
                connection.Close();
            }

            return data;
        }

    }
}
