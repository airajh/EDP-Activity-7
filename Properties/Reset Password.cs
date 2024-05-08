using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace skincare.Properties
{
    public partial class Reset_Password : Form
    {
        private string username;
        public Reset_Password(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void label2_Click(object sender, EventArgs e)
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
                string password = this.textBox4.Text;

                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "UPDATE employee SET emppassword = @password WHERE empname = @name;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@name", username);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Password updated successfully!");
                var dashboard = new Dashboard();
                this.Hide();
                dashboard.Show();


            }
             catch (MySqlException ex)
             {
                 MessageBox.Show("Error: " + ex.Message);
             }
             finally
             {
                 //conn.Close();
             }
            


      }          
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
