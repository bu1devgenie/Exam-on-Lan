using System;
using OTS.DAO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTS.ManageSubject
{
    public partial class frmInsert : Form
    {
        String subjectCode = "";
        String subjectName = "";
        
        String rgxsubjectCode = "^[a-zA-Z0-9]{2,50}$";
        String rgxsubjectName = "^[a-zA-Z0-9 .-]{2,255}$";
        SubjectDBContext subjectDBContext = new SubjectDBContext();
        int rowefect = 0;
        public frmInsert()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIsert_Click(object sender, EventArgs e)
        {
            try
            {
                subjectCode = txtSubjectCodeInput.Text.Trim();
                subjectName = txtSubjectNameInput.Text.Trim();
                var subjectss = new DAO.SubjectDBContext().FindSubject("FindBySubjectCodeAndName", subjectCode, subjectName).ToList();
                foreach (var s  in subjectss)
                {
                    subjectCode=s.SubjectCode;
                    subjectName=s.SubjectName;
                    throw new Exception();
                }
                if (!String.IsNullOrEmpty(subjectCode))
                {
                    if (!String.IsNullOrEmpty(subjectName))
                    {
                        if(Regex.IsMatch(subjectCode, rgxsubjectCode))
                        {
                            if(Regex.IsMatch(subjectName, rgxsubjectName))
                            {
                                rowefect = subjectDBContext.InsertSubject(subjectCode, subjectName);
                                if (rowefect > 0)
                                {
                                    MessageBox.Show("Insert successful", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Characters of subject name are not allowed ", "warring", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Characters of subject code are not allowed ", "warring", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Subject Name could not be null", "warring", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                MessageBox.Show("Subject Code could not be null","warring",MessageBoxButtons.OK,MessageBoxIcon.Error);

                }



            }
            catch(Exception ex)
            {
                MessageBox.Show($"Your Subject Code containt in data: {subjectCode}-{subjectName} ","warring",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private void frmInsert_Load(object sender, EventArgs e)
        {

        }
    }
}
