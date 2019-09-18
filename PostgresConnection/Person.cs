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
    public partial class Person : Form
    {
        public Form form;
        public DataGridView data;
        public string tableName = "person";
        public int profId;
        public Person(Form form)
        {
            InitializeComponent();
            this.form = form;
            groupBox2.Visible = false;
            DataGridRefresh();
        }

        //initialy loads the grid and also refreshes it when there is a change in the table
        private void DataGridRefresh() 
        {
            DataTable initData;
            initData = QueryService.ViewQuery("Select * from person;");
            if (initData != null)
            {
                dataGridView1.DataSource = initData;
                data = dataGridView1;
                dataGridView1.Sort(dataGridView1.Columns["personid"], ListSortDirection.Ascending);
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

        //assigning selected row data for use in the sql query
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
                button1.Text = "Ok";
            }
            else if (radioButton4.Checked)
            {
                button1.Text = "Συνέχεια";
                groupBox2.Visible = false;
            }
            else
            {
                groupBox2.Visible = false;
                button1.Text = "Ok";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                string sql = "insert into person (name, dateofbirth, country, contactinfo, profilepic, primaryprofessionid) values ('" + textName.Text + "', '" + dateTimePicker1.Text + "', '" + textCountry.Text + "', '" + textContact.Text + "', '" + textPoster.Text + "', " + profId + ");";

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
                string sql = "update person set name = '" + textName.Text + "', dateofbirth = '" + dateTimePicker1.Text + "', contactinfo = '" + textContact.Text + "', country = '" + textCountry.Text + "', poster = '" + textPoster.Text + "', primaryprofessionid = " + profId + " where personId = " + data.SelectedRows[0].Cells[0].Value + ";";

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
                //First deleting relations of person in other tables
                string sql1 = " delete from movieassignment where personId = " + data.SelectedRows[0].Cells[0].Value + ";";
                string sql2 = " delete from tvassignment where personId = " + data.SelectedRows[0].Cells[0].Value + ";";
                string sql3 = " delete from person where personId = " + data.SelectedRows[0].Cells[0].Value + ";";

                if (QueryService.InsertQuery(sql1) & QueryService.InsertQuery(sql2) & QueryService.InsertQuery(sql3))
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
                int personId = int.Parse(data.SelectedRows[0].Cells[0].Value.ToString()); 
                var prs = new PersonAssignment(personId); 
                prs.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form.Show();
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int personId = int.Parse(data.SelectedRows[0].Cells[0].Value.ToString());
            Filmography filmography = new Filmography(personId);
            filmography.Show();
        }

        private void textProf_Enter(object sender, EventArgs e)
        {
            ProfessionForm prof = new ProfessionForm(this);
            prof.Show();
        }

        public void ProfessionSelected(string title, int id)
        {
            textProf.Text = title;
            profId = id;
        }
    }
}
