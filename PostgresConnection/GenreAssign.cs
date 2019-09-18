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
    public partial class GenreAssign : Form
    {
        public bool isMovie;
        public int id;
        public int genreId;
        public GenreAssign(bool isMovie, int id)
        {
            InitializeComponent();
            DatagridLoad(dataGridView1);
            this.id = id;
            this.isMovie = isMovie;
        }

        public void DatagridLoad(DataGridView dataGrid)
        {
            DataTable initData;
            initData = QueryService.ViewQuery("Select * from genre;");
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            label1.Visible = true;
            genreId = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isMovie)
            {
                string sql = "insert into moviecategorization (movieid, genreid) values (" + id + ", " + genreId + ");";

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
                string sql = "insert into tvcategorization (tvserieid, genreid) values (" + id + ", " + genreId + ");";

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
