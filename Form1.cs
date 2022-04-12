using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platform_game
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;

        // for the player
        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;

        // for the platforms
        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        // for the enemies
        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;

        public Form1()
        {
            InitializeComponent();
        }


        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

            player.Top += jumpSpeed;

            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player.Left += playerSpeed;
            }
            if (jumping == true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;

                            if ((string)x.Name == "horizontalPlatform" && goLeft == false || (string)x.Name == "horizontalPlatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;
                            }

                        }

                        x.BringToFront(); 
                    }

                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    if (((string)x.Tag == "enemy"))
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score: " + score + Environment.NewLine + "You were killed" + Environment.NewLine + "Game over!!!";
                        }
                    }
                }

            }

            // the platforms are moving

            horizontalPlatform.Left -= horizontalSpeed;

            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            verticalPlatform.Top += verticalSpeed;

            if (verticalPlatform.Top < 174 || verticalPlatform.Top > 300)
            {
                verticalSpeed = -verticalSpeed;
            }

            // the enemies are moving

            enemyOne.Left += enemyOneSpeed;

            if (enemyOne.Left < pictureBox6.Left || enemyOne.Left + enemyOne.Width > pictureBox6.Left + pictureBox6.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemyTwo.Left += enemyTwoSpeed;

            if (enemyTwo.Left < pictureBox3.Left || enemyTwo.Left+ enemyTwo.Width > pictureBox3.Left + pictureBox3.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            //Game over when player fell off

            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You fell off" + Environment.NewLine + "Game over!!!";
            }

            //

            if (player.Bounds.IntersectsWith(door.Bounds) && score == 30)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "Congratulations, You won!";
            }
            if (player.Bounds.IntersectsWith(door.Bounds) && score != 30)
            {
                txtScore.Text = "Score: " + score + Environment.NewLine + "Collect all the coins";
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;

            txtScore.Text = "Score: " + score;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            // reset the position of:

            //player
            player.Left = 50;
            player.Top = 660;

            //enemies
            enemyOne.Left = 360;
            enemyTwo.Left = 430;

            //platforms
            verticalPlatform.Top = 290;
            horizontalPlatform.Left = 420;

            gameTimer.Start();
        }
    }
}
