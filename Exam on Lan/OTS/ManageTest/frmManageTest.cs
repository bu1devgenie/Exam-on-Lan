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

namespace OTS.ManageTest
{
    public partial class frmManageTest : Form
    {
        public frmManageTest()
        {
            InitializeComponent();
        }

        TestDBContext testDB = new TestDBContext();
        SubjectDBContext subjectDB = new SubjectDBContext();

        public void buildDGV()
        {
            dgvTest.AutoGenerateColumns = false;
            dgvTest.Columns.Clear();

            DataGridViewTextBoxColumn testId = new DataGridViewTextBoxColumn();
            testId.DataPropertyName = "Id";
            testId.HeaderText = "Id";
            testId.Width = 30;
            dgvTest.Columns.Add(testId);

            DataGridViewTextBoxColumn testCode = new DataGridViewTextBoxColumn();
            testCode.DataPropertyName = "Code";
            testCode.HeaderText = "Code";
            dgvTest.Columns.Add(testCode);

            DataGridViewTextBoxColumn subject = new DataGridViewTextBoxColumn();
            subject.DataPropertyName = "Subject";
            subject.HeaderText = "Subject";
            dgvTest.Columns.Add(subject);

            DataGridViewTextBoxColumn testDate = new DataGridViewTextBoxColumn();
            testDate.DataPropertyName = "TestDate";
            testDate.HeaderText = "TestDate";
            testDate.Width = 70;
            dgvTest.Columns.Add(testDate);

            DataGridViewTextBoxColumn createDate = new DataGridViewTextBoxColumn();
            createDate.DataPropertyName = "CreateDate";
            createDate.HeaderText = "CreateDate";
            createDate.Width = 70;
            dgvTest.Columns.Add(createDate);

            DataGridViewTextBoxColumn status = new DataGridViewTextBoxColumn();
            status.HeaderText = "Status";
            status.Width = 70;
            dgvTest.Columns.Add(status);

            DataGridViewButtonColumn buttonCol1 = new DataGridViewButtonColumn();
            buttonCol1.Text = "Start";
            buttonCol1.Width = 70;
            dgvTest.Columns.Add(buttonCol1);

            DataGridViewButtonColumn buttonCol2 = new DataGridViewButtonColumn();
            buttonCol2.Text = "End";
            buttonCol2.Width = 70;
            dgvTest.Columns.Add(buttonCol2);

            DataGridViewButtonColumn buttonCol3 = new DataGridViewButtonColumn();
            buttonCol3.Text = "View detail";
            buttonCol3.Width = 70;
            dgvTest.Columns.Add(buttonCol3);

            DataGridViewButtonColumn buttonCol4 = new DataGridViewButtonColumn();
            buttonCol4.Text = "View grades";
            dgvTest.Columns.Add(buttonCol4);

        }

        public void LoadSubject()
        {
            try
            {
                var subjects = subjectDB.GetSubjects();
                cbSubject.DataSource = subjects;
                cbSubject.DisplayMember = "SubjectCode";
                cbSubject.ValueMember = "SubjectCode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        public void LoadTest()
        {
            string subjectCode = null;
            DateTime createFrom = new DateTime();
            DateTime createTo = new DateTime();
            DateTime testFrom = new DateTime();
            DateTime testTo = new DateTime();
            string status = null;
            int pageIndex;
            int pageSize = 10;

            //get filter value when corresponding checkbox is checked
            if (chkSubject.Checked)
            {
                subjectCode = cbSubject.SelectedValue.ToString();
            }
            if (chkCreate.Checked)
            {
                createFrom = dtpCreateFrom.Value.Date;
                createTo = dtpCreateTo.Value.Date;
            }
            if (chkTest.Checked)
            {
                testFrom = dtpTestFrom.Value.Date;
                testTo = dtpTestTo.Value.Date;
            }
            if (chkStatus.Checked)
            {
                status = cbStatus.SelectedItem.ToString();
            }

            int totalRecords = testDB.CountTests(subjectCode, createFrom, createTo,
                    testFrom, testTo, status);
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
            var tests = testDB.GetTests(pageIndex, pageSize, subjectCode
                , createFrom, createTo, testFrom, testTo, status);

            //set data to DGV
            dgvTest.DataSource = tests;
            for (int i = 0; i < tests.Count; i++)
            {
                //Set status for status cell
                if (tests[i].TestDate == DateTime.Now.Date
                    && tests[i].StartTime <= DateTime.Now.TimeOfDay
                    && tests[i].EndTime > DateTime.Now.TimeOfDay)
                {
                    dgvTest.Rows[i].Cells[5].Value = "Started";
                }
                else if (tests[i].TestDate < DateTime.Now.Date
                    || (tests[i].TestDate == DateTime.Now.Date
                    && tests[i].EndTime < DateTime.Now.TimeOfDay))
                {
                    dgvTest.Rows[i].Cells[5].Value = "Ended";
                }
                else
                {
                    dgvTest.Rows[i].Cells[5].Value = "Not started";
                }
                //Set text for button cell
                dgvTest.Rows[i].Cells[6].Value = "Start";
                dgvTest.Rows[i].Cells[7].Value = "End";
                dgvTest.Rows[i].Cells[8].Value = "View detail";
                dgvTest.Rows[i].Cells[9].Value = "View grades";
            }
            //set data for paging text
            txtPageIndex.Text = pageIndex.ToString();
            lbTotalPage.Text = $"/{totalPages}";
        }

        private void frmManageTest_Load(object sender, EventArgs e)
        {
            try
            {
                buildDGV();
                LoadSubject();
                LoadTest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ClearCheckBox()
        {
            chkSubject.Checked = false;
            chkCreate.Checked = false;
            chkTest.Checked = false;
            chkStatus.Checked = false;
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtPageIndex.Text = "1";
                ClearCheckBox();
                LoadTest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtPageIndex.Text = "1";
                LoadTest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkSubject_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubject.Checked)
            {
                cbSubject.Enabled = true;
            }
            else
            {
                cbSubject.Enabled = false;
            }
        }

        private void chkCreate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCreate.Checked)
            {
                dtpCreateFrom.Enabled = true;
                dtpCreateTo.Enabled = true;
            }
            else
            {
                dtpCreateFrom.Enabled = false;
                dtpCreateTo.Enabled = false;
            }
        }

        private void chkTest_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTest.Checked)
            {
                dtpTestFrom.Enabled = true;
                dtpTestTo.Enabled = true;
            }
            else
            {
                dtpTestFrom.Enabled = false;
                dtpTestTo.Enabled = false;
            }
        }

