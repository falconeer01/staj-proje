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
    public partial class GeneralManagerLogIn : Form
    {
        public GeneralManagerLogIn()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H1UUN1V\SQLEXPRESS;Initial Catalog=ISPARK_DB;Integrated Security=True");

        private void GeneralManagerLogIn_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand comm = new SqlCommand("select * from General_Manager where GM_Record_Number=@p1 and GM_Name=@p2", conn);
            comm.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            comm.Parameters.AddWithValue("@p2", textBox1.Text);
            
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.Read())
            {
                GMPanel GMP = new GMPanel();
                GMP.Show();
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
