using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgresConnection
{
    public partial class CustomQueryForm : Form
    {
        public Form form;
        public CustomQueryForm(Form form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                var data = QueryService.ViewQuery(richTextBox1.Text);

                if (data != null)
                {
                    dataGridView1.DataSource = data;
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
            else if (radioButton2.Checked)
            {
                if (QueryService.InsertQuery(richTextBox1.Text))
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

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo(openFileDialog1.FileName);
            string script = file.OpenText().ReadToEnd();

            if (radioButton1.Checked)
            {
                var data = QueryService.ViewQuery(script);

                if (data != null)
                {
                    dataGridView1.DataSource = data;
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
            else if (radioButton2.Checked)
            {
                if (QueryService.InsertQuery(script))
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
