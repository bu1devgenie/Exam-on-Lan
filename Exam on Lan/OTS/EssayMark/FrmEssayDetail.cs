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

namespace OTS.EssayMark
{
    public partial class FrmEssayDetail : Form
    {
        private int essayId;
        public FrmEssayDetail(int essayId)
        {
            this.essayId = essayId;
            InitializeComponent();
            dtpSubmitTime.Format = DateTimePickerFormat.Custom;
            dtpSubmitTime.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtpDuration.Format = DateTimePickerFormat.Custom;
            dtpDuration.CustomFormat = "HH:mm:ss";
            nudFontSize.Value = (decimal) richtxtQuestion.Font.Size;
        }

        public void LoadEssaySubmit()
        {
            try
            {
                Essay essay = new EssayDBContext().GetEssay(essayId);
                txtId.Text = essay.Id.ToString();
                richtxtQuestion.Text = essay.Question.Content;
                richtxtSolution.Text = essay.Content;
                dtpSubmitTime.Value = essay.SubmitDate;
                DateTime dt = new DateTime(2022, 03, 14);
                dtpDuration.Value = dt.Add(essay.Duration);
            }catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void FrmEssayMark_Load(object sender, EventArgs e)
        {
            LoadEssaySubmit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nudFontSize_ValueChanged(object sender, EventArgs e)
        {
            richtxtQuestion.Font = new Font(richtxtQuestion.Font.FontFamily, (float) nudFontSize.Value);//Edit your size asper your requirement. it's float value
            richtxtSolution.Font = new Font(richtxtSolution.Font.FontFamily, (float) nudFontSize.Value);//Edit your size asper your requirement. it's float value

        }
    }
}
