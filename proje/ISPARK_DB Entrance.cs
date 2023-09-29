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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    ButtonHoverHelper.AttachHoverEffect(button);
                }
            }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(255, 218, 0);

            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    button.BackColor = Color.FromArgb(255, 218, 0);
                    button.ForeColor = Color.FromArgb(0, 94, 161);
                }
            }
        }
    }
}
