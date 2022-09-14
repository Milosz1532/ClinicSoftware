using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class Room
    {
        private int number;
        static List<Room> room_list = new List<Room>(); // list of Rooms
        public object[] ToObjTbl => new object[] { "Gabinet "+Number };
        public int Number { get => number; set => number = value; }
        public static List<Room> Room_list { get => room_list; set => room_list = value; }
    }
}
