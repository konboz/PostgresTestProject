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
    //I klasi auti egine giati oi methodoi edw kanoun leitourgies pou xreiazontai se polles formes, opote anti na exeis ton idio kwdika se polla simeia kai polles formes apla kaleis autes tis methodous
    //To oti einai static simainei oti den xreiazetai na kaneis new kathe fora pou thes na kaleseis mia methodo tis i na diavaseis mia metavliti tis opws to dbException, tha borouse na einai kai kanoniki klasi
    //alla apti stigmi pou de xreiazetai constructor kai apla exei mesa methodous pou ekteloun standar pragmata voleuei to static
    public static class QueryService
    {
        public static string connstring;
        //public static string connstring = "Server=localhost;Port=5432;Database=test;User ID=postgres;Password=passw0rd;";
        public static string DbException; // edw apothikeuoun oi methodoi to sfalma an iparxei

        public static void InsertQueryFromFile(string sqlScript) //Idia douleia me apo katw apla diavazei apo arxeio to script
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            FileInfo file = new FileInfo("Database/" + sqlScript + ".sql");
            string script = file.OpenText().ReadToEnd();
            conn.Open();
            NpgsqlCommand query = new NpgsqlCommand(script, conn);
            query.ExecuteNonQuery();
            conn.Close();
        }

        public static bool InsertQuery(string sql) // Gia eisagwgi dedomenwn
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
            DataSet dataSet = new DataSet(); //den xerw giati kai an xreiazetai to dataset, i diafora me to dataTable einai oti mporei na apothikeusei pollous pinakes
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
