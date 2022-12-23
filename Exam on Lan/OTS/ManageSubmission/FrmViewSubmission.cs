using OTS.DAO;
using OTS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTS.ManageSubmission
{
    public partial class FrmViewSubmission : Form
    {
        public FrmViewSubmission()
        {
            InitializeComponent();
        }

        private List<string> FilePath(string dir, string type)
        {
            List<string> fileList = new List<string>();
            DirectoryInfo d = new DirectoryInfo(dir);

            FileInfo[] Files = d.GetFiles($"{type}");

            foreach (FileInfo file in Files)
            {
                fileList.Add(dir + "\\" + file.Name);
            }
            return fileList;
        }

        private void FrmViewSubmission_Load(object sender, EventArgs e)
        {

            

            SubmissionDBContext dBContext = new SubmissionDBContext();
            Submission s = dBContext.GetSubmission(1);

            string logName = $"{s.Student.StudentCode}_{s.Test.Code}_{s.Test.TestDate.Date}.txt";

            List<string> paths = FilePath(@"F:\", "*.txt");
            foreach(var file in paths)
            {
                if(file.Equals(logName))
                {
                    string text = File.ReadAllText(file);
                    this.textBox1.Text = text;
                    break;
                }
                this.textBox1.Text = "";
            }
            //Student
            this.textBox2.Text = s.Student.StudentCode;
            this.textBox3.Text = s.Student.FullName;
            this.textBox4.Text = s.Student.Class.ClassCode;
            //Test
            this.textBox7.Text = s.Test.Code;
            this.textBox6.Text = s.Test.Subject.SubjectCode;





            this.textBox1.ReadOnly = true;
            this.textBox1.Font = new Font(this.textBox1.Font.FontFamily, 13F);
            
        }
    }
}
