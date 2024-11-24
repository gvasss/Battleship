using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Reflection;

namespace Game1
{
    public partial class Form1 : Form
    {
        //database sqlite
        String connectionString = "Data source=csharp2022_1.db;Version=3";
        SQLiteConnection connection;

        string Username;
        int sumW, sumL;

        public Form1(int tries, int time, string winner, string Username, int sumW, int sumL)
        {
            InitializeComponent();

            this.Username = Username;
            this.sumW = sumW;
            this.sumL = sumL;

            label1.Text = tries.ToString();
            label2.Text = time.ToString();
            label3.Text = winner;
            label7.Text = Username;
            label11.Text = sumW.ToString();
            label10.Text = sumL.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //create if dont exist
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            String createSQL = "Create table if not exists Game(Req_ID integer primary key autoincrement," +
                "username Text, winner Text, time integer)";
            SQLiteCommand command = new SQLiteCommand(createSQL, connection);
            command.ExecuteNonQuery();
            connection.Close();

            // add data
            connection.Open();
            String insertSQL = "Insert into Game(username, winner, time) values(@username, @winner, @time)";
            SQLiteCommand command2 = new SQLiteCommand(insertSQL, connection);
            command2.Parameters.AddWithValue("username", label7.Text);
            command2.Parameters.AddWithValue("winner", label3.Text);
            command2.Parameters.AddWithValue("time", Convert.ToInt16(label2.Text));
            command2.ExecuteNonQuery();
            connection.Close();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            new Game(Username, sumW, sumL).Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
