using OTS.DAO;
using OTS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTS.ManageTest
{
    public partial class frmStartTest : Form
    {
        private Test currentTest;
        frmManageTest frmManageTest;
        TestDBContext testDB = new TestDBContext();

        public frmStartTest()
        {
            InitializeComponent();
        }

        public frmStartTest(Test test, frmManageTest frmManage)
        {
            currentTest = test;
            frmManageTest = frmManage;
            InitializeComponent();
        }

        private void frmStartTest_Load(object sender, EventArgs e)
        {
            lbTestCode.Text = currentTest.Code;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool Validate()
        {
            if (dtpStart.Value < DateTime.Now)
            {
                MessageBox.Show("Start date time must be after current time", "Warning"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dtpEnd.Value.Date != dtpStart.Value.Date)
            {
                MessageBox.Show("End date must be in the same date as start date", "Warning"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dtpEnd.Value < DateTime.Now)
            {
                MessageBox.Show("End date time must be after current time", "Warning"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dtpEnd.Value < dtpStart.Value)
            {
                MessageBox.Show("End date time must be after Start date time", "Warning"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //validate date time
            if (Validate())
            {
                currentTest.TestDate = dtpStart.Value.Date;
                currentTest.StartTime = dtpStart.Value.TimeOfDay;
                currentTest.EndTime = dtpEnd.Value.TimeOfDay;
                try
                {
                    testDB.ChangeTestTime(currentTest);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    frmManageTest.Activate();
                    this.Close();
                }
            }
        }
    }
}
