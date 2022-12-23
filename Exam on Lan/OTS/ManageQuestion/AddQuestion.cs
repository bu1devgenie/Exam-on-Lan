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
    public partial class AddQuestion : Form
    {
        public Dictionary<TextBox, CheckBox> list = new Dictionary<TextBox, CheckBox>();
        private ListQuestionBank lq;
        private List<Answer> answers;
        public AddQuestion(ListQuestionBank listquest)
        {
            lq = listquest;
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddQuestion_Load(object sender, EventArgs e)
        {
            LevelDBContext levelDB = new LevelDBContext();
            SubjectDBContext sDb = new SubjectDBContext();
            TypeDBContext tDb = new TypeDBContext();
            List<Level> levels = levelDB.GetLevels();
            List<Subject> subjects = sDb.subjects();
            List<Models.Type> types = tDb.GetTypes();
            Levels.DataSource = levels;
            Levels.DisplayMember = "name";
            Subjects.DataSource = subjects;
            Subjects.DisplayMember = "SubjectCode";
            Types.DataSource = types;
            Types.DisplayMember = "name";
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

        private void Types_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Types.SelectedIndex == 1)
            {
                foreach (var item in list)
                {
                    item.Key.Enabled = false;
                }
            }
            else
            {
                foreach (var item in list)
                {
                    item.Key.Enabled = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Remove(list.Last().Key);
            flowLayoutPanel1.Controls.Remove(list.Last().Value);
            list.Remove(list.Last().Key);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Level level = (Level)Levels.SelectedItem;
            Models.Type type = (Models.Type)Types.SelectedItem;
            Subject subject = (Subject)Subjects.SelectedItem;
            string content = Content.Text;
            if (!(content.Length == 0))
            {
                try
                {
                    QuestionDBContext qDb = new QuestionDBContext();
                    AnswerDBContext aDb = new AnswerDBContext();
                    Question q = new Question() { Level = level, Content = content, Type = type, Subject = subject };
                    answers = new List<Answer>();
                    int id = qDb.AddQues(q);
                    if (id > 0)
                    {
                        foreach (var item in list)
                        {
                            Answer a = new Answer();
                            q.Id = id;
                            a.Question = q;
                            a.Content = item.Key.Text;
                            a.IsCorrect = item.Value.Checked;
                            aDb.AddAns(a);
                            answers.Add(a);
                        }
                        q.Answers = answers;
                        MessageBox.Show("Added Question Succesful!");
                        this.Close();
                        lq.loadQues();
                    }
                    //foreach (var item in list)
                    //{
                    //    Answer a = new Answer();
                    //    a.Question = q;
                    //    a.Content = item.Key.Text;
                    //    a.IsCorrect = item.Value.Checked;
                    //    aDb.AddAns(a);
                    //    answers.Add(a);
                    //}

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Fields must not empty!", "Warning");
            }
        }
    }
}
