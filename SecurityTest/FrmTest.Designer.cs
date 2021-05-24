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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelHint = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.labelQNum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCurEmp = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelQ = new System.Windows.Forms.Panel();
            this.labQ = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBoxQ = new System.Windows.Forms.PictureBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelAnswer = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panelQ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQ)).BeginInit();
            this.panelAnswer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.labelHint);
            this.panel1.Controls.Add(this.buttonNext);
            this.panel1.Controls.Add(this.labelQNum);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelCurEmp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(15, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1116, 68);
            this.panel1.TabIndex = 0;
            // 
            // labelHint
            // 
            this.labelHint.AutoSize = true;
            this.labelHint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelHint.ForeColor = System.Drawing.Color.Chocolate;
            this.labelHint.Location = new System.Drawing.Point(0, 49);
            this.labelHint.Margin = new System.Windows.Forms.Padding(3);
            this.labelHint.Name = "labelHint";
            this.labelHint.Padding = new System.Windows.Forms.Padding(3);
            this.labelHint.Size = new System.Drawing.Size(454, 19);
            this.labelHint.TabIndex = 10;
            this.labelHint.Text = "Для просмотра изображения наведите на него курсор и нажмите левую кнопку мыши\r\n";
            this.labelHint.Visible = false;
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.BackColor = System.Drawing.Color.Khaki;
            this.buttonNext.Enabled = false;
            this.buttonNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNext.Location = new System.Drawing.Point(899, 16);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(184, 49);
            this.buttonNext.TabIndex = 9;
            this.buttonNext.Text = "Далее";
            this.buttonNext.UseVisualStyleBackColor = false;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
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
            // labelCurEmp
            // 
            this.labelCurEmp.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCurEmp.Location = new System.Drawing.Point(0, 0);
            this.labelCurEmp.Name = "labelCurEmp";
            this.labelCurEmp.Size = new System.Drawing.Size(1116, 17);
            this.labelCurEmp.TabIndex = 1;
            this.labelCurEmp.Text = "Текущий сотрудник";
            this.labelCurEmp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(15, 705);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1116, 23);
            this.panel2.TabIndex = 1;
            // 
            // panelQ
            // 
            this.panelQ.Controls.Add(this.labQ);
            this.panelQ.Controls.Add(this.panel3);
            this.panelQ.Controls.Add(this.pictureBoxQ);
            this.panelQ.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelQ.Location = new System.Drawing.Point(15, 73);
            this.panelQ.Name = "panelQ";
            this.panelQ.Padding = new System.Windows.Forms.Padding(5);
            this.panelQ.Size = new System.Drawing.Size(319, 632);
            this.panelQ.TabIndex = 8;
            // 
            // labQ
            // 
            this.labQ.BackColor = System.Drawing.Color.SeaShell;
            this.labQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labQ.Location = new System.Drawing.Point(5, 252);
            this.labQ.Margin = new System.Windows.Forms.Padding(3);
            this.labQ.Name = "labQ";
            this.labQ.Padding = new System.Windows.Forms.Padding(3);
            this.labQ.Size = new System.Drawing.Size(309, 375);
            this.labQ.TabIndex = 20;
            this.labQ.Text = "label2";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(5, 247);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(309, 5);
            this.panel3.TabIndex = 17;
            // 
            // pictureBoxQ
            // 
            this.pictureBoxQ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxQ.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxQ.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxQ.Location = new System.Drawing.Point(5, 5);
            this.pictureBoxQ.Name = "pictureBoxQ";
            this.pictureBoxQ.Size = new System.Drawing.Size(309, 242);
            this.pictureBoxQ.TabIndex = 16;
            this.pictureBoxQ.TabStop = false;
            this.pictureBoxQ.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.RoyalBlue;
            this.splitter1.Location = new System.Drawing.Point(334, 73);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 632);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // panelAnswer
            // 
            this.panelAnswer.BackColor = System.Drawing.Color.SeaShell;
            this.panelAnswer.Controls.Add(this.tableLayoutPanel1);
            this.panelAnswer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAnswer.Location = new System.Drawing.Point(339, 73);
            this.panelAnswer.Name = "panelAnswer";
            this.panelAnswer.Size = new System.Drawing.Size(792, 632);
            this.panelAnswer.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 632);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1146, 733);
            this.Controls.Add(this.panelAnswer);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelQ);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmTest";
            this.Padding = new System.Windows.Forms.Padding(15, 5, 15, 5);
            this.Text = "Выполнение теста. Осталось вопросов: ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTest_FormClosing);
            this.Shown += new System.EventHandler(this.FrmTest_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelQ.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQ)).EndInit();
            this.panelAnswer.ResumeLayout(false);
            this.panelAnswer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelQNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label labelCurEmp;
        private System.Windows.Forms.Label labelHint;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Panel panelQ;
        private System.Windows.Forms.PictureBox pictureBoxQ;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panelAnswer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labQ;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer timer1;
    }
}