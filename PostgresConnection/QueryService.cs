using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgresConnection
{
    public static class QueryService
    {
        static string connstring = "Server=localhost;Port=5432;Database=test;User ID=postgres;Password=passw0rd;";
        public static string DbException;

        public static void InsertQueryFromFile(string sqlScript)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            FileInfo file = new FileInfo("Database/" + sqlScript + ".sql");
            string script = file.OpenText().ReadToEnd();
            conn.Open();
            NpgsqlCommand query = new NpgsqlCommand(script, conn);
            query.ExecuteNonQuery();
            conn.Close();
        }

        public static bool InsertQuery(string sql)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                NpgsqlCommand query = new NpgsqlCommand(sql, conn);
                query.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                DbException = e.ToString();
                return false; 
            }  
        }
    }
}
