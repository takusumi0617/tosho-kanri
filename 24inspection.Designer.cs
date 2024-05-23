namespace 総合図書管理システム新版
{
    partial class _24inspection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_24inspection));
            label1 = new Label();
            label2 = new Label();
            listView1 = new ListView();
            蔵書番号 = new ColumnHeader();
            textBox1 = new TextBox();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            linkLabel1 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel3 = new LinkLabel();
            linkLabel4 = new LinkLabel();
            label4 = new Label();
            linkLabel5 = new LinkLabel();
            label5 = new Label();
            label6 = new Label();
            button6 = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            button7 = new Button();
            groupBox2 = new GroupBox();
            checkBox5 = new CheckBox();
            checkBox6 = new CheckBox();
            checkBox7 = new CheckBox();
            groupBox1 = new GroupBox();
            checkBox4 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            label9 = new Label();
            checkBox1 = new CheckBox();
            label8 = new Label();
            numericUpDown1 = new NumericUpDown();
            label7 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(110, 32);
            label1.TabIndex = 0;
            label1.Text = "蔵書点検";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(128, 23);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 1;
            label2.Text = "inspection";
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { 蔵書番号 });
            listView1.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            listView1.Location = new Point(13, 139);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.Size = new Size(697, 213);
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // 蔵書番号
            // 
            蔵書番号.Text = "蔵書番号";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(74, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(183, 23);
            textBox1.TabIndex = 3;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 15);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 4;
            label3.Text = "蔵書番号";
            // 
            // button1
            // 
            button1.Location = new Point(431, 12);
            button1.Name = "button1";
            button1.Size = new Size(105, 23);
            button1.TabIndex = 5;
            button1.Text = "CSVから取り込み";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(13, 89);
            button2.Name = "button2";
            button2.Size = new Size(79, 28);
            button2.TabIndex = 6;
            button2.Text = "点検済み";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(98, 89);
            button3.Name = "button3";
            button3.Size = new Size(79, 28);
            button3.TabIndex = 7;
            button3.Text = "貸出中";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(255, 192, 192);
            button4.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button4.Location = new Point(183, 89);
            button4.Name = "button4";
            button4.Size = new Size(79, 28);
            button4.TabIndex = 8;
            button4.Text = "不明";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(268, 89);
            button5.Name = "button5";
            button5.Size = new Size(88, 28);
            button5.TabIndex = 9;
            button5.Text = "貸出中+不明";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(39, 120);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(19, 15);
            linkLabel1.TabIndex = 10;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "？";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(129, 120);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(19, 15);
            linkLabel2.TabIndex = 11;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "？";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Location = new Point(211, 120);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(19, 15);
            linkLabel3.TabIndex = 12;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "？";
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            // 
            // linkLabel4
            // 
            linkLabel4.AutoSize = true;
            linkLabel4.Location = new Point(304, 120);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(19, 15);
            linkLabel4.TabIndex = 13;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "？";
            linkLabel4.LinkClicked += linkLabel4_LinkClicked;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(263, 15);
            label4.Name = "label4";
            label4.Size = new Size(150, 15);
            label4.TabIndex = 14;
            label4.Text = "Enterで追加　　　　　又は";
            // 
            // linkLabel5
            // 
            linkLabel5.AutoSize = true;
            linkLabel5.Location = new Point(542, 16);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(61, 15);
            linkLabel5.TabIndex = 15;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "CSVの書式";
            linkLabel5.LinkClicked += linkLabel5_LinkClicked;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(13, 61);
            label5.Name = "label5";
            label5.Size = new Size(130, 25);
            label5.TabIndex = 16;
            label5.Text = "表示中のデータ:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(138, 61);
            label6.Name = "label6";
            label6.Size = new Size(70, 25);
            label6.TabIndex = 17;
            label6.Text = "(None)";
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button6.BackColor = Color.FromArgb(255, 128, 128);
            button6.Font = new Font("Yu Gothic UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            button6.Location = new Point(13, 358);
            button6.Name = "button6";
            button6.Size = new Size(697, 47);
            button6.TabIndex = 18;
            button6.Text = "点検を終了し報告を発行";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(listView1);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(linkLabel5);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(linkLabel4);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(linkLabel3);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(linkLabel2);
            panel1.Controls.Add(linkLabel1);
            panel1.Location = new Point(-1, 44);
            panel1.Name = "panel1";
            panel1.Size = new Size(723, 422);
            panel1.TabIndex = 19;
            // 
            // panel2
            // 
            panel2.Controls.Add(button7);
            panel2.Controls.Add(groupBox2);
            panel2.Controls.Add(checkBox7);
            panel2.Controls.Add(groupBox1);
            panel2.Controls.Add(checkBox2);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(checkBox1);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(numericUpDown1);
            panel2.Controls.Add(label7);
            panel2.Location = new Point(-1, 44);
            panel2.Name = "panel2";
            panel2.Size = new Size(723, 422);
            panel2.TabIndex = 20;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(255, 128, 128);
            button7.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button7.Location = new Point(13, 311);
            button7.Name = "button7";
            button7.Size = new Size(235, 53);
            button7.TabIndex = 10;
            button7.Text = "指定した操作を実行する";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(checkBox5);
            groupBox2.Controls.Add(checkBox6);
            groupBox2.Enabled = false;
            groupBox2.Location = new Point(57, 215);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(134, 68);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(6, 40);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(110, 19);
            checkBox5.TabIndex = 7;
            checkBox5.Text = "全除籍図書一覧";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(6, 15);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(110, 19);
            checkBox6.TabIndex = 6;
            checkBox6.Text = "全不明図書一覧";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.Location = new Point(39, 197);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new Size(227, 19);
            checkBox7.TabIndex = 8;
            checkBox7.Text = "関連書類の文書をコンピュータに保存する。";
            checkBox7.UseVisualStyleBackColor = true;
            checkBox7.CheckedChanged += checkBox7_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox4);
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Enabled = false;
            groupBox1.Location = new Point(57, 123);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(134, 68);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(6, 40);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(110, 19);
            checkBox4.TabIndex = 7;
            checkBox4.Text = "全除籍図書一覧";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(6, 15);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(110, 19);
            checkBox3.TabIndex = 6;
            checkBox3.Text = "全不明図書一覧";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(39, 105);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(145, 19);
            checkBox2.TabIndex = 5;
            checkBox2.Text = "関連書類の印刷を行う。";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(13, 77);
            label9.Name = "label9";
            label9.Size = new Size(156, 25);
            label9.TabIndex = 4;
            label9.Text = "2、印刷/出力設定";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(39, 45);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(15, 14);
            checkBox1.TabIndex = 3;
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(98, 44);
            label8.Name = "label8";
            label8.Size = new Size(190, 15);
            label8.TabIndex = 2;
            label8.Text = "回連続検出されない蔵書を除籍する。";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(60, 42);
            numericUpDown1.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(37, 23);
            numericUpDown1.TabIndex = 1;
            numericUpDown1.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(13, 12);
            label7.Name = "label7";
            label7.Size = new Size(111, 25);
            label7.TabIndex = 0;
            label7.Text = "1、除籍処理";
            // 
            // _24inspection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(721, 464);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "_24inspection";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "蔵書点検";
            FormClosing += _24inspection_FormClosing;
            Load += _24inspection_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ListView listView1;
        private TextBox textBox1;
        private Label label3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel4;
        private Label label4;
        private ColumnHeader 蔵書番号;
        private LinkLabel linkLabel5;
        private Label label5;
        private Label label6;
        private Button button6;
        private Panel panel1;
        private Panel panel2;
        private Label label7;
        private Label label9;
        private CheckBox checkBox1;
        private Label label8;
        private NumericUpDown numericUpDown1;
        private GroupBox groupBox1;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox4;
        private GroupBox groupBox2;
        private CheckBox checkBox5;
        private CheckBox checkBox6;
        private CheckBox checkBox7;
        private Button button7;
    }
}