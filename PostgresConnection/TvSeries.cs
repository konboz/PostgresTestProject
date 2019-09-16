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
    public partial class TvSeries : Form
    {
        public Form form;
        public DataGridView data;
        public string tableName = "tvseries"; //isws na mi xreiastei pouthena auto
        public TvSeries(Form form)
        {
            InitializeComponent();
            this.form = form;
            groupBox2.Visible = false;
            DataGridRefresh();
        }

        private void DataGridRefresh() // kanei refresh to datagrid otan kanoyme allagi sti vasi, xrhsimopoieitai kai gia tin arxiki fortwsi giauto kaleitai kai apo panw
        {
            DataTable initData;
            initData = QueryService.ViewQuery("Select * from tvseries;"); //kalw tin viewQuery apo tin klasi queryservice kai bazw to apotelesma sta initData
            if (initData != null) // H viewQuery an exei sfalma to epistrefei null to dataGrid
            {
                dataGridView1.DataSource = initData;
                data = dataGridView1;
                dataGridView1.Sort(dataGridView1.Columns["tvserieid"], ListSortDirection.Ascending);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            data = sender as DataGridView; // to sender einai geniko periexei ola ta dedomena tou datagrid kai to kanw typou datagrid gia na to xrisimopoiiso vazontas to sta data pou thelw na einai autou tou typou
        }

        private void RadioButtonChanged(object sender, EventArgs e)
        {
            button1.Visible = true;
            if (radioButton1.Checked || radioButton2.Checked)
            {
                groupBox2.Visible = true;
            }
            else if (radioButton4.Checked)
            {
                button1.Text = "Συνέχεια";
                groupBox2.Visible = false;
            }
            else
            {
                groupBox2.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                string sql = "insert into tvseries (title, startDate, language, country, poster) values ('" + textTitle.Text + "', '" + dateTimePicker1.Text + "', '" + textLang.Text + "', '" + textCountry.Text + "', '" + textPoster.Text + "');";

                if (QueryService.InsertQuery(sql))
                {
                    MessageBox.Show("Επιτυχής εισαγωγή δεδομένων!");
                    DataGridRefresh();
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
                // pairnei tis times apo ta textBox kai tis vazei sto string gia na ginei to query
                string sql = "update tvseries set title = '" + textTitle.Text + "', startDate = '" + dateTimePicker1.Text + "', language = '" + textLang.Text + "', country = '" + textCountry.Text + "', poster = '" + textPoster.Text + "' where tvserieId = " + data.SelectedRows[0].Cells[0].Value + ";";

                // H InsertQuery epistrefei bool an deis stin class queryService, opote kanontas auto tin ektelei kai an einai true to apotelesma bainei mesa, alliws paei sto else kai an patiseis Yes diavazei to sfalma pou apothikeutike
                if (QueryService.InsertQuery(sql))
                {
                    MessageBox.Show("Επιτυχής εισαγωγή δεδομένων!");
                    DataGridRefresh();
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
            else if (radioButton3.Checked)
            {
                // paei sto datagrid kai paei sthn prwti epilegmeni seira (mia einai etsi k alliws), sto prwto keli kai pairnei tin timi tou, giati ekei einai to id
                string sql = " delete from tvseries where tvserieId = " + data.SelectedRows[0].Cells[0].Value + ";";

                if (QueryService.InsertQuery(sql))
                {
                    MessageBox.Show("Επιτυχής διαγραφή!");
                    DataGridRefresh();
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Παρουσιάστηκε σφάλμα κατά τη διαγραφή!\nΠροβολή σφάλματος;", "Σφάλμα", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        var exception = new ExceptionForm();
                        exception.Show();
                    }
                }
            }
            else if (radioButton4.Checked)
            {
                int tvserieId = int.Parse(data.SelectedRows[0].Cells[0].Value.ToString()); //To Parse einai gia na kanei tin timi stin epilegmeni seira sto epilegmeno keli integer, giati emeis xeroume oti einai int alla o compiler to vlepei san object. ToString kaneis giati parse kaneis se keimeno
                var crew = new CrewAssignmentForm(true, tvserieId); //to pername stin forma gia na to xrisimopoiisoume gia tin kataxwrisi ston endiameso pinaka
                crew.Show();
                //epitides den kanw hide tin forma auti giati tha xanagyrisoyme otan ginei i kataxwrisi
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form.Show();
            Close();
        }
    }
}
