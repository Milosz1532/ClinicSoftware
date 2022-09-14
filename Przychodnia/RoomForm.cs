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
    public partial class RoomForm : Form
    {
        Room room = null; 
        public RoomForm(Room room)
        {
            InitializeComponent();
            this.room = room;
            if (guna2NumericUpDown1 != null)
            {
                guna2NumericUpDown1.Value = room.Number;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; // IMPORTANT!
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2NumericUpDown1.Value <= 0)
            {
                MessageBox.Show("Numer gabinetu nie może być równy 0");
                return;
            }
            if (Schedule.ScheduleList.Any(item => ((item.Room.Number) == guna2NumericUpDown1.Value)))
            {
                MessageBox.Show("Istnieje już gabinet o takim numerze.");
                return;
            }
            room.Number = (int)guna2NumericUpDown1.Value;
            DialogResult = DialogResult.OK;
        }
    }
}
