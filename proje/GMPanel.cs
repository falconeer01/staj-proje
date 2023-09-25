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
    public partial class GMPanel : Form
    {
        public GMPanel()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H1UUN1V\SQLEXPRESS;Initial Catalog=ISPARK_DB;Integrated Security=True");

        private void GMPanel_Load(object sender, EventArgs e)
        {
            
        }

        // dataGridView'e comboBox1'de seçilen tablodaki verileri yansıtma:
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand comm = new SqlCommand($"select * from {comboBox1.Text}", conn);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured.");
            }
            finally
            {
                conn.Close();
            }
        }

        //dataGridView'de üstüne tıklanan satırın ilgili bilgilerini ilgili textBox'lara yansıtma:
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.SelectedCells[0].RowIndex;

            if (comboBox1.Text == "Manager")
            {
                IDBox.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
            }
            else if (comboBox1.Text == "Chef")
            {
                IDBox.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            }
            else if (comboBox1.Text == "Employee")
            {
                IDBox.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            }
        }

        // Veri ekleme işlemleri:
        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();

            if (comboBox1.Text == "Manager")
            {
                bool hasError = false;
                int gmNo = 1;

                try
                {
                    SqlCommand comm = new SqlCommand("INSERT INTO Manager (GM_Record_Number, Department_ID, Manager_Name, isActive, isUpdated) VALUES (@p9, @DepartmentID, @ManagerName, 1, 0)", conn);
                    comm.Parameters.AddWithValue("@DepartmentID", textBox2.Text);
                    comm.Parameters.AddWithValue("@ManagerName", textBox1.Text);
                    comm.Parameters.AddWithValue("@p9", gmNo.ToString());
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
                        MessageBox.Show("You added a new Manager to the database.");
                    }
                }
            }
            else if (comboBox1.Text == "Chef")
            {
                bool hasError = false;

                try
                {
                    SqlCommand comm = new SqlCommand("INSERT INTO Chef (M_Record_Number, Chef_Name, isActive, isUpdated) VALUES (@MRecordNumber, @ChefName, 1, 0)", conn);
                    comm.Parameters.AddWithValue("@MRecordNumber", textBox3.Text);
                    comm.Parameters.AddWithValue("@ChefName", textBox1.Text);
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
                        MessageBox.Show("You added a new Manager to the database.");
                    }
                }
            }
            else if (comboBox1.Text == "Employee")
            {
                bool hasError = false;

                try
                {
                    SqlCommand comm = new SqlCommand("INSERT INTO Employee (C_Record_Number, Employee_Name, isActive, isUpdated) VALUES (@CRecordNumber, @EmployeeName, 1, 0)", conn);
                    comm.Parameters.AddWithValue("@CRecordNumber", textBox3.Text);
                    comm.Parameters.AddWithValue("@EmployeeName", textBox1.Text);
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
                        MessageBox.Show("You added a new Manager to the database.");
                    }
                }
            }
            else if (comboBox1.Text == "Department")
            {
                bool hasError = false;

                try
                {
                    SqlCommand comm = new SqlCommand("insert into Department (Department_Name, isActive, isUpdated) values (@p1, 1, 0)", conn);
                    comm.Parameters.AddWithValue("@p1", textBox1.Text);
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
                        MessageBox.Show("You added a new Department to the database.");
                    }
                }
            }

            conn.Close();
        }

        // List buttonunun işlevi:
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand comm = new SqlCommand($"select * from {comboBox1.Text}", conn);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured.");
            }
            finally
            {
                conn.Close();
            }
        }

        // Update işlemleri:
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Manager")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Manager set Department_ID=@p1, Manager_Name=@p2, isUpdated=1 where M_Record_Number=@p3", conn);
                    comm.Parameters.AddWithValue("@p1", textBox2.Text);
                    comm.Parameters.AddWithValue("@p2", textBox1.Text);
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
                        MessageBox.Show("You updated the related Managers data.");
                    }
                }
            }
            else if (comboBox1.Text == "Chef")
            { 
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Chef set M_Record_Number=@p1, Chef_Name=@p2, isUpdated=1 where C_Record_Number=@p3", conn);
                    comm.Parameters.AddWithValue("@p1", textBox3.Text);
                    comm.Parameters.AddWithValue("@p2", textBox1.Text);
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
                    comm.Parameters.AddWithValue("@p1", textBox3.Text);
                    comm.Parameters.AddWithValue("@p2", textBox1.Text);
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

        // Delete işlemleri:
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Manager")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Manager set isActive=0, isUpdated=1 where M_Record_Number=@p1", conn);
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
                        MessageBox.Show("You deleted the related Managers data.");
                    }
                }
            }
            else if (comboBox1.Text == "Chef")
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
