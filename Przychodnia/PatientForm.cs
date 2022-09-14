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
using System.Text.RegularExpressions;


namespace Przychodnia
{
    public partial class PatientForm : Form
    {
        Patient patient = null;
        public PatientForm(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;

            guna2NumericUpDown2.Maximum = DateTime.Now.Year;
            guna2NumericUpDown2.Value = DateTime.Now.Year;
        }

        private void PatientForm_Load(object sender, EventArgs e)
        {
            if (patient.First_name != null)
            {
                guna2TextBox1.Text = patient.First_name;
                guna2TextBox2.Text = patient.Last_name;
                guna2TextBox5.Text = patient.Pesel.ToString();
                guna2NumericUpDown2.Value = patient.Year_birth;
                guna2TextBox3.Text = patient.Address;
                guna2NumericUpDown3.Value = patient.Phone_number;
                guna2TextBox4.Text = patient.Mail;
                refreshAuthorizedPersonDataGridView();
            }
        }

        public static void showError(string errorText)
        {
            MessageBox.Show(errorText,"Wystąpił błąd");
        }

        private static bool EmailIsValid(string email)
        {
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expression))
            {
                if (Regex.Replace(email, expression, string.Empty).Length == 0)
                {
                    return true;
                }
            }
            return false;
        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; // important!!! 
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text.Length <= 2)
            {
                showError("Wprowadzone imię jest zbyt krótkie");
            }
            else if (guna2TextBox2.Text.Length <= 2)
            {
                showError("Wprowadzone nazwisko jest zbyt krótkie");
            }
            else if (guna2TextBox5.Text.Length != 11)
            {
                showError("Wprowadzony numer pesel jest nieprawidłowy");
            }
            else if (guna2TextBox3.Text.Length < 5)
            {
                showError("Wprowadzony adres zamieszkania jest zbyt krótki");
            }
            else if (guna2TextBox4.Enabled == true && !EmailIsValid(guna2TextBox4.Text))
            {
                showError("Wprowadzone adres e-mail jest nieprawidłowy");
            }
            else
            {
                patient.First_name = guna2TextBox1.Text;
                patient.Last_name = guna2TextBox2.Text;
                patient.Pesel = long.Parse(guna2TextBox5.Text);
                patient.Year_birth = (int)guna2NumericUpDown2.Value;
                patient.Address = guna2TextBox3.Text;
                patient.Phone_number = (long)guna2NumericUpDown3.Value;
                if (guna2CheckBox1.Checked == false)
                {
                    guna2TextBox4.Text = "";
                }
                patient.Mail = guna2TextBox4.Text;
                DialogResult = DialogResult.OK;


            }

            
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            AuthorizedPerson newAuthorizedPerson = new AuthorizedPerson();
            AuthorizedPersonsForm authorizedPersonsForm = new AuthorizedPersonsForm(newAuthorizedPerson);
            if (authorizedPersonsForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            patient.Authorized_person_list.Add(newAuthorizedPerson);
            refreshAuthorizedPersonDataGridView();
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count <= 0)
            {
                return;
            }
            AuthorizedPerson editAuthorizedPerson = (AuthorizedPerson)guna2DataGridView1.SelectedRows[0].Tag;
            AuthorizedPersonsForm AuthorizedPersonsForm = new AuthorizedPersonsForm(editAuthorizedPerson);
            if (AuthorizedPersonsForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            refreshAuthorizedPersonDataGridView();
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count <= 0)
            {
                return;
            }
            DialogResult result;
            result = MessageBox.Show("Czy na pewno chcesz usunąć upoważnioną osobę", "Usuwanie upoważnionej osoby", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            if (result == DialogResult.Yes)
            {
                AuthorizedPerson removeAuthorizedPerson = (AuthorizedPerson)guna2DataGridView1.SelectedRows[0].Tag;
                patient.Authorized_person_list.Remove(removeAuthorizedPerson);
                refreshAuthorizedPersonDataGridView();
            }
        }



        void refreshAuthorizedPersonDataGridView()
        {
            guna2DataGridView1.Rows.Clear();
            foreach (AuthorizedPerson person in patient.Authorized_person_list)
            {
                string setCheck1 = "";
                string setCheck2 = "";
                if (person.Health_information)
                {
                    setCheck1 = "✔ TAK";
                }else
                {
                    setCheck1 = "❌ Nie";
                }
                if (person.Documentation_information)
                {
                    setCheck2 = "✔ TAK";
                }
                else
                {
                    setCheck2 = "❌ Nie";
                }
                int rowIndex = guna2DataGridView1.Rows.Add(new object[] {person.First_name, person.Last_name, person.Pesel, person.Phone_number, person.Address, setCheck1, setCheck2});
                guna2DataGridView1.Rows[rowIndex].Tag = person;
            }
        }

        public static void textBoxOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            for (int h = 58; h <= 127; h++)
            {
                if (e.KeyChar == h)
                {
                    e.Handled = true;
                }
            }
            for (int k = 32; k <= 47; k++)
            {
                if (e.KeyChar == k) 
                {
                    e.Handled = true;
                }
            }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            guna2TextBox4.Enabled = guna2CheckBox1.Checked;
        }
    }
}