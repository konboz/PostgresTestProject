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
    public partial class Movies : Form
    {
        public Form form;
        public DataGridView data;
        public string tableName = "movies";
        public Movies(Form form)
        {
            InitializeComponent();
            groupBox2.Visible = false;
            this.form = form;
            DataGridRefresh();
        }

        private void DataGridRefresh()
        {
            DataTable initData;
            initData = QueryService.ViewQuery("Select * from movies;");
            if (initData != null)
            {
                dataGridView1.DataSource = initData;
                data = dataGridView1;
                dataGridView1.Sort(dataGridView1.Columns["movieid"], ListSortDirection.Ascending);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            data = sender as DataGridView;
        }

        private void RadioButtonChanged(object sender, EventArgs e)
        {
            button1.Visible = true;
            if (radioButton1.Checked || radioButton2.Checked)
            {
                groupBox2.Visible = true;
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
                string sql = "insert into movies (title, releaseDate, language, country, poster) values ('" + textTitle.Text + "', '" + dateTimePicker1.Text + "', '" + textLang.Text + "', '" + textCountry.Text + "', '" + textPoster.Text + "');";

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
                string sql = "update movies set title = '" + textTitle.Text + "', releaseDate = '" + dateTimePicker1.Text + "', language = '" + textLang.Text + "', country = '" + textCountry.Text + "', poster = '" + textPoster.Text + "' where movieId = " + data.SelectedRows[0].Cells[0].Value + ";";

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
                string sql = " delete from movies where movieId = " + data.SelectedRows[0].Cells[0].Value + ";";

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
        }
    }
}
