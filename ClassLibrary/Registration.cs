using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class Registration
    {
        private Patient patient;
        private Employee employee;
        private Treatment treatment;
        private Schedule schedule;
        private string hour;

        static List<Registration> registrationList = new List<Registration>();

        public object[] ToObjTbl => new object[] { Schedule.Date.ToString("dd.MM.yyyy"), Hour, Patient.First_name + " " + Patient.Last_name, Employee.First_name + " " + Employee.Last_name, Treatment.Name, "Gabinet " + Schedule.Room.Number };

        public Patient Patient { get => patient; set => patient = value; }
        public Employee Employee { get => employee; set => employee = value; }
        public Schedule Schedule { get => schedule; set => schedule = value; }
        public string Hour { get => hour; set => hour = value; }
        public static List<Registration> RegistrationList { get => registrationList; set => registrationList = value; }
        public Treatment Treatment { get => treatment; set => treatment = value; }
    }
}
