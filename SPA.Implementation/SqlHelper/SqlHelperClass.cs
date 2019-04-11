using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;



namespace SPA.Implementation.SqlHelper
{
 
    static class SqlHelperClass
    {
        /// <summary> 
        /// Set the connection, command, and then execute the command with non query. 
        /// </summary> 
        public static async Task<Int32> ExecuteNonQuery(String connectionString, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect  
                    // type is only for OLE DB.   
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary> 
        /// Set the connection, command, and then execute the command and only return one value. 
        /// </summary> 
        public static async  Task<Object> ExecuteScalarAsync(String connectionString, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return await cmd.ExecuteScalarAsync();
                }
            }
        }

        /// <summary> 
        /// Set the connection, command, and then execute the command with query and return the reader. 
        /// </summary> 
        public static  async Task<SqlDataReader> ExecuteReaderAsync(String connectionString, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the  
                // IDataReader is closed. 
                SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                return reader;
            }
        }
    }
}
