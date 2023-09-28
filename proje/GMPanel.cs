using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace proje
{
    public partial class GMPanel : Form
    {
        public GMPanel()
        {
            InitializeComponent();
        }

        ISPARK_DBEntities db = new ISPARK_DBEntities();

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H1UUN1V\SQLEXPRESS;Initial Catalog=ISPARK_DB;Integrated Security=True");

        public void FuncList(string tableName)
        {
            //SqlCommand comm = new SqlCommand($"select * from {comboBox1.Text}", conn);
            //SqlDataAdapter da = new SqlDataAdapter(comm);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //dataGridView1.DataSource = dt;

            SqlCommand comm = new SqlCommand($"select * from {tableName}", conn);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void FuncAdd(string tableName)
        {
            conn.Open();

            string query = "";

            if (tableName == "Manager")
            {
                query = $"insert into Manager (GM_Record_Number, Department_ID, Manager_Name, isActive, isUpdated) values (1, '{textBox2.Text}', '{textBox1.Text}', 1, 0)";
            }
            else if (tableName == "Chef")
            {
                query = $"insert into Chef (M_Record_Number, Chef_Name, isActive, isUpdated) VALUES ('{textBox3.Text}', '{textBox1.Text}', 1, 0)";
            }
            else if (tableName == "Employee")
            {
                query = $"insert into Employee (C_Record_Number, Employee_Name, isActive, isUpdated) VALUES ('{textBox3.Text}', '{textBox1.Text}', 1, 0)";
            }
            else if (tableName == "Department")
            {
                query = $"insert into Department (Department_Name, isActive, isUpdated) values ('{textBox1.Text}', 1, 0)";
            }

            SqlCommand comm = new SqlCommand(query, conn);
            comm.ExecuteNonQuery();

            conn.Close();
        }

        public void FuncUpdate(string tableName)
        {
            conn.Open();

            string query = "";
            string id = IDBox.Text;

            if (tableName == "Manager")
            {
                query = $"update {tableName} set DepartmentID='{textBox2.Text}', Manager_Name='{textBox1.Text}', isUpdated = 1 where M_Record_Number = {id}";
            }
            else if (tableName == "Chef")
            {
                query = $"update {tableName} set M_Record_Number='{textBox3.Text}', Chef_Name='{textBox1.Text}', isUpdated = 1 where C_Record_Number = {id}";
            }
            else if (tableName == "Employee")
            {
                query = $"update {tableName} set C_Record_Number='{textBox3.Text}', Employee_Name='{textBox1.Text}', isUpdated = 1 where E_Record_Number = {id}";
            }
            else if (tableName == "Department")
            {
                query = $"update {tableName} set Department_Name='{textBox1.Text}', isUpdated = 1 where Department_ID = {id}";
            }

            SqlCommand comm = new SqlCommand(query, conn);
            comm.ExecuteNonQuery();

            conn.Close();
        }

        public void FuncDelete(string tableName)
        {
            //id = Convert.ToInt32(IDBox.Text);
            //int x = Convert.ToInt32(IDBox.Text);
            //var TBL_Name = db.tableName.Find(x);
            //TBL_Name.isActive = false;
            //TBL_Name.isUpdated = true;
            //db.SaveChanges();

            conn.Open();

            string query = "";
            string id = IDBox.Text;

            if (tableName == "Manager")
            {
                query = $"update {tableName} set isActive = 0, isUpdated = 1 where M_Record_Number = {id}";
            }
            else if (tableName == "Chef")
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

        // dataGridView'e comboBox1'de seçilen tablodaki verileri yansıtma:
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //string selectedTable = comboBox1.Text;
                //var tableName = $"{db}.{selectedTable}".ToList();
                //dataGridView1.DataSource = db.Manager.ToList();

                conn.Open();

                FuncList(comboBox1.Text);

                //SqlCommand comm = new SqlCommand($"select * from {comboBox1.Text}", conn);
                //SqlDataAdapter da = new SqlDataAdapter(comm);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //dataGridView1.DataSource = dt;
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
            else if (comboBox1.Text == "Department")
            {
                IDBox.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            }
        }

        // Veri ekleme işlemleri:
        private void button2_Click(object sender, EventArgs e)
        {
            //conn.Open();

            if (comboBox1.Text == "Manager")
            {
                bool hasError = false;
                int gmNo = 1;

                try
                {
                    FuncAdd("Manager");

                    //Manager TBL_Manager = new Manager();
                    //TBL_Manager.GM_Record_Number = gmNo;
                    //TBL_Manager.Department_ID = Convert.ToInt32(textBox2.Text);
                    //TBL_Manager.Manager_Name = textBox1.Text;
                    //TBL_Manager.isActive = true;
                    //TBL_Manager.isUpdated = false;
                    //db.Manager.Add(TBL_Manager);
                    //db.SaveChanges();

                    //SqlCommand comm = new SqlCommand("INSERT INTO Manager (GM_Record_Number, Department_ID, Manager_Name, isActive, isUpdated) VALUES (@p9, @DepartmentID, @ManagerName, 1, 0)", conn);
                    //comm.Parameters.AddWithValue("@DepartmentID", textBox2.Text);
                    //comm.Parameters.AddWithValue("@ManagerName", textBox1.Text);
                    //comm.Parameters.AddWithValue("@p9", gmNo.ToString());
                    //comm.ExecuteNonQuery();
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
                    FuncAdd("Chef");

                    //Chef TBL_Chef = new Chef();
                    //TBL_Chef.M_Record_Number = Convert.ToInt32(textBox3.Text);
                    //TBL_Chef.Chef_Name = textBox1.Text;
                    //TBL_Chef.isActive = true;
                    //TBL_Chef.isUpdated = false;
                    //db.Chef.Add(TBL_Chef);
                    //db.SaveChanges();

                    //SqlCommand comm = new SqlCommand("INSERT INTO Chef (M_Record_Number, Chef_Name, isActive, isUpdated) VALUES (@MRecordNumber, @ChefName, 1, 0)", conn);
                    //comm.Parameters.AddWithValue("@MRecordNumber", textBox3.Text);
                    //comm.Parameters.AddWithValue("@ChefName", textBox1.Text);
                    //comm.ExecuteNonQuery();
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
                    FuncAdd("Employee");

                    //Employee TBL_Employee = new Employee();
                    //TBL_Employee.C_Record_Number = Convert.ToInt32(textBox3.Text);
                    //TBL_Employee.Employee_Name = textBox1.Text;
                    //TBL_Employee.isActive = true;
                    //TBL_Employee.isUpdated = false;
                    //db.Employee.Add(TBL_Employee);
                    //db.SaveChanges();

                    //SqlCommand comm = new SqlCommand("INSERT INTO Employee (C_Record_Number, Employee_Name, isActive, isUpdated) VALUES (@CRecordNumber, @EmployeeName, 1, 0)", conn);
                    //comm.Parameters.AddWithValue("@CRecordNumber", textBox3.Text);
                    //comm.Parameters.AddWithValue("@EmployeeName", textBox1.Text);
                    //comm.ExecuteNonQuery();
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
            else if (comboBox1.Text == "Department")
            {
                bool hasError = false;

                try
                {
                    FuncAdd("Department");

                    //Department TBL_Department = new Department();
                    //TBL_Department.Department_Name = textBox1.Text;
                    //TBL_Department.isActive = true;
                    //TBL_Department.isUpdated = false;
                    //db.Department.Add(TBL_Department);
                    //db.SaveChanges();

                    //SqlCommand comm = new SqlCommand("insert into Department (Department_Name, isActive, isUpdated) values (@p1, 1, 0)", conn);
                    //comm.Parameters.AddWithValue("@p1", textBox1.Text);
                    //comm.ExecuteNonQuery();
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

            //conn.Close();
        }

        // List buttonunun işlevi:
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //string selectedTable = comboBox1.Text;
                //var tableName = $"{db}.{selectedTable}".ToList();
                //dataGridView1.DataSource = tableName;

                conn.Open();

                FuncList(comboBox1.Text);

                //SqlCommand comm = new SqlCommand($"select * from {comboBox1.Text}", conn);
                //SqlDataAdapter da = new SqlDataAdapter(comm);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //dataGridView1.DataSource = dt;
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

        // Update işlemleri:
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Manager")
            {
                bool hasError = false;

                try
                {
                    FuncUpdate("Manager");

                    //int x = Convert.ToInt32(IDBox.Text);
                    //var TBL_Manager = db.Manager.Find(x);
                    //TBL_Manager.Department_ID = Convert.ToInt32(textBox2.Text);
                    //TBL_Manager.Manager_Name = textBox1.Text;
                    //TBL_Manager.isUpdated = true;
                    //db.Manager.Add(TBL_Manager);
                    //db.SaveChanges();

                    //conn.Open();

                    //SqlCommand comm = new SqlCommand("update Manager set Department_ID=@p1, Manager_Name=@p2, isUpdated=1 where M_Record_Number=@p3", conn);
                    //comm.Parameters.AddWithValue("@p1", textBox2.Text);
                    //comm.Parameters.AddWithValue("@p2", textBox1.Text);
                    //comm.Parameters.AddWithValue("@p3", IDBox.Text);
                    //comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}

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
                    FuncUpdate("Chef");

                    //int x = Convert.ToInt32(IDBox.Text);
                    //var TBL_Chef = db.Chef.Find(x);
                    //TBL_Chef.M_Record_Number = Convert.ToInt32(textBox3.Text);
                    //TBL_Chef.Chef_Name = textBox1.Text;
                    //TBL_Chef.isUpdated = true;
                    //db.Chef.Add(TBL_Chef);
                    //db.SaveChanges();

                    //conn.Open();

                    //SqlCommand comm = new SqlCommand("update Chef set M_Record_Number=@p1, Chef_Name=@p2, isUpdated=1 where C_Record_Number=@p3", conn);
                    //comm.Parameters.AddWithValue("@p1", textBox3.Text);
                    //comm.Parameters.AddWithValue("@p2", textBox1.Text);
                    //comm.Parameters.AddWithValue("@p3", IDBox.Text);
                    //comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}

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

                    //int x = Convert.ToInt32(IDBox.Text);
                    //var TBL_Employee = db.Employee.Find(x);
                    //TBL_Employee.C_Record_Number = Convert.ToInt32(textBox3.Text);
                    //TBL_Employee.Employee_Name = textBox1.Text;
                    //TBL_Employee.isUpdated = true;
                    //db.Employee.Add(TBL_Employee);
                    //db.SaveChanges();

                    //conn.Open();

                    //SqlCommand comm = new SqlCommand("update Employee set C_Record_Number=@p1, Employee_Name=@p2, isUpdated=1 where E_Record_Number=@p3", conn);
                    //comm.Parameters.AddWithValue("@p1", textBox3.Text);
                    //comm.Parameters.AddWithValue("@p2", textBox1.Text);
                    //comm.Parameters.AddWithValue("@p3", IDBox.Text);
                    //comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}

                    if (!hasError && !string.IsNullOrEmpty(IDBox.Text))
                    {
                        MessageBox.Show("You updated the related Employees data.");
                    }
                }
            }
            else if (comboBox1.Text == "Department")
            {
                bool hasError = false;

                try
                {
                    FuncUpdate("Department");

                    //int x = Convert.ToInt32(IDBox.Text);
                    //var TBL_Department = db.Department.Find(x);
                    //TBL_Department.Department_Name = textBox1.Text;
                    //TBL_Department.isUpdated = true;
                    //db.Department.Add(TBL_Department);
                    //db.SaveChanges();

                    //conn.Open();

                    //SqlCommand comm = new SqlCommand("update Department set Department_Name=@p1, isUpdated=1 where Department_ID=@p3", conn);
                    //comm.Parameters.AddWithValue("@p1", textBox1.Text);
                    //comm.Parameters.AddWithValue("@p3", IDBox.Text);
                    //comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}

                    if (!hasError && !string.IsNullOrEmpty(IDBox.Text))
                    {
                        MessageBox.Show("You updated the related Departments data.");
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
                    FuncDelete("Manager");

                    //int x = Convert.ToInt32(IDBox.Text);
                    //var TBL_Manager = db.Manager.Find(x);
                    //TBL_Manager.isActive = false;
                    //TBL_Manager.isUpdated = true;
                    //db.SaveChanges();

                    //conn.Open();

                    //SqlCommand comm = new SqlCommand("update Manager set isActive=0, isUpdated=1 where M_Record_Number=@p1", conn);
                    //comm.Parameters.AddWithValue("@p1", IDBox.Text);
                    //comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}

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
                    FuncDelete("Chef");

                    //int x = Convert.ToInt32(IDBox.Text);
                    //var TBL_Chef = db.Chef.Find(x);
                    //TBL_Chef.isActive = false;
                    //TBL_Chef.isUpdated = true;
                    //db.SaveChanges();

                    //conn.Open();

                    //SqlCommand comm = new SqlCommand("update Chef set isActive=0, isUpdated=1 where C_Record_Number=@p1", conn);
                    //comm.Parameters.AddWithValue("@p1", IDBox.Text);
                    //comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}

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

                    //int x = Convert.ToInt32(IDBox.Text);
                    //var TBL_Employee = db.Employee.Find(x);
                    //TBL_Employee.isActive = false;
                    //TBL_Employee.isUpdated = true;
                    //db.SaveChanges();

                    //conn.Open();

                    //SqlCommand comm = new SqlCommand("update Employee set isActive=0, isUpdated=1 where E_Record_Number=@p1", conn);
                    //comm.Parameters.AddWithValue("@p1", IDBox.Text);
                    //comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}

                    if (!hasError && !string.IsNullOrEmpty(IDBox.Text))
                    {
                        MessageBox.Show("You deleted the related Employee data.");
                    }
                }
            }
            else if (comboBox1.Text == "Department")
            {
                bool hasError = false;

                try
                {
                    FuncDelete("Department");

                    //int x = Convert.ToInt32(IDBox.Text);
                    //var TBL_Department = db.Department.Find(x);
                    //TBL_Department.isActive = false;
                    //TBL_Department.isUpdated = true;
                    //db.SaveChanges();

                    //conn.Open();

                    //SqlCommand comm = new SqlCommand("update Department set isActive=0, isUpdated=1 where Department_ID=@p1", conn);
                    //comm.Parameters.AddWithValue("@p1", IDBox.Text);
                    //comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    hasError = true;
                    MessageBox.Show(err.ToString());
                }
                finally
                {
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //}

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
