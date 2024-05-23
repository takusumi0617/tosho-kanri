namespace 総合図書管理システム新版
{
    partial class _02Menu
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_02Menu));
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            button3 = new Button();
            label2 = new Label();
            groupBox1 = new GroupBox();
            label12 = new Label();
            label11 = new Label();
            label3 = new Label();
            groupBox2 = new GroupBox();
            button8 = new Button();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            groupBox3 = new GroupBox();
            label13 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label10 = new Label();
            textBox3 = new TextBox();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            splitContainer1 = new SplitContainer();
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(412, 38);
            button1.Name = "button1";
            button1.Size = new Size(65, 33);
            button1.TabIndex = 0;
            button1.Text = "確定";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(18, 28);
            label1.Name = "label1";
            label1.Size = new Size(75, 40);
            label1.TabIndex = 1;
            label1.Text = "貸出";
            // 
            // button2
            // 
            button2.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(152, 3);
            button2.Name = "button2";
            button2.Size = new Size(143, 47);
            button2.TabIndex = 2;
            button2.TabStop = false;
            button2.Text = "蔵書検索";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(3, 3);
            button3.Name = "button3";
            button3.Size = new Size(143, 47);
            button3.TabIndex = 3;
            button3.TabStop = false;
            button3.Text = "利用者照会";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 4;
            label2.Text = "利用者ID";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(299, 100);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.MouseCaptureChanged += groupBox1_Enter;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(205, 43);
            label12.Name = "label12";
            label12.Size = new Size(65, 21);
            label12.TabIndex = 4;
            label12.Text = "(MODE)";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(174, 48);
            label11.Name = "label11";
            label11.Size = new Size(34, 15);
            label11.TabIndex = 3;
            label11.Text = "権限:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(99, 48);
            label3.Name = "label3";
            label3.Size = new Size(30, 15);
            label3.TabIndex = 2;
            label3.Text = "lend";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(button8);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(3, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(483, 170);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            // 
            // button8
            // 
            button8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button8.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button8.Location = new Point(323, 37);
            button8.Name = "button8";
            button8.Size = new Size(83, 33);
            button8.TabIndex = 7;
            button8.Text = "ICスキャン";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(6, 77);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new Size(471, 83);
            textBox2.TabIndex = 6;
            textBox2.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.ImeMode = ImeMode.Off;
            textBox1.Location = new Point(6, 37);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(311, 33);
            textBox1.TabIndex = 5;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label4);
            groupBox3.Location = new Point(3, 109);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(299, 143);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "返却日";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(80, 100);
            label13.Name = "label13";
            label13.Size = new Size(35, 30);
            label13.TabIndex = 6;
            label13.Text = "00";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(133, 100);
            label9.Name = "label9";
            label9.Size = new Size(34, 30);
            label9.TabIndex = 5;
            label9.Text = "日";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 112);
            label8.Name = "label8";
            label8.Size = new Size(55, 15);
            label8.TabIndex = 4;
            label8.Text = "貸出期間";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Yu Gothic UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(55, 66);
            label7.Name = "label7";
            label7.Size = new Size(230, 32);
            label7.TabIndex = 3;
            label7.Text = "0000年00月00日(曜)";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Yu Gothic UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(55, 22);
            label6.Name = "label6";
            label6.Size = new Size(230, 32);
            label6.TabIndex = 2;
            label6.Text = "0000年00月00日(曜)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 80);
            label5.Name = "label5";
            label5.Size = new Size(43, 15);
            label5.TabIndex = 1;
            label5.Text = "返却日";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 36);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 0;
            label4.Text = "今日";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(3, 189);
            label10.Name = "label10";
            label10.Size = new Size(25, 15);
            label10.TabIndex = 8;
            label10.Text = "ログ";
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox3.Font = new Font("ＭＳ Ｐゴシック", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBox3.Location = new Point(3, 207);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox3.Size = new Size(483, 363);
            textBox3.TabIndex = 9;
            textBox3.TabStop = false;
            textBox3.Text = "＝＝ログ開始＝＝";
            // 
            // button4
            // 
            button4.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            button4.Location = new Point(152, 56);
            button4.Name = "button4";
            button4.Size = new Size(143, 47);
            button4.TabIndex = 10;
            button4.TabStop = false;
            button4.Text = "伝言・メモ";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            button5.Location = new Point(3, 109);
            button5.Name = "button5";
            button5.Size = new Size(143, 47);
            button5.TabIndex = 11;
            button5.TabStop = false;
            button5.Text = "設定・保守";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(255, 192, 192);
            button6.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            button6.Location = new Point(152, 109);
            button6.Name = "button6";
            button6.Size = new Size(143, 47);
            button6.TabIndex = 12;
            button6.TabStop = false;
            button6.Text = "ログアウト";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            button7.Location = new Point(3, 56);
            button7.Name = "button7";
            button7.Size = new Size(143, 47);
            button7.TabIndex = 13;
            button7.TabStop = false;
            button7.Text = "予約";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.BackColor = SystemColors.Control;
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = SystemColors.Control;
            splitContainer1.Panel1.Controls.Add(flowLayoutPanel1);
            splitContainer1.Panel1.Controls.Add(groupBox1);
            splitContainer1.Panel1.Controls.Add(groupBox3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox2);
            splitContainer1.Panel2.Controls.Add(label10);
            splitContainer1.Panel2.Controls.Add(textBox3);
            splitContainer1.Size = new Size(805, 584);
            splitContainer1.SplitterDistance = 307;
            splitContainer1.TabIndex = 14;
            splitContainer1.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.Controls.Add(button3);
            flowLayoutPanel1.Controls.Add(button2);
            flowLayoutPanel1.Controls.Add(button7);
            flowLayoutPanel1.Controls.Add(button4);
            flowLayoutPanel1.Controls.Add(button5);
            flowLayoutPanel1.Controls.Add(button6);
            flowLayoutPanel1.Location = new Point(0, 258);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(306, 325);
            flowLayoutPanel1.TabIndex = 14;
            // 
            // _02Menu
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(805, 584);
            Controls.Add(splitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "_02Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "貸出/返却処理";
            FormClosing += _02Menu_FormClosing;
            Load += _02Menu_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Label label1;
        private Button button2;
        private Button button3;
        private Label label2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox textBox1;
        private Label label3;
        private GroupBox groupBox3;
        private Label label4;
        private Label label5;
        private Label label7;
        private Label label6;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label12;
        private Label label11;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private System.Windows.Forms.Timer timer1;
        private Label label13;
        private SplitContainer splitContainer1;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}