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
    public partial class ShowEmployeeTreatments : Form
    {
        Employee employee = null;
        public ShowEmployeeTreatments(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
        }

        private void ShowEmployeeTreatments_Load(object sender, EventArgs e)
        {
            guna2GroupBox1.Text = "Uprawnienia pracownika " + employee.First_name + " " + employee.Last_name;

            guna2DataGridView1.Rows.Clear();
            foreach (Treatment treatment in employee.Treatments)
            {
                int rowIndex = guna2DataGridView1.Rows.Add(new object[] { treatment.Name});
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; // IMPORTANT!!!
        }
    }
}
