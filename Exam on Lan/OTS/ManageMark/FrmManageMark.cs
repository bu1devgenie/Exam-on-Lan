using OTS.DAO;
using OTS.Dashboard;
using OTS.Login;
using OTS.ManageQuestion;
using OTS.ManageStudent;
using OTS.ManageSubject;
using OTS.ManageSubmission;
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

namespace OTS.ManageMark
{
    public partial class FrmManageMark : Form
    {
        Test currentTest = null;
        Student currentStudent = null;
        Class currentClass = null;
        MarkDBContext markDB = new MarkDBContext();
        ClassDBContext classDB = new ClassDBContext();
        SubmissionDBContext submissionDB = new SubmissionDBContext();
        EssayDBContext essayDB = new EssayDBContext();
        public FrmManageMark()
        {
            InitializeComponent();
        }

        public FrmManageMark(Test test)
        {
            currentTest = test;
            InitializeComponent();
        }

        public FrmManageMark(Student student)
        {
            currentStudent = student;
            InitializeComponent();
        }

        public FrmManageMark(Class classObj)
        {
            currentClass = classObj;
            InitializeComponent();
        }

        public void buildDGV()
        {
            dgvMark.AutoGenerateColumns = false;
            dgvMark.Columns.Clear();

            DataGridViewTextBoxColumn testCode = new DataGridViewTextBoxColumn();
            testCode.HeaderText = "TestCode";
            testCode.Name = "colTestCode";
            dgvMark.Columns.Add(testCode);

            DataGridViewTextBoxColumn studentCode = new DataGridViewTextBoxColumn();
            studentCode.HeaderText = "StudentCode";
            studentCode.Name = "colStudentCode";
            dgvMark.Columns.Add(studentCode);

            DataGridViewTextBoxColumn className = new DataGridViewTextBoxColumn();
            className.HeaderText = "Class";
            className.Name = "colClassName";
            dgvMark.Columns.Add(className);

            DataGridViewTextBoxColumn submitDate = new DataGridViewTextBoxColumn();
            submitDate.HeaderText = "SubmitDate";
            submitDate.Name = "colSubmitDate";
            dgvMark.Columns.Add(submitDate);

            DataGridViewTextBoxColumn mark = new DataGridViewTextBoxColumn();
            mark.HeaderText = "Mark";
            mark.DataPropertyName = "Grade";
            dgvMark.Columns.Add(mark);

            DataGridViewTextBoxColumn note = new DataGridViewTextBoxColumn();
            note.HeaderText = "Note";
            note.DataPropertyName = "Note";
            dgvMark.Columns.Add(note);
        }

