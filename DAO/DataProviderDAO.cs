using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.DAO
{
    class DataProviderDAO
    {
        private static DataProviderDAO instance;

        private string connectionStr = @"Data Source=DELL-PC;Initial Catalog=QLGaraOto;Integrated Security=True";

        public static DataProviderDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DataProviderDAO();
                }
                return DataProviderDAO.instance;
            }
            private set { instance = value; }
        }

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using(SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int i = 0;
                if(parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    foreach (string item in listPara)
                    {
                        if(item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                        
                    }
                }
                
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();

                return data;
            }

        }
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int i = 0;
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    foreach (string item in listPara)
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

                return data;
            }
        }
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int i = 0;
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    foreach (string item in listPara)
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

                return data;
            }
        }

        public string GetLastIdOfTable(string TableName, string IdFieldName)
        {
            string query = "SELECT MAX(" + IdFieldName + ") FROM dbo." + TableName + "";
            if(DataProviderDAO.Instance.ExecuteScalar(query).ToString()=="")
            {
                return "0";
            }
            return DataProviderDAO.Instance.ExecuteScalar(query).ToString();
        }
    }
}
