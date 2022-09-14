using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class Treatment
    {
        private string name;
        static List<Treatment> treatment_list = new List<Treatment>(); // List of medical treatments
        public object[] ToObjTbl => new object[] { name };
        public string Name { get => name; set => name = value; }
        public static List<Treatment> Treatment_list { get => treatment_list; set => treatment_list = value; }
    }
}
