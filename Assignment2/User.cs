using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string right { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }

        public void user() {
            username = "";
            password = "";
            right = "";
            firstName = "";
            lastName = "";
            dateOfBirth = "";
        }

        public void loadUser(string line) {
            string[] userData = line.Split(',');
            username = userData[0];
            password = userData[1];
            right = userData[2];
            firstName = userData[3];
            lastName = userData[4];
            dateOfBirth = userData[5];
        }
    }
}
