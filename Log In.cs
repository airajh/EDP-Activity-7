using MySql.Data.MySqlClient;
using skincare.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace skincare
{
    public partial class Log_In : Form
    {
        public Log_In()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = this.username.Text;
            string password = this.passwordtext.Text;

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=airajhean;database=skincare";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string sql = "SELECT COUNT(*) from employee where empname = @username AND emppassword = @password";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());


                if (count > 0)
                {
                    //MessageBox.Show("You are now logged in");
                    var dashboard = new Dashboard();
                    this.Hide();
                    dashboard.Show();

                }
                else
                {
                    MessageBox.Show("Invalid username/password");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var passrecovery = new Password_Recovery();
            this.Hide();
            passrecovery.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var create = new Form1();
            this.Hide();
            create.Show();
        }
    }
    
}
