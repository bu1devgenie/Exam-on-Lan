
namespace OTS.ManageQuestion
{
    partial class ListQuestionBank
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.searchQues = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.addQues = new System.Windows.Forms.Button();
            this.editQues = new System.Windows.Forms.Button();
            this.delQues = new System.Windows.Forms.Button();
            this.dataQuestion = new System.Windows.Forms.DataGridView();
            this.checkType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.studentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTestListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createATestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questionBankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataQuestion)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(10, 26);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 52);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(173, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(363, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "Manage Question Bank";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 82);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(142, 23);
            this.textBox1.TabIndex = 1;
            // 
            // searchQues
            // 
            this.searchQues.Location = new System.Drawing.Point(158, 82);
            this.searchQues.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.searchQues.Name = "searchQues";
            this.searchQues.Size = new System.Drawing.Size(86, 20);
            this.searchQues.TabIndex = 2;
            this.searchQues.Text = "Search";
            this.searchQues.UseVisualStyleBackColor = true;
            this.searchQues.Click += new System.EventHandler(this.searchQues_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(244, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 37);
            this.label2.TabIndex = 4;
            this.label2.Text = "Question Bank List";
            // 
            // addQues
            // 
            this.addQues.Location = new System.Drawing.Point(158, 305);
            this.addQues.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addQues.Name = "addQues";
            this.addQues.Size = new System.Drawing.Size(113, 26);
            this.addQues.TabIndex = 5;
            this.addQues.Text = "New Question";
            this.addQues.UseVisualStyleBackColor = true;
            this.addQues.Click += new System.EventHandler(this.addQues_Click);
            // 
            // editQues
            // 
            this.editQues.Location = new System.Drawing.Point(298, 305);
            this.editQues.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.editQues.Name = "editQues";
            this.editQues.Size = new System.Drawing.Size(113, 26);
            this.editQues.TabIndex = 6;
            this.editQues.Text = "Edit";
            this.editQues.UseVisualStyleBackColor = true;
            this.editQues.Click += new System.EventHandler(this.editQues_Click);
            // 
            // delQues
            // 
            this.delQues.Location = new System.Drawing.Point(436, 305);
            this.delQues.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.delQues.Name = "delQues";
            this.delQues.Size = new System.Drawing.Size(113, 26);
            this.delQues.TabIndex = 7;
            this.delQues.Text = "Delete";
            this.delQues.UseVisualStyleBackColor = true;
            this.delQues.Click += new System.EventHandler(this.delQues_Click);
            // 
            // dataQuestion
            // 
            this.dataQuestion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataQuestion.Location = new System.Drawing.Point(10, 150);
            this.dataQuestion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataQuestion.Name = "dataQuestion";
            this.dataQuestion.RowHeadersWidth = 51;
            this.dataQuestion.RowTemplate.Height = 29;
            this.dataQuestion.Size = new System.Drawing.Size(760, 141);
            this.dataQuestion.TabIndex = 9;
            // 
            // checkType
            // 
            this.checkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.checkType.FormattingEnabled = true;
            this.checkType.Items.AddRange(new object[] {
            "Multiple-choice",
            "Essay"});
            this.checkType.Location = new System.Drawing.Point(598, 107);
            this.checkType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkType.Name = "checkType";
            this.checkType.Size = new System.Drawing.Size(142, 23);
            this.checkType.TabIndex = 10;
            this.checkType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(606, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "Type of question";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.logoutToolStripMenuItem,
            this.logoutToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(780, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.homeToolStripMenuItem.Text = "Home";
            this.homeToolStripMenuItem.Click += new System.EventHandler(this.homeToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.studentToolStripMenuItem,
            this.subjectToolStripMenuItem,
            this.classToolStripMenuItem,
            this.testToolStripMenuItem,
            this.questionBankToolStripMenuItem,
            this.submissionToolStripMenuItem,
            this.classToolStripMenuItem1});
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.logoutToolStripMenuItem.Text = "Manage";
            // 
            // studentToolStripMenuItem
            // 
            this.studentToolStripMenuItem.Name = "studentToolStripMenuItem";
            this.studentToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.studentToolStripMenuItem.Text = "Student";
            this.studentToolStripMenuItem.Click += new System.EventHandler(this.studentToolStripMenuItem_Click);
            // 
            // subjectToolStripMenuItem
            // 
            this.subjectToolStripMenuItem.Name = "subjectToolStripMenuItem";
            this.subjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.subjectToolStripMenuItem.Text = "Subject";
            this.subjectToolStripMenuItem.Click += new System.EventHandler(this.subjectToolStripMenuItem_Click);
            // 
            // classToolStripMenuItem
            // 
            this.classToolStripMenuItem.Name = "classToolStripMenuItem";
            this.classToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.classToolStripMenuItem.Text = "Class";
            this.classToolStripMenuItem.Click += new System.EventHandler(this.classToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewTestListToolStripMenuItem,
            this.createATestToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // viewTestListToolStripMenuItem
            // 
            this.viewTestListToolStripMenuItem.Name = "viewTestListToolStripMenuItem";
            this.viewTestListToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewTestListToolStripMenuItem.Text = "View test list";
            this.viewTestListToolStripMenuItem.Click += new System.EventHandler(this.viewTestListToolStripMenuItem_Click);
            // 
            // createATestToolStripMenuItem
            // 
            this.createATestToolStripMenuItem.Name = "createATestToolStripMenuItem";
            this.createATestToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.createATestToolStripMenuItem.Text = "Create a test";
            this.createATestToolStripMenuItem.Click += new System.EventHandler(this.createATestToolStripMenuItem_Click);
            // 
            // questionBankToolStripMenuItem
            // 
            this.questionBankToolStripMenuItem.Checked = true;
            this.questionBankToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.questionBankToolStripMenuItem.Name = "questionBankToolStripMenuItem";
            this.questionBankToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.questionBankToolStripMenuItem.Text = "Question bank";
            // 
            // submissionToolStripMenuItem
            // 
            this.submissionToolStripMenuItem.Name = "submissionToolStripMenuItem";
            this.submissionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.submissionToolStripMenuItem.Text = "Submission";
            this.submissionToolStripMenuItem.Click += new System.EventHandler(this.submissionToolStripMenuItem_Click);
            // 
            // classToolStripMenuItem1
            // 
            this.classToolStripMenuItem1.Name = "classToolStripMenuItem1";
            this.classToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.classToolStripMenuItem1.Text = "Mark";
            this.classToolStripMenuItem1.Click += new System.EventHandler(this.markToolStripMenuItem1_Click);
            // 
            // logoutToolStripMenuItem1
            // 
            this.logoutToolStripMenuItem1.Name = "logoutToolStripMenuItem1";
            this.logoutToolStripMenuItem1.Size = new System.Drawing.Size(57, 20);
            this.logoutToolStripMenuItem1.Text = "Logout";
            this.logoutToolStripMenuItem1.Click += new System.EventHandler(this.logoutToolStripMenuItem1_Click);
            // 
            // ListQuestionBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 562);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkType);
            this.Controls.Add(this.dataQuestion);
            this.Controls.Add(this.delQues);
            this.Controls.Add(this.editQues);
            this.Controls.Add(this.addQues);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.searchQues);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ListQuestionBank";
            this.Text = "ListQuestionBank";
            this.Load += new System.EventHandler(this.ListQuestionBank_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataQuestion)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button searchQues;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addQues;
        private System.Windows.Forms.Button editQues;
        private System.Windows.Forms.Button delQues;
        private System.Windows.Forms.DataGridView dataQuestion;
        private System.Windows.Forms.ComboBox checkType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem studentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewTestListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createATestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem questionBankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem submissionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem1;
    }
}