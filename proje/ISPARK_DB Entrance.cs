using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H1UUN1V\SQLEXPRESS;Initial Catalog=ISPARK_DB;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            GeneralManagerLogIn GML = new GeneralManagerLogIn();
            GML.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManagerLogIn MLI = new ManagerLogIn();
            MLI.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChefLogIn CLI = new ChefLogIn();
            CLI.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EmployeeLogIn ELI = new EmployeeLogIn();
            ELI.Show();
        }
    }
}
