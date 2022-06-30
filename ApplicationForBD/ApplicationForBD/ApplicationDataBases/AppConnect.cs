using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class AppConnect
    {
        private static SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-CU5NMA4;Initial Catalog=lang2;Integrated Security=True;MultipleActiveResultSets=true");
        //SqlCommand cmd = sqlConnection.CreateCommand();

        public static void OpenConnection() 
        {
            if(sqlConnection.State == System.Data.ConnectionState.Closed)
                sqlConnection.Open();
        }
        public static void OpenConnectionAsync()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
                sqlConnection.OpenAsync();
        }
        public static void CloseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
                sqlConnection.Close();
        }
        public static SqlConnection GetConnection
        {
            get => sqlConnection;
        }

        public static SqlDataReader GetOpenReader(string query)
            => GetOpenSqlCommand(query).ExecuteReader();
        
        public static SqlCommand GetOpenSqlCommand(string query)
        {
            OpenConnection();
            return new SqlCommand(query, GetConnection);
        }
    }
}
