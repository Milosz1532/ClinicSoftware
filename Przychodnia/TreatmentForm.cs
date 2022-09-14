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
    public partial class TreatmentForm : Form
    {
        Treatment treatment = null;
        public TreatmentForm(Treatment treatment)
        {
            InitializeComponent();
            this.treatment = treatment;

            if (treatment.Name != null)
            {
                guna2TextBox1.Text = treatment.Name;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; // IMPORTANT!
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text.Length <= 2)
            {
                MessageBox.Show("Nazwa czynności jest zbyt krótka.");
                return;
            }
            treatment.Name = guna2TextBox1.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
