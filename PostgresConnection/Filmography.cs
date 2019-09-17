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
    public partial class Filmography : Form
    {
        public int personId;
        public Filmography(int personId)
        {
            InitializeComponent();
            this.personId = personId;
            DatagridLoad(dataGridView1, true);
            DatagridLoad(dataGridView2, false);
        }

        public void DatagridLoad(DataGridView dataGrid, bool isMovie)
        {
            DataTable initData;
            string crewQuery;
            if (isMovie)
            {
                crewQuery = "select movies.title, releasedate, profession.title from movies, profession, movieassignment where movieassignment.movieid = movies.movieid and movieassignment.personid = " + personId + " and movieassignment.professionid = profession.professionid;";
            }
            else
            {
                crewQuery = "select tvseries.title, startdate, profession.title from tvseries, profession, tvassignment where tvassignment.tvserieid = tvseries.tvserieid and tvassignment.personid = " + personId + " and tvassignment.professionid = profession.professionid;";
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
