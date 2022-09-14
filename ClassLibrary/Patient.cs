using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class Patient
    {
        private string first_name, last_name;
        private long pesel;
        private int year_birth;
        private string address;
        private long phone_number;
        private string mail;
        
        static List<Patient> patient_list = new List<Patient>(); // List of Patients
        private List<AuthorizedPerson> authorized_person_list = new List<AuthorizedPerson>();

        public object[] ToObjTbl => new object[] { First_name, Last_name, Pesel, Year_birth, Address, Phone_number, Mail };

        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public long Pesel { get => pesel; set => pesel = value; }
        public int Year_birth { get => year_birth; set => year_birth = value; }
        public string Address { get => address; set => address = value; }
        public long Phone_number { get => phone_number; set => phone_number = value; }
        public string Mail { get => mail; set => mail = value; }
        public static List<Patient> Patient_list { get => patient_list; set => patient_list = value; }
        public List<AuthorizedPerson> Authorized_person_list { get => authorized_person_list; set => authorized_person_list = value; }
    }
}
