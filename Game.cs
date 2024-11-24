using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


namespace Game1
{
    public partial class Game : Form
    {
        string Username;
        int sumW;
        int sumL;

        // enemy
        private int[,] board;
        private int[] ships = { 5, 4, 3, 2 };

        // player
        private int[,] boardE;
        private int[] shipsE = { 5, 4, 3, 2 };
        private Button[,] buttonsE;
        
        public Game(string Username, int sumW, int sumL)
        {
            InitializeComponent();
            this.Username = Username;
            this.sumW = sumW;
            this.sumL = sumL;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Initialize the board and ships of Enemy
            board = new int[10, 10];
            PlaceShips();

            // Initialize the board and ships of Player
            boardE = new int[10, 10];
            for ( int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    boardE[i, j] = 0;
                }
            }
            buttonsE = new Button[10, 10] {
                {button00E, button01E, button02E, button03E, button04E, button05E, button06E, button07E, button08E, button09E},
                {button10E, button11E, button12E, button13E, button14E, button15E, button16E, button17E, button18E, button19E},
                {button20E, button21E, button22E, button23E, button24E, button25E, button26E, button27E, button28E, button29E},
                {button30E, button31E, button32E, button33E, button34E, button35E, button36E, button37E, button38E, button39E},
                {button40E, button41E, button42E, button43E, button44E, button45E, button46E, button47E, button48E, button49E},
                {button50E, button51E, button52E, button53E, button54E, button55E, button56E, button57E, button58E, button59E},
                {button60E, button61E, button62E, button63E, button64E, button65E, button66E, button67E, button68E, button69E},
                {button70E, button71E, button72E, button73E, button74E, button75E, button76E, button77E, button78E, button79E},
                {button80E, button81E, button82E, button83E, button84E, button85E, button86E, button87E, button88E, button89E},
                {button90E, button91E, button92E, button93E, button94E, button95E, button96E, button97E, button98E, button99E} };

            PlaceShipsE();
            timer1.Enabled = true;
        }

        //
        // enemy board
        //

