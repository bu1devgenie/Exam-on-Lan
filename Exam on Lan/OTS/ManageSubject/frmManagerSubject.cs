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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using OTS.Dashboard;

namespace OTS.ManageSubject
{
    public partial class frmManagerSubject : Form
    {
        String subjectCode = "";
        String subjectName = "";
        String option = "";
        String rgxsubjectCode = "^[a-zA-Z0-9]{2,50}$";
        String rgxsubjectName = "^[a-zA-Z0-9 .-]{2,255}$";
        int rowselected = 0;
        ManageSubject.frmInsert insert;
        SubjectDBContext subjectDBContext;
        //

        public frmManagerSubject()
        {
            InitializeComponent();

        }
        Subject target = null;
        private void ClearData()
        {
            dgvManageSubject.Rows.Clear();
            txtSubjectCode.Text = "";
            txtSubjectName.Text = "";
            target = null;
        }
        public String getOption(String subjectCode, String subjectName)
        {
            String result = "";
            if (!String.IsNullOrEmpty(subjectCode) && String.IsNullOrEmpty(subjectName))
            {
                result = "FindBySubjectCode";
            }
            else if (!String.IsNullOrEmpty(subjectName) && String.IsNullOrEmpty(subjectCode))
            {
                result = "FindBySubjectName";
            }
            else if (!String.IsNullOrEmpty(subjectName) && !String.IsNullOrEmpty(subjectCode))
            {
                result = "FindBySubjectCodeAndName";
            }
            else
            {
                result = "getAll";
            }
            return result;
        }

        private void LoadSubject(String option)
        {
            subjectDBContext = new SubjectDBContext();
            var subjectss = new DAO.SubjectDBContext().FindSubject(option, subjectCode, subjectName).ToList();
            foreach (var s in subjectss)
            {
                dgvManageSubject.Rows.Add(s.SubjectCode, s.SubjectName);
            }

        }
        private void ManagerSubject_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSubject("getAll");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnFind_Click(object sender, EventArgs e)
        {

            SubjectDBContext subjectDBC = new SubjectDBContext();
            try
            {
                subjectCode = txtSubjectCode.Text;
                subjectName = txtSubjectName.Text;
                option = getOption(subjectCode, subjectName);
                if (!String.IsNullOrEmpty(option))
                {
                    ClearData();
                    LoadSubject(option);
                }
                else
                {
                    throw new Exception();
                }
                rowselected = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Value", "Warnning",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                insert = new ManageSubject.frmInsert();
                insert.Show();
                LoadSubject("getAll");
                rowselected = 0;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (rowselected >0)
                {
                    frmUpdate frmUpdate = new frmUpdate(txtSubjectCode.Text.Trim(), txtSubjectName.Text.Trim());

                    frmUpdate.Show();
                    
                }
                else
                {
                    MessageBox.Show("Plss select 1 row for update","warrning",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ClearData();
            LoadSubject("getAll");
            rowselected = 0;
        }

        

        private void dgvManageSubject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgvManageSubject.SelectedRows[0];
                subjectDBContext = new SubjectDBContext();

                String subjectCodes = row.Cells[0].Value.ToString();

                target = subjectDBContext.GetSubject("FindBySubjectCode", subjectCodes, "");
                if (target != null)
                {
                    txtSubjectCode.Text = target.SubjectCode.ToString();
                    txtSubjectName.Text = target.SubjectName.ToString();
                    rowselected++;
                }
            }
            catch(Exception ex)
            {
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SubjectDBContext subjectDBC = new SubjectDBContext();

            try
            {

                if (rowselected > 0)
                {
                    DialogResult result= MessageBox.Show($"Are you sure to delete this Subject: {txtSubjectCode.Text.Trim()}-{txtSubjectName.Text.Trim()}", "warrning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            subjectDBC = new SubjectDBContext();
                            subjectDBC.DeleteSubject(txtSubjectCode.Text.Trim());
                            break;
                        case DialogResult.No:
                            break;
                    }
                    
                }
                else
                {
                    MessageBox.Show("Plss select 1 row for Delete", "warrning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ClearData();
            LoadSubject("getAll");
            rowselected = 0;
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
