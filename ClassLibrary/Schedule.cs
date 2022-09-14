using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class Schedule
    {
        private DateTime date;
        private int startJob;
        private int endJob;
        private Employee employee;
        private Room room;
        private static List<Schedule> scheduleList = new List<Schedule>();
        public object[] ToObjTbl => new object[] { Date.ToString("dd.MM.yyyy"), StartJob+":00", EndJob+":00", Room.Number };

        public DateTime Date { get => date; set => date = value; }
        public int StartJob { get => startJob; set => startJob = value; }
        public int EndJob { get => endJob; set => endJob = value; }
        public Employee Employee { get => employee; set => employee = value; }
        public Room Room { get => room; set => room = value; }
        public static List<Schedule> ScheduleList { get => scheduleList; set => scheduleList = value; }
    }
}
