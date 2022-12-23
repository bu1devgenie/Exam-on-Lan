
namespace OTS.ManageTest
{
    partial class ViewResultFinish
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
            this.exRe = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.corrAns = new System.Windows.Forms.TextBox();
            this.Score = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // exRe
            // 
            this.exRe.AutoSize = true;
            this.exRe.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.exRe.Location = new System.Drawing.Point(249, 104);
            this.exRe.Name = "exRe";
            this.exRe.Size = new System.Drawing.Size(199, 35);
            this.exRe.TabIndex = 0;
            this.exRe.Text = "Exam Result";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(103, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Correct Answer";
            // 
            // corrAns
            // 
            this.corrAns.Enabled = false;
            this.corrAns.Location = new System.Drawing.Point(276, 193);
            this.corrAns.Name = "corrAns";
            this.corrAns.Size = new System.Drawing.Size(125, 27);
            this.corrAns.TabIndex = 2;
            // 
            // Score
            // 
            this.Score.Enabled = false;
            this.Score.Location = new System.Drawing.Point(276, 290);
            this.Score.Name = "Score";
            this.Score.Size = new System.Drawing.Size(125, 27);
            this.Score.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Showcard Gothic", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(145, 290);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Total Score";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Snap ITC", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(181, 352);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(312, 30);
            this.label3.TabIndex = 5;
            this.label3.Text = "Take Exam Succesful";
            this.label3.UseMnemonic = false;
            // 
            // ViewResultFinish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 418);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Score);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.corrAns);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exRe);
            this.Name = "ViewResultFinish";
            this.Text = "ViewResultFinish";
            this.Load += new System.EventHandler(this.ViewResultFinish_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label exRe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox corrAns;
        private System.Windows.Forms.TextBox Score;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}