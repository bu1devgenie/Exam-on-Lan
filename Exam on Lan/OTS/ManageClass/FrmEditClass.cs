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

namespace OTS.ManageClass
{
    public partial class FrmEditClass : Form
    {
        private FrmManageClass parentFormMangageClass;
        private Class editClass;
        public FrmEditClass(FrmManageClass formManageClass, Class targetClass)
        {
            parentFormMangageClass = formManageClass;
            editClass = targetClass;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (editClass != null && editClass.ClassName.Length != 0)
            {
                try
                {
                    ClassDBContext classDBC = new ClassDBContext();
                    if (!classDBC.IsClassExist(txtClassName.Text))
                    {
                        if (classDBC.UpdateClass(new Class()
                        {
                            ClassName = txtClassName.Text,
                            ClassCode = editClass.ClassCode
                        }) > 0)
                        {
                            MessageBox.Show("Update Successful");
                            parentFormMangageClass.LoadClassData();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Duplicate class!", "Warring");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Fields are empty!", "Warring");

            }
        }

        private void FrmEditClass_Load(object sender, EventArgs e)
        {
            txtClassCode.Text = editClass.ClassCode.ToString();
            txtClassName.Text = editClass.ClassName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
