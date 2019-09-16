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

        //H metavliti isMovie einai gia na xerw se poio pinaka tha paw na kanw tin kataxwrisi, true gia tainia, false gia seira
        public CrewAssignmentForm(bool isMovie, int id)
        {
            InitializeComponent();
            //Kalw tin DataGridLoad dio fores gia na gemisw ta datagrid me ta dedomena twn person kai profession
            DatagridLoad("person", dataGridView1); 
            DatagridLoad("profession", dataGridView2);
            this.isMovie = isMovie;
            this.id = id;
            // To ekana na mi fainetai arxika giati otan to grid fortwnei einai epilegmeni i prwti seira alla edw mesa de mporw gia kapoio logo na pw px "personid = to epilegmeno keli" giati to epilegmeno keli einai akoma null mesa ston constructor
            button1.Visible = false; 
            //dataGridView2.CurrentCell.Selected = false;
        }

        private void CrewAssignmentForm_Load(object sender, EventArgs e)
        {
        }

        //Kanei load to datagrid me ta dedomena tou pinaka kai ta apothikeuei sto datagrid pou tou dineis gia na ginei provoli sti forma
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
