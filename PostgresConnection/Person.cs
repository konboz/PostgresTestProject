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
        public Person(Form form)
        {
            InitializeComponent();
            this.form = form;
        }
    }
}
