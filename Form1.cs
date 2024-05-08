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

namespace skincare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
        private void AddRecordToDatabase(string username, string password, string salary, string email)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;" +
            "pwd=airajhean;database=skincare";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();



                string sql = "INSERT INTO employee (empname, empsalary, emppassword, empemail) VALUES (@username, @password, @salary, @email)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@salary", salary);
                cmd.Parameters.AddWithValue("@email", email);
           


                cmd.ExecuteNonQuery();

                MessageBox.Show("New Record added successfully!");
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
             
            }
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            string username = usertxt.Text;
            string password = passtxt.Text;
            string salary = salarytxt.Text;
            string email = emailtxt.Text;
           




            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(salary) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill in all fields. or Upload a Picture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {

                // Call AddRecordToDatabase with imagePath as parameter
                AddRecordToDatabase(username, password, salary, email);
            }
            catch (Exception)
            {
                MessageBox.Show("Please Upload a Picture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cancel_Click(object sender, EventArgs e)
        {
          /*  AddAccount addAccount = new AddAccount();
            addAccount.Show();
            this.Hide();*/
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
    }

