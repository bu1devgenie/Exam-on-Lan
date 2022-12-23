using OTS.DAO;
using OTS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTS.ManageTest
{

    
    public partial class TakeTest : Form
    {
        class ThreadSafeRandom
        {
            [ThreadStatic] private Random Local;

            public Random ThisThreadsRandom
            {
                get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
            }
        }

        class MyExtensions
        {
            public void Shuffle<T>(IList<T> list)
            {
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    int k = new ThreadSafeRandom().ThisThreadsRandom.Next(n + 1);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        }

        Test test;
        Student student;

        public TakeTest(Test t, Student s)
        {
            test = t;
            student = s;
            InitializeComponent();
            //Random_Img();
            Process_Load(@"D:\logfinal.txt");
        }
        public TakeTest()
        {
            InitializeComponent();
            //Random_Img();
            Process_Load(@"D:\logfinal.txt");
        }

        //Copy
        public void Shuffle<T>(IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // Luu cau hoi va cau tra loi cua nguoi dung
        private Dictionary<Question, Answer> questionanswerPairs = new Dictionary<Question, Answer>();
        private Dictionary<Question, Essay> questionessayPairs = new Dictionary<Question, Essay>();

        private void TakeTest_Load(object sender, EventArgs e)
        {
            Deploy(test, student);
            

        }


        private void Deploy(Test t, Student s)
        {
            InitLabel(t, s);

            // Tinh thoi gian chenh lech
            int timeDiff = (int)Math.Round(t.EndTime.TotalMinutes - DateTime.Now.TimeOfDay.TotalMinutes);

            // Thoi gian ly thuyet cua bai thi
            int testTime = t.Duration.Hours * 60 + t.Duration.Minutes;

            // Goi timer va assign cron-job
            CalculateTimer(testTime, timeDiff);

            // Set time kieu cron-job
            /*
             * Trong truong hop ma timeDiff < 0 thi form se tat ngay lap tuc
             */

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            DateTime closeDownAt = DateTime.Now.AddMinutes(timeDiff);
            timer.Interval = 30000;   //30 seconds in milliseconds 
                                      //timer.Tick += new EventHandler(Tick_Tack);

            timer.Tick += new EventHandler(delegate (object sender, EventArgs e)
            {
                if (DateTime.Now >= closeDownAt)
                {
                    Application.Exit();
                }
            });
            timer.Start();


            List<QuestionTest> qts = t.QuestionTests;
            Shuffle(qts);
            foreach (var q in qts)
            {
                List<Answer> ans = q.Question.Answers;
                Shuffle(ans);

                //Button b = new Button() {
                //    Text = $"{t.QuestionTests.IndexOf(q)}",
                //    Size = new Size(27, 27),
                //    BackColor=Color.LightGray , Margin = new Padding(0)};
                //b.Click += new EventHandler(delegate (object sender, EventArgs e) { this.tabControl1.SelectedIndex = t.QuestionTests.IndexOf(q); });
                //this.flowLayoutPanel1.Controls.Add(b);

                TabPage p = new TabPage() { Name = $"{t.QuestionTests.IndexOf(q)}", Text = $"Q{t.QuestionTests.IndexOf(q) + 1}", BackColor = Color.Transparent };


                dynamic answer;

                /*new CheckedListBox().*/
                
                if (q.Question.Type.Id == 1)
                {

                    Dictionary<int, string> bind_alphabet = new Dictionary<int, string>();
                    answer = new CheckedListBox();
                    //answer.ItemCheck += new EventHandler(delegate (object sender, EventArgs e) {
                    //    for (int ix = 0; ix < checkedListBox1.Items.Count; ++ix)
                    //        if (ix != e.Index) checkedListBox1.SetItemChecked(ix, false);
                    //});
                    answer.Font = new Font(answer.Font.FontFamily, 13F);
                    answer.CheckOnClick = true;
                    answer.Width = 150;
                    answer.Height = 610;

                    answer.SelectedIndexChanged += new EventHandler(delegate (object sender, EventArgs e) {
                        int index = answer.SelectedIndex;
                        int count = answer.Items.Count;
                        for(int x=0; x<count;x++)
                        {
                            if(index != x)
                            {
                                answer.SetItemCheckState(x, CheckState.Unchecked);
                            }
                        }
                    });

                    questionanswerPairs.Add(q.Question, null);


                    for (int i = 0; i < q.Question.Answers.Count; i++)
                    {
                        answer.Items.Add(alphabet[i].ToString(), CheckState.Unchecked);
                        bind_alphabet.Add(i, ans[i].Content);
                    }
            
                    
                    answer.SelectedIndexChanged += new EventHandler(delegate (object sender, EventArgs e)
                    {
                    /* NeedsToBeDrawn = true; */



                    questionanswerPairs[q.Question] = new Answer() { Content = bind_alphabet[answer.SelectedIndex] 
                        
                    };
                        //String answerContent = System.Text.Json.JsonSerializer.Serialize(answer.CheckedItems);
                        //questionanswerPairs[$"{q.Question.Content}"] = ans;
                        //ProcessWrite(@"F:\loglocal.txt", System.Text.Json.JsonSerializer.Serialize(questionanswerPairs), FileMode.OpenOrCreate);
                    });

                }

                else
                {



                    answer = new TextBox();
                    answer.Multiline = true;
                    answer.BackColor = Color.White;
                    answer.Width = 1000;
                    answer.Height = 610;
                    answer.TextChanged += new EventHandler(delegate (object sender, EventArgs e)
                    {
                        
                       questionessayPairs.Add(q.Question, new Essay() { Content = answer.SelectedItem.ToString() });
                        /*NeedsToBeDrawn = true; */
                        //questionanswerPairs[$"{q.Question.Content}"] = answer.Text;
                        //ProcessWrite(@"F:\loglocal.txt", answer.Text, FileMode.OpenOrCreate);
                    });

                }

                // Local Function
                void SelectionChangedEventHandler(object sender, EventArgs ev)
                {
                    //this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(0, 0);
                    Cursor.Clip = new Rectangle(this.Location, this.Size);

                }

                TextBox questionContent = new TextBox();
                questionContent.Multiline = true;
                questionContent.ReadOnly = true;
                questionContent.Cursor = Cursors.No;
                questionContent.Font = new Font(questionContent.Font.FontFamily, 15F);
                questionContent.BackColor = Color.White;
                questionContent.Text = q.Question.Content;

                questionContent.Focus();

                questionContent.MouseLeave += new EventHandler(SelectionChangedEventHandler);
                questionContent.Click += new EventHandler(SelectionChangedEventHandler);
                questionContent.MouseEnter += new EventHandler(SelectionChangedEventHandler);
                questionContent.Click += new EventHandler(delegate (object sender, EventArgs e) { HideCaret(questionContent.Handle); });



                for (int i = 0; i < q.Question.Answers.Count; i++)
                {
                    questionContent.Text += $"\r\n{alphabet[i]}.  {ans[i].Content}";
                }


                if (answer is CheckedListBox)
                {
                    questionContent.Width = 1350;
                    questionContent.Height = 610;
                }
                else
                {
                    questionContent.Width = 545;
                    questionContent.Height = 610;
                }


                FlowLayoutPanel flp = new FlowLayoutPanel();

                flp.Width = 1560;
                flp.Height = 610;


                flp.Controls.Add(answer);
                flp.Controls.Add(questionContent);


                p.Controls.Add(flp);


                this.page_question1.TabPages.Add(p);

                this.page_question1.SelectedIndexChanged += new EventHandler(delegate (object sender, EventArgs e) { this.label_index.Text = $"{this.page_question1.SelectedIndex + 1}"; });
            }
        }


        // An di caret
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);




        public Task ProcessWrite(string path, string text, FileMode fm)
        {
            string filePath = @$"{path}";
            //string text = "Hello World\r\n";

            return WriteTextAsync(filePath, text, fm);

        }

        public async Task WriteTextAsync(string filePath, string text, FileMode fm)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using (FileStream sourceStream = new FileStream(filePath,
                fm, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }






        // Cau tra loi
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();




        // Init
        private void InitLabel(Test t, Student s)
        {
            //Test t = new TestDBContext().GetTest("PRO192_PT2");
            this.label_index.Text = "1";
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.label_mcname.Text = Environment.MachineName;

            this.button2.Enabled = false;


            this.label_examcode.Text = t.Code;

            string duration = (t.Duration.Hours * 60 + t.Duration.Minutes).ToString();


            this.label_subject.Text = t.Subject.SubjectCode;
            this.label_totalmark.Text = "10";
            this.label_mark.Text = Math.Round(10.0 / t.QuestionTests.Count, 1).ToString();
            this.label_studentcode.Text = s.StudentCode; /*query statement*/
            this.label_duration.Text = $"{duration} minutes"; /*((int)Math.Round(t.EndTime.TotalMinutes - DateTime.Now.TimeOfDay.TotalMinutes)).ToString();*/
        }

        // Ham nay de tinh va goi ra timer
        private void CalculateTimer(int testTime, int timeDiff)
        {
            int realDuration = testTime < timeDiff ? testTime : timeDiff;

            int hour = realDuration % 60;
            int min = realDuration - hour * 60;

            Timer(hour, min, 0);
        }

        // Ham bind timer vao label
        private void Timer(int hr, int min, int sec)
        {
            CountDownTimer timer = new CountDownTimer();
            timer.SetTime(hr, min, sec);
            timer.Start();
            timer.TimeChanged += () => counter.Text = timer.TimeLeftHrsStr;
            timer.CountDownFinished += () => counter.Text = "Finish!";
            timer.StepMs = 77;
        }


        // Ham nay de goi ra danh sach cac anh 
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

        // Ham nay de random anh
        private void Random_Img()
        {
            Random seed = new Random();
            List<string> paths = FilePath(@"C:\Users\trung\Desktop\Flag", "*.png");
            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox1.Image = Image.FromFile(paths.ElementAt(seed.Next(0, paths.Count)));
        }


        void Process_Load(string path)
        {
            String process = "";
            ServiceController[] svcs = ServiceController.GetServices();

            foreach (ServiceController svc in svcs)
            {


                process += (svc.ServiceName + "|");

            }
            ProcessWrite(@$"{path}", process, FileMode.OpenOrCreate);
        }

        //public static Stream ToStream(string str)
        //{
        //    MemoryStream stream = new MemoryStream();
        //    StreamWriter writer = new StreamWriter(stream);
        //    writer.Write(str);
        //    writer.Flush();
        //    stream.Position = 0;
        //    return stream;
        //}

        private void button2_Click(object sender, EventArgs e)
        {


            List<string> path_logs = FilePath(@"F:\", "*.txt");
            path_logs.Sort();
            foreach (var file in path_logs)
            {
                if (!file.Contains("final"))
                {
                    using (Stream input = File.OpenRead(@$"{file}"))
                    using (Stream output = new FileStream(@"F:\logfinal.txt", FileMode.Append,
                                                          FileAccess.Write, FileShare.None))
                    {
                        input.CopyTo(output); // Using .NET 4
                    }
                    File.Delete(@$"{ file}");
                }


            }




            //Submit();



            Application.Exit();
        }


        // Navigate giua cac cau hoi
        private void next_question_Click(object sender, EventArgs e)
        {

            if (this.page_question1.SelectedIndex == this.page_question1.TabPages.Count - 1)
            {
                this.page_question1.SelectedIndex = 0;
                this.label_index.Text = (this.page_question1.SelectedIndex + 1).ToString();
            }
            else if (this.page_question1.SelectedIndex < this.page_question1.TabPages.Count - 1)
            {
                this.page_question1.SelectedIndex += 1;
                this.label_index.Text = (this.page_question1.SelectedIndex + 1).ToString();
            }
        }

        // Submit bai thi
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
                this.button2.Enabled = true;
            else
            {
                this.button2.Enabled = false;
            }
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }



    }
}
