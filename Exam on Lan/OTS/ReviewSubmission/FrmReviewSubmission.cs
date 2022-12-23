using OTS.DAO;
using OTS.Login;
using OTS.Models;
using OTS.StudenDashBoard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTS.ReviewSubmission
{
    public partial class FrmReviewSubmission : Form
    {
        Submission currentSubmission = null;
        Mark mark;
        List<SubmissionQA> submissionQAs;
        List<Essay> essays = null;
        SubmissionDBContext submissionDB = new SubmissionDBContext();
        MarkDBContext markDB = new MarkDBContext();
        SubmissionQADBContext submissionQADB = new SubmissionQADBContext();
        EssayDBContext essayDB = new EssayDBContext();
        AnswerDBContext answerDB = new AnswerDBContext();
        public FrmReviewSubmission()
        {
            InitializeComponent();
        }

        public FrmReviewSubmission(int submissionID)
        {
            //init value when reviewing the multiple choice test
            currentSubmission = submissionDB.GetSubmission(submissionID);
            if (currentSubmission != null)
            {
                mark = markDB.GetMark(currentSubmission.Test.Id, currentSubmission.Student.Id);
            }
            submissionQAs = submissionQADB.GetListSubmissionQAs(submissionID);
            if (submissionQAs != null && submissionQAs.Count > 0)
            {
                foreach (var submissionQA in submissionQAs)
                {
                    submissionQA.Question.Answers = answerDB.getAnswerByQues(submissionQA.Question.Id);
                }
            }

            InitializeComponent();
        }

        public FrmReviewSubmission(int testID, int studentID)
        {
            //init value when reviewing the essay test
            essays = essayDB.GetEssays(testID, studentID);
            mark = markDB.GetMark(testID, studentID);

            InitializeComponent();
        }
        
        //This variable to save total height of previous groupboxes to determine the Y-axis
        //value of the new groupbox location
        int groupBoxHeight = 0;

        //this function build and add a multiple choice question review to the window
        public void buildMultipleChoice(int questionNo, Question question, Answer answer)
        {
            Label lbCorrect = new Label();
            Label lbQuestion = new Label();
            GroupBox gbQuestionNo = new GroupBox();

            // 
            // gbQuestionNo
            // 
            gbQuestionNo.Controls.Add(lbCorrect);
            gbQuestionNo.Controls.Add(lbQuestion);
            gbQuestionNo.AutoSize = true;
            gbQuestionNo.Location = new System.Drawing.Point(64, 234 + groupBoxHeight);
            gbQuestionNo.Name = "gbQuestionNo";
            gbQuestionNo.Size = new System.Drawing.Size(777, 104);
            gbQuestionNo.TabIndex = 13;
            gbQuestionNo.TabStop = false;
            gbQuestionNo.Text = $"Question {questionNo}";
            // 
            // lbQuestion
            // 
            lbQuestion.AutoSize = true;
            lbQuestion.Location = new System.Drawing.Point(12, 23);
            lbQuestion.Name = "lbQuestion";
            lbQuestion.Size = new System.Drawing.Size(38, 15);
            lbQuestion.TabIndex = 10;
            lbQuestion.Text = question.Content;

            int radioHeight = 0;
            foreach (var a in question.Answers)
            {
                RadioButton rbAnswer = new RadioButton();
                // rbAnswer
                // 
                rbAnswer.AutoSize = true;
                if (a.Id == answer.Id)
                {
                    rbAnswer.Checked = true;
                }
                rbAnswer.Enabled = false;
                rbAnswer.Location = new System.Drawing.Point(12, 44 + radioHeight);
                rbAnswer.Name = "rbAnswer";
                rbAnswer.Size = new System.Drawing.Size(94, 19);
                rbAnswer.TabIndex = 13;
                rbAnswer.TabStop = true;
                rbAnswer.Text = a.Content;
                rbAnswer.UseVisualStyleBackColor = true;
                gbQuestionNo.Controls.Add(rbAnswer);

                radioHeight += 30;
            }

            if (answer.IsCorrect)
            {
                lbCorrect.Text = "Correct";
                lbCorrect.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lbCorrect.Text = "Incorrect";
                lbCorrect.ForeColor = System.Drawing.Color.Red;
            }
            // 
            // lbCorrect
            // 
            lbCorrect.AutoSize = true;
            lbCorrect.Location = new System.Drawing.Point(12, 44 + radioHeight);
            lbCorrect.Name = "lbCorrect";
            lbCorrect.Size = new System.Drawing.Size(38, 15);
            lbCorrect.TabIndex = 12;


            this.Controls.Add(gbQuestionNo);

            //extra 5pt for distance between groupboxes
            groupBoxHeight += gbQuestionNo.Height+5;

        }

        //this function build and add a essay question review to the window
        public void buildEssay(int questionNo, Essay essay)
        {
            Label lbQuestion = new Label();
            RichTextBox rtbAnswer = new RichTextBox();
            Label lbSolution = new Label();
            GroupBox gbQuestionNo = new GroupBox();

            // 
            // gbQuestionNo
            // 
            gbQuestionNo.Controls.Add(lbQuestion);
            gbQuestionNo.Controls.Add(lbSolution);
            gbQuestionNo.Controls.Add(rtbAnswer);
            gbQuestionNo.AutoSize = true;
            gbQuestionNo.Location = new System.Drawing.Point(64, 234 + groupBoxHeight);
            gbQuestionNo.Name = "gbQuestionNo";
            gbQuestionNo.Size = new System.Drawing.Size(777, 104);
            gbQuestionNo.TabIndex = 13;
            gbQuestionNo.TabStop = false;
            gbQuestionNo.Text = $"Question {questionNo}";
            // 
            // lbQuestion
            // 
            lbQuestion.AutoSize = true;
            lbQuestion.Location = new System.Drawing.Point(12, 23);
            lbQuestion.Name = "lbQuestion";
            lbQuestion.Size = new System.Drawing.Size(38, 15);
            lbQuestion.TabIndex = 10;
            lbQuestion.Text = essay.Question.Content;
            // 
            // lbSolution
            // 
            lbSolution.AutoSize = true;
            lbSolution.Location = new System.Drawing.Point(12, 53);
            lbSolution.Name = "lbSolution";
            lbSolution.Size = new System.Drawing.Size(38, 15);
            lbSolution.TabIndex = 11;
            lbSolution.Text = "Solution:";
            lbSolution.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            // 
            // rtbAnswer
            // 
            rtbAnswer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            rtbAnswer.Location = new System.Drawing.Point(12, 83);
            rtbAnswer.Name = "rtbAnswer";
            rtbAnswer.ReadOnly = true;
            rtbAnswer.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            rtbAnswer.Size = new System.Drawing.Size(774, 300);
            rtbAnswer.TabIndex = 9;
            rtbAnswer.Text = essay.Content;

            this.Controls.Add(gbQuestionNo);

            //extra 5pt for distance between groupboxes
            groupBoxHeight += gbQuestionNo.Height + 5;

        }

        private void FrmReviewSubmission_Load(object sender, EventArgs e)
        {
            //if the submission reviewed is a multiple choice question
            if (currentSubmission != null)
            {
                lbStudentCode.Text = currentSubmission.Student.StudentCode;
                lbStudentName.Text = currentSubmission.Student.FullName;
                lbClass.Text = currentSubmission.Student.Class.ClassCode;
                lbTestCode.Text = currentSubmission.Test.Code;
                lbSubject.Text = currentSubmission.Test.Subject.ToString();
                lbSubmitTime.Text = currentSubmission.SubmitDate.ToString();
                lbTestDate.Text = currentSubmission.Test.TestDate.Date.ToShortDateString();

                if (!currentSubmission.Test.IsReview)
                {
                    lbTitle.Text = "This test does not allow to be reviewed";
                }
                else
                {
                    lbTitle.Text = "Review Submission";
                    //reset value for groupbox height
                    groupBoxHeight = 0;
                    for (int i = 0; i < submissionQAs.Count; i++)
                    {                        
                        buildMultipleChoice(i + 1, submissionQAs[i].Question, submissionQAs[i].Answer);
                    }
                }
            }

            //if the submission reviewed is a essay question
            if (essays != null && essays.Count > 0)
            {
                lbStudentCode.Text = essays[0].Student.StudentCode;
                lbStudentName.Text = essays[0].Student.FullName;
                lbClass.Text = essays[0].Student.Class.ClassCode;
                lbTestCode.Text = essays[0].Test.Code;
                lbSubject.Text = essays[0].Test.Subject.ToString();
                lbSubmitTime.Text = essays[0].SubmitDate.ToString();
                lbTestDate.Text = essays[0].Test.TestDate.Date.ToShortDateString();

                if (!essays[0].Test.IsReview)
                {
                    lbTitle.Text = "This test does not allow to be reviewed";
                }
                else
                {
                    lbTitle.Text = "Review submission";
                    //reset value for groupbox height
                    groupBoxHeight = 0;
                    for (int i = 0; i < essays.Count; i++)
                    {
                        buildEssay(i + 1, essays[i]);
                    }
                }
            }


            if (mark != null)
            {
                lbMark.Text = mark.Grade.ToString();
            }
            else
            {
                lbMark.Text = "";
            }
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //redirect to student dashboard
            StudentDashBoard frmStudentDashBoard = new StudentDashBoard();
            frmStudentDashBoard.Closed += (s, args) => this.Close();
            this.Hide();
            frmStudentDashBoard.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //redirect to login page
            FrmLoginStudent frmLoginStudent = new FrmLoginStudent();
            frmLoginStudent.Closed += (s, args) => this.Close();
            this.Hide();
            frmLoginStudent.Show();
        }
    }
}
