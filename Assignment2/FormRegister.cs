using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class FormRegister : Form
    {
        public const string seperateCharacter = ",";
        public static FormRegister current;
        public FormRegister()
        {
            InitializeComponent();
            current = this;
        }

        public enum RegisterError
        {
            None,
            ExistUser,
            InvalidPassword,
            PasswordMissmatched,
            InvalidName,
            InvalidInfo
        }

        public class RegisterException
        {
            public RegisterError registerError;
            public string message;
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            RegisterException registerException = new RegisterException();
            registerException = checkRegisterInfo();
            if (registerException.registerError != RegisterError.None)
            {
                MessageBox.Show(registerException.message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            createNewUser();

        }

        public void createNewUser() {
            string fileName = "login.txt";
            
            User user = new User();
            user.username = txtUserName.Text.Trim();
            user.password = txtPassword.Text.Trim();
            user.firstName = txtFirstName.Text.Trim();
            user.lastName = txtLastName.Text.Trim();
            user.right = cbbUserType.SelectedItem.ToString();
            user.dateOfBirth = dobPicker.Value.ToString("dd-MM-yyyy");

            string line = "\n" + user.username.Trim() + seperateCharacter + user.password + seperateCharacter + user.firstName + seperateCharacter + user.lastName + seperateCharacter + user.right + seperateCharacter + user.dateOfBirth;
            
            File.AppendAllText(fileName, line);
            MessageBox.Show("User has been created successfully!", "Successful", MessageBoxButtons.OK);
        }

        public RegisterException checkRegisterInfo()
        {
            RegisterException registerException = new RegisterException();
            registerException.registerError = RegisterError.None;
            ListUser listUser = new ListUser();
            User user = new User();
            listUser.LoadUser("login.txt");
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            //Check exist Username
            if (listUser.listUser.ContainsKey(username))
            {
                registerException.registerError = RegisterError.ExistUser;
                registerException.message = "User is existed! Please try another username!";
                txtUserName.Focus();
                return registerException;
            }
            //Check exists Password
            if (password == "") {
                registerException.registerError = RegisterError.InvalidPassword;
                registerException.message = "The password is not valid, please try again!";
                return registerException;
            }
            //Check Password missmatched
            if (password != txtRepassword.Text)
            {
                registerException.registerError = RegisterError.PasswordMissmatched;
                registerException.message = "The confirm password is not correct, please try again!";
                return registerException;
            }
            //Check Invalid Info
            if (txtFirstName.Text == "" || txtLastName.Text == "")
            {
                txtFirstName.Focus();
                registerException.registerError = RegisterError.InvalidInfo;
                registerException.message = "Please input your detailed info!";
                return registerException;
            }

            return registerException;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin.current.ShowDialog();
        }
      
    }
}
