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

namespace OTS.ViewTest
{
    public partial class FrmCreateTest : Form
    {
        public FrmCreateTest()
        {
            InitializeComponent();
            //InitCustomStyle();
        }
        SubjectDBContext subjectDBContext = new SubjectDBContext();
        TestDBContext testDBContext = new TestDBContext();
        TypeDBContext typeDBContext = new TypeDBContext();
        QuestionDBContext questionDBContext = new QuestionDBContext();
        //private void InitCustomStyle()
        //{
        //    dtpStartTime.Format = DateTimePickerFormat.Custom;
        //    dtpStartTime.CustomFormat = "HH:mm:ss";
        //    dtpTestDate.Format = DateTimePickerFormat.Custom;
        //    dtpTestDate.CustomFormat = "dd/MM/yyyy";
        //    dtpDuration.Format = DateTimePickerFormat.Time;
        //    dtpDuration.CustomFormat = "HH:mm:ss";
        //    dtpStartTime.Format = DateTimePickerFormat.Time;
        //    dtpStartTime.CustomFormat = "HH:mm:ss";
        //}

        private void LoadSubject()
        {
            List<Subject> subjects = subjectDBContext.GetSubjects();
            cbSubject.DataSource = subjects;
            cbSubject.ValueMember = "SubjectCode";
            cbSubject.DisplayMember = "SubjectName";

        }

        private void LoadTest()
        {
            List<Test> tests = testDBContext.GetTests();
            dgvTest.DataSource = tests;
        }

        private void LoadType()
        {
            List<Models.Type> types = typeDBContext.GetType();
            cbType.DataSource = types;
            cbType.ValueMember = "Id";
            cbType.DisplayMember = "Name";

        }

        private bool CheckInput()
        {
            string mess = "";

            if (txtTotalQuest.Text.Equals(""))
            {
                mess = "You must select number of questions";
            }
            else if (txtTestCode.Text.Equals(""))
            {
                mess = "Test Code cannot empty";
            }
            else if (dtpTestDate.Value.CompareTo(DateTime.Now) < 0)
            {
                mess = "Invalid Test Date";
            }
            else if (cbSubject.Text.Equals(""))
            {
                mess = "Subject Code cannot empty";
            }

            if (mess.Equals(""))
            {
                return true;
            }
            else
            {
                MessageBox.Show(mess, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void LoadTotal()
        {
            int total = GetTotal((int)nudEasy.Value, (int)nudMedium.Value, (int)nudHard.Value);
            if (total > 0)
            {
                txtTotalQuest.Text = total.ToString();
            }
        }

        private int GetTotal(int easy, int medium, int hard)
        {
            int total = 0;
            if (easy + medium + hard > 0)
            {
                total = easy + medium + hard;
            }
            return total;
        }

        private int GenQuest(List<Question> questions, int test)
        {
            int row = 0;
            foreach(Question question in questions)
            {
                row = questionDBContext.InsertQuestion_Test(question.Id, test);
            }
            return row;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                List<Question> easyQuests = questionDBContext.GetRandomQuestions((int)nudEasy.Value, (Int16)cbType.SelectedValue, 1, cbSubject.SelectedValue.ToString());
                List<Question> medQuests = questionDBContext.GetRandomQuestions((int)nudMedium.Value, (Int16)cbType.SelectedValue, 2, cbSubject.SelectedValue.ToString());
                List<Question> hardQuests = questionDBContext.GetRandomQuestions((int)nudHard.Value, (Int16)cbType.SelectedValue, 3, cbSubject.SelectedValue.ToString());
                if (nudEasy.Value > easyQuests.Count)
                {
                    MessageBox.Show("Ran out of Easy question for this Subject");
                }
                else if (nudMedium.Value > medQuests.Count)
                {
                    MessageBox.Show("Ran out of Medium question for this Subject");
                }
                else if (nudHard.Value > hardQuests.Count)
                {
                    MessageBox.Show("Ran out of Hard question for this Subject");
                }
                else
                {
                    Test test = new Test()
                    {
                        Code = txtTestCode.Text,
                        StartTime = TimeSpan.Parse(dtpStartTime.Text),
                        TestDate = DateTime.Parse(dtpTestDate.Text),
                        Duration = TimeSpan.Parse(dtpDuration.Text),
                        CreateDate = DateTime.Now.Date,
                        EndTime = TimeSpan.Parse(dtpEndTime.Text),
                        IsReview = checkReview.Checked,
                    };
                    Subject s = (Subject)cbSubject.SelectedItem;
                    Subject subject = subjectDBContext.GetSubject(s.SubjectCode.Trim());
                    test.Subject = subject;
                    int row = testDBContext.InsertTest(test);

                    int testId = testDBContext.GetLatestTestId();

                    int easy = GenQuest(easyQuests, testId);
                    int med = GenQuest(medQuests, testId);
                    int hard = GenQuest(hardQuests, testId);


                    if (!(row > 0 && easy > 0 && med > 0 && hard > 0))
                    {
                        testDBContext.DeleteTest(testId);
                        MessageBox.Show("Create failed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoadTest();
                    }
                    else
                    {
                        MessageBox.Show("Create successful");
                        LoadTest();
                        txtTestCode.Text = "";
                        dtpTestDate.Value = DateTime.Now;
                        cbSubject.SelectedIndex = 0;
                        cbType.SelectedIndex = 0;
                        nudEasy.Value = 0;
                        nudMedium.Value = 0;
                        nudHard.Value = 0;
                        checkReview.Checked = false;
                    }
                }
            }
        }

        private void FrmCreateTest_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSubject();
                LoadTest();
                LoadType();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        private void nudEasy_ValueChanged(object sender, EventArgs e)
        {
            LoadTotal();
        }

        private void nudMedium_ValueChanged(object sender, EventArgs e)
        {
            LoadTotal();
        }

        private void nudHard_ValueChanged(object sender, EventArgs e)
        {
            LoadTotal();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to cancel!\nAll change will be canceled", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
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
