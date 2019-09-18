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
    public partial class CrewAssignmentForm : Form
    {
        public bool isMovie;
        public int id;
        public int personId;
        public int profId;

        public CrewAssignmentForm(bool isMovie, int id)
        {
            InitializeComponent();
            DatagridLoad("person", dataGridView1); 
            DatagridLoad("profession", dataGridView2);
            this.isMovie = isMovie;
            this.id = id;
            button1.Visible = false; 
        }

        private void CrewAssignmentForm_Load(object sender, EventArgs e)
        {
        }

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
                    exception.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            label1.Visible = true;
            personId = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            button1.Visible = true;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            label2.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            label2.Visible = true;
            profId = int.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
            button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isMovie)
            {
                string sql = "insert into movieAssignment (personid, professionid, movieid) values (" + personId + ", " + profId + ", " + id + ");";

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
            else
            {
                string sql = "insert into tvAssignment (personid, professionid, tvserieid) values (" + personId + ", " + profId + ", " + id + ");";

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
    }
}
