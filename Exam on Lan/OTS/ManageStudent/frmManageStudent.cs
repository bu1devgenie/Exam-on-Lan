using OTS.DAO;
using OTS.Login;
using OTS.ManageMark;
using OTS.ManageQuestion;
using OTS.ManageStudent;
using OTS.ManageSubject;
using OTS.ManageSubmission;
using OTS.ManageClass;
using OTS.ManageTest;
using OTS.Models;
using OTS.ViewTest;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using OTS.Models;
using System;
using OTS.Dashboard;

namespace OTS.ManageStudent
{
    public partial class frmManageStudent : Form
    {
        String classCode = "";
        String studentCode = "";
        String option = "";
        String rgxClassCode = "^[a-zA-Z0-9]{2,50}$";
        String rgxsStudentCode = "^[a-zA-Z0-9]{2,255}$";
        int rowselected = 0;
        int IDtoDeleteAndUpdate = 0;
        StudentDBContext studentDBContext;
        Student target = null;

        public frmManageStudent()
        {
            InitializeComponent();
        }
        private void ClearData()
        {
            dgvStudent.Rows.Clear();
            txtClassCode.Text = "";
            txtStudentCode.Text = "";

        }
        private void frmManageStudent_Load(object sender, EventArgs e)
        {
            try
            {
                ClearData();
                LoadStudent("getAll", classCode, studentCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadStudent(string option, String classCode, String studentCode)
        {

            studentDBContext = new StudentDBContext();

            var students = new DAO.StudentDBContext().FindStudent(option, classCode, studentCode).ToList();
            foreach (var s in students)
            {
                dgvStudent.Rows.Add(s.Id, s.FullName, s.Password, s.DateOfBirth, s.StudentCode, s.Class.ClassCode);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            frmInsertStudent frmInsertStudent = new frmInsertStudent();
            frmInsertStudent.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ManageStudent.frmUpdate frmUpdate = new frmUpdate(IDtoDeleteAndUpdate);
            int rowefect = 0;

            classCode = txtClassCode.Text.Trim().ToString();
            studentCode = txtStudentCode.Text.Trim().ToString();
            frmUpdate.Show();

           
        }
        public String getoption(String classCode, String studentCode)
        {


            if (Regex.IsMatch(classCode, rgxClassCode) && !String.IsNullOrEmpty(classCode) && String.IsNullOrEmpty(studentCode))
            {
                option = "ClassCode";

            }
            else if (Regex.IsMatch(studentCode, rgxsStudentCode) && !String.IsNullOrEmpty(studentCode) && String.IsNullOrEmpty(classCode))
            {
                option = "StudentCode";
            }
            else if (Regex.IsMatch(classCode, rgxClassCode) && !String.IsNullOrEmpty(classCode) && Regex.IsMatch(studentCode, rgxsStudentCode) && !String.IsNullOrEmpty(studentCode))
            {
                option = "ClassCodeAndStudentCode";
            }
            else
            {
                option = "getAll";
            }
            return option;
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {

                classCode = txtClassCode.Text.ToString().Trim();
                studentCode = txtStudentCode.Text.ToString().Trim();
                ClearData();
                LoadStudent(getoption(classCode, studentCode), classCode, studentCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgvStudent.SelectedRows[0];
                StudentDBContext student = new StudentDBContext();

                String Id = row.Cells[0].Value.ToString();

                target = student.GetStudent(Int32.Parse(Id));
                if (target != null)
                {
                    txtClassCode.Text = target.Class.ClassCode.ToString();
                    txtStudentCode.Text = target.StudentCode.ToString();
                    IDtoDeleteAndUpdate = target.Id;
                    rowselected++;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int rowefect = 0;

            classCode = txtClassCode.Text.Trim().ToString();
            studentCode = txtStudentCode.Text.Trim().ToString();
            if (rowselected > 0)
            {
                DialogResult result = MessageBox.Show($"Are you sure to delete Student: {classCode}-{studentCode}", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                switch (result)
                {
                    case DialogResult.Yes:
                        StudentDBContext student = new StudentDBContext();



                        rowefect = student.DeleteStudent(IDtoDeleteAndUpdate.ToString());
                        break;
                    case DialogResult.No:
                        break;
                }

            }
            else
            {
                MessageBox.Show("Plss select 1 row for Delete", "warrning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            rowselected = 0;
            frmManageStudent_Load(sender, e);
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmModDashboard frmModDashboard = new FrmModDashboard();
            frmModDashboard.Closed += (s, args) => this.Close();
            this.Hide();
            frmModDashboard.Show();
        }

        private void subjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagerSubject frmManagerSubject = new frmManagerSubject();
            frmManagerSubject.Closed += (s, args) => this.Close();
            this.Hide();
            frmManagerSubject.Show();
        }

        private void classToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManageClass frmManageClass = new FrmManageClass();
            frmManageClass.Closed += (s, args) => this.Close();
            this.Hide();
            frmManageClass.Show();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTest frmManageTest = new frmManageTest();
            frmManageTest.Closed += (s, args) => this.Close();
            this.Hide();
            frmManageTest.Show();
        }

        private void viewTestListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTest frmManageTest = new frmManageTest();
            frmManageTest.Closed += (s, args) => this.Close();
            this.Hide();
            frmManageTest.Show();
        }

        private void createATestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCreateTest frmCreateTest = new FrmCreateTest();
            frmCreateTest.Closed += (s, args) => this.Close();
            this.Hide();
            frmCreateTest.Show();
        }

        private void questionBankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListQuestionBank frmQuestionBank = new ListQuestionBank();
            frmQuestionBank.Closed += (s, args) => this.Close();
            this.Hide();
            frmQuestionBank.Show();
        }

        private void submissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManageSubmission frmManageSubmission = new FrmManageSubmission();
            frmManageSubmission.Closed += (s, args) => this.Close();
            this.Hide();
            frmManageSubmission.Show();
        }

        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmLoginModerator frmLoginModerator = new FrmLoginModerator();
            frmLoginModerator.Closed += (s, args) => this.Close();
            this.Hide();
            frmLoginModerator.Show();
        }

        private void markToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmManageMark frmManageMark = new FrmManageMark();
            frmManageMark.Closed += (s, args) => this.Close();
            this.Hide();
            frmManageMark.Show();
        }
    }
}
