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
using TextBox = System.Windows.Forms.TextBox;

namespace proje
{
    public partial class EmployeePanel : Form
    {
        public EmployeePanel()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H1UUN1V\SQLEXPRESS;Initial Catalog=ISPARK_DB;Integrated Security=True");

        //Listeleme fonksiyonu:
        public void FuncList(string tableName)
        {
            SqlCommand comm = new SqlCommand($"select * from {tableName}", conn);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        //Ekleme fonksiyonu:
        public void FuncAdd(string tableName)
        {
            conn.Open();

            string query = "";

            if (tableName == "Activity_Type")
            {
                query = $"insert into Activity_Type (Activity_Name, Activity_Category, isActive, isUpdated) VALUES ('{NameBox.Text}', '{ActCatBox.Text}', 1, 0)";
            }
            else if (tableName == "Product_Activity_Date")
            {
                query = $"insert into Product_Activity_Date (Product_ID, Record_Number, Activity_Category, Old_Owner_Number, Confirmation_Status, isActive, isUpdated) VALUES ('{ProCatID.Text}', '{R_Num.Text}', '{ActCatBox.Text}', '{OldOwnNum.Text}', '{Status.Text}', 1, 0)";
            }
            else if (tableName == "Product_Type")
            {
                query = $"insert into Product_Type (Category_Name, isActive, isUpdated) VALUES ('{NameBox.Text}', 1, 0)";
            }
            else if (tableName == "Products")
            {
                query = $"insert into Products (Category_ID, Product_Name, isActive, isUpdated) VALUES ('{ProCatID}', '{NameBox.Text}', 1, 0)";
            }

            SqlCommand comm = new SqlCommand(query, conn);
            comm.ExecuteNonQuery();

            conn.Close();
        }

        //Update fonksiyonu:
        public void FuncUpdate(string tableName)
        {
            conn.Open();

            string query = "";
            string id = IDBox.Text;

            if (tableName == "Activity_Type")
            {
                query = $"update {tableName} set Activity_Name = '{NameBox.Text}', Activity_Category = '{ActCatBox.Text}', isUpdated = 1 where Activity_ID = {id}";
            }
            else if (tableName == "Product_Activity_Date")
            {
                query = $"update {tableName} set Product_ID = '{ProCatID.Text}', Record_Number = '{R_Num.Text}', Activity_Category = '{ActCatBox.Text}', Old_Owner_Number = '{OldOwnNum.Text}', Confirmation_Status = '{Status.Text}', isUpdated = 1 where Activity_ID = {id}";
            }
            else if (tableName == "Product_Type")
            {
                query = $"update {tableName} set Category_Name = '{NameBox.Text}', isUpdated = 1 where Category_ID = {id}";
            }
            else if (tableName == "Products")
            {
                query = $"update {tableName} set Category_ID = '{ProCatID.Text}', Product_Name = '{NameBox.Text}', isUpdated = 1 where Product_ID = {id}";
            }

            SqlCommand comm = new SqlCommand(query, conn);
            comm.ExecuteNonQuery();

            conn.Close();
        }

        //Delete fonksiyonu:
        public void FuncDelete(string tableName)
        {
            conn.Open();

            string query = "";
            string id = IDBox.Text;

            if (tableName == "Activity_Type")
            {
                query = $"update {tableName} set isActive = 0, isUpdated = 1 where Activity_ID = {id}";
            }
            else if (tableName == "Product_Activity_Date")
            {
                query = $"update {tableName} set isActive = 0, isUpdated = 1 where Activity_ID = {id}";
            }
            else if (tableName == "Product_Type")
            {
                query = $"update {tableName} set isActive = 0, isUpdated = 1 where Category_ID = {id}";
            }
            else if (tableName == "Products")
            {
                query = $"update {tableName} set isActive = 0, isUpdated = 1 where Product_ID = {id}";
            }

            SqlCommand comm = new SqlCommand(query, conn);
            comm.ExecuteNonQuery();

            conn.Close();
        }

        //Clear fonksiyonu:
        public void FuncClear()
        {
            foreach (Control control in groupBox1.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Text = "";
                }
            }
        }

        //Listeleme işlemi:
        private void ListBtn_Click(object sender, EventArgs e)
        {
            try
            {
                FuncList(comboBox1.Text);
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

        //Listeleme işlemleri devam:
        private void ViewBtn_Click(object sender, EventArgs e)
        {
            try
            {
                FuncList(comboBox1.Text);
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
                    FuncAdd("Activity_Type");
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
                        MessageBox.Show("You added a new Activity to the database.");
                    }
                }
            }
            else if (comboBox1.Text == "Product_Activity_Date")
            {
                bool hasError = false;

                try
                {
                    FuncAdd("Product_Activity_Date");
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
                        MessageBox.Show("You added a new Product_Activity to the database.");
                    }
                }
            }
            else if (comboBox1.Text == "Product_Type")
            {
                bool hasError = false;

                try
                {
                    FuncAdd("Product_Type");
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
                        MessageBox.Show("You added a new Product_Type to the database.");
                    }
                }
            }
            else if (comboBox1.Text == "Products")
            {
                bool hasError = false;

                try
                {
                    FuncAdd("Products");
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
                    FuncUpdate("Activity_Type");
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
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
                    FuncUpdate("Product_Activity_Date");
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
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
                    FuncUpdate("Product_Type");
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
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
                    FuncUpdate("Products");
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
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
                    FuncDelete("Activity_Type");
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
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
                    FuncDelete("Product_Activity_Date");
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
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
                    FuncDelete("Product_Type");
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
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
                    FuncDelete("Products");
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    if (!hasError && !string.IsNullOrEmpty(IDBox.Text))
                    {
                        MessageBox.Show("You deleted the data.");
                    }
                }
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            FuncClear();
        }
    }
}
