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
        public Form1()
        {
            InitializeComponent();
            this.Visible = false;
            LoginForm login = new LoginForm(this);
            login.Show();
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

        private void button4_Click(object sender, EventArgs e)
        {
            TvSeries series = new TvSeries(this);
            series.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Person person = new Person(this);
            person.Show();
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                QueryService.InsertQueryFromFile("tables");
                QueryService.InsertQueryFromFile("movies");
                QueryService.InsertQueryFromFile("tvSeries");
                QueryService.InsertQueryFromFile("genre");
                QueryService.InsertQueryFromFile("profession");
                QueryService.InsertQueryFromFile("person");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
