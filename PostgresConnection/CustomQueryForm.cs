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
            string script;

            if (checkBox1.Checked && label1.Text.Length > 0)
            {
                FileInfo file = new FileInfo(label1.Text);
                script = file.OpenText().ReadToEnd();
            }
            else
            {
                script = richTextBox1.Text;
            }

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

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = openFileDialog1.FileName;
            }
            else
            {
                label1.Text = null; 
            }
        }
        private void CheckBoxCheckChanged (object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button3.Visible = true;
                richTextBox1.ReadOnly = true;
                button2.Visible = false;
                label1.Visible = true;
                textBox1.Visible = true;
            }
            else
            {
                button3.Visible = false;
                richTextBox1.ReadOnly = false;
                button2.Visible = true;
                label1.Visible = false;
                textBox1.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form.Show();
            Close();
        }
    }
}