        public void LoadClass()
        {
            try
            {
                var classList = classDB.GetClasses();
                cbClass.DataSource = classList;
                cbClass.DisplayMember = "ClassName";
                cbClass.ValueMember = "ClassCode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadMark()
        {
            string testCode = null;
            string studentCode = null;
            string classCode = null;
            DateTime from = new DateTime();
            DateTime to = new DateTime();
            int pageIndex;
            int pageSize = 15;

            //get filter value when corresponding checkbox is checked
            if (chkTestCode.Checked)
            {
                testCode = txtTestCode.Text.Trim();
            }
            if (chkStudentCode.Checked)
            {
                studentCode = txtStudentCode.Text.Trim();
            }
            if (chkSubmitDate.Checked)
            {
                from = dtpFrom.Value.Date;
                to = dtpTo.Value.Date;
            }
            if (chkClass.Checked)
            {
                classCode = cbClass.SelectedValue.ToString();
            }

            int totalRecords = markDB.CountMarks(testCode, studentCode, classCode, from, to);
            int totalPages = totalRecords % pageSize == 0 ? totalRecords / pageSize : (totalRecords / pageSize) + 1;

            //set the page index
            if (txtPageIndex.Text.Trim().Equals(""))
            {
                pageIndex = 1;
            }
            else
            {
                try
                {
                    pageIndex = int.Parse(txtPageIndex.Text);
                    //if page index over the total of pages then redirect to end page
                    if (pageIndex > totalPages)
                    {
                        pageIndex = totalPages;
                    }
                }
                catch (Exception ex)
                {
                    //if user enter a non-numeric to page index then redirect to first page
                    pageIndex = 1;
                }
            }
            //enable the appropriate pagging button
            EnablePageButton(pageIndex, totalPages);

            //gets the data using corressponding filter data
            var marks = markDB.GetMarks(pageIndex, pageSize, testCode, studentCode, classCode, from, to);

            //set data to DGV
            dgvMark.DataSource = marks;
            for (int i = 0; i < marks.Count; i++)
            {
                //get submission
                Submission submission = submissionDB.GetSubmission(marks[i].Test.Id, marks[i].Student.Id);
                //get essay
                Essay essay = essayDB.GetEssay(marks[i].Test.Id, marks[i].Student.Id);

                dgvMark.Rows[i].Cells["colTestCode"].Value = marks[i].Test.Code;
                dgvMark.Rows[i].Cells["colStudentCode"].Value = marks[i].Student.StudentCode;
                dgvMark.Rows[i].Cells["colClassName"].Value = marks[i].Student.Class.ClassName;
                //a test can only be submission (multiple choice) or essay => one != null when another is null
                if (submission != null)
                {
                    dgvMark.Rows[i].Cells["colSubmitDate"].Value = submission.SubmitDate.Date;
                }
                if (essay != null)
                {
                    dgvMark.Rows[i].Cells["colSubmitDate"].Value = essay.SubmitDate.Date;
                }

            }
            //set data for paging text
            txtPageIndex.Text = pageIndex.ToString();
            lbTotalPage.Text = $"/{totalPages}";
        }

        public void EnablePageButton(int pageIndex, int totalPage)
        {
            if (pageIndex > 1)
            {
                btnFirstPage.Enabled = true;
                btnPrevPage.Enabled = true;
            }
            else
            {
                btnFirstPage.Enabled = false;
                btnPrevPage.Enabled = false;
            }

            if (pageIndex < totalPage)
            {
                btnLastPage.Enabled = true;
                btnNextPage.Enabled = true;
            }
            else
            {
                btnLastPage.Enabled = false;
                btnNextPage.Enabled = false;
            }
        }

        private void FrmManageMark_Load(object sender, EventArgs e)
        {
            try
            {
                buildDGV();
                LoadClass();
                if (currentTest != null)
                {
                    chkTestCode.Checked = true;
                    txtTestCode.Text = currentTest.Code;
                }
                if (currentStudent != null)
                {
                    chkStudentCode.Checked = true;
                    txtStudentCode.Text = currentStudent.StudentCode;
                }
                if (currentClass != null)
                {
                    chkClass.Checked = true;
                    cbClass.SelectedValue = currentClass.ClassCode;
                }
                LoadMark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkTestCode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTestCode.Checked)
            {
                txtTestCode.Enabled = true;
            }
            else
            {
                txtTestCode.Enabled = false;
            }
        }

        private void chkStudentCode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStudentCode.Checked)
            {
                txtStudentCode.Enabled = true;
            }
            else
            {
                txtStudentCode.Enabled = false;
            }
        }

