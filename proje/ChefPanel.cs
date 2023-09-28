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
    public partial class ChefPanel : Form
    {
        public ChefPanel()
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

        //dataGrdiView'de tıklanan satırın verilerini ilgili textBoxlara yazdırma:
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.SelectedCells[0].RowIndex;

            if (comboBox1.Text == "Employee")
            {
                IDBox.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                NameBox.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            }
        }

        //Ekleme fonksiyonu:
        public void FuncAdd(string tableName)
        {
            conn.Open();

            string query = "";

            if (tableName == "Employee")
            {
                query = $"insert into Employee (C_Record_Number, Employee_Name, isActive, isUpdated) VALUES ('{RecNumBox.Text}', '{NameBox.Text}', 1, 0)";
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

            if (tableName == "Employee")
            {
                query = $"update {tableName} set C_Record_Number = '{RecNumBox.Text}', Employee_Name = '{NameBox.Text}', isUpdated = 1 where E_Record_Number = {id}";
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

            if (tableName == "Employee")
            {
                query = $"update {tableName} set isActive = 0, isUpdated = 1 where E_Record_Number = {id}";
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

        //Listeleme işlemleri:
        private void ListBtn_Click(object sender, EventArgs e)
        {
            try
            {
                FuncList(comboBox1.Text);
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

        //Listeleme işlemlerine devam:
        private void ViewBtn_Click(object sender, EventArgs e)
        {
            try
            {
                FuncList(comboBox1.Text);
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

        //Ekleme işlemleri:
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Employee")
            {
                bool hasError = false;

                try
                {
                    FuncAdd("Employee");
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

        //Update işlemleri:
        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Employee")
            {
                bool hasError = false;

                try
                {
                    FuncUpdate("Employee");
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
                        MessageBox.Show("You updated the related Employees data.");
                    }
                }
            }
        }

        //Delete işlemleri:
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Employee")
            {
                bool hasError = false;

                try
                {
                    FuncDelete("Employee");
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
                        MessageBox.Show("You deleted the related Employee data.");
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
