using Npgsql;
using NpgsqlTypes;
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
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                //First run with empty database
                //CreateTables("tables");
                //InsertQuery("profession");
                //InsertQuery("genre");
                //InsertQuery("movies");
                //InsertQuery("tvSeries");

                //  Custom queries

                //string addGnr = "insert into movieCategorization (tvCategorizationId, movieId, genreId) values (8, 9, 9)";
                //ExcecuteInsertQuery(addGnr);
                //string search = "select coalesce(MAX(genreId), 0 ) from genre where title = 'Documentary'";
                //conn.Open();
                //NpgsqlCommand newQuery = new NpgsqlCommand(search, conn);
                ////newQuery.Parameters.Add(new NpgsqlParameter("genreId", NpgsqlDbType.Integer));
                //Object genreId = newQuery.ExecuteScalar();
                //conn.Close();
                //string search2 = "select movieId from movieCategorization where genreId=" + genreId;
                //conn.Open();
                //NpgsqlCommand com = new NpgsqlCommand(search2, conn);
                //NpgsqlDataReader dRead = com.ExecuteReader();
                //List<int> movies = new List<int>();
                //while (dRead.Read())
                //{
                //    for (int i = 0; i < dRead.FieldCount; i++)
                //    {
                //        movies.Add(int.Parse(dRead[i].ToString()));
                //    }
                //}
                //string moviesrc = "select title from movies where movieId=" + dRead[i];
                //NpgsqlCommand query = new NpgsqlCommand(moviesrc, conn);
                //Object movie = newQuery.ExecuteScalar();
                //movies.Add(movie.ToString());
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                throw;
            }
        }
        private void InsertQuery(string sqlScript)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            FileInfo file = new FileInfo("Database/" + sqlScript + ".sql");
            string script = file.OpenText().ReadToEnd();
            conn.Open();
            NpgsqlCommand query = new NpgsqlCommand(script, conn);
            query.ExecuteNonQuery();
            conn.Close();
        }

        private void CreateTables(string sqlScript)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            FileInfo file = new FileInfo("Database/" + sqlScript + ".sql");
            string script = file.OpenText().ReadToEnd();
            conn.Open();
            NpgsqlCommand createTables = new NpgsqlCommand(script, conn);
            createTables.ExecuteNonQuery();
            conn.Close();
        }

        private void GetQuery(string sql)  //  Gia diavasma kataxoriseon kai provoli sti forma
        {
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
      
            dataSet.Reset();
            ////// filling DataSet with result from NpgsqlDataAdapter
            da.Fill(dataSet);
            ////// since it C# DataSet can handle multiple tables, we will select first
            dataTable = dataSet.Tables[0];
            ////// connect grid to DataTable
            dataGridView1.DataSource = dataTable;

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomQueryForm queryForm = new CustomQueryForm(this);
            queryForm.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Movies movies = new Movies(this);
            movies.Show();
            Hide();
        }
    }
}
