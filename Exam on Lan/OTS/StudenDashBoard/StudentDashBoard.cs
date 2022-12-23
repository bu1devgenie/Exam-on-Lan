using OTS.DAO;
using OTS.Login;
using OTS.ManageTest;
using OTS.Models;
using OTS.ReviewSubmission;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace OTS.StudenDashBoard
{
    public partial class StudentDashBoard : Form
    {
        private int stuId;
        TestDBContext tDB = new TestDBContext();
        StudentDBContext sDB = new StudentDBContext();
        public StudentDashBoard()
        {
            InitializeComponent();
        }

        public StudentDashBoard(int id)
        {
            stuId = id;
            InitializeComponent();
        }

        public void LoadData()
        {
            StudentDBContext sDb = new StudentDBContext();
            SubmissionDBContext smDB = new SubmissionDBContext();
            Dictionary<Submission, Mark> recentTest = smDB.viewListResult(stuId);
            Student s = sDb.getStudent(stuId);
            if (recentTest == null)
            {
                MessageBox.Show("No test");
                return;
            }
            var list = recentTest.Select(l => new
            {
                Code = l.Key.Test.Code,
                SubDate = l.Key.SubmitDate,
                CodeSub = l.Value.Test.Subject.SubjectName,
                Class = l.Value.Student.Class.ClassName,
                Mark = l.Value.Grade,
                IsReview = l.Key.Test.IsReview,
                SubId = l.Key.Id
            }).ToList();
            dataGridView1.DataSource = list.ToList();
            StuCode.Text = s.StudentCode;
            StuName.Text = s.FullName;
            StuClass.Text = s.Class.ClassName;
            //DataGridViewButtonColumn dgvbt = new DataGridViewButtonColumn();
            //dgvbt.HeaderText = "Review";
            //dgvbt.Text = "Review";                        // works also when bound
            //dgvbt.UseColumnTextForButtonValue = true;
            //dataGridView1.Columns.Add(dgvbt);//
            dataGridView1.CellContentClick += dataGridView1_CellClick;
            DataGridViewDisableButtonColumn but = new DataGridViewDisableButtonColumn();
            but.Name = "Review";
            but.HeaderText = "Review";
            dataGridView1.Columns.Insert(6, but);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["Review"].Value = "Review";
            }
            this.dataGridView1.Columns[5].Visible = false;
            this.dataGridView1.Columns[7].Visible = false;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                DataGridViewDisableButtonCell Review = (DataGridViewDisableButtonCell)dataGridView1.Rows[i].Cells["Review"];
                if (!list[i].IsReview)
                {
                    Review.Enabled = false;
                }
                else
                {
                    Review.Enabled = true;
                }
            }

            dataGridView1.Invalidate();
        }



        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Review")
            {
                DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells["Review"];
                if (buttonCell.Enabled)
                {
                    StudentDBContext sDb = new StudentDBContext();
                    SubmissionDBContext smDB = new SubmissionDBContext();
                    Dictionary<Submission, Mark> recentTest = smDB.viewListResult(4);
                    Student s = sDb.getStudent(4);
                    string testId = dataGridView1.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                    string subId = dataGridView1.Rows[e.RowIndex].Cells["SubId"].Value.ToString();
                    FrmReviewSubmission fRD = new FrmReviewSubmission(Int32.Parse(subId));
                    fRD.Show();
                }
            }

        }

        private void StudentDashBoard_Load(object sender, EventArgs e)
        {

            LoadData();
        }


        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //redirect to login page
            FrmLoginStudent frmLoginStudent = new FrmLoginStudent();
            frmLoginStudent.Closed += (s, args) => this.Close();
            this.Hide();
            frmLoginStudent.Show();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void TakeExam_Click(object sender, EventArgs e)
        {
            //TestDBContext tDB = new TestDBContext();
            //StudentDBContext sDB = new StudentDBContext();
            string check = exCode.Text;
            Student s = sDB.getStudent(stuId);
            List<Test> allowTests = tDB.allowTest(stuId);
            Test t = tDB.GetTest(check);
            if (t != null)
            {
                DateTime dt = DateTime.Now;
                var date = dt.Date;
                int result = DateTime.Compare(date, t.TestDate.Date);
                int result2 = TimeSpan.Compare(dt.TimeOfDay, t.StartTime);
                int result3 = TimeSpan.Compare(dt.TimeOfDay, t.EndTime);

                if (!tDB.GetTestsbyStu(stuId, t.Id))
                {
                    if (result == 0)
                    {
                        if (result2 > 0 && result3 < 0)
                        {
                            TakeTest test = new TakeTest(t,s);
                            test.Show();
                        }
                        else
                        {
                            MessageBox.Show("Exam time is not open yet");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Exam date not yet");
                    }
                } else
                {
                    MessageBox.Show("Exam exist in your exam !");
                }
            }
            else
            {
                MessageBox.Show("Exam Code not exist !");

            }
        }

        private void exCode_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
    {
        public DataGridViewDisableButtonColumn()
        {
            this.CellTemplate = new DataGridViewDisableButtonCell();
        }
    }

    public class DataGridViewDisableButtonCell : DataGridViewButtonCell
    {
        private bool enabledValue;
        public bool Enabled
        {
            get
            {
                return enabledValue;
            }
            set
            {
                enabledValue = value;
            }
        }

        public override object Clone()
        {
            DataGridViewDisableButtonCell cell = (DataGridViewDisableButtonCell)base.Clone();
            cell.Enabled = this.Enabled;
            return cell;
        }

        public DataGridViewDisableButtonCell()
        {
            this.enabledValue = true;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState,
                                        object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
                                        DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (!this.enabledValue)
            {
                if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                {
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                }

                Rectangle buttonArea = cellBounds;
                Rectangle buttonAdjustment = this.BorderWidths(advancedBorderStyle);
                buttonArea.X += buttonAdjustment.X;
                buttonArea.Y += buttonAdjustment.Y;
                buttonArea.Height -= buttonAdjustment.Height;
                buttonArea.Width -= buttonAdjustment.Width;

                ButtonRenderer.DrawButton(graphics, buttonArea, PushButtonState.Disabled);

                if (this.FormattedValue is String)
                {
                    TextRenderer.DrawText(graphics, (string)this.FormattedValue, this.DataGridView.Font, buttonArea, SystemColors.GrayText);
                }
            }
            else
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            }
        }
    }
}

