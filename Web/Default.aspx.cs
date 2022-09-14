using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLibrary;
using System.IO;

namespace Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FileStream fileStream = new FileStream(@"C:\Users\Milosz\Desktop\dane.medic", FileMode.Open, FileAccess.Read);
            var binaryFormatter = new BinaryFormatter();
            Patient.Patient_list = (List<Patient>)binaryFormatter.Deserialize(fileStream);
            Treatment.Treatment_list = (List<Treatment>)binaryFormatter.Deserialize(fileStream);
            Room.Room_list = (List<Room>)binaryFormatter.Deserialize(fileStream);
            Employee.Employee_list = (List<Employee>)binaryFormatter.Deserialize(fileStream);
            Schedule.ScheduleList = (List<Schedule>)binaryFormatter.Deserialize(fileStream);
            Registration.RegistrationList = (List<Registration>)binaryFormatter.Deserialize(fileStream);

            fileStream.Close();

            Label label1 = new Label();
            label1.Text = "Pracownicy przychodni:<br>";
            Panel1.Controls.Add(label1);

            foreach (Employee emp in Employee.Employee_list)

            {
                //ListBox1.Items.Add(s.First_name + " " + s.Last_name);
                Label label = new Label();
                string treatmentS = "";
                foreach (Treatment t in emp.Treatments)
                {
                    treatmentS += "<br>" + t.Name;
                }
                label.Text = "<br>" + emp.First_name + " " + emp.Last_name+": "+treatmentS;
                Panel1.Controls.Add(label);
            }
        }
    }
}