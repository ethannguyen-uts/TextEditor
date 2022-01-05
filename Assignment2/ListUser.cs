using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class ListUser
    {
        public Dictionary<string, User> listUser = new Dictionary<string, User>();
        public void LoadUser(string filename)
        {
            // Read the file content using the StreamReader
            StreamReader fileContent = new StreamReader(filename);
            // Create an object of Employee class
            User userTemp;

            // Read the StremReader till the last line
            while (!fileContent.EndOfStream)
            {
                // there are still employees to be loaded
                userTemp = new User();
                // Read each line and from the StreamReader
                string line = fileContent.ReadLine();
                // Load the employee detail from file to respective fields
                userTemp.loadUser(line);
                // Add the detall to the list collection
                listUser.Add(userTemp.username, userTemp);
            }
            // Close the StreamReader
            fileContent.Close();
        }

        public void showUsers()
        {
            foreach (User a in listUser.Values)
            {
                Console.WriteLine(a.username);
            }
        }
    }
}
