namespace SecurityTest
{
    partial class FrmTest
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
            this.labelCurEmp = new System.Windows.Forms.Label();
            this.labelQNum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.labQ = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panAnswer = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Bisque;
            this.panel1.Controls.Add(this.labelCurEmp);
            this.panel1.Controls.Add(this.labelQNum);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 46);
            this.panel1.TabIndex = 0;
            // 
            // labelCurEmp
            // 
            this.labelCurEmp.Location = new System.Drawing.Point(537, 9);
            this.labelCurEmp.Name = "labelCurEmp";
            this.labelCurEmp.Size = new System.Drawing.Size(427, 23);
            this.labelCurEmp.TabIndex = 1;
            this.labelCurEmp.Text = "Текущий сотрудник";
            this.labelCurEmp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelQNum
            // 
            this.labelQNum.AutoSize = true;
            this.labelQNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelQNum.Location = new System.Drawing.Point(136, 9);
            this.labelQNum.Name = "labelQNum";
            this.labelQNum.Size = new System.Drawing.Size(145, 29);
            this.labelQNum.TabIndex = 0;
            this.labelQNum.Text = "labelQNum";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Вопрос №";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Bisque;
            this.panel2.Controls.Add(this.buttonPrev);
            this.panel2.Controls.Add(this.buttonNext);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 648);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(976, 66);
            this.panel2.TabIndex = 1;
            // 
            // buttonPrev
            // 
            this.buttonPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPrev.Location = new System.Drawing.Point(12, 10);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(88, 49);
            this.buttonPrev.TabIndex = 9;
            this.buttonPrev.Text = "<";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Visible = false;
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNext.Location = new System.Drawing.Point(780, 10);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(184, 49);
            this.buttonNext.TabIndex = 8;
            this.buttonNext.Text = "Далее";
            this.buttonNext.UseVisualStyleBackColor = false;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // labQ
            // 
            this.labQ.Dock = System.Windows.Forms.DockStyle.Top;
            this.labQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labQ.Location = new System.Drawing.Point(0, 46);
            this.labQ.Margin = new System.Windows.Forms.Padding(3);
            this.labQ.Name = "labQ";
            this.labQ.Padding = new System.Windows.Forms.Padding(3);
            this.labQ.Size = new System.Drawing.Size(976, 157);
            this.labQ.TabIndex = 2;
            this.labQ.Text = "label2";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Bisque;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 203);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(976, 10);
            this.panel3.TabIndex = 3;
            // 
            // panAnswer
            // 
            this.panAnswer.BackColor = System.Drawing.Color.BurlyWood;
            this.panAnswer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panAnswer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panAnswer.Location = new System.Drawing.Point(0, 213);
            this.panAnswer.Name = "panAnswer";
            this.panAnswer.Size = new System.Drawing.Size(976, 435);
            this.panAnswer.TabIndex = 4;
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 714);
            this.ControlBox = false;
            this.Controls.Add(this.panAnswer);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.labQ);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmTest";
            this.Text = "FrmTest";
            this.Shown += new System.EventHandler(this.FrmTest_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelQNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Label labQ;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel panAnswer;
        public System.Windows.Forms.Label labelCurEmp;
    }
}