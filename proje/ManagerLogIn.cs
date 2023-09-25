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
    public partial class ManagerLogIn : Form
    {
        public ManagerLogIn()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H1UUN1V\SQLEXPRESS;Initial Catalog=ISPARK_DB;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand comm = new SqlCommand("select * from Manager where M_Record_Number=@p1 and Manager_Name=@p2", conn);
            comm.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            comm.Parameters.AddWithValue("@p2", textBox1.Text);

            SqlDataReader dr = comm.ExecuteReader();
            if (dr.Read())
            {
                ManagerPanel MP = new ManagerPanel();
                MP.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("You entered wrong info.", "Wrong Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
        }
    }
}
