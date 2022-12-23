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
using OTS.DAO;
using OTS.Models;

namespace OTS.ManageStudent
{
    public partial class frmInsertStudent : Form
    {
        int Id = 0;
        String FullName = "";
        String Password = "";
        DateTime Dob;
        String StudentCode = "";
        String ClassCode = "";
        String regexName = "^( ?[a-zA-Z]+){1,}$";
        String regexPassword = "^[a-zA-z0-9]+$";
        String regexStudentCode = "^[a-zA-z0-9]+$";
        DAO.StudentDBContext student;



        public frmInsertStudent()
        {
            InitializeComponent();

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int rowefected = 0;
            StudentDBContext studentDBContext = new StudentDBContext();
            try
            {

                FullName = txtFullName.Text.Trim();
                if (Regex.IsMatch(FullName, regexName) && !String.IsNullOrEmpty(FullName))
                {
                    Password = txtPassword.Text.Trim();
                    if (Regex.IsMatch(Password, regexPassword) && !String.IsNullOrEmpty(Password))
                    {

                        StudentCode = txtStudentCode.Text.Trim();
                        if (Regex.IsMatch(StudentCode, regexStudentCode) && !String.IsNullOrEmpty(StudentCode))
                        {
                            ClassCode = txtClasscode.Text.Trim();

                            if (Regex.IsMatch(ClassCode, regexStudentCode) && !String.IsNullOrEmpty(ClassCode))
                            {
                                Dob = dtPDob.Value;

                                List<Class> classes = new List<Class>();
                                classes = studentDBContext.getClassCode();
                                foreach (Class c in classes)
                                {
                                    if (ClassCode.Equals(c.ClassCode))
                                    {
                                        rowefected = studentDBContext.InsertStudent(FullName, Password, Dob, StudentCode, ClassCode);
                                        MessageBox.Show("Insert Sucessfull", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;

                                    }


                                }
                                if (rowefected == 0)
                                {
                                    MessageBox.Show("Class Code doesn't exsit!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                }
                            }
                            else
                            {
                                throw new Exception();
                            }

                        }
                        else
                        {
                            throw new Exception();
                        }

                    }
                    else
                    {
                        throw new Exception();
                    }



                }
                else
                {
                    throw new Exception();
                }






            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
