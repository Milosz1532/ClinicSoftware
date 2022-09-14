using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class Employee
    {
        private string first_name,last_name;
        private  int start_year; // year of commencement of work
        private List<Treatment> treatments = new List<Treatment>(); // list of treatments that the employee can perform
        static List <Employee> employee_list = new List<Employee>();

        public object[] ToObjTbl => new object[] { First_name,Last_name,Start_year};

        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public int Start_year { get => start_year; set => start_year = value; }
        public List<Treatment> Treatments { get => treatments; set => treatments = value; }
        public static List<Employee> Employee_list { get => employee_list; set => employee_list = value; }
    }
}
