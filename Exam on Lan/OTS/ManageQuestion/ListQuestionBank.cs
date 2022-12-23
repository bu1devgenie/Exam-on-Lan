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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OTS.Dashboard;

namespace OTS.ManageQuestion
{
    public partial class ListQuestionBank : Form
    {
        private string searchKey = "";
        public ListQuestionBank()
        {
            InitializeComponent();
        }

        private void ListQuestionBank_Load(object sender, EventArgs e)
        {
            loadQues();
        }


        public void loadQues()
        {
            
            try
            {
                QuestionDBContext qDB = new QuestionDBContext();
                AnswerDBContext aDB = new AnswerDBContext();
                TypeDBContext tDB = new TypeDBContext();
                List<Question> questions = qDB.getQues(searchKey,"content");
                List<Answer> answers = aDB.getAnswer();
                var types = tDB.GetTypes();
                checkType.DataSource = types;
                checkType.DisplayMember = "Name";

                foreach (var item in questions)
                {
                    item.Answers = answers.Where(a => a.Question.Id == item.Id).ToList();
                }
                if (checkType.SelectedIndex == 0)
                {
                    var listQues = questions.Where(q => q.Type.Id == 1).Select(l => new
                    {
                        Id = l.Id,
                        Content = l.Content,
                        Level = l.Level.Name,
                        SubCode = l.Subject.SubjectCode,
                        Type = l.Type.Name,
                        CorrectAns = l.Answers.FirstOrDefault(c => c.IsCorrect).Content.ToString()
                    }).ToList();
                    dataQuestion.DataSource = listQues;
                }
                else
                {
                    var listQues = questions.Where(q => q.Type.Id == 2).Select(l => new
                    {
                        Id = l.Id,
                        Content = l.Content,
                        Level = l.Level.Name,
                        SubCode = l.Subject.SubjectCode,
                        Type = l.Type.Name,
                    }).ToList();
                    dataQuestion.DataSource = listQues;
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnswerDBContext aDB = new AnswerDBContext();
            QuestionDBContext qDB = new QuestionDBContext();
            TypeDBContext tDB = new TypeDBContext();
            List<Question> questions = qDB.getQues(searchKey,"content");
            List<Answer> answers = aDB.getAnswer();
            foreach (var item in questions)
            {
                item.Answers = answers.Where(a => a.Question.Id == item.Id).ToList();
            }
            if (checkType.SelectedIndex == 0)
            {
                checkType.SelectedItem = tDB.GetTypes().FirstOrDefault(c => c.Id == 1);
                var listQues = questions.Where(q => q.Type.Id == 1).Select(l => new
                {
                    Id = l.Id,
                    Content = l.Content,
                    Level = l.Level.Name,
                    SubCode = l.Subject.SubjectCode,
                    Type = l.Type.Name,
                    CorrectAns = l.Answers.FirstOrDefault(c => c.IsCorrect).Content.ToString()
                }).ToList();
                dataQuestion.DataSource = listQues;
                return;
            }
            else
            {
                checkType.SelectedItem = tDB.GetTypes().FirstOrDefault(c => c.Id == 2);
                var listQues = questions.Where(q => q.Type.Id == 2).Select(l => new
                {
                    Id = l.Id,
                    Content = l.Content,
                    Level = l.Level.Name,
                    SubCode = l.Subject.SubjectCode,
                    Type = l.Type.Name,
                }).ToList();
                dataQuestion.DataSource = listQues;
            }
        }

        private void addQues_Click(object sender, EventArgs e)
        {
            AddQuestion aq = new AddQuestion(this);
            aq.Show();
        }

        private void searchQues_Click(object sender, EventArgs e)
        {
            searchKey = textBox1.Text;

            loadQues();
        }

        private void editQues_Click(object sender, EventArgs e)
        {
            QuestionDBContext qDb = new QuestionDBContext();
            AnswerDBContext aDB = new AnswerDBContext();
            if(dataQuestion.SelectedRows.Count > 0)
            {
                int id = (int)dataQuestion.SelectedRows[0].Cells["Id"].Value;
                string content = dataQuestion.SelectedRows[0].Cells["Content"].Value.ToString();
                string levelName = dataQuestion.SelectedRows[0].Cells["Level"].Value.ToString();
                string subCode = dataQuestion.SelectedRows[0].Cells["SubCode"].Value.ToString();
                string typeName = dataQuestion.SelectedRows[0].Cells["Type"].Value.ToString();
                Question q = qDb.getQues(id);
                q.Answers = aDB.getByQues(q);
                EditQuestion eq = new EditQuestion(this, q);
                eq.Show();
            }

        }

        private void delQues_Click(object sender, EventArgs e)
        {
            QuestionDBContext qDb = new QuestionDBContext();
            AnswerDBContext aDB = new AnswerDBContext();
            if (dataQuestion.SelectedRows.Count > 0)
            {
                int id = (int)dataQuestion.SelectedRows[0].Cells["Id"].Value;
                string content = dataQuestion.SelectedRows[0].Cells["Content"].Value.ToString();
                string levelName = dataQuestion.SelectedRows[0].Cells["Level"].Value.ToString();
                string subCode = dataQuestion.SelectedRows[0].Cells["SubCode"].Value.ToString();
                string typeName = dataQuestion.SelectedRows[0].Cells["Type"].Value.ToString();
                Question q = qDb.getQues(id);
                q.Answers = aDB.getByQues(q);
                try
                {
                    foreach (var item in q.Answers)
                    {
                        int delAns = aDB.DeleteAns(item);
                    }
                    int result = qDb.DeleteQues(q);
                    if (result > 0)
                    {
                        MessageBox.Show($"Delete Successful!");
                        loadQues();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
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
