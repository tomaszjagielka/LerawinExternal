using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Lerawin.Forms
{
    public partial class Loader : Form
    {
        public Loader()
        {
            InitializeComponent();
        }

        bool loggedIn = false;
        private bool DBConnection()
        {
            MySqlConnection con = new MySqlConnection("server=remotemysql.com; uid=uid; pwd=pwd; database=database");
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT count(*) FROM credentials WHERE username = '" + txtUsername.Text + "' AND password = '" + txtPassword.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows[0][0].ToString() == "1")
                {
                    //Console.WriteLine("All good!");
                    con.Close();
                    return true;
                }
                else
                {
                    txtWarning.ForeColor = Color.Red;
                    txtWarning.Text = "Invalid username or password!";
                    txtWarning.Visible = true;
                    con.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                txtWarning.ForeColor = Color.Red;
                txtWarning.Text = "An unexpected error occured.";
                txtWarning.Visible = true;
                return false;
            }
        }

        private void Connect()
        {
            if (DBConnection())
            {
                loggedIn = true;
                txtWarning.ForeColor = Color.LawnGreen;
                if (Main.RunStartup())
                {
                    txtWarning.Text = "Welcome back, " + txtUsername.Text + "! Starting program...";
                    Menu menu = new Menu();
                    this.Hide();
                    menu.Show();
                }
                else
                {
                    btnLogin.Enabled = false;
                    txtWarning.Text = "Welcome back, " + txtUsername.Text + "! Waiting for CS:GO...";
                    txtWarning.Visible = true;

                    new Thread(() =>
                    {
                        while (true)
                        {
                            if (Main.RunStartup())
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    Menu menu = new Menu();
                                    this.Hide();
                                    menu.Show();
                                });
                                break;
                            }
                            Thread.Sleep(200);
                        }
                    }).Start();

                    //if (allow)
                    //{
                    //    Menu menu = new Menu();
                    //    this.Hide();
                    //    menu.Show();
                    //}
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Username")
            {
                txtUsername.Clear();
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Clear();
            }
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Username")
            {
                txtUsername.Clear();
            }
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Clear();
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && loggedIn == false)
            {
                Connect();

                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && loggedIn == false)
            {
                Connect();

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnSuperSecret_Click(object sender, EventArgs e)
        {
            if (Main.RunStartup())
            {
                Menu menu = new Menu();
                this.Hide();
                menu.Show();
            }
        }

        // TEST ANIMATED '...' CODE:
        //new Thread(() =>
        //            {
        //    while (true)
        //    {
        //        for (int i = 0; i <= 4; i++)
        //        {
        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                switch (i)
        //                {
        //                    case 1:
        //                        if (txtWarning.Text == "Welcome back, " + txtUsername.Text + "! Waiting for CS:GO")
        //                        {
        //                            goto skip;
        //                        }
        //                        else
        //                        {
        //                            txtWarning.Text = "Welcome back, " + txtUsername.Text + "! Waiting for CS:GO";
        //                        }
        //                        break;
        //                    case 2:
        //                        skip:
        //                        txtWarning.Text = "Welcome back, " + txtUsername.Text + "! Waiting for CS:GO.";
        //                        break;
        //                    case 3:
        //                        txtWarning.Text = "Welcome back, " + txtUsername.Text + "! Waiting for CS:GO..";
        //                        break;
        //                    case 4:
        //                        txtWarning.Text = "Welcome back, " + txtUsername.Text + "! Waiting for CS:GO...";
        //                        i = 0;
        //                        break;
        //                }
        //            });

        //            Thread.Sleep(200);
        //        }

        //        Console.WriteLine("After switch.");


        //        if (Main.RunStartup())
        //        {
        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                Menu menu = new Menu();
        //                this.Hide();
        //                menu.Show();
        //            });
        //            break;
        //        }
        //    }
        //}).Start();
    }
}
