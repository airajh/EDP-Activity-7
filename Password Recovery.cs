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
    public partial class Password_Recovery : Form
    {
        public Password_Recovery()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=airajhean;database=skincare";


            try
            {
                string username = this.textBox1.Text;
                string email = this.textBox3.Text;

                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string query = "SELECT COUNT(*) FROM employee WHERE empname = @username AND empemail = @email";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);

                int count = Convert.ToInt32(cmd.ExecuteScalar());



                if (count > 0)
                {
                    //MessageBox.Show("You are now logged in");
                    Reset_Password reset_Password = new Reset_Password(username);
                    reset_Password.Show();
                    this.Hide();


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

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