        private void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStatus.Checked)
            {
                cbStatus.Enabled = true;
                cbStatus.SelectedIndex = 0;
            }
            else
            {
                cbStatus.Enabled = false;
                cbStatus.SelectedIndex = 0;
            }
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            txtPageIndex.Text = "1";
            try
            {
                LoadTest();
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
                LoadTest();
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
                LoadTest();
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
                LoadTest();
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
                LoadTest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //col0: test id
            //col6: start button
            //col7: end button
            //col8: detail button
            int testID = int.Parse(dgvTest.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (e.ColumnIndex == 6)
            {
                btnStart_Click(testID);
            }
            else if (e.ColumnIndex == 7)
            {
                btnEnd_Click(testID);
            }
            else if (e.ColumnIndex == 8)
            {
                btnDetail_Click(testID);
            }
            else if (e.ColumnIndex == 9)
            {
                btnGrade_Click(testID);
            }
        }

        private void btnGrade_Click(int testID)
        {
            //hide current form and show again when close view detail test form
            this.Hide();
            FrmManageMark frmManageMark = new FrmManageMark(testDB.GetTest(testID));
            frmManageMark.FormClosed += (s, args) => this.Show();
            frmManageMark.Show();
        }

        private void btnDetail_Click(int testID)
        {
            //hide current form and show again when close view detail test form
            this.Hide();
            FrmViewTest frmViewTest = new FrmViewTest(testID);
            frmViewTest.FormClosed += (s, args) => this.Show();
            frmViewTest.Show();
        }

        private void btnEnd_Click(int testID)
        {
            Test test = testDB.GetTest(testID);

            // if the test is already ended, inform to user and do nothìng
            if (test.TestDate < DateTime.Now.Date
                || (test.TestDate == DateTime.Now.Date
                && test.EndTime < DateTime.Now.TimeOfDay))
            {
                MessageBox.Show("This test is already ended");
            }
            else
            {
                if (MessageBox.Show("Are you sure to end the test now?"
                , "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    //if the test is not started yet, update the start time to current time
                    if (test.TestDate > DateTime.Now.Date)
                    {
                        test.TestDate = DateTime.Now.Date;
                    }
                    if (test.TestDate == DateTime.Now.Date
                        && test.StartTime > DateTime.Now.TimeOfDay)
                    {
                        test.StartTime = DateTime.Now.TimeOfDay;
                    }
                    //update endtime to current time
                    test.EndTime = DateTime.Now.TimeOfDay;
                    //update to DB
                    testDB.ChangeTestTime(test);
                    //reload form
                    try
                    {
                        LoadTest();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnStart_Click(int testID)
        {
            Test test = testDB.GetTest(testID);
            //if the test is already started, inform to user and do nothing
            if (test.TestDate == DateTime.Now.Date
                && test.StartTime <= DateTime.Now.TimeOfDay
                && test.EndTime > DateTime.Now.TimeOfDay)
            {
                MessageBox.Show("This test is already started");
            }
            else
            {
                frmStartTest frmStart = new frmStartTest(test, this);
                frmStart.Show();
            }
        }

        private void frmManageTest_Activated(object sender, EventArgs e)
        {
            try
            {
                LoadTest();
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
