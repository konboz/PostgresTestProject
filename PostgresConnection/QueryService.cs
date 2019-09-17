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
    // static class for standard sql query functions and global variable storage
    public static class QueryService
    {
        public static string connstring;
        public static string DbException; // last reported exception is saved here for viewing

        //Insert/update/delete query method that takes filename input
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

        //Insert/update/delete query method that takes full sql string input
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

        //Method for select queries
        public static DataTable ViewQuery(string sql)  
        {
            DataSet dataSet = new DataSet();            //can store multiple tables
            DataTable dataTable = new DataTable();      //  for storing result data
            dataSet.Reset();
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                //filling DataSet with result from NpgsqlDataAdapter
                da.Fill(dataSet);
                //store first table to the dataTable
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
