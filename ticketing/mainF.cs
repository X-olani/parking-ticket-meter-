using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ticketing
{
    public partial class mainF : Form
    {
        List<UserModel> users = new List<UserModel>();
        List<TicketModel> ticket = new List<TicketModel>();
        public mainF()
        {
            InitializeComponent();
    
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string ticketCode =ticketcodeBtn.Text.Trim();


          
            
            ticket = SqlUsers.GetTicket(ticketCode);

         

                if (ticket.Count()>0)
            {
DateTime ticketDate = DateTime.Parse(ticket[0].ticketdate.ToString("yyyy-MM-dd hh:mm:ss tt"));

            var dateNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");

            TimeSpan elapsed = DateTime.Parse(dateNow).Subtract(ticketDate);

            rtbDisply.Text = ("\n Time in: " + ticket[0].ticketdate + "\n Time out:" + dateNow + "\n Time spent:" + elapsed.ToString());


            GetPrice(elapsed);
                btnComplete.Enabled = true;

            }
            else
            {

                MessageBox.Show("Ticket not found");
            }
            





            /* users = SqlUsers.LoadUser();
             listBox1.DataSource = null;
             listBox1.DisplayMember ="Fullname";



             listBox1.DataSource = users;

             mainForm form = new mainForm();
                 form.Show();*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // create ticket button

            var ticket = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");

            string code = RandomString(6);

            int checkCode = SqlUsers.CkeckIfCodeExist(code);

            TicketModel t = new TicketModel();

     
            if (checkCode ==0) {

               // does not exist
     
                t.ticketdate = DateTime.Parse(ticket);
                t.code = code;
                SqlUsers.CreateTicket(t);

                listBox1.Items.Clear();
                listBox1.Items.Add(ticket);
            }
            else
            {    
                while (checkCode==1)
            {
                    code = RandomString(6);
                  

                    checkCode = SqlUsers.CkeckIfCodeExist(code);

            }

                t.ticketdate = DateTime.Parse(ticket);
                t.code = code;
                SqlUsers.CreateTicket(t);

                listBox1.Items.Clear();
                listBox1.Items.Add(ticket);

              
            }
            LoadData();

          
            /*   TicketModel t = new TicketModel();
            t.ticketdate = DateTime.Parse(ticket);
            SqlUsers.CreateTicket(t);
            listBox1.Items.Add(ticket);
             * save user to db
                        UserModel u = new UserModel();
                        u.Username = usernsmetxt.Text;
                        u.email = emailtxt.Text;
                        u.password = "";
                        SqlUsers.SaveUser(u);
                        emailtxt.Text = "";
                        usernsmetxt.Text = "";   
*/

        }
        public void GetPrice(TimeSpan time)


        {
          


           
            int price = 0;
            if (time <new TimeSpan(02,00,00))
            {
                price = 8;
            }
            else if (new TimeSpan(02, 00, 00) > time && time < new TimeSpan(05, 00, 00))
            {
                price = 10;
            }
            else if (new TimeSpan(05, 00, 00) > time && time < new TimeSpan(06, 00, 00))
            {
                price = 15;
            }
            else if (new TimeSpan(06, 00, 00) > time && time < new TimeSpan(07, 00, 00))
            {
                price = 20;
            }
            else if (new TimeSpan(07, 00, 00) > time && time< new TimeSpan(08, 00, 00))
            {
                price = 25;
            }
            else if ( time > new TimeSpan(09, 00, 00))
            {
                price = 100;
            }

            rtbDisply.AppendText("=====================\nR" +
                price + "");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        public void LoadData() 
        {
            ticket = SqlUsers.LoadTickets();
            gdvtable.DataSource = null;
            gdvtable.DataSource = ticket;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string deleteTicket = ticketcodeBtn.Text;
            if (deleteTicket.Trim()=="")
            {
                MessageBox.Show("Please enter ticket code");
            }
            else
            {
 SqlUsers.PaymentComplete(deleteTicket);
            LoadData();
            }
           
        }
    }
}
