using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Game1
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        SumW sumW = new SumW(0);
        SumL sumL = new SumL(0);

        private void strButton_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text != "")
            {
                new Game(textBoxUsername.Text, Convert.ToInt32(sumW.number), Convert.ToInt32(sumL.number)).Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Λάθος στοιχεία");
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
