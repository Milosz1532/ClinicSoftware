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
    public partial class AuthorizedPersonsForm : Form
    {
        AuthorizedPerson person = null;
        public AuthorizedPersonsForm(AuthorizedPerson person)
        {
            InitializeComponent();
            this.person = person;
        }

        private void AuthorizedPersonsForm_Load(object sender, EventArgs e)
        {
            guna2TextBox1.Text = person.First_name;
            guna2TextBox2.Text = person.Last_name;
            guna2TextBox4.Text = person.Pesel.ToString();
            guna2NumericUpDown2.Value = person.Phone_number;
            guna2TextBox3.Text = person.Address;
            guna2CheckBox1.Checked = person.Health_information;
            guna2CheckBox2.Checked = person.Documentation_information;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; //IMPORTANT!!!
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (guna2TextBox1.Text.Length < 3)
            {
                PatientForm.showError("Wprowadzone imię jest zbyt krótkie");
            }
            else if (guna2TextBox2.Text.Length < 3)
            {
                PatientForm.showError("Wprowadzone nazwisko jest zbyt krótkie");
            }
            else if (guna2TextBox4.Text.Length != 11)
            {
                PatientForm.showError("Wprowadzony numer pesel jest nieprawidłowy");
            }
            else if (guna2TextBox3.Text.Length < 5)
            {
                PatientForm.showError("Wprowadzony adres zamieszkania jest zbyt krótki");
            }
            else if (guna2CheckBox1.Checked == false && guna2CheckBox2.Checked == false)
            {
                PatientForm.showError("Proszę wybrać typ upoważnienia");
            }
            else
            {
                person.First_name = guna2TextBox1.Text;
                person.Last_name = guna2TextBox2.Text;
                person.Pesel = long.Parse(guna2TextBox4.Text);
                person.Phone_number = (long)guna2NumericUpDown2.Value;
                person.Address = guna2TextBox3.Text;
                person.Health_information = guna2CheckBox1.Checked;
                person.Documentation_information = guna2CheckBox2.Checked;
                DialogResult = DialogResult.OK;
            }
            
        }
    }
}
