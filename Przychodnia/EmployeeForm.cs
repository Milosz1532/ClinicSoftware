using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;

namespace Przychodnia
{
    public partial class EmployeeForm : Form
    {
        Employee employee = null;
        public EmployeeForm(Employee employee)
        {
            InitializeComponent();
            guna2NumericUpDown1.Maximum = DateTime.Now.Year;
            guna2NumericUpDown1.Value = DateTime.Now.Year;
            this.employee = employee;

            if (employee.First_name != null)
            {
                guna2TextBox1.Text = employee.First_name;
                guna2TextBox2.Text = employee.Last_name;
                guna2NumericUpDown1.Value = employee.Start_year;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; // important!!! 
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            if (Treatment.Treatment_list.Count == 0)
            {
                
                flowLayoutPanel1.Visible = false;
                error_label.Visible = true;
                error_img.Visible = true;
            }
            foreach (Treatment treatment in Treatment.Treatment_list)
            {
                CheckBox newCheckBox = new Guna.UI2.WinForms.Guna2CheckBox()
                {
                    ForeColor = Color.Gray,
                    //Size = new System.Drawing.Size(200, 21),
                    AutoSize = true,
                    Text = treatment.Name,
                    Tag = treatment,
                };
                flowLayoutPanel1.Controls.Add(newCheckBox);
            //    if (employee.Treatments.Contains(treatment))
            //    {
            //        newCheckBox.Checked = true;
            //    }
                if (employee.Treatments.Any(item => (item.Name == treatment.Name)))
                {
                    newCheckBox.Checked = true;
                }
            }

        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //Accept Button//

            if (guna2TextBox1.Text.Length < 3)
            {
                MessageBox.Show("Wprowadź imię pracownika", "Wystąpił błąd!");
                return;
            }else if (guna2TextBox2.Text.Length < 3)
            {
                MessageBox.Show("Wprowadź nazwisko pracownika", "Wystąpił błąd!");
                return;
            }

            employee.First_name = guna2TextBox1.Text;
            employee.Last_name = guna2TextBox2.Text;
            employee.Start_year = (int)guna2NumericUpDown1.Value;
            employee.Treatments.Clear();
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if ((c is CheckBox) && ((CheckBox)c).Checked)
                {
                    employee.Treatments.Add((Treatment)c.Tag);
                }
            }
            DialogResult = DialogResult.OK;
        }
    }
}
