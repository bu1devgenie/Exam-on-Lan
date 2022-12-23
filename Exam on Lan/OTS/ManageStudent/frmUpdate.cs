using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OTS.DAO;
using OTS.Models;

namespace OTS.ManageStudent
{
    public partial class frmUpdate : Form
    {
        Int32 oldId = 0;
        String oldFullName = "";
        String oldPassword = "";
        DateTime oldDob;
        String oldStudentCode = "";
        String oldClassCode = "";
        StudentDBContext studentDBContext;
        Student target = null;
        public frmUpdate(Int32 Id)
        {
            InitializeComponent();
            oldId = Id;
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            loadStudent(oldId);
        }

        private void loadStudent(Int32 old)
        {
            target = new Student();
            studentDBContext = new StudentDBContext();
            target = studentDBContext.GetStudent(oldId);
            txtFullName.Text = target.FullName.Trim();
            txtClassCode.Text = target.Class.ClassCode.Trim();
            txtPassword.Text = target.Password.Trim();
            dtpDob.Value = target.DateOfBirth;
            txtPassword.Text = target.Password.Trim();
            txtStudentCode.Text = target.StudentCode.Trim();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            oldFullName = target.FullName.Trim();
            oldClassCode = target.Class.ClassCode.Trim();
            oldDob = target.DateOfBirth.Date;
            oldPassword = target.Password.Trim();
            oldStudentCode = target.StudentCode.Trim();
            studentDBContext = new StudentDBContext();
            int rowefect = studentDBContext.UpdateStudent(target.Id.ToString().Trim(), txtFullName.Text.ToString().Trim(), txtPassword.Text.ToString().Trim(), dtpDob.Value, txtStudentCode.Text.ToString().Trim(), txtClassCode.Text.ToString().Trim());
            if (rowefect > 0)
            {
                MessageBox.Show("Update sucessfull!!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            rowefect = 0;
        }
    }
}
