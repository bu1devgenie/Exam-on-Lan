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

namespace OTS.EssayMark
{
    public partial class FrmEssayTestList : Form
    {
        private int testId, studentId;
        public FrmEssayTestList(int testId, int studentId)
        {
            this.testId = testId;
            this.studentId = studentId;
            InitializeComponent();
        }

        public void LoadEssaySubmission()
        {
            try
            {
                TestDBContext testDBC = new TestDBContext();
                Test test = testDBC.GetTest(testId);
                Mark mark = new MarkDBContext().GetMark(testId, studentId);
                List<Essay> essays = new EssayDBContext().GetEssays(testId, studentId);
                txtTestId.Text = test.Id.ToString();
                txtSubject.Text = test.Subject.ToString();
                richtxtNote.Text = (mark == null)? "" : mark.Note;
                nudMark.Value = (mark == null)?  0 : (decimal)mark.Grade;
                foreach (Essay essay in essays)
                {
                    dgvSolution.Rows.Add(essay.Id, essay.Question.Content, "Detail");
                }

            }catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            try {
                Mark mark = new Mark()
                {
                    Note = richtxtNote.Text,
                    Grade = (float) nudMark.Value,
                    Student = new Student()
                    {
                        Id = studentId,
                    },
                    Test = new Test()
                    {
                        Id=testId,
                    },
                };

                MarkDBContext markDBC = new MarkDBContext();
                markDBC.DeleteMark(testId, studentId);
                if (markDBC.SetMark(mark) > 0)
                {
                    MessageBox.Show("Save Successful");
                };
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
            this.Close();
        }

        private void dgvSolution_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if(e.ColumnIndex == 2 && e.RowIndex != -1)
            {
                int essayId = Int32.Parse(dgv.Rows[e.RowIndex].Cells["EssayId"].Value.ToString());
                FrmEssayDetail frmEssayDetail = new FrmEssayDetail(essayId);
                frmEssayDetail.Show();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Every changes will be ignored!.\nDo you want to Cancel?"
                , "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnTestDetail_Click(object sender, EventArgs e)
        {
            FrmViewTest frmViewTest = new FrmViewTest(testId);
            frmViewTest.Show();
        }

        private void nudMark_ValueChanged(object sender, EventArgs e)
        {
            if (nudMark.Value > nudMark.Maximum)
            {
                MessageBox.Show($"Mark can not greater than {nudMark.Maximum}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void FrmEssayTestList_Load(object sender, EventArgs e)
        {
            LoadEssaySubmission();
        }
    }
}
