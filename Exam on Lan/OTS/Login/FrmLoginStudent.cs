using OTS.DAO;
using OTS.Dashboard;
using OTS.Models;
using OTS.StudenDashBoard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTS.Login
{
    public partial class FrmLoginStudent : Form
    {
        public FrmLoginStudent()
        {
            InitializeComponent();
        }
        private bool ValidateLogin()
        {
            string regex = "^[A-Za-z0-9]+$";
            string mess = "";
            if(txtUsername.Text.Equals(""))
            {
                mess = "Username cannot empty";
            } else if (txtPassword.Text.Equals(""))
            {
                mess = "Password cannot empty";
            }
            else if (!Regex.IsMatch(txtUsername.Text.Trim(), regex))
            {
                mess = "Username invalid format";
            } else if (!Regex.IsMatch(txtPassword.Text.Trim(), regex))
            {
                mess = "Password invalid format";
            }

                if (mess.Equals(""))
            {
                return true;
            } else
            {
                MessageBox.Show(mess, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateLogin())
            {
                StudentDBContext dbStudent = new StudentDBContext();
                if (dbStudent.GetStudent(txtUsername.Text, txtPassword.Text) != null)
                {

                    StudentDashBoard frmStudentDashboard = new StudentDashBoard(dbStudent.GetStudent(txtUsername.Text, txtPassword.Text).Id);
                    frmStudentDashboard.FormClosed += (s, args) => this.Close();
                    frmStudentDashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Username or Password is incorrect.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
