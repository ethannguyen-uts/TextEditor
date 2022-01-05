using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class FormLogin : Form
    {
        public static FormLogin current;
        
        public FormLogin()
        {
            InitializeComponent();
            current = this;
        }
        public User currentUser;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public bool validateUser() {
            ListUser listUser = new ListUser();
            User user = new User();
            listUser.LoadUser("login.txt");
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            
            if (listUser.listUser.ContainsKey(username))
            {
                if (listUser.listUser.TryGetValue(username, out user))
                {
                    if (user.password == password)
                    {
                        this.currentUser = user;
                        return true;
                    }
                }
            }
            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (validateUser())
            {
                this.Hide();
                FormEditor formEditor = new FormEditor();
                formEditor.ShowDialog();
            } else
            {
                MessageBox.Show("The username or password are not correct", "Invalid Login", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormRegister formRegister = new FormRegister();
            formRegister.ShowDialog();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
