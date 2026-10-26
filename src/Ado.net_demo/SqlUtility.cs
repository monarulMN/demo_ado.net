using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ado.net_demo
{
    public class SqlUtility(string connectionString)
    {

        public void ExecuteSql(string sql, Dictionary<string, object> parameters )
        {
            using SqlCommand command = CreateCommand(sql, parameters);

            int rowAffected = command.ExecuteNonQuery();
        }

        public List<Dictionary<string, object>> ExecuteQuery(string query, 
            Dictionary<string, object>? parameters = null)
        {
            
            using SqlCommand command = CreateCommand(query, parameters);

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                for(int i = 0; i< reader.FieldCount; i++)
                {                    
                    row.Add(reader.GetName(i), reader.GetValue(i));
                }
                data.Add(row);
            }
            return data;
        }

        private SqlCommand CreateCommand(string sql, Dictionary<string, object>? parameters = null)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            SqlCommand command = new SqlCommand(sql, connection);

            if(parameters is not null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);

                    //other way
                    //command.Parameters.Add(new SqlParameter(param.Key, param.Value ?? DBNull.Value));
                }
            }
            

            return command;
        }
    }
}
