using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;

namespace Przychodnia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Operating hours of the clinic
        int open = 7;
        int close = 18;

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            datetime_label.Text = DateTime.Now.ToString("dddd, dd.MM.yyyy");
            guna2DateTimePicker1.Value = DateTime.Now;

            //setTester();
            //Set Employers to ListBox1 //
            setScheduleEmployeeList();
            //Set Rooms to combobox //
            setComboboxRoomItems();
            // Set operating hours of the clinic
            setHoursToComboBox();
            refreshWorkHours();
            setStats();
        }

        private void refreshWorkHours()
        {
            dateTimePicker1.Value = DateTime.Parse(TimeSpan.FromHours(open).ToString("hh':'mm"));
            dateTimePicker2.Value = DateTime.Parse(TimeSpan.FromHours(close).ToString("hh':'mm"));
        }

        private void setStats() // Main Page
        {
            stats_1.Text = Patient.Patient_list.Count.ToString();
            stats_2.Text = Employee.Employee_list.Count.ToString();
            stats_3.Text = Treatment.Treatment_list.Count.ToString();
            stats_5.Text = Registration.RegistrationList.Count.ToString();
            int i = 0;
            foreach(Registration reg in Registration.RegistrationList)
            {
                if (reg.Schedule.Date.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy"))
                {
                    i++;
                }
            }
            stats_4.Text = i.ToString();
        }

        public void showMessage(string text, string title, string icon)
        {
            guna2MessageDialog1.Show(text, title);
            if (icon != null)
            {
                if (icon == "information")
                {
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                }else if (icon == "error")
                {
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                }else if (icon == "warning")
                {
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                }
                
            }
        }

        private void setHoursToComboBox()
        {
            guna2ComboBox1.Items.Clear();
            guna2ComboBox2.Items.Clear();
            int ClinicHours = close - open;
            for (int i = 0; i <= ClinicHours; i++)
            {
                if ((open + i) < 10)
                {
                    guna2ComboBox1.Items.Add("0" + (open + i).ToString() + ":00");
                    guna2ComboBox2.Items.Add("0" + (open + i).ToString() + ":00");
                }
                else
                {
                    guna2ComboBox1.Items.Add((open + i).ToString() + ":00");
                    guna2ComboBox2.Items.Add((open + i).ToString() + ":00");
                }
            }
            foreach(Schedule schedule in Schedule.ScheduleList)
            {
                if (schedule.StartJob < open)
                {
                    showMessage("W zaplanowanym grafiku występują terminy, w których godziny rozpoczęcia są wcześniejsze od zaplanowanych \nd","Ostrzeżenie", "warning");
                    break;
                }else if (schedule.EndJob > close)
                {
                    showMessage("W zaplanowanym grafiku występują terminy, w których godziny rozpoczęcia są późniejsze od zaplanowanych \nd", "Ostrzeżenie", "warning");
                    break;
                }
            }
        }

        void setTester()
        {
            Patient patient1 = new Patient();
            patient1.First_name = "Miłosz";
            patient1.Last_name = "Konopka";
            patient1.Year_birth = 2001;
            patient1.Pesel = 2121321323;
            patient1.Phone_number = 547698257;
            patient1.Address = "Anonimowa 1";
            patient1.Mail = "milosz@mail.com";
            Patient patient2 = new Patient();
            patient2.First_name = "Jan";
            patient2.Last_name = "Kowalski";
            patient2.Year_birth = 1985;
            patient2.Phone_number = 674587965;
            patient2.Pesel = 4524324323;
            patient2.Address = "Anonimowa 14";
            patient2.Mail = "kowalski@mail.com";
            Patient patient3 = new Patient();
            patient3.First_name = "Anna";
            patient3.Last_name = "Lewandowska";
            patient3.Year_birth = 1975;
            patient3.Pesel = 98327542353;
            patient3.Phone_number = 875698254;
            patient3.Address = "Anonimowa 12/42";
            patient3.Mail = "annalewandowska@mail.com";
            Patient.Patient_list.Add(patient1);
            Patient.Patient_list.Add(patient2);
            Patient.Patient_list.Add(patient3);
            refreshPatientsDataGridView();

            Treatment treatment1 = new Treatment();
            treatment1.Name = "Wykonywanie szczepień/zastrzyków";
            Treatment treatment2 = new Treatment();
            treatment2.Name = "Pediatra dziecięcy ";
            Treatment treatment3 = new Treatment();
            treatment3.Name = "Pediatra dla osób dorosłych";
            Treatment treatment4 = new Treatment();
            treatment4.Name = "Elektrokardiografia serca";

            Treatment.Treatment_list.Add(treatment1);
            Treatment.Treatment_list.Add(treatment2);
            Treatment.Treatment_list.Add(treatment3);
            Treatment.Treatment_list.Add(treatment4);
            refreshTreatmentsDataGridView();


            Employee employee1 = new Employee();
            employee1.First_name = "Joanna";
            employee1.Last_name = "Lipińska";
            employee1.Start_year = 2004;
            employee1.Treatments = new List<Treatment>();
            employee1.Treatments.Add(Treatment.Treatment_list[0]);
            employee1.Treatments.Add(Treatment.Treatment_list[1]);
            employee1.Treatments.Add(Treatment.Treatment_list[2]);
            employee1.Treatments.Add(Treatment.Treatment_list[3]);
            Employee employee2 = new Employee();
            employee2.First_name = "Grzegorz";
            employee2.Last_name = "Hebko";
            employee2.Start_year = 2007;
            employee2.Treatments.Add(Treatment.Treatment_list[2]);
            employee2.Treatments.Add(Treatment.Treatment_list[3]);
            Employee employee3 = new Employee();
            employee3.First_name = "Marcin";
            employee3.Last_name = "Jędrzejewski";
            employee3.Start_year = 2015;

            Employee.Employee_list.Add(employee1);
            Employee.Employee_list.Add(employee2);
            Employee.Employee_list.Add(employee3);
            refreshEmployersDataGridView();

            Room room1 = new Room();
            room1.Number = 1;
            Room room2 = new Room();
            room2.Number = 2;
            Room room3 = new Room();
            room3.Number = 3;
            Room room4 = new Room();
            room4.Number = 4;

            Room.Room_list.Add(room1);
            Room.Room_list.Add(room2);
            Room.Room_list.Add(room3);
            Room.Room_list.Add(room4);
            refreshRoomsDataGridView();

            Schedule schedule1 = new Schedule();
            schedule1.Employee = employee1;
            schedule1.Room = room1;
            schedule1.Date = DateTime.Now;
            schedule1.StartJob = 7;
            schedule1.EndJob = 10;
            Schedule.ScheduleList.Add(schedule1);
            refreshScheduleDataGridView();

        }


        // CLOCK //
        private void timer1_Tick(object sender, EventArgs e)
        {
            clock_label.Text = DateTime.Now.ToString("t");
        }

        //////////////////////// PATIENT ////////////////////////////////////////////

        private void addPatient()
        {
            Patient newPatient = new Patient();
            PatientForm patientForm = new PatientForm(newPatient);
            if (patientForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Patient.Patient_list.Add(newPatient);
            refreshPatientsDataGridView();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Add Patient //
            addPatient();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Edit Patient //
            if (guna2DataGridView1.SelectedRows.Count <= 0)
            {
                return;
            }
            Patient editPatient = (Patient)guna2DataGridView1.SelectedRows[0].Tag;
            PatientForm PatientForm = new PatientForm(editPatient);
            if (PatientForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            refreshPatientsDataGridView();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Delete Patient //
            if (guna2DataGridView1.SelectedRows.Count <= 0)
            {
                return;
            }
            DialogResult result;
            result = MessageBox.Show("Czy na pewno chcesz usunąć tego pacjenta?", "Usuwanie pacjenta", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            if (result == DialogResult.Yes)
            {
                Patient removePatient = (Patient)guna2DataGridView1.SelectedRows[0].Tag;
                Patient.Patient_list.Remove(removePatient);
                refreshPatientsDataGridView();
            }
        }

        void refreshPatientsDataGridView()
        {
            guna2DataGridView1.Rows.Clear();
            foreach (Patient patient in Patient.Patient_list)
            {
                int rowIndex = guna2DataGridView1.Rows.Add(patient.ToObjTbl);
                guna2DataGridView1.Rows[rowIndex].Tag = patient;
            }
        }

        //////////////////////// EMPLOYERS ////////////////////////////////////////////
        
        private void addEmployee()
        {
            Employee newEmployee = new Employee();
            EmployeeForm newEmployeeForm = new EmployeeForm(newEmployee);
            if (newEmployeeForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Employee.Employee_list.Add(newEmployee);
            refreshEmployersDataGridView();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // Add Employee //
            addEmployee();
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // Edit Employee //
            if (guna2DataGridView2.SelectedRows.Count <= 0)
            {
                return;
            }
            Employee editEmployee = (Employee)guna2DataGridView2.SelectedRows[0].Tag;
            EmployeeForm EmployeeForm = new EmployeeForm(editEmployee);
            if (EmployeeForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            refreshEmployersDataGridView();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Delete employee //
            if (guna2DataGridView2.SelectedRows.Count <= 0)
            {
                return;
            }
            DialogResult result;
            result = MessageBox.Show("Czy na pewno chcesz usunąć tego pracownika?", "Usuwanie pracownika", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            if (result == DialogResult.Yes)
            {
                Employee removeEmployee = (Employee)guna2DataGridView2.SelectedRows[0].Tag;
                Employee.Employee_list.Remove(removeEmployee);
                refreshEmployersDataGridView();
            }
        }

        void setScheduleEmployeeList()
        {
            listBox1.Items.Clear();
            foreach (Employee employee in Employee.Employee_list)
            {
                listBox1.Items.Add(employee.First_name + " " + employee.Last_name);
            }
        }

        void refreshEmployersDataGridView()
        {
            guna2DataGridView2.Rows.Clear();
            foreach (Employee employee in Employee.Employee_list)
            {
                int rowIndex = guna2DataGridView2.Rows.Add(employee.ToObjTbl);
                guna2DataGridView2.Rows[rowIndex].Tag = employee;
            }
            setScheduleEmployeeList();
        }
        ///////////////////////////////////////////////////////////////////

        //////////////////////// ROOMS ////////////////////////////////////
        private void guna2Button9_Click(object sender, EventArgs e)
        {
            // Add new room //
            Room newRoom = new Room();
            RoomForm roomForm = new RoomForm(newRoom);
            if (roomForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Room.Room_list.Add(newRoom);
            refreshRoomsDataGridView();
        }
        private void guna2Button8_Click(object sender, EventArgs e)
        {
            // Edit Room //
            if (guna2DataGridView3.SelectedRows.Count <= 0)
            {
                return;
            }
            Room editRoom = (Room)guna2DataGridView3.SelectedRows[0].Tag;
            RoomForm roomForm = new RoomForm(editRoom);
            if (roomForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            refreshRoomsDataGridView();
        }
        private void guna2Button7_Click(object sender, EventArgs e)
        {
            // Delete Room //
            if (guna2DataGridView3.SelectedRows.Count <= 0)
            {
                return;
            }
            DialogResult result;
            result = MessageBox.Show("Czy na pewno chcesz usunąć ten wpis?", "Usuwanie wpisu", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            if (result == DialogResult.Yes)
            {
                Room removeRoom = (Room)guna2DataGridView3.SelectedRows[0].Tag;
                Room.Room_list.Remove(removeRoom);
                refreshRoomsDataGridView();
            }
        }

        void setComboboxRoomItems()
        {
            guna2ComboBox3.Items.Clear();
            foreach (Room room in Room.Room_list)
            {
                guna2ComboBox3.Items.Add("Gabinet " + room.Number);
            }
        }
        void refreshRoomsDataGridView()
        {
            guna2DataGridView3.Rows.Clear();
            foreach (Room room in Room.Room_list)
            {
                int rowIndex = guna2DataGridView3.Rows.Add(room.ToObjTbl);
                guna2DataGridView3.Rows[rowIndex].Tag = room;
            }
            setComboboxRoomItems();
        }
        ///////////////////////////////////////////////////////////////////


        ///////////////////////// Treatments //////////////////////////////

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            // Add Treatment //
            Treatment newTreatment = new Treatment();
            TreatmentForm treatmentForm = new TreatmentForm(newTreatment);
            if (treatmentForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Treatment.Treatment_list.Add(newTreatment);
            refreshTreatmentsDataGridView();
        }
    
        private void guna2Button11_Click(object sender, EventArgs e)
        {
            // Edit Treatment //
            if (guna2DataGridView4.SelectedRows.Count <= 0)
            {
                return;
            }
            Treatment editTreatment = (Treatment)guna2DataGridView4.SelectedRows[0].Tag;
            TreatmentForm TreatmentForm = new TreatmentForm(editTreatment);
            if (TreatmentForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            refreshTreatmentsDataGridView();
        }
        private void guna2Button10_Click(object sender, EventArgs e)
        {
            // Delete Treatment //
            if (guna2DataGridView4.SelectedRows.Count <= 0)
            {
                return;
            }
            DialogResult result;
            result = MessageBox.Show("Czy na pewno chcesz usunąć ten wpis?", "Usuwanie wpisu", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            if (result == DialogResult.Yes)
            {
                Treatment removeTreatment = (Treatment)guna2DataGridView4.SelectedRows[0].Tag;
                Treatment.Treatment_list.Remove(removeTreatment);
                refreshTreatmentsDataGridView();
            }
        }
        void refreshTreatmentsDataGridView()
        {
            guna2DataGridView4.Rows.Clear();
            foreach (Treatment treatment in Treatment.Treatment_list)
            {
                int rowIndex = guna2DataGridView4.Rows.Add(treatment.ToObjTbl);
                guna2DataGridView4.Rows[rowIndex].Tag = treatment;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////// WORK SCHEDULE //////////////////////////////////

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                return;
            }
            guna2GroupBox2.Enabled = true;
            refreshScheduleDataGridView();

        }

        private bool scheduleValidation()
        {
            if (guna2ComboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Proszę wybrać godzinę rozpoczęcia pracy", "Wystąpił błąd");
                return false;
            }
            else if (guna2ComboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Proszę wybrać godzinę zakończenia pracy", "Wystąpił błąd");
                return false;
            }
            else if (int.Parse(guna2ComboBox2.Text.Substring(0, 2)) <= int.Parse(guna2ComboBox1.Text.Substring(0, 2)))
            {
                MessageBox.Show("Godzina zakończenia pracy nie może być mniejsza lub równa godziny rozpoczęcia.", "Wystąpił błąd");
                return false;
            }
            else if (guna2ComboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Proszę wybrać gabinet", "Wystąpił błąd");
                return false;
            }
            else if (Schedule.ScheduleList.Any(item => (("Gabinet " + item.Room.Number) == guna2ComboBox3.Text) && item.Date.ToString("dd.MM.yyyy") == guna2DateTimePicker1.Value.ToString("dd.MM.yyyy")))
            {
                if (Schedule.ScheduleList.Any(item => (("Gabinet " + item.Room.Number) == guna2ComboBox3.Text) && item.Employee.First_name + " " + item.Employee.Last_name != listBox1.SelectedItem.ToString()))
                {
                    MessageBox.Show("Wybrany gabinet jest już zajęty", "Wystąpił błąd");
                    return false;
                }
            }else if (Employee.Employee_list.First(item => string.Equals(listBox1.SelectedItem, item.First_name + " " + item.Last_name)).Treatments.Count == 0)
            {
                MessageBox.Show("Wybrany pracownik nie posiada żadnych uprawnień.", "Wystąpił błąd");
                return false;
            }
            return true;
        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            if (scheduleValidation())
            {
                if (Schedule.ScheduleList.Any(item => (item.Date.ToString("dd.MM.yyyy") == guna2DateTimePicker1.Value.ToString("dd.MM.yyyy")) && item.Employee.First_name+" "+item.Employee.Last_name == listBox1.SelectedItem.ToString()))
                {
                    MessageBox.Show("Na wybrany dzień jest już ustalony grafik", "Wystąpił błąd");
                    return;
                }
                Schedule newSchedule = new Schedule();
                newSchedule.Date = guna2DateTimePicker1.Value;
                newSchedule.Employee = Employee.Employee_list.First(item => string.Equals(listBox1.SelectedItem, item.First_name + " " + item.Last_name));
                newSchedule.StartJob = int.Parse(guna2ComboBox1.Text.Substring(0, 2));
                newSchedule.EndJob = int.Parse(guna2ComboBox2.Text.Substring(0, 2));
                newSchedule.Room = Room.Room_list.First(item => string.Equals(guna2ComboBox3.Text, "Gabinet " + item.Number));
                Schedule.ScheduleList.Add(newSchedule);
                refreshScheduleDataGridView();
            }
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView5.SelectedRows.Count <= 0)
            {
                return;
            }
            if (scheduleValidation())
            {
                Schedule editSchedule = (Schedule)guna2DataGridView5.SelectedRows[0].Tag;
                editSchedule.Date = guna2DateTimePicker1.Value;
                editSchedule.StartJob= int.Parse(guna2ComboBox1.Text.Substring(0, 2));
                editSchedule.EndJob= int.Parse(guna2ComboBox2.Text.Substring(0,2));
                editSchedule.Room = Room.Room_list.First(item => string.Equals(guna2ComboBox3.Text, "Gabinet " + item.Number));
                refreshScheduleDataGridView();
            }
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView5.SelectedRows.Count <= 0)
            {
                return;
            }
            DialogResult result;
            result = MessageBox.Show("Czy na pewno chcesz usunąć tego pacjenta?", "Usuwanie pacjenta", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            if (result == DialogResult.Yes)
            {
                Schedule removeSchedule = (Schedule)guna2DataGridView5.SelectedRows[0].Tag;
                Schedule.ScheduleList.Remove(removeSchedule);
                refreshScheduleDataGridView();
            }
        }

        void refreshScheduleDataGridView()
        {
            guna2DataGridView5.Rows.Clear();
            foreach (Schedule schedule in Schedule.ScheduleList)
            {
                if ((schedule.Employee.First_name+" "+schedule.Employee.Last_name) == (string)listBox1.SelectedItem)
                {
                    int rowIndex = guna2DataGridView5.Rows.Add(schedule.ToObjTbl);
                    guna2DataGridView5.Rows[rowIndex].Tag = schedule;
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////

        ////////////////////////////// Registration ////////////////////////
        
        private void addRegistration()
        {
            Registration newRegistration = new Registration();
            RegistrationForm registrationForm = new RegistrationForm(newRegistration);
            if (registrationForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Registration.RegistrationList.Add(newRegistration);
            refreshRegistrationDataGridView();
        }

        private void guna2Button18_Click(object sender, EventArgs e)
        {
            addRegistration();
        }

        private void guna2Button17_Click(object sender, EventArgs e)
        {
            // Edit Registration //
            if (guna2DataGridView6.SelectedRows.Count <= 0)
            {
                return;
            }
            Registration editRegistration = (Registration)guna2DataGridView6.SelectedRows[0].Tag;
            RegistrationForm registrationForm = new RegistrationForm(editRegistration);
            if (registrationForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            refreshRegistrationDataGridView();
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView6.SelectedRows.Count <= 0)
            {
                return;
            }
            DialogResult result;
            result = MessageBox.Show("Czy na pewno chcesz usunąć tą wizytę?", "Usuwanie wizyty", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            if (result == DialogResult.Yes)
            {
                Registration removeRegistration = (Registration)guna2DataGridView6.SelectedRows[0].Tag;
                Registration.RegistrationList.Remove(removeRegistration);
                refreshRegistrationDataGridView();
            }
        }

        void refreshRegistrationDataGridView()
        {
            guna2DataGridView6.Rows.Clear();
            foreach (Registration reg in Registration.RegistrationList)
            {
                int rowIndex = guna2DataGridView6.Rows.Add(reg.ToObjTbl);
                guna2DataGridView6.Rows[rowIndex].Tag = reg;
            }
        }


        ////////////////////////////// APLICATION BUTTONS ////////////////////////
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        ///////////////////////////////////////////////////////////////////////////
        
        ////////////////////////////// MOVE FORM //////////////////////////////////
        int mov,movX,movY;

        private void guna2GradientPanel2_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void guna2GradientPanel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }
        private void guna2GradientPanel2_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void guna2Button19_Click(object sender, EventArgs e)
        {
            addPatient();
            guna2TabControl1.SelectedIndex = 1;
        }

        private void guna2Button21_Click(object sender, EventArgs e)
        {
            addRegistration();
            guna2TabControl1.SelectedIndex = 6;
        }

        private void guna2Button20_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedIndex = 5;
        }

        private void guna2Button22_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddMinutes(-dateTimePicker1.Value.Minute);
            dateTimePicker2.Value = dateTimePicker2.Value.AddMinutes(-dateTimePicker2.Value.Minute);
            if (int.Parse(dateTimePicker1.Value.ToString("HH")) > int.Parse(dateTimePicker2.Value.ToString("HH")))
            {
                showMessage("Godzina zamknięcia przychodni nie może być mniejsza od godziny otwarcia.", "Wystąpił błąd!", "error");
                dateTimePicker1.Value = DateTime.Parse(TimeSpan.FromHours(open).ToString("hh':'mm"));
                dateTimePicker2.Value = DateTime.Parse(TimeSpan.FromHours(close).ToString("hh':'mm"));
                return;
            }
            open = int.Parse(dateTimePicker1.Value.ToString("HH"));
            close = int.Parse(dateTimePicker2.Value.ToString("HH"));
            setHoursToComboBox();
        }

        const string filter = "Przychodnia placebo|*.medic";
        private void guna2Button23_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = filter;
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                //e.Cancel = true;
                return;
            }
            FileStream fileStream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write);
            

            var binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(fileStream, Patient.Patient_list);
            binaryFormatter.Serialize(fileStream, Treatment.Treatment_list);
            binaryFormatter.Serialize(fileStream, Room.Room_list);
            binaryFormatter.Serialize(fileStream, Employee.Employee_list);
            binaryFormatter.Serialize(fileStream, Schedule.ScheduleList);
            binaryFormatter.Serialize(fileStream, Registration.RegistrationList);
            binaryFormatter.Serialize(fileStream, open);
            binaryFormatter.Serialize(fileStream, close);

            fileStream.Close();
            showMessage("Dane zostały pomyślnie zapisane.", "Zapis danych", "information");
        }

        private void guna2Button24_Click(object sender, EventArgs e)
        {
            DialogResult result = guna2MessageDialog2.Show("Czy na pewno chcesz wczytać dane z innego pliku?\nWszystkie aktualne dane zostaną utracone.", "Wczytywanie danych");
            if (result == DialogResult.No)
                return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
            var binaryFormatter = new BinaryFormatter();

            Patient.Patient_list = (List<Patient>)binaryFormatter.Deserialize(fileStream);
            Treatment.Treatment_list = (List<Treatment>)binaryFormatter.Deserialize(fileStream);
            Room.Room_list = (List<Room>)binaryFormatter.Deserialize(fileStream);
            Employee.Employee_list = (List<Employee>)binaryFormatter.Deserialize(fileStream);
            Schedule.ScheduleList = (List<Schedule>)binaryFormatter.Deserialize(fileStream); 
            Registration.RegistrationList = (List<Registration>)binaryFormatter.Deserialize(fileStream);

            open = (int)binaryFormatter.Deserialize(fileStream);
            close = (int)binaryFormatter.Deserialize(fileStream);
            refreshWorkHours();
            setStats();

            refreshPatientsDataGridView();
            refreshTreatmentsDataGridView();
            refreshRoomsDataGridView();
            refreshEmployersDataGridView();
            refreshScheduleDataGridView();
            refreshRegistrationDataGridView();
            fileStream.Close();
            showMessage("Dane zostały pomyślnie wczytane.", "Wczytywanie danych","information");
        }

        private void guna2DataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView5.SelectedRows.Count <= 0)
            {
                return;
            }
            Schedule editSchedule = (Schedule)guna2DataGridView5.SelectedRows[0].Tag;
            guna2DateTimePicker1.Value = editSchedule.Date;
            if (editSchedule.StartJob < 10)
            {
                guna2ComboBox1.SelectedIndex = guna2ComboBox1.FindStringExact("0" + editSchedule.StartJob + ":00");
            }
            else
            {
                guna2ComboBox1.SelectedIndex = guna2ComboBox1.FindStringExact(editSchedule.StartJob + ":00");
            }
            if (editSchedule.EndJob < 10)
            {
                guna2ComboBox2.SelectedIndex = guna2ComboBox2.FindStringExact("0" + editSchedule.EndJob + ":00");
            }
            else
            {
                guna2ComboBox2.SelectedIndex = guna2ComboBox2.FindStringExact(editSchedule.EndJob + ":00");
            }
            guna2ComboBox3.SelectedIndex = guna2ComboBox3.FindStringExact("Gabinet " + editSchedule.Room.Number);

        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedIndex == 0)
            {
                setStats();
            }
            
        }


        ///////////////////////////////////////////////////////////////////

        //////////////////////DataGridView1 Icon Click/////////////////////
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                Patient autorizedPersons = (Patient)guna2DataGridView1.SelectedRows[0].Tag;
                ShowAutorizedPerson showAutorizedPerson = new ShowAutorizedPerson(autorizedPersons);
                if (showAutorizedPerson.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
        }
        //////////////////////DataGridView2 Icon Click/////////////////////
        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                Employee EmpyloeeTreatments = (Employee)guna2DataGridView2.SelectedRows[0].Tag;
                ShowEmployeeTreatments ShowEmployeeTreatments = new ShowEmployeeTreatments(EmpyloeeTreatments);
                if (ShowEmployeeTreatments.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
        }
    }
}
