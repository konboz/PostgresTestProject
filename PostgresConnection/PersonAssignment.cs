using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgresConnection
{
    public partial class PersonAssignment : Form
    {
        public int personId;
        public int movieId;
        public int tvserieId;
        public int profId;

        public PersonAssignment(int id)
        {
            InitializeComponent();
            DatagridLoad("profession", dataGridView3);
            this.personId = id;
            button1.Visible = false;
        }

        //Fills dataGrid with data from given table
        public void DatagridLoad(string table, DataGridView dataGrid) 
        {
            DataTable initData;
            initData = QueryService.ViewQuery("Select * from " + table + ";");
            if (initData != null)
            {
                dataGrid.DataSource = initData;
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Παρουσιάστηκε σφάλμα κατά την επικοινωνία με τη βάση!\nΠροβολή σφάλματος;", "Σφάλμα", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    var exception = new ExceptionForm(); 
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (radioButton1.Checked)
            {
                label1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                label1.Visible = true;
                movieId = int.Parse(dataGridView3.SelectedRows[0].Cells[0].Value.ToString());
                button1.Visible = true;
            }
            else if (radioButton2.Checked)
            {
                label1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                label1.Visible = true;
                tvserieId = int.Parse(dataGridView3.SelectedRows[0].Cells[0].Value.ToString());
                button1.Visible = true;
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label2.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
            label2.Visible = true;
            profId = int.Parse(dataGridView3.SelectedRows[0].Cells[0].Value.ToString());
            button1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                DatagridLoad("movies", dataGridView1);
            }
            else if(radioButton2.Checked)
            {
                DatagridLoad("tvseries", dataGridView1);
            }
        }

        private void PersonAssignment_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                string sql = "insert into movieAssignment (personid, professionid, movieid) values (" + personId + ", " + profId + ", " + movieId + ");";

                if (QueryService.InsertQuery(sql))
                {
                    MessageBox.Show("Επιτυχής εισαγωγή δεδομένων!");
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Παρουσιάστηκε σφάλμα κατά την εισαγωγή των δεδομένων!\nΠροβολή σφάλματος;", "Σφάλμα", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        var exception = new ExceptionForm();
                        exception.Show();
                    }
                }
            }
            else if (radioButton2.Checked)
            {
                string sql = "insert into tvAssignment (personid, professionid, tvserieid) values (" + personId + ", " + profId + ", " + tvserieId + ");";

                if (QueryService.InsertQuery(sql))
                {
                    MessageBox.Show("Επιτυχής εισαγωγή δεδομένων!");
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Παρουσιάστηκε σφάλμα κατά την εισαγωγή των δεδομένων!\nΠροβολή σφάλματος;", "Σφάλμα", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        var exception = new ExceptionForm();
                        exception.Show();
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
