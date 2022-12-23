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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OTS.Dashboard;
using OTS.ReviewSubmission;

namespace OTS.ManageSubmission
{
    public partial class FrmManageSubmission : Form
    {
        public FrmManageSubmission()
        {
            InitializeComponent();
        }
        SubmissionDBContext submissionDB = new();
        ClassDBContext classDB = new();
        SubjectDBContext subjectDB = new();
        MarkDBContext markDB = new();

        private void LoadClass()
        {
            List<Class> classes = classDB.GetClasses();
            cbClass.DataSource = classes;
            cbClass.ValueMember = "ClassCode";
            cbClass.DisplayMember = "ClassCode";
        }

        private void ReloadDgv()
        {
            dgvSubmission.Rows.Clear();
        }

        private void LoadDgvSubmission()
        {
            string testCode = "";
            if (txtTestCode.Text != null)
            {
                testCode = txtTestCode.Text;
            }
            string classCode = "";
            if (cbClass.Text != null)
            {
                classCode = cbClass.Text;
            }
            string stuCode = "";
            if (txtStudentCode.Text != null)
            {
                stuCode = txtStudentCode.Text;
            }
            List<Submission> submissions = submissionDB.GetManageSubmissions(testCode, classCode, stuCode);
            foreach (Submission submission in submissions)
            {
                Subject subject = subjectDB.GetSubjectBySubmission(submission.Id);
                Mark mark = markDB.GetMarkSubmission(submission.Id, submission.Test.Code, submission.Student.StudentCode);
                dgvSubmission.Rows.Add(submission.Id,
                    submission.Test.Code,
                    submission.Student.Class.ClassCode,
                    subject.SubjectCode,
                    submission.Student.StudentCode,
                    submission.Student.FullName,
                    submission.SubmitDate,
                    mark.Grade,
                    "Review");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            ReloadDgv();
            string testCode = "";
            if (txtTestCode.Text != null)
            {
                testCode = txtTestCode.Text;
            }
            string classCode = "";
            if (cbClass.Text != null)
            {
                classCode = cbClass.Text;
            }
            string stuCode = "";
            if (txtStudentCode.Text != null)
            {
                stuCode = txtStudentCode.Text;
            }
            List<Submission> submissions = submissionDB.GetManageSubmissions(testCode, classCode, stuCode);
            foreach (Submission submission in submissions)
            {
                Subject subject = subjectDB.GetSubjectBySubmission(submission.Id);
                Mark mark = markDB.GetMarkSubmission(submission.Id, submission.Test.Code, submission.Student.StudentCode);
                dgvSubmission.Rows.Add(submission.Id,
                    submission.Test.Code,
                    submission.Student.Class.ClassCode,
                    subject.SubjectCode,
                    submission.Student.StudentCode,
                    submission.Student.FullName,
                    submission.SubmitDate,
                    mark.Grade,
                    "Review");
            }
        }

        private void FrmManageSubmission_Load(object sender, EventArgs e)
        {
            try
            {
                LoadClass();
                LoadDgvSubmission();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvSubmission_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                int sid = int.Parse(dgvSubmission.CurrentRow.Cells[0].Value.ToString());
                //Transfer to other form and close this form
                FrmReviewSubmission frmReviewSubmission = new FrmReviewSubmission(sid);
                this.Hide();
                frmReviewSubmission.FormClosed += (s, args) => this.Show();
                frmReviewSubmission.Show();
            }
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmModDashboard frmModDashboard = new FrmModDashboard();
            frmModDashboard.Closed += (s, args) => this.Close();
            this.Hide();
            frmModDashboard.Show();
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageStudent frmManageStudent = new frmManageStudent();
            frmManageStudent.Closed += (s, args) => this.Close();
            this.Hide();
            frmManageStudent.Show();
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
