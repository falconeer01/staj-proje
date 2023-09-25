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
    public partial class EmployeePanel : Form
    {
        public EmployeePanel()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H1UUN1V\SQLEXPRESS;Initial Catalog=ISPARK_DB;Integrated Security=True");

        //Listeleme işlemi:
        private void ListBtn_Click(object sender, EventArgs e)
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

        //Listeleme işlemleri devamı
        private void ViewBtn_Click(object sender, EventArgs e)
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

        //Veri ekleme işlemleri:
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Activity_Type")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("insert into Activity_Type (Activity_Name, Activity_Category, isUpdated, isActive) values (@p1, @p2, 0, 1)", conn);
                    comm.Parameters.AddWithValue("@p1", NameBox.Text);
                    comm.Parameters.AddWithValue("@p2", ActCatBox.Text);
                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    conn.Close();

                    if (!hasError)
                    {
                        MessageBox.Show("You added a new Activity to the database.");
                    }
                }
            }
            else if (comboBox1.Text == "Product_Activity_Date")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("insert into Product_Activity_Date (Product_ID, Record_Number, Activity_Category, Old_Owner_Number, Confirmation_Status, isUpdated, isActive) values (@p1, @p2, @p3, @p4, @p5, 0, 1)", conn);
                    comm.Parameters.AddWithValue("@p1", ProCatID.Text);
                    comm.Parameters.AddWithValue("@p2", R_Num.Text);
                    comm.Parameters.AddWithValue("@p3", ActCatBox.Text);
                    comm.Parameters.AddWithValue("@p4", OldOwnNum.Text);
                    comm.Parameters.AddWithValue("@p5", Status.Text);
                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    conn.Close();

                    if (!hasError)
                    {
                        MessageBox.Show("You added a new Product_Activity to the database.");
                    }
                }
            }
            else if (comboBox1.Text == "Product_Type")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("insert into Product_Type (Category_Name, isActive, isUpdated) values (@p1, 1, 0)", conn);
                    comm.Parameters.AddWithValue("@p1", NameBox.Text);
                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    conn.Close();

                    if (!hasError)
                    {
                        MessageBox.Show("You added a new Product_Type to the database.");
                    }
                }
            }
            else if (comboBox1.Text == "Products")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("insert into Products (Category_ID, Product_Name, isActive, isUpdated) values (@p1, @p2, 1, 0)", conn);
                    comm.Parameters.AddWithValue("@p1", ProCatID.Text);
                    comm.Parameters.AddWithValue("@p2", NameBox.Text);
                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    conn.Close();

                    if (!hasError)
                    {
                        MessageBox.Show("You added a new Product to the database.");
                    }
                }
            }
        }

        //Update işlemleri:
        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Activity_Type")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Activity_Type set Activity_Name=@p1, Activity_Category=@p2, isUpdated=1 where Activity_ID=@p0", conn);
                    comm.Parameters.AddWithValue("@p0", IDBox.Text);
                    comm.Parameters.AddWithValue("@p1", NameBox.Text);
                    comm.Parameters.AddWithValue("@p2", ActCatBox.Text);
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
                        MessageBox.Show("You updated the data.");
                    }
                }
            }
            else if (comboBox1.Text == "Product_Activity_Date")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Product_Activity_Date set Product_ID=@p1, Record_Number=@p2, Activity_Category=@p3, Old_Owner_Number=@p4, Confirmation_Status=@p5, isUpdated=1 where Activity_ID=@p0", conn);
                    comm.Parameters.AddWithValue("@p0", IDBox.Text);
                    comm.Parameters.AddWithValue("@p1", ProCatID.Text);
                    comm.Parameters.AddWithValue("@p2", R_Num.Text);
                    comm.Parameters.AddWithValue("@p3", ActCatBox.Text);
                    comm.Parameters.AddWithValue("@p4", OldOwnNum.Text);
                    comm.Parameters.AddWithValue("@p5", Status.Text);
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
                        MessageBox.Show("You updated the data.");
                    }
                }
            }
            else if (comboBox1.Text == "Product_Type")
            {
                bool hasError = false;

                try
                {
                    SqlCommand comm = new SqlCommand("update Product_Type set Category_Name=@p1, isUpdated=1 where Category_ID=@p0", conn);
                    comm.Parameters.AddWithValue("@p0", IDBox.Text);
                    comm.Parameters.AddWithValue("@p1", NameBox.Text);
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
                        MessageBox.Show("You updated the data.");
                    }
                }
            }
            else if (comboBox1.Text == "Products")
            {
                bool hasError = false;

                try
                {
                    SqlCommand comm = new SqlCommand("update Products set Category_ID=@p1, Product_Name=@p2, isUpdated=1 where Product_ID=@p0", conn);
                    comm.Parameters.AddWithValue("@p0", IDBox.Text);
                    comm.Parameters.AddWithValue("@p1", ProCatID.Text);
                    comm.Parameters.AddWithValue("@p2", NameBox.Text);
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
                        MessageBox.Show("You updated the data.");
                    }
                }
            }
        }

        //dataGridView'de üstüne tıklanan satırın ilgili bilgilerini ilgili textBox'lara yansıtma:
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.SelectedCells[0].RowIndex;

            if (comboBox1.Text == "Activity_Type")
            {
                IDBox.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
                NameBox.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                ActCatBox.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
                ProCatID.Text = "";
                R_Num.Text = "";
                OldOwnNum.Text = "";
                Status.Text = "";
            }
            else if (comboBox1.Text == "Product_Activity_Date")
            {
                IDBox.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                ProCatID.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                R_Num.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
                ActCatBox.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
                OldOwnNum.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
                Status.Text = dataGridView1.Rows[index].Cells[5].Value.ToString();
                NameBox.Text = "";
            }
            else if (comboBox1.Text == "Product_Type")
            {
                IDBox.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                NameBox.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                ActCatBox.Text = "";
                ProCatID.Text = "";
                R_Num.Text = "";
                OldOwnNum.Text = "";
                Status.Text = "";
            }
            else if (comboBox1.Text == "Products")
            {
                IDBox.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                ProCatID.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                NameBox.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
                ActCatBox.Text = "";
                R_Num.Text = "";
                OldOwnNum.Text = "";
                Status.Text = "";
            }
        }

        //Delete işlemleri:
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Activity_Type")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Activity_Type set isActive=0, isUpdated=1 where Activity_ID=@p1", conn);
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
                        MessageBox.Show("You deleted the data.");
                    }
                }
            }
            else if (comboBox1.Text == "Product_Activity_Date")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Product_Activity_Date set isActive=0, isUpdated=1 where Activity_ID=@p1", conn);
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
                        MessageBox.Show("You deleted the data.");
                    }
                }
            }
            else if (comboBox1.Text == "Product_Type")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Product_Type set isActive=0, isUpdated=1 where Category_ID=@p1", conn);
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
                        MessageBox.Show("You deleted the data.");
                    }
                }
            }
            else if (comboBox1.Text == "Products")
            {
                bool hasError = false;

                try
                {
                    conn.Open();

                    SqlCommand comm = new SqlCommand("update Products set isActive=0, isUpdated=1 where Product_ID=@p1", conn);
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
                        MessageBox.Show("You deleted the data.");
                    }
                }
            }
        }
    }
}
