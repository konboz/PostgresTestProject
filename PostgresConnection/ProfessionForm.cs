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
    public partial class ProfessionForm : Form
    {
        Person person;
        public ProfessionForm(Person person)
        {
            InitializeComponent();
            this.person = person;
            DatagridLoad(dataGridView1);
        }

        public void DatagridLoad(DataGridView dataGrid)
        {
            DataTable initData;
            string crewQuery;

            crewQuery = "select * from profession";
            initData = QueryService.ViewQuery(crewQuery);
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

        private void button1_Click(object sender, EventArgs e)
        {
            string title = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            int profId = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            person.ProfessionSelected(title, profId);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           string title =  dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
           int profId =  int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }
    }
}
