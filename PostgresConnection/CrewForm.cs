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
    public partial class CrewForm : Form
    {
        public bool isMovie;    //True if it's a movie, false if it isn't
        public int id;
        public CrewForm(int id, bool isMovie)
        {
            InitializeComponent();
            this.isMovie = isMovie;
            this.id = id;
            DatagridLoad(dataGridView1);
        }

        public void DatagridLoad(DataGridView dataGrid)
        {
            DataTable initData;
            string crewQuery;
            if (isMovie)
            {
                crewQuery = "select name, title from person, profession, movieassignment where movieassignment.movieid = " + id + "and person.personid = movieassignment.personid and profession.professionid = movieassignment.professionid;";
            }
            else
            {
                crewQuery = "select name, title from person, profession, tvassignment where tvassignment.tvserieid = " + id + "and person.personid = tvassignment.personid and profession.professionid = tvassignment.professionid;";
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
