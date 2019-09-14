using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

        public static DataTable ViewQuery(string sql)  //  Gia diavasma kataxoriseon kai provoli sti forma
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            dataSet.Reset();
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ////// filling DataSet with result from NpgsqlDataAdapter
                da.Fill(dataSet);
                ////// since it C# DataSet can handle multiple tables, we will select first
                dataTable = dataSet.Tables[0];
                conn.Close();
                return dataTable;
            }
            catch (Exception e)
            {
                dataTable = null;
                DbException = e.ToString();
                return dataTable;
            }
        }
    }
}
