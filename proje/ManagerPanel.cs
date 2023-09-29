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
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace proje
{
    public partial class ManagerPanel : Form
    {
        public ManagerPanel()
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

            foreach (Control control in groupBox1.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    ButtonHoverHelper.AttachHoverEffect(button);
                }
            }
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

        //Ekleme fonksiyonu:
        public void FuncAdd(string tableName)
        {
            conn.Open();

            string query = "";

            if (tableName == "Chef")
            {
                query = $"insert into Chef (M_Record_Number, Chef_Name, isActive, isUpdated) VALUES ('{RecNumBox.Text}', '{NameBox.Text}', 1, 0)";
            }
            else if (tableName == "Employee")
            {
                query = $"insert into Employee (C_Record_Number, Employee_Name, isActive, isUpdated) VALUES ('{RecNumBox.Text}', '{NameBox.Text}', 1, 0)";
            }
            else if (tableName == "Department")
            {
                query = $"insert into Department (Department_Name, isActive, isUpdated) values ('{RecNumBox.Text}', 1, 0)";
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

            if (tableName == "Chef")
            {
                query = $"update {tableName} set M_Record_Number='{RecNumBox.Text}', Chef_Name='{NameBox.Text}', isUpdated = 1 where C_Record_Number = {id}";
            }
            else if (tableName == "Employee")
            {
                query = $"update {tableName} set C_Record_Number='{RecNumBox.Text}', Employee_Name='{NameBox.Text}', isUpdated = 1 where E_Record_Number = {id}";
            }
            else if (tableName == "Department")
            {
                query = $"update {tableName} set Department_Name='{NameBox.Text}', isUpdated = 1 where Department_ID = {id}";
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

            if (tableName == "Chef")
            {
                query = $"update {tableName} set isActive = 0, isUpdated = 1 where C_Record_Number = {id}";
            }
            else if (tableName == "Employee")
            {
                query = $"update {tableName} set isActive = 0, isUpdated = 1 where E_Record_Number = {id}";
            }
            else if (tableName == "Department")
            {
                query = $"update {tableName} set isActive = 0, isUpdated = 1 where Department_ID = {id}";
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
                    IDBox.Text = "";
                }
            }
        }

        //Listeleme işlemleri
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

        //Listeleme işlemleri devam
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

        //Veri ekleme işlemleri
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Chef")
            {
                bool hasError = false;

                try
                {
                    FuncAdd("Chef");
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
                }
            }
            else if (comboBox1.Text == "Employee")
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
                    FuncUpdate("Chef");
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
                        MessageBox.Show("You updated the related Chefs data.");
                    }
                }
            }
            else if (comboBox1.Text == "Employee")
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
            if (comboBox1.Text == "Chef")
            {
                bool hasError = false;

                try
                {
                    FuncDelete("Chef");
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
                        MessageBox.Show("You deleted the related Chefs data.");
                    }
                }
            }
            else if (comboBox1.Text == "Employee")
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

        private void ManagerPanel_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    button.BackColor = Color.FromArgb(255, 218, 0);
                    button.ForeColor = Color.FromArgb(0, 94, 161);
                }

                if (control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;
                    comboBox.ForeColor = Color.FromArgb(15, 15, 15);
                }
            }

            foreach (Control control in groupBox1.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    button.BackColor = Color.FromArgb(255, 218, 0);
                    button.ForeColor = Color.FromArgb(0, 94, 161);
                }

                if (control is Label)
                {
                    Label label = (Label)control;
                    label.ForeColor = Color.FromArgb(15, 15, 15);
                }
            }
        }
    }
}
