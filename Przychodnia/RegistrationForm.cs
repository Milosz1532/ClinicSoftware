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
    public partial class RegistrationForm : Form
    {
        Registration registration = null;
        public RegistrationForm(Registration registration)
        {
            InitializeComponent();
            this.registration = registration;
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
            comboBox2.DisplayMember = "Text";
            comboBox2.ValueMember = "Value";
            comboBox3.DisplayMember = "Text";
            comboBox3.ValueMember = "Value";
            comboBox4.DisplayMember = "Text";
            comboBox4.ValueMember = "Value";
            comboBox1.Items.Clear();
            foreach (Patient patient in Patient.Patient_list)
            {
                comboBox1.Items.Add(new { Text = patient.First_name+" "+patient.Last_name+" ("+patient.Pesel+")", Value = patient });
            }
            comboBox2.Items.Clear();
            foreach (Employee employee in Employee.Employee_list)
            {
                if (Schedule.ScheduleList.Any(item => (item.Employee.First_name+item.Employee.Last_name == employee.First_name+employee.Last_name)))
                {
                    comboBox2.Items.Add(new { Text = employee.First_name + " " + employee.Last_name, Value = employee });
                }

            }
            if (registration.Patient != null)
            {
                comboBox1.SelectedIndex = comboBox1.FindStringExact(registration.Patient.First_name + " " + registration.Patient.Last_name + " (" + registration.Patient.Pesel + ")");
                comboBox2.SelectedIndex = comboBox2.FindStringExact(registration.Employee.First_name + " " + registration.Employee.Last_name);
                comboBox3.SelectedIndex = comboBox3.FindStringExact(registration.Treatment.Name);
                comboBox4.SelectedIndex = comboBox4.FindStringExact(registration.Schedule.Date.ToString("dd.MM.yyyy"));
                comboBox5.Items.Add(registration.Hour);
                comboBox5.SelectedIndex = comboBox5.FindStringExact(registration.Hour);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; // IMPORTANT!
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0 || comboBox2.SelectedIndex < 0 || comboBox3.SelectedIndex < 0 || comboBox4.SelectedIndex < 0 || comboBox5.Text == "")
            {
                MessageBox.Show("Proszę zaznaczyć wszystkie pola!", "Wystąpił błąd");
                return;
            }

            registration.Patient = (comboBox1.SelectedItem as dynamic).Value;
            registration.Employee = (comboBox2.SelectedItem as dynamic).Value;
            registration.Treatment = (comboBox3.SelectedItem as dynamic).Value;
            registration.Schedule = (comboBox4.SelectedItem as dynamic).Value;
            registration.Hour = comboBox5.Text;
        
            DialogResult = DialogResult.OK;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            foreach (Treatment treatment in (comboBox2.SelectedItem as dynamic).Value.Treatments)
            {
                comboBox3.Items.Add(new { Text = treatment.Name, Value = treatment });
            }
            foreach(Schedule schedule in Schedule.ScheduleList)
            {
                if (schedule.Employee.First_name+schedule.Employee.Last_name == (comboBox2.SelectedItem as dynamic).Value.First_name+ (comboBox2.SelectedItem as dynamic).Value.Last_name)
                {
                    comboBox4.Items.Add(new { Text = schedule.Date.ToString("dd.MM.yyyy"), Value = schedule });
                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            Schedule schedule = (comboBox4.SelectedItem as dynamic).Value;
            int allVisits = (schedule.EndJob-schedule.StartJob)*6;
            int addHours = 0;
            int minutes = 0;
            string showHour = "";
            for (int i = 0; i < allVisits; i++)
            {
                if (minutes == 0)
                {
                    showHour = schedule.StartJob + addHours + ":0" + minutes;
                }else
                {
                    showHour = schedule.StartJob + addHours + ":" + minutes;
                }
                if (!Registration.RegistrationList.Any(item => (item.Hour == showHour) && (item.Employee == (comboBox2.SelectedItem as dynamic).Value)))
                {
                    comboBox5.Items.Add(showHour);  
                }
                minutes += 10;
                if (minutes == 60)
                {
                    addHours++;
                    minutes = 0;
                }
            }
        }
    }
}
