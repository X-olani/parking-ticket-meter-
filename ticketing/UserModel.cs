using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ticketing
{
    public class UserModel
    {
        // class for the user 
        public int id { get; set; }
        public string Username { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public string Fullname
        {
            get
            {
                return $"{Username} {email}";
            }
        }
    }
}
