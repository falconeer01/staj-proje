using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    public partial class ChefLogIn : Form
    {
        public ChefLogIn()
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
            conn.Open();

            SqlCommand comm = new SqlCommand("select * from Chef where C_Record_Number=@p1 and Chef_Name=@p2", conn);
            comm.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            comm.Parameters.AddWithValue("@p2", textBox1.Text);

            SqlDataReader dr = comm.ExecuteReader();
            if (dr.Read())
            {
                ChefPanel CP = new ChefPanel();
                CP.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("You entered wrong info.", "Wrong Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
        }

        private void ChefLogIn_Load(object sender, EventArgs e)
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
