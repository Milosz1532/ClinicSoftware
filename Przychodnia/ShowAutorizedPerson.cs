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
    public partial class ShowAutorizedPerson : Form
    {
        Patient patient = null;
        public ShowAutorizedPerson(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            guna2GroupBox1.Text = "Dostęp do informacji o pacjencie: " + patient.First_name + " " + patient.Last_name;

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
                    setCheck1 = "❌Nie";
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; // Important!!!
        }
    }
}
