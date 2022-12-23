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

namespace OTS.ManageQuestion
{
    public partial class EditQuestion : Form
    {
        public Dictionary<TextBox, CheckBox> list = new Dictionary<TextBox, CheckBox>();
        private ListQuestionBank lq;
        private Question editQues;
        public EditQuestion(Question targerQues)
        {
            editQues = targerQues;
            InitializeComponent();
        }
        public EditQuestion(ListQuestionBank listquest, Question targerQues)
        {
            lq = listquest;
            editQues = targerQues;
            InitializeComponent();
        }

        private void EditQuestion_Load(object sender, EventArgs e)
        {
            LevelDBContext levelDB = new LevelDBContext();
            SubjectDBContext sDb = new SubjectDBContext();
            TypeDBContext tDb = new TypeDBContext();
            List<Level> levels = levelDB.GetLevels();
            List<Subject> subjects = sDb.subjects();
            List<Models.Type> types = tDb.GetTypes();
            Levels.DataSource = levels;
            Levels.DisplayMember = "name";
            Levels.Text = editQues.Level.Name;
            Levels.ValueMember = "id";
            Subjects.DataSource = subjects;
            Subjects.DisplayMember = "SubjectCode";
            Subjects.Text = editQues.Subject.SubjectCode;
            Subjects.ValueMember = "SubjectCode";
            Types.DataSource = types;
            Types.DisplayMember = "name";
            Types.Text = editQues.Type.Name;
            Types.ValueMember = "id";
            Content.Text = editQues.Content;
            foreach (var item in editQues.Answers)
            {
                TextBox t = new TextBox();
                CheckBox c = new CheckBox();
                t.Size = new System.Drawing.Size(314, 27);
                c.Size = new System.Drawing.Size(23, 27);
                t.Text = item.Content;
                c.Checked = item.IsCorrect;
                list.Add(t, c);
            }
            foreach (var item in list)
            {

                flowLayoutPanel1.Controls.Add(item.Key);
                flowLayoutPanel1.Controls.Add(item.Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextBox t = new TextBox();
            CheckBox c = new CheckBox();
            t.Size = new System.Drawing.Size(314, 27);
            c.Size = new System.Drawing.Size(23, 27);
            flowLayoutPanel1.Controls.Add(t);
            flowLayoutPanel1.Controls.Add(c);
            list.Add(t, c);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Remove(list.Last().Key);
            flowLayoutPanel1.Controls.Remove(list.Last().Value);
            list.Remove(list.Last().Key);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LevelDBContext lDB = new LevelDBContext();
            AnswerDBContext aDB = new AnswerDBContext();
            SubjectDBContext sDB = new SubjectDBContext();
            TypeDBContext tDB = new TypeDBContext();
            if (editQues.Content != null)
            {
                try
                {
                    string content = Content.Text;
                    string lId = Levels.SelectedValue.ToString();
                    string subjectcode = Subjects.SelectedValue.ToString();
                    string typeId = Types.SelectedValue.ToString();
                    Level l = lDB.GetLevelById(short.Parse((lId)));
                    Subject s = sDB.getSubbyId(subjectcode);
                    Models.Type t = tDB.GetTypeById(Int32.Parse(typeId));
                    QuestionDBContext qDb = new QuestionDBContext();
                    Question q = new Question() { Id = editQues.Id, Content = content, Level = l, Subject = s, Type = t };
                    int result = qDb.UpdateQues(q);
                    if (result > 0)
                    {
                        foreach (var a in list)
                        {
                            Answer ans = new Answer();
                            ans.Content = a.Key.Text;
                            ans.IsCorrect = a.Value.Checked;
                            ans.Question = q;
                            int i = 0;
                            editQues.Answers[i].Content = a.Key.Text;
                            editQues.Answers[i].IsCorrect = a.Value.Checked;
                            aDB.UpdateAns(editQues.Answers[i]);
                            i++;
                        }
                        MessageBox.Show("Update Successful");
                        this.Close();
                        if (lq != null)
                            lq.loadQues();
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
    }
}
