using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employee_Management_System_
{
    public partial class Form1 : Form
    {
        private List<Employee> employeeList = new List<Employee>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmployees.MultiSelect = false;
            dgvEmployees.ReadOnly = true;
            dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            RefreshGrid();
        }

       

        private void RefreshGrid()
        {
            dgvEmployees.DataSource = null;
            dgvEmployees.DataSource = employeeList;
        }

        private void ClearFields()
        {
            txtEmpID.Clear();
            txtEmpName.Clear();
            numEmpAge.Value = 18;
            txtEmpDept.Clear();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtEmpID.Text) ||
                string.IsNullOrWhiteSpace(txtEmpName.Text) ||
                string.IsNullOrWhiteSpace(txtEmpDept.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }
            return true;
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                var row = dgvEmployees.SelectedRows[0];
                txtEmpID.Text = row.Cells[0].Value.ToString();
                txtEmpName.Text = row.Cells[1].Value.ToString();
                numEmpAge.Value = Convert.ToDecimal(row.Cells[2].Value);
                txtEmpDept.Text = row.Cells[3].Value.ToString();
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                var existing = employeeList.Any(x => x.ID == txtEmpID.Text.Trim());
                if (existing)
                {
                    MessageBox.Show("Employee with this ID already exists.");
                    return;
                }

                Employee emp = new Employee
                {
                    ID = txtEmpID.Text.Trim(),
                    Name = txtEmpName.Text.Trim(),
                    Age = (int)numEmpAge.Value,
                    Department = txtEmpDept.Text.Trim()
                };

                employeeList.Add(emp);
                RefreshGrid();
                ClearFields();
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0 && ValidateInput())
            {
                string selectedID = dgvEmployees.SelectedRows[0].Cells[0].Value.ToString();
                Employee emp = employeeList.FirstOrDefault(x => x.ID == selectedID);

                if (emp != null)
                {
                    emp.Name = txtEmpName.Text.Trim();
                    emp.Age = (int)numEmpAge.Value;
                    emp.Department = txtEmpDept.Text.Trim();
                    RefreshGrid();
                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Select a record to update.");
            }

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                string selectedID = dgvEmployees.SelectedRows[0].Cells[0].Value.ToString();
                var emp = employeeList.FirstOrDefault(x => x.ID == selectedID);
                if (emp != null)
                {
                    employeeList.Remove(emp);
                    RefreshGrid();
                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Select a record to delete.");
            }

        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            ClearFields();

        }
    }
}
