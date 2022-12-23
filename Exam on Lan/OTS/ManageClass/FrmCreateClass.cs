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
    public partial class FrmCreateClass : Form
    {
        private FrmManageClass parentFormManageClass;
        public FrmCreateClass(FrmManageClass formManageClass)
        {
            parentFormManageClass = formManageClass;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newClassCode = txtClassCode.Text.Trim();
            string newClassName = txtClassName.Text.Trim();
            if (!(newClassCode.Length == 0 || newClassName.Length == 0)) {
                try
                {
                    ClassDBContext classDBC = new ClassDBContext();
                    if (!classDBC.IsClassExist(newClassCode))
                    {
                        if (classDBC.AddClass(new Class() { ClassCode = newClassCode, ClassName = newClassName }) > 0)
                        {
                            MessageBox.Show("Added Class Succesful!");
                            this.Close();
                            parentFormManageClass.LoadClassData();
                        }
                    }else
                    {
                        MessageBox.Show("Class Code is duplicate!", "Warning");
                    }
            } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                } 
            } else
            {
                MessageBox.Show("Fields must not empty!", "Warning");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
