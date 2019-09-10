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
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // PostgeSQL-style connection string
                string connstring = "Server=localhost;Port=5432;Database=test;User ID=postgres;Password=passw0rd;";

                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                FileInfo file = new FileInfo("Database/person.sql");
                //FileInfo file2 = new FileInfo("Database/Person.sql");

                conn.Open();
                // quite complex sql statement
                string sql = file.OpenText().ReadToEnd();
                // data adapter making request from our connection
                NpgsqlCommand insert = new NpgsqlCommand(sql, conn);
                insert.ExecuteNonQuery();
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
                // since we only showing the result we don't need connection anymore
                conn.Close();
            }
            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show(msg.ToString());
                throw;
            }
        }
    }
}