        private void PlaceShips()
        {
            Random rand = new Random();
            foreach (int shipLength in ships)
            {
                bool placed = false;
                while (!placed)
                {
                    // Choose a random starting point and direction for the ship
                    int row = rand.Next(0, 10);
                    int col = rand.Next(0, 10);
                    int direction = rand.Next(0, 2);

                    // Check if the ship fits on the board in the chosen direction
                    if (direction == 0 && row + shipLength <= 10)
                    {
                        placed = true;
                        
                        for (int i = 0; i < shipLength; i++)
                        {
                            // main check
                            if (board[row + i, col] != 0)
                            {
                                placed = false;
                                break;
                            }
                            // first block
                            if (i == 0)
                            {
                                if (col > 0 && board[row, col - 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (row > 0 && board[row - 1, col] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (col < 9 && board[row, col + 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                            }
                            // last block
                            else if (i == shipLength - 1)
                            {
                                if (row + i < 9 && board[row + i + 1, col] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (col > 0 && board[row + i, col - 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (col < 9 && board[row + i, col + 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                            }
                            // middle block
                            else
                            {
                                if (col > 0 && board[row + i, col - 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (col < 9 && board[row + i, col + 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                            }
                        }

                        // placement
                        if (placed)
                        {
                            for (int i = 0; i < shipLength; i++)
                            {
                                board[row + i, col] = shipLength;
                            }
                        }
                        
                    }
                    else if (direction == 1 && col + shipLength <= 10)
                    {
                        placed = true;

                        // main check
                        for (int i = 0; i < shipLength; i++)
                        {
                            if (board[row, col + i] != 0)
                            {
                                placed = false;
                                break;
                            }
                            if (i == 0)
                            {
                                if (row > 0 && board[row - 1, col] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (col > 0 && board[row, col - 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (row < 9 && board[row + 1, col] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                            }
                            else if (i == shipLength - 1)
                            {
                                if (col + i < 9 && board[row, col + i + 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (row > 0 && board[row - 1, col + i] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (row < 9 && board[row + 1, col + i] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (row > 0 && board[row - 1, col + i] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (row < 9 && board[row + 1, col + i] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                            }
                        }

                        // placement
                        if (placed)
                        {
                            for (int i = 0; i < shipLength; i++)
                            {
                                board[row, col + i] = shipLength;
                            }
                        }
                    }
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            // Get the button that was clicked
            Button button = (Button)sender;

            // Get the row and column of the button
            int row = int.Parse(button.Name.Substring(6,1));
            int col = int.Parse(button.Name.Substring(7,1));

            // Check if button is a hit or miss
            CheckHit(row, col);

            // change the button color
            if (board[row, col] == 6 || board[row, col] == 9 || board[row, col] == 12 || board[row, col] == 15)
            {
                button.BackColor = System.Drawing.Color.MediumPurple;
                button.Enabled = false;
            }
            else
            {
                button.BackColor = System.Drawing.Color.Black;
                button.Enabled = false;
            }
            
            // ship shunk
            CheckShip(row, col);

            // Check if all ships are sunk
            if (CheckWin())
            {
                timer1.Stop();
                string win = "You win!";
                MessageBox.Show("Βύθισες όλα του τα πλοία! Νίκησες!");
                sumW++;
                new Form1(tries, time, win, Username, sumW, sumL).Show();
                this.Hide();
            }
            else
            {
                // enemy turn
                Enemy_Hit(boardE);
            }
        }

        private void CheckShip(int row, int col)
        {
            if (board[row, col] == 6 || board[row, col] == 9 || board[row, col] == 12 || board[row, col] == 15)
            {
                if (board[row, col] == 6)
                {
                    int counter = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (board[i, j] == 6)
                            {
                                counter++;
                            }
                        }
                    }
                    if (counter == 2)
                    {
                        richTextBox1.AppendText("Βύθισες το υποβρύχιο του");
                        richTextBox1.AppendText(Environment.NewLine);
                        MessageBox.Show("Βύθισες το υποβρύχιο του");
                    }
                }
                if (board[row, col] == 9)
                {
                    int counter = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (board[i, j] == 9)
                            {
                                counter++;
                            }
                        }
                    }
                    if (counter == 3)
                    {
                        richTextBox1.AppendText("Βύθισες το πολεμικό του πλοίο");
                        richTextBox1.AppendText(Environment.NewLine);
                        MessageBox.Show("Βύθισες το πολεμικό του πλοίο");
                    }
                }
                if (board[row, col] == 12)
                {
                    int counter = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (board[i, j] == 12)
                            {
                                counter++;
                            }
                        }
                    }
                    if (counter == 4)
                    {
                        richTextBox1.AppendText("Βύθισες το αντιτορπιλικό πλοίο του");
                        richTextBox1.AppendText(Environment.NewLine);
                        MessageBox.Show("Βύθισες το αντιτορπιλικό πλοίο του");
                    }
                }
                if (board[row, col] == 15)
                {
                    int counter = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (board[i, j] == 15)
                            {
                                counter++;
                            }
                        }
                    }
                    if (counter == 5)
                    {
                        richTextBox1.AppendText("Βύθισες το αεροπλανοφόρο του");
                        richTextBox1.AppendText(Environment.NewLine);
                        MessageBox.Show("Βύθισες το αεροπλανοφόρο του");
                    }
                }
            }
        }

        private void CheckHit(int row, int col)
        {
            if (board[row, col] == 2)
            {
                board[row, col] = 6;
            }
            else if (board[row, col] == 3)
            {
                board[row, col] = 9;
            }
            else if (board[row, col] == 4)
            {
                board[row, col] = 12;
            }
            else if (board[row, col] == 5)
            {
                board[row, col] = 15;
            }
        }

        private bool CheckWin()
        {
            int counter = 0;

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (board[row, col] == 6 || board[row, col] == 9 || board[row, col] == 12 || board[row, col] == 15)
                    {
                        counter++;
                    }
                }
            }

            if (counter == 14)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //
        // player board
        //

        private void PlaceShipsE()
        {
            Random randE = new Random();
            foreach (int shipLengthE in shipsE)
            {
                bool placed = false;
                while (!placed)
                {
                    
                    // Choose a random starting point and direction for the ship
                    int rowE = randE.Next(0, 10);
                    int colE = randE.Next(0, 10);
                    int direction = randE.Next(0, 2);

                    // Check if the ship fits on the board in the chosen direction
                    if (direction == 1 && rowE + shipLengthE <= 10)
                    {
                        placed = true;

                        // main check
                        for (int i = 0; i < shipLengthE; i++)
                        {
                            if (boardE[rowE + i, colE] != 0)
                            {
                                placed = false;
                                break;
                            }
                            if (i == 0)
                            {
                                if (colE > 0 && boardE[rowE, colE - 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (rowE > 0 && boardE[rowE - 1, colE] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (colE < 9 && boardE[rowE, colE + 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                            }
                            else if (i == shipLengthE - 1)
                            {
                                if (rowE + i < 9 && boardE[rowE + i + 1, colE] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (colE > 0 && boardE[rowE + i, colE - 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (colE < 9 && boardE[rowE + i, colE + 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (colE > 0 && boardE[rowE + i, colE - 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (colE < 9 && boardE[rowE + i, colE + 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                            }
                        }

                        // placement
                        if (placed)
                        {
                            for (int i = 0; i < shipLengthE; i++)
                            {
                                boardE[rowE + i, colE] = shipLengthE;
                                buttonsE[rowE + i, colE].BackColor = System.Drawing.Color.DarkGreen;
                            }
                        }
                    }
                    else if (direction == 0 && colE + shipLengthE <= 10)
                    {
                        placed = true;

                        // main check
                        for (int i = 0; i < shipLengthE; i++)
                        {
                            if (boardE[rowE, colE + i] != 0)
                            {
                                placed = false;
                                break;
                            }
                            if (i == 0)
                            {
                                if (rowE > 0 && boardE[rowE - 1, colE] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (colE > 0 && boardE[rowE, colE - 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (rowE < 9 && boardE[rowE + 1, colE] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                            }
                            else if (i == shipLengthE - 1)
                            {
                                if (colE + i < 9 && boardE[rowE, colE + i + 1] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (rowE > 0 && boardE[rowE - 1, colE + i] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (rowE < 9 && boardE[rowE + 1, colE + i] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (rowE > 0 && boardE[rowE - 1, colE + i] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                                if (rowE < 9 && boardE[rowE + 1, colE + i] != 0)
                                {
                                    placed = false;
                                    break;
                                }
                            }
                        }

                        // placement
                        if (placed)
                        {
                            for (int i = 0; i < shipLengthE; i++)
                            {
                                boardE[rowE, colE + i] = shipLengthE;
                                buttonsE[rowE, colE + i].BackColor = System.Drawing.Color.DarkGreen;
                            }
                        }
                    }
                }
            }
        }

        private void Enemy_Hit(int[,] boardE)
        {
            Random rand = new Random();
            Button[] buttonsH = {
                button00E, button01E, button02E, button03E, button04E, button05E, button06E, button07E, button08E, button09E,
                button10E, button11E, button12E, button13E, button14E, button15E, button16E, button17E, button18E, button19E,
                button20E, button21E, button22E, button23E, button24E, button25E, button26E, button27E, button28E, button29E,
                button30E, button31E, button32E, button33E, button34E, button35E, button36E, button37E, button38E, button39E,
                button40E, button41E, button42E, button43E, button44E, button45E, button46E, button47E, button48E, button49E,
                button50E, button51E, button52E, button53E, button54E, button55E, button56E, button57E, button58E, button59E,
                button60E, button61E, button62E, button63E, button64E, button65E, button66E, button67E, button68E, button69E,
                button70E, button71E, button72E, button73E, button74E, button75E, button76E, button77E, button78E, button79E,
                button80E, button81E, button82E, button83E, button84E, button85E, button86E, button87E, button88E, button89E,
                button90E, button91E, button92E, button93E, button94E, button95E, button96E, button97E, button98E, button99E };

            // get random button
            int rowE;
            int colE;
            bool placed = false;
           
            while (!placed)
            {
                int randomIndex = rand.Next(0, buttonsH.Length);
                Button selectedButton = buttonsH[randomIndex];
                rowE = int.Parse(selectedButton.Name.Substring(6, 1));
                colE = int.Parse(selectedButton.Name.Substring(7, 1));

                // check if it can be placed
                if (boardE[rowE, colE] == 0 || boardE[rowE, colE] == 2 || boardE[rowE, colE] == 3 || boardE[rowE, colE] == 4 || boardE[rowE, colE] == 5)
                {
                    placed = true;
                    
                    CheckHitE(rowE, colE);
                    selectButton(selectedButton, rowE, colE);
                }
                else
                {
                    placed = false;
                }
            }
        }

        int tries = 0;
        private void selectButton(Button selectedButton, int rowE, int colE)
        {
            tries++;
            label41.Text = tries.ToString();

            // change button color
            if (boardE[rowE, colE] == 6 || boardE[rowE, colE] == 9 || boardE[rowE, colE] == 12 || boardE[rowE, colE] == 15)
            {
                selectedButton.BackColor = System.Drawing.Color.Red;
                selectedButton.Enabled = false;
                
                // ship shunk
                CheckShipE(rowE, colE);

                // Check if all ships are sunk
                if (CheckWinE())
                {
                    timer1.Stop();
                    string win = "You lost!";
                    MessageBox.Show("Βυθίστηκαν όλα τα πλοία σου! Έχασες!");
                    sumL++;
                    new Form1(tries, time, win, Username, sumW, sumL).Show();
                    this.Hide();
                }
            }
            else if (boardE[rowE, colE] == 1)
            {
                selectedButton.BackColor = System.Drawing.Color.Black;
                selectedButton.Enabled = false;
            }
        }

        private void CheckShipE(int rowE, int colE)
        {
            if (boardE[rowE, colE] == 6)
            {
                int counter = 0;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (boardE[i, j] == 6)
                        {
                            counter++;
                        }
                    }
                }
                if (counter == 2)
                {
                    richTextBox2.AppendText("Βυθίστηκε το υποβρύχιο σου");
                    richTextBox2.AppendText(Environment.NewLine);
                    MessageBox.Show("Βυθίστηκε το υποβρύχιο σου");
                }
            }
            if (boardE[rowE, colE] == 9)
            {
                int counter = 0;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (boardE[i, j] == 9)
                        {
                            counter++;
                        }
                    }
                }
                if (counter == 3)
                {
                    richTextBox2.AppendText("Βυθίστηκε το πολεμικό σου πλοίο");
                    richTextBox2.AppendText(Environment.NewLine);                    
                    MessageBox.Show("Βυθίστηκε το πολεμικό σου πλοίο");
                }
            }
            if (boardE[rowE, colE] == 12)
            {
                int counter = 0;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (boardE[i, j] == 12)
                        {
                            counter++;
                        }
                    }
                }
                if (counter == 4)
                {
                    richTextBox2.AppendText("Βυθίστηκε το αντιτορπιλικό πλοίο σου");
                    richTextBox2.AppendText(Environment.NewLine);
                    MessageBox.Show("Βυθίστηκε το αντιτορπιλικό πλοίο σου");
                }
            }
            if (boardE[rowE, colE] == 15)
            {
                int counter = 0;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (boardE[i, j] == 15)
                        {
                            counter++;
                        }
                    }
                }
                if (counter == 5)
                {
                    richTextBox2.AppendText("Βυθίστηκε το αεροπλανοφόρο σου");
                    richTextBox2.AppendText(Environment.NewLine);
                    MessageBox.Show("Βυθίστηκε το αεροπλανοφόρο σου");
                }
            }
        }

        private void CheckHitE(int rowE, int colE)
        {
            if (boardE[rowE, colE] == 2)
            {
                boardE[rowE, colE] = 6;
            }
            else if (boardE[rowE, colE] == 3)
            {
                boardE[rowE, colE] = 9;
            }
            else if (boardE[rowE, colE] == 4)
            {
                boardE[rowE, colE] = 12;
            }
            else if (boardE[rowE, colE] == 5)
            {
                boardE[rowE, colE] = 15;
            }
            else if (boardE[rowE, colE] == 0)
            {
                boardE[rowE, colE] = 1;
            }
        }

        private bool CheckWinE()
        {
            int counter = 0;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (boardE[i, j] == 6 || boardE[i, j] == 9 || boardE[i, j] == 12 || boardE[i, j] == 15)
                    {
                        counter++;
                    }
                }
            }

            if (counter == 14)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            label42.Text = time.ToString();
        }
    }
}
