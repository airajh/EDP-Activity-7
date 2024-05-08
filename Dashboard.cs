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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var Profile = new update_account();
            
            Profile.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var home = new Home();

            home.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var User = new Search();
            
            User.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var login = new Log_In();
            this.Hide();
            login.Show();
        }
    }
}
