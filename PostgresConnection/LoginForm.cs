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
    public partial class LoginForm : Form
    {
        Form1 form;
        public LoginForm(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QueryService.connstring = "Server=localhost;Port=5432;Database=" + textBox1.Text + ";User ID=" + textBox3.Text + ";Password=" + textBox2.Text + ";";
            Close();
            form.Visible = true;
        }
    }
}
