using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OTS.DAO;

namespace OTS.ManageSubject
{
    
    public partial class frmUpdate : Form
    {
        String oldsubjectCode = "";
        String oldsubjectName = "";
        String newsubjectCode = "";
        String newsubjectName = "";
        String rgxsubjectCode = "^[a-zA-Z0-9]{2,50}$";
        String rgxsubjectName = "^[a-zA-Z0-9 .-]{2,255}$";
        int rowefect=0;
        SubjectDBContext subjectDBContext;
        public frmUpdate( String subjectCode,String subjectName)
        {
            InitializeComponent();
            oldsubjectCode = subjectCode;
            oldsubjectName = subjectName;
            txtSubjectCodeInput.Text = oldsubjectCode.Trim().ToString();
            txtSubjectNameInput.Text = oldsubjectName.Trim().ToString();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public String getOption(String oldCode,String oldName,String newCode,String newName)
        {
            if (!oldCode.Equals(newCode) && oldCode != null && (oldName.Equals(newName)||String.IsNullOrEmpty(newName)))
            {
                return "UpdateCode";
            }else if (!oldName.Equals(newName) && oldName != null && (oldCode.Equals(oldCode) || String.IsNullOrEmpty(oldCode)))
            {
                return "UpdateName";
            }
            else if(!oldCode.Equals(newCode) && oldCode != null && !oldName.Equals(newName) && oldName != null)
            {
                return "UpdateCodeAndName";
            }
            else 
            {
                return "false";
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String option = "";
            subjectDBContext = new SubjectDBContext();
            try
            {
                newsubjectCode=txtSubjectCodeInput.Text.Trim();
                newsubjectName=txtSubjectNameInput.Text.Trim(); 
                option=getOption(oldsubjectCode,oldsubjectName,newsubjectCode,newsubjectName);
                if (!option.Equals("false"))
                {
                    rowefect=subjectDBContext.UpdateSubject(option, oldsubjectCode, oldsubjectName, newsubjectCode, newsubjectName);
                    if (rowefect > 0)
                    {
                        MessageBox.Show("Update Sucessfull!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Plss Add the value you want to change for the Subject", "warrning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

           
        }
    }
}