        private void chkClass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClass.Checked)
            {
                cbClass.Enabled = true;
            }
            else
            {
                cbClass.Enabled = false;
            }
        }

        private void chkSubmitDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubmitDate.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }
            else
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtPageIndex.Text = "1";
                LoadMark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ClearCheckBox()
        {
            chkTestCode.Checked = false;
            chkStudentCode.Checked = false;
            chkClass.Checked = false;
            chkSubmitDate.Checked = false;
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtPageIndex.Text = "1";
                ClearCheckBox();
                LoadMark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            txtPageIndex.Text = "1";
            try
            {
                LoadMark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            int currentIndex = int.Parse(txtPageIndex.Text);
            txtPageIndex.Text = $"{--currentIndex}";
            try
            {
                LoadMark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            int currentIndex = int.Parse(txtPageIndex.Text);
            txtPageIndex.Text = $"{++currentIndex}";
            try
            {
                LoadMark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            //Eg: lbTotalPage.Text = "/10" => substring from 1
            int totalPage = int.Parse(lbTotalPage.Text.Substring(1));
            txtPageIndex.Text = $"{totalPage}";
            try
            {
                LoadMark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPageIndex_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadMark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void LoadAllMark()
        {
            string testCode = null;
            string studentCode = null;
            string classCode = null;
            DateTime from = new DateTime();
            DateTime to = new DateTime();
            int pageIndex;

            //get filter value when corresponding checkbox is checked
            if (chkTestCode.Checked)
            {
                testCode = txtTestCode.Text.Trim();
            }
            if (chkStudentCode.Checked)
            {
                studentCode = txtStudentCode.Text.Trim();
            }
            if (chkSubmitDate.Checked)
            {
                from = dtpFrom.Value.Date;
                to = dtpTo.Value.Date;
            }
            if (chkClass.Checked)
            {
                classCode = cbClass.SelectedValue.ToString();
            }

            int totalRecords = markDB.CountMarks(testCode, studentCode, classCode, from, to);
            //set sz = total to get all record
            int pageSize = totalRecords;

            int totalPages = totalRecords % pageSize == 0 ? totalRecords / pageSize : (totalRecords / pageSize) + 1;

            //set the page index
            if (txtPageIndex.Text.Trim().Equals(""))
            {
                pageIndex = 1;
            }
            else
            {
                try
                {
                    pageIndex = int.Parse(txtPageIndex.Text);
                    //if page index over the total of pages then redirect to end page
                    if (pageIndex > totalPages)
                    {
                        pageIndex = totalPages;
                    }
                }
                catch (Exception ex)
                {
                    //if user enter a non-numeric to page index then redirect to first page
                    pageIndex = 1;
                }
            }
            //enable the appropriate pagging button
            EnablePageButton(pageIndex, totalPages);

            //gets the data using corressponding filter data
            var marks = markDB.GetMarks(pageIndex, pageSize, testCode, studentCode, classCode, from, to);

            //set data to DGV
            dgvMark.DataSource = marks;
            for (int i = 0; i < marks.Count; i++)
            {
                //get submission
                Submission submission = submissionDB.GetSubmission(marks[i].Test.Id, marks[i].Student.Id);
                //get essay
                Essay essay = essayDB.GetEssay(marks[i].Test.Id, marks[i].Student.Id);

                dgvMark.Rows[i].Cells["colTestCode"].Value = marks[i].Test.Code;
                dgvMark.Rows[i].Cells["colStudentCode"].Value = marks[i].Student.StudentCode;
                dgvMark.Rows[i].Cells["colClassName"].Value = marks[i].Student.Class.ClassName;
                //a test can only be submission (multiple choice) or essay => one != null when another is null
                //if(submission != null)
                //{
                //    dgvMark.Rows[i].Cells["colSubmitDate"].Value = submission.SubmitDate.Date;
                //}
                //if (essay != null)
                //{
                //    dgvMark.Rows[i].Cells["colSubmitDate"].Value = essay.SubmitDate.Date;
                //}
                dgvMark.Rows[i].Cells["colSubmitDate"].Value = submission.SubmitDate.Date;
            }
            //set data for paging text
            txtPageIndex.Text = pageIndex.ToString();
            lbTotalPage.Text = $"/{totalPages}";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //Load all to dgv
            try
            {
                LoadAllMark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(System.Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Mark Reported";
            // storing header part in Excel  
            for (int i = 1; i < dgvMark.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dgvMark.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dgvMark.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dgvMark.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dgvMark.Rows[i].Cells[j].Value.ToString();
                }
            }
            // save the application  
            try
            {
                //workbook.SaveAs(@"C:\Users\mark_report.xlsx");
                workbook.SaveAs(@"D:\mark_report.xlsx");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Exception happen when file cannot save in this location or choose dont replace old file
                //===> Do nothing and leave it to execute finally (close app) then the excel will automatically ask to save
            }
            finally
            {
                // Exit from the application
                workbook.Close();
                app.Quit();

            }

            //reload data to dgv paging
            try
            {
                LoadMark();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            frmManageTest.Closed += (s,args) => this.Close();
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
    }
}
