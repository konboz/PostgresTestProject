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

        public Movies(Form form)
        {
            InitializeComponent();
            this.form = form;
            DataTable data;
            data = QueryService.ViewQuery("Select * from movies;");
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var data = sender as DataGridView;
            var row = data.SelectedRows;
        }
    }
}
