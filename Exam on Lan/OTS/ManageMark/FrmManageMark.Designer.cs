namespace OTS.ManageMark
{
    partial class FrmManageMark
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
            this.dgvMark = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtStudentCode = new System.Windows.Forms.TextBox();
            this.txtTestCode = new System.Windows.Forms.TextBox();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkClass = new System.Windows.Forms.CheckBox();
            this.cbClass = new System.Windows.Forms.ComboBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.chkSubmitDate = new System.Windows.Forms.CheckBox();
            this.chkStudentCode = new System.Windows.Forms.CheckBox();
            this.chkTestCode = new System.Windows.Forms.CheckBox();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.lbTotalPage = new System.Windows.Forms.Label();
            this.txtPageIndex = new System.Windows.Forms.TextBox();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvMark)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMark
            // 
            this.dgvMark.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMark.Location = new System.Drawing.Point(294, 99);
            this.dgvMark.Name = "dgvMark";
            this.dgvMark.RowTemplate.Height = 25;
            this.dgvMark.Size = new System.Drawing.Size(643, 290);
            this.dgvMark.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(421, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "MARK REPORT";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtStudentCode);
            this.groupBox1.Controls.Add(this.txtTestCode);
            this.groupBox1.Controls.Add(this.btnClearSearch);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.chkClass);
            this.groupBox1.Controls.Add(this.cbClass);
            this.groupBox1.Controls.Add(this.dtpTo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpFrom);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chkSubmitDate);
            this.groupBox1.Controls.Add(this.chkStudentCode);
            this.groupBox1.Controls.Add(this.chkTestCode);
            this.groupBox1.Location = new System.Drawing.Point(28, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 352);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // txtStudentCode
            // 
            this.txtStudentCode.Enabled = false;
            this.txtStudentCode.Location = new System.Drawing.Point(11, 115);
            this.txtStudentCode.Name = "txtStudentCode";
            this.txtStudentCode.Size = new System.Drawing.Size(216, 23);
            this.txtStudentCode.TabIndex = 18;
            // 
            // txtTestCode
            // 
            this.txtTestCode.Enabled = false;
            this.txtTestCode.Location = new System.Drawing.Point(11, 54);
            this.txtTestCode.Name = "txtTestCode";
            this.txtTestCode.Size = new System.Drawing.Size(216, 23);
            this.txtTestCode.TabIndex = 17;
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(128, 318);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(107, 23);
            this.btnClearSearch.TabIndex = 16;
            this.btnClearSearch.Text = "View all";
            this.btnClearSearch.UseVisualStyleBackColor = true;
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(13, 318);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkClass
            // 
            this.chkClass.AutoSize = true;
            this.chkClass.Location = new System.Drawing.Point(11, 155);
            this.chkClass.Name = "chkClass";
            this.chkClass.Size = new System.Drawing.Size(53, 19);
            this.chkClass.TabIndex = 14;
            this.chkClass.Text = "Class";
            this.chkClass.UseVisualStyleBackColor = true;
            this.chkClass.CheckedChanged += new System.EventHandler(this.chkClass_CheckedChanged);
            // 
            // cbClass
            // 
            this.cbClass.Enabled = false;
            this.cbClass.FormattingEnabled = true;
            this.cbClass.Items.AddRange(new object[] {
            "Not Started",
            "Started",
            "Ended"});
            this.cbClass.Location = new System.Drawing.Point(10, 179);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(217, 23);
            this.cbClass.TabIndex = 13;
            // 
            // dtpTo
            // 
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(82, 271);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(146, 23);
            this.dtpTo.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "To:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(82, 242);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(146, 23);
            this.dtpFrom.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "From:";
            // 
            // chkSubmitDate
            // 
            this.chkSubmitDate.AutoSize = true;
            this.chkSubmitDate.Location = new System.Drawing.Point(11, 217);
            this.chkSubmitDate.Name = "chkSubmitDate";
            this.chkSubmitDate.Size = new System.Drawing.Size(91, 19);
            this.chkSubmitDate.TabIndex = 8;
            this.chkSubmitDate.Text = "Submit Date";
            this.chkSubmitDate.UseVisualStyleBackColor = true;
            this.chkSubmitDate.CheckedChanged += new System.EventHandler(this.chkSubmitDate_CheckedChanged);
            // 
            // chkStudentCode
            // 
            this.chkStudentCode.AutoSize = true;
            this.chkStudentCode.Location = new System.Drawing.Point(11, 90);
            this.chkStudentCode.Name = "chkStudentCode";
            this.chkStudentCode.Size = new System.Drawing.Size(98, 19);
            this.chkStudentCode.TabIndex = 3;
            this.chkStudentCode.Text = "Student Code";
            this.chkStudentCode.UseVisualStyleBackColor = true;
            this.chkStudentCode.CheckedChanged += new System.EventHandler(this.chkStudentCode_CheckedChanged);
            // 
            // chkTestCode
            // 
            this.chkTestCode.AutoSize = true;
            this.chkTestCode.Location = new System.Drawing.Point(11, 28);
            this.chkTestCode.Name = "chkTestCode";
            this.chkTestCode.Size = new System.Drawing.Size(77, 19);
            this.chkTestCode.TabIndex = 2;
            this.chkTestCode.Text = "Test Code";
            this.chkTestCode.UseVisualStyleBackColor = true;
            this.chkTestCode.CheckedChanged += new System.EventHandler(this.chkTestCode_CheckedChanged);
            // 
            // btnLastPage
            // 
            this.btnLastPage.Enabled = false;
            this.btnLastPage.Location = new System.Drawing.Point(553, 409);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(45, 29);
            this.btnLastPage.TabIndex = 14;
            this.btnLastPage.Text = "Last";
            this.btnLastPage.UseVisualStyleBackColor = true;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Enabled = false;
            this.btnNextPage.Location = new System.Drawing.Point(502, 409);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(45, 29);
            this.btnNextPage.TabIndex = 13;
            this.btnNextPage.Text = "Next";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // lbTotalPage
            // 
            this.lbTotalPage.AutoSize = true;
            this.lbTotalPage.Location = new System.Drawing.Point(455, 416);
            this.lbTotalPage.Name = "lbTotalPage";
            this.lbTotalPage.Size = new System.Drawing.Size(30, 15);
            this.lbTotalPage.TabIndex = 12;
            this.lbTotalPage.Text = "/100";
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.Location = new System.Drawing.Point(415, 411);
            this.txtPageIndex.Name = "txtPageIndex";
            this.txtPageIndex.Size = new System.Drawing.Size(34, 23);
            this.txtPageIndex.TabIndex = 11;
            this.txtPageIndex.TextChanged += new System.EventHandler(this.txtPageIndex_TextChanged);
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Enabled = false;
            this.btnPrevPage.Location = new System.Drawing.Point(347, 407);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(45, 29);
            this.btnPrevPage.TabIndex = 10;
            this.btnPrevPage.Text = "Prev";
            this.btnPrevPage.UseVisualStyleBackColor = true;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Enabled = false;
            this.btnFirstPage.Location = new System.Drawing.Point(296, 408);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(45, 29);
            this.btnFirstPage.TabIndex = 9;
            this.btnFirstPage.Text = "First";
            this.btnFirstPage.UseVisualStyleBackColor = true;
            this.btnFirstPage.Click += new System.EventHandler(this.btnFirstPage_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(810, 405);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(127, 31);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "Export file";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.logoutToolStripMenuItem,
            this.logoutToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(957, 24);
            this.menuStrip1.TabIndex = 16;
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
            this.studentToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.studentToolStripMenuItem.Text = "Student";
            this.studentToolStripMenuItem.Click += new System.EventHandler(this.studentToolStripMenuItem_Click);
            // 
            // subjectToolStripMenuItem
            // 
            this.subjectToolStripMenuItem.Name = "subjectToolStripMenuItem";
            this.subjectToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.subjectToolStripMenuItem.Text = "Subject";
            this.subjectToolStripMenuItem.Click += new System.EventHandler(this.subjectToolStripMenuItem_Click);
            // 
            // classToolStripMenuItem
            // 
            this.classToolStripMenuItem.Name = "classToolStripMenuItem";
            this.classToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
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
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // viewTestListToolStripMenuItem
            // 
            this.viewTestListToolStripMenuItem.Name = "viewTestListToolStripMenuItem";
            this.viewTestListToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.viewTestListToolStripMenuItem.Text = "View test list";
            this.viewTestListToolStripMenuItem.Click += new System.EventHandler(this.viewTestListToolStripMenuItem_Click);
            // 
            // createATestToolStripMenuItem
            // 
            this.createATestToolStripMenuItem.Name = "createATestToolStripMenuItem";
            this.createATestToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.createATestToolStripMenuItem.Text = "Create a test";
            this.createATestToolStripMenuItem.Click += new System.EventHandler(this.createATestToolStripMenuItem_Click);
            // 
            // questionBankToolStripMenuItem
            // 
            this.questionBankToolStripMenuItem.Name = "questionBankToolStripMenuItem";
            this.questionBankToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.questionBankToolStripMenuItem.Text = "Question bank";
            this.questionBankToolStripMenuItem.Click += new System.EventHandler(this.questionBankToolStripMenuItem_Click);
            // 
            // submissionToolStripMenuItem
            // 
            this.submissionToolStripMenuItem.Name = "submissionToolStripMenuItem";
            this.submissionToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.submissionToolStripMenuItem.Text = "Submission";
            this.submissionToolStripMenuItem.Click += new System.EventHandler(this.submissionToolStripMenuItem_Click);
            // 
            // classToolStripMenuItem1
            // 
            this.classToolStripMenuItem1.Checked = true;
            this.classToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.classToolStripMenuItem1.Name = "classToolStripMenuItem1";
            this.classToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.classToolStripMenuItem1.Text = "Mark";
            // 
            // logoutToolStripMenuItem1
            // 
            this.logoutToolStripMenuItem1.Name = "logoutToolStripMenuItem1";
            this.logoutToolStripMenuItem1.Size = new System.Drawing.Size(57, 20);
            this.logoutToolStripMenuItem1.Text = "Logout";
            this.logoutToolStripMenuItem1.Click += new System.EventHandler(this.logoutToolStripMenuItem1_Click);
            // 
            // FrmManageMark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 484);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnLastPage);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.lbTotalPage);
            this.Controls.Add(this.txtPageIndex);
            this.Controls.Add(this.btnPrevPage);
            this.Controls.Add(this.btnFirstPage);
            this.Controls.Add(this.dgvMark);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmManageMark";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmManageMark";
            this.Load += new System.EventHandler(this.FrmManageMark_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMark)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtStudentCode;
        private System.Windows.Forms.TextBox txtTestCode;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkClass;
        private System.Windows.Forms.ComboBox cbClass;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkSubmitDate;
        private System.Windows.Forms.CheckBox chkStudentCode;
        private System.Windows.Forms.CheckBox chkTestCode;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Label lbTotalPage;
        private System.Windows.Forms.TextBox txtPageIndex;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Button btnExport;
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