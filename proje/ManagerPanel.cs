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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proje
{
    public partial class ManagerPanel : Form
    {
        public ManagerPanel()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H1UUN1V\SQLEXPRESS;Initial Catalog=ISPARK_DB;Integrated Security=True");

        //Listeleme işlemleri
        private void ListBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string tableName = comboBox1.Text;
                conn.Open();

                string query = "SELECT * FROM " + tableName;
                SqlCommand comm = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        //Listeleme işlemleri devam
        private void ViewBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string tableName = comboBox1.Text;
                conn.Open();

                string query = "SELECT * FROM " + tableName;
                SqlCommand comm = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        //Veri ekleme işlemleri
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Chef")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("insert into Chef (Chef_Name, M_Record_Number, isActive, isUpdated) values (@p1, @p2, 1, 0)", conn);
                    comm.Parameters.AddWithValue("@p1", NameBox.Text);
                    comm.Parameters.AddWithValue("@p2", RecNumBox.Text);
                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    if (!hasError)
                    {
                        MessageBox.Show("You added a new Chef to the database.");
                    }

                    conn.Close();
                }
            }
            else if (comboBox1.Text == "Employee")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("insert into Employee (Employee_Name, C_Record_Number, isActive, isUpdated) values (@p1, @p2, 1, 0)", conn);
                    comm.Parameters.AddWithValue("@p1", NameBox.Text);
                    comm.Parameters.AddWithValue("@p2", RecNumBox.Text);
                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    if (!hasError)
                    {
                        MessageBox.Show("You added a new Employee to the database.");
                    }
                    conn.Close();
                }
            }
        }

        //Update işlemleri
        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Chef")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Chef set M_Record_Number=@p1, Chef_Name=@p2, isUpdated=1 where C_Record_Number=@p3", conn);
                    comm.Parameters.AddWithValue("@p1", RecNumBox.Text);
                    comm.Parameters.AddWithValue("@p2", NameBox.Text);
                    comm.Parameters.AddWithValue("@p3", IDBox.Text);
                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    if (!hasError && !string.IsNullOrEmpty(IDBox.Text))
                    {
                        MessageBox.Show("You updated the related Chefs data.");
                    }
                }
            }
            else if (comboBox1.Text == "Employee")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Employee set C_Record_Number=@p1, Employee_Name=@p2, isUpdated=1 where E_Record_Number=@p3", conn);
                    comm.Parameters.AddWithValue("@p1", RecNumBox.Text);
                    comm.Parameters.AddWithValue("@p2", NameBox.Text);
                    comm.Parameters.AddWithValue("@p3", IDBox.Text);
                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    if (!hasError && !string.IsNullOrEmpty(IDBox.Text))
                    {
                        MessageBox.Show("You updated the related Employees data.");
                    }
                }
            }
        }

        //dataGrdiView'de tıklanan satırın verilerini ilgili textBoxlara yazdırma:
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.SelectedCells[0].RowIndex;

            if (comboBox1.Text == "Chef")
            {
                IDBox.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                NameBox.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            }
            else if (comboBox1.Text == "Employee")
            {
                IDBox.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                NameBox.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            }
        }

        //Delete işlemleri:
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Chef")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Chef set isActive=0, isUpdated=1 where C_Record_Number=@p1", conn);
                    comm.Parameters.AddWithValue("@p1", IDBox.Text);
                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    if (!hasError && !string.IsNullOrEmpty(IDBox.Text))
                    {
                        MessageBox.Show("You deleted the related Chefs data.");
                    }
                }
            }
            else if (comboBox1.Text == "Employee")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Employee set isActive=0, isUpdated=1 where E_Record_Number=@p1", conn);
                    comm.Parameters.AddWithValue("@p1", IDBox.Text);
                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    if (!hasError && !string.IsNullOrEmpty(IDBox.Text))
                    {
                        MessageBox.Show("You deleted the related Employee data.");
                    }
                }
            }
        }

        private void ManagerPanel_Load(object sender, EventArgs e)
        {

        }
    }
}
