using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgresConnection
{
    public partial class Form1 : Form
    {
        private DataSet dataSet = new DataSet();
        private DataTable dataTable = new DataTable();
        // PostgeSQL-style connection string
        string connstring = "Server=localhost;Port=5432;Database=test;User ID=postgres;Password=passw0rd;";
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);

                FileInfo file1 = new FileInfo("Database/profession.sql");
                FileInfo file2 = new FileInfo("Database/person.sql");
                FileInfo file3 = new FileInfo("Database/genre.sql");
                FileInfo file4 = new FileInfo("Database/movies.sql");
                FileInfo file5 = new FileInfo("Database/tvSeries.sql");

                conn.Open();
                //string sqlJobs = file1.OpenText().ReadToEnd();
                //string sqlPerson = file2.OpenText().ReadToEnd();
                //string sqlGenre = file3.OpenText().ReadToEnd();
                //string sqlMovies = file4.OpenText().ReadToEnd();
                //string sqlTv = file5.OpenText().ReadToEnd();
                ////// data adapter making request from our connection
                //NpgsqlCommand insertJobs = new NpgsqlCommand(sqlJobs, conn);
                //insertJobs.ExecuteNonQuery();
                //NpgsqlCommand insertPerson = new NpgsqlCommand(sqlPerson, conn);
                //insertPerson.ExecuteNonQuery();
                //NpgsqlCommand insertGenre = new NpgsqlCommand(sqlGenre, conn);
                //insertGenre.ExecuteNonQuery();
                //NpgsqlCommand insertMovies = new NpgsqlCommand(sqlMovies, conn);
                //insertMovies.ExecuteNonQuery();
                //NpgsqlCommand insertTv = new NpgsqlCommand(sqlTv, conn);
                //insertTv.ExecuteNonQuery();
                string addCrew = "insert into movieAssignment (assignmentId, personId, professionId, movieId) values (1, 5, 4, 1)";
                ExcecuteInsertQuery(addCrew);
                //NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                //// i always reset DataSet before i do
                //// something with it.... i don't know why :-)
                //dataSet.Reset();
                ////// filling DataSet with result from NpgsqlDataAdapter
                //da.Fill(dataSet);
                ////// since it C# DataSet can handle multiple tables, we will select first
                //dataTable = dataSet.Tables[0];
                ////// connect grid to DataTable
                //dataGridView1.DataSource = dataTable;
                conn.Close();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                throw;
            }
        }
        private void ExcecuteInsertQuery(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();

            NpgsqlCommand newQuery = new NpgsqlCommand(sql, conn);
            newQuery.ExecuteNonQuery();

            conn.Close();
        }
    }
}
