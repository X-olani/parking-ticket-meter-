using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ticketing
{
    public partial class loginF : Form
    {
        List<TicketModel> ticket = new List<TicketModel>();
        public loginF()
        {
            InitializeComponent();
           
        }

        private void btnticketgen_Click(object sender, EventArgs e)
        {
            var ticket = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");

            TicketModel t = new TicketModel();
            t.ticketdate = DateTime.Parse(ticket);
            SqlUsers.CreateTicket(t);

        }

        private void button1_Click(object sender, EventArgs e)
        {
        

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private string hashPass(string pass)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                //Hash data
                byte[] data = md5.ComputeHash(utf8.GetBytes(pass));
                return Convert.ToBase64String(data);
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password =hashPass(txtPassword.Text);
         

            int pass = SqlUsers.Login(username, password);
            MessageBox.Show(pass.ToString());
            if (pass == 1)
            {
                mainF  main = new mainF();
                main.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Password or Username incorrect","Error", MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }
    }
}