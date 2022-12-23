using OTS.DAO;
using OTS.Login;
using OTS.ManageMark;
using OTS.ManageQuestion;
using OTS.ManageStudent;
using OTS.ManageSubject;
using OTS.ManageSubmission;
using OTS.ManageClass;
using OTS.ManageTest;
using OTS.ManageQuestion;
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
    public partial class FrmViewTest : Form
    {
        private Test old;
        private int testID;
        public FrmViewTest(int testID)
        {
            this.testID = testID;
            InitializeComponent();

            InitCustomStyle();
        }

        private void InitCustomStyle()
        {
            dtpCreateDate.Format = DateTimePickerFormat.Custom;
            dtpCreateDate.CustomFormat = "dd/MM/yyyy";
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.CustomFormat = "HH:mm:ss";
            dtpStartDate.Format = DateTimePickerFormat.Custom;
            dtpStartDate.CustomFormat = "dd/MM/yyyy";
            dtpDuration.Format = DateTimePickerFormat.Time;
            dtpDuration.CustomFormat = "HH:mm:ss";
            dtpEndTime.Format = DateTimePickerFormat.Time;
            dtpEndTime.CustomFormat = "HH:mm:ss";
        }
        public void LoadQuestionsList()
        {
            dgvQuestion.Rows.Clear();
            try
            {
                QuestionDBContext questionDBC = new QuestionDBContext();
                foreach (QuestionTest questionTest in questionDBC.GetQuestionByTests(testID))
                {
                    dgvQuestion.Rows.Add(
                        questionTest.Question.Id,
                        questionTest.Question.Content,
                        questionTest.Question.Type.Name,
                        questionTest.Question.Level.Name,
                        "View",
                         "Change"
                        );
                }
                txtTotalQuestion.Text = dgvQuestion.Rows.Count.ToString();
                //dgvQuestion.DataSource = questionTestDisplays;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void LoadClassList()
        {
            try
            {
                ClassDBContext classDBC = new ClassDBContext();
                foreach (Class c in classDBC.GetClassByTest(testID))
                {
                    lbClasses.Items.Add(c.ClassCode + " - " + c.ClassName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void LoadTestInformation()
        {
            try
            {
                TestDBContext testDBC = new TestDBContext();
                Test test = testDBC.GetTest(testID);
                if (test != null)
                {
                    old = test;
                    txtTestID.Text = test.Id.ToString();
                    txtTestCode.Text = test.Code;
                    txtSubject.Text = test.Subject.SubjectCode
                        + " - " + test.Subject.SubjectName;
                    dtpCreateDate.Value = test.CreateDate;
                    dtpStartDate.Value = test.TestDate;
                    DateTime dt = new DateTime(2022, 12, 31);
                    dtpStartTime.Value = dt.Add(test.StartTime);
                    dtpDuration.Value = dt.Add(test.Duration);
                    dtpEndTime.Value = dt.Add(test.EndTime);
                    cbReview.Checked = test.IsReview;
                    LoadQuestionsList();
                    LoadClassList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void FrmViewTest_Load(object sender, EventArgs e)
        {
            LoadTestInformation();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to cancel!\nAll change will be ignored", "Warning",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                this.Close();
        }

        private void dgvQuestion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridViewQuestion = (DataGridView)sender;
            if (e.ColumnIndex == 5 && e.RowIndex != -1)
            {
                try
                {
                    int selectedQuestionId = Int32.Parse(
                        dataGridViewQuestion.Rows[e.RowIndex].Cells["QuestionID"].Value.ToString());

                    QuestionDBContext questionDBC = new QuestionDBContext();
                    Question selectedQuestion = questionDBC.GetQuestion(selectedQuestionId);
                    if (selectedQuestion != null)
                    {
                        List<int> existQuestionId = new List<int>();
                        foreach (DataGridViewRow row in dgvQuestion.Rows)
                        {
                            existQuestionId.Add(Int32.Parse(row.Cells[0].Value.ToString()));
                        }
                        Question newQuestion = null;
                        newQuestion = questionDBC.GetRandomQuestionWithLevel(
                            selectedQuestion.Level.Id, selectedQuestion.Subject.SubjectCode, selectedQuestion.Type.Id, existQuestionId
                            );
                        if (newQuestion != null)
                        {
                            dataGridViewQuestion.Rows[e.RowIndex].SetValues(newQuestion.Id, newQuestion.Content, newQuestion.Type.Name, newQuestion.Level.Name, "View", "Change");
                        } else
                        {
                            MessageBox.Show("Latest question\nEnd of questions in Question Bank.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Question not found", "Error");
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
            }
            if (e.ColumnIndex == 4 && e.RowIndex != -1)
            {
                // thực hiện hành động khi chọn view trên dgvQuestions
                try
                {
                    QuestionDBContext questionDBC = new QuestionDBContext();
                    AnswerDBContext answerDBC = new AnswerDBContext();
                    int questionID = Int32.Parse(dataGridViewQuestion.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Question question = questionDBC.GetQuestion(questionID);
                    EditQuestion editQuestion = new EditQuestion(question);
                    question.Answers = answerDBC.getByQues(question);
                    editQuestion.Show();
                    editQuestion.FormClosed += (s, args) => LoadQuestionsList();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
            }
        }

        private Test GetTestInfo()
        {
            Test test = new Test()
            {
                Id = Int32.Parse(txtTestID.Text),
                Code = txtTestCode.Text,
                Duration = dtpDuration.Value.TimeOfDay,
                StartTime = dtpStartTime.Value.TimeOfDay,
                TestDate = dtpStartDate.Value.Date,
                EndTime = dtpEndTime.Value.TimeOfDay,
                IsReview = cbReview.Checked,
                Subject = new Subject()
                {
                    SubjectCode = txtSubject.Text.Split("-")[0].Trim(),
                    SubjectName = txtSubject.Text.Split("-")[1].Trim(),
                }
            };
            return test;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTestCode.Text.Length != 0)
            {
                Test test = GetTestInfo();
                try
                {
                    TestDBContext testDBC = new TestDBContext();
                    QuestionDBContext questionDBC = new QuestionDBContext();
                    Test testByCode = testDBC.GetTestByCode(test.Code);

                    if (testByCode == null || old.Code.Equals(testByCode.Code))
                    {

                        testDBC.UpdateTest(test);

                        List<int> questionIds = new List<int>();
                        foreach (DataGridViewRow row in dgvQuestion.Rows)
                        {
                            questionIds.Add(
                            Int32.Parse(row.Cells["QuestionId"].Value.ToString())
                            );
                        }
                        List<string> classCodes = new List<string>();
                        for (int i = 0; i < lbClasses.Items.Count; i++)
                        {
                            string classText = lbClasses.Items[i].ToString();
                            classCodes.Add(classText.Split(" - ")[0].Trim());
                        }
                        questionDBC.UpdateTestQuestion(test.Id, questionIds);
                        testDBC.UpdateClassesTest(testID, classCodes);
                        MessageBox.Show("Update succesful");

                    }
                    else
                    {
                        MessageBox.Show($"Duplicate Test Code {txtTestCode.Text}\nUpdate Fail");
                    }

                    }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    MessageBox.Show("Update Fail");
                }
            }
            else
            {
                MessageBox.Show("Test Code must not be empty!", "Waring");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Delete!\nThis action can not revert", "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                int testId = Int32.Parse(txtTestID.Text);

                try
                {
                    SubmissionDBContext submissionDBC = new SubmissionDBContext();
                    EssayDBContext essayDBC = new EssayDBContext();
                    if (!(submissionDBC.CheckIsTested(testId) && essayDBC.CheckIsTested(testId)))
                    {
                        TestDBContext testDBC = new TestDBContext();
                        if (testDBC.DeleteTest(testId) > 0)
                        {
                            MessageBox.Show("Delete Sucessful");
                        };
                    }
                    else
                    {
                        MessageBox.Show("This test had been taken\nCan not delete", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void txtAddClassCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string inputClassCode = txtAddClassCode.Text;
                if (inputClassCode.Length > 0)
                {
                    for (int i = 0; i < lbClasses.Items.Count; i++)
                    {
                        string classTextList = lbClasses.Items[i].ToString();
                        if (classTextList.Split("-")[0].Trim().Equals(inputClassCode))
                        {
                            MessageBox.Show($"Class {inputClassCode} already exists", "Notice");
                            return;
                        }

                    }
                    try
                    {
                        ClassDBContext classDBC = new ClassDBContext();
                        Class newClass = classDBC.GetClass(inputClassCode);
                        if (newClass != null)
                        {
                            lbClasses.Items.Add(newClass);
                            txtAddClassCode.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Class does not exist!", "Warning");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRemoveClass_Click(object sender, EventArgs e)
        {
            lbClasses.Items.Remove(lbClasses.SelectedItem);
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
