using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;
using System.Data;
using Dapper;

namespace ticketing
{
    public class SqlUsers
    {
        //method to loafd users
        public static List<UserModel> LoadUser() {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                var output = cnn.Query<UserModel>("SELECT * FROM [userlogins]", new DynamicParameters());
                return output.ToList();
            }
        
        }
        // create a user
        public static void SaveUser(UserModel user)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO [userlogins] (Username ,Email, Password) VALUES (@Username, @Email,@Password)", user);
            }
        }

        // create a ticket 
        public static void  CreateTicket(TicketModel data)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO [tickets] (ticketdate, code ) VALUES (@ticketdate,@code)", data);
            }

        }
        //method to shwo all active tickets
        public static List<TicketModel> LoadTickets()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TicketModel>("SELECT * FROM [tickets]", new DynamicParameters());
                return output.ToList();

            }
        }
        // method to show  the active tickets 
        public static List<TicketModel> GetTicket(string tcode)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TicketModel>("SELECT ticketdate FROM [tickets] WHERE Code='"+tcode+"' ", new DynamicParameters());
                return output.ToList();
            }
        }

        //logs in the ticket master 
        public static int Login(string username,string pass) {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.ExecuteScalar("SELECT COUNT(*) FROM [userlogins] WHERE Username='"+username+"'AND Password='" + pass + "'", new DynamicParameters());

                return int.Parse( output.ToString());
            }


        }


        //checks if the ticket is in the db

        public static int CkeckIfCodeExist (string codes)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.ExecuteScalar("SELECT COUNT(*) FROM [tickets] WHERE Code= '"+codes+"' ", new DynamicParameters());

                return  int.Parse(output.ToString());
            }
        }

        // method that removes the ticket 
        public static void PaymentComplete(string codes)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Execute("DELETE FROM [tickets]  WHERE Code='"+codes+"';", new DynamicParameters());

            
            }
        }










        private static string LoadConnectionString(string id = "Default") {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        
        }

    }
}
