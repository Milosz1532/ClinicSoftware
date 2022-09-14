using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class AuthorizedPerson
    {
        private string first_name, last_name;
        private long pesel;
        private string address;
        private long phone_number;
        private bool health_information, documentation_information;

        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public long Pesel { get => pesel; set => pesel = value; }
        public string Address { get => address; set => address = value; }
        public long Phone_number { get => phone_number; set => phone_number = value; }
        public bool Health_information { get => health_information; set => health_information = value; }
        public bool Documentation_information { get => documentation_information; set => documentation_information = value; }
    }
}
