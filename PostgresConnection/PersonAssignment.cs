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
        public int id;
        public int personId;
        public int profId;
        public PersonAssignment(int id)
        {
            InitializeComponent();
            DatagridLoad("person", dataGridView1);
            DatagridLoad("profession", dataGridView3);
            this.id = id;
            button1.Visible = false;
        }

        public void DatagridLoad(string table, DataGridView dataGrid)
        {
            DataTable initData;
            initData = QueryService.ViewQuery("Select * from " + table + ";"); //kalw tin viewQuery apo tin klasi queryservice kai bazw to apotelesma sta initData
            if (initData != null) // H viewQuery an exei sfalma to epistrefei null to dataGrid
            {
                dataGrid.DataSource = initData;
            }
            else //Opote an einai null tou lew na vgalei minima kai na deixei to sfalma
            {
                DialogResult dialog = MessageBox.Show("Παρουσιάστηκε σφάλμα κατά την επικοινωνία με τη βάση!\nΠροβολή σφάλματος;", "Σφάλμα", MessageBoxButtons.YesNo); //auti einai i suntaxi an thes to messageBox na einai typou Yes/No
                if (dialog == DialogResult.Yes)
                {
                    var exception = new ExceptionForm(); //To mono pou kanei auti i forma einai na diavazei ti metavliti exception pou exei apothikeumeno mesa to teleutaio sfalma, i metavliti orizetai sto queryService
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
                DatagridLoad("movies", dataGridView2);
            }
            else if(radioButton2.Checked)
            {
                DatagridLoad("tvseries", dataGridView2);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(radioButton1.Checked)
            {
                label3.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                label3.Visible = true;
                button1.Visible = true;
            }
            else if(radioButton2.Checked)
            {
                label3.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                label3.Visible = true;
                button1.Visible = true;
            }
            
        }

        private void PersonAssignment_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
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
            else if (radioButton2.Checked)
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
