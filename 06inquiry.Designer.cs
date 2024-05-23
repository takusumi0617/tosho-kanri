namespace 総合図書管理システム新版
{
    partial class _06inquiry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_06inquiry));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            groupBox1 = new GroupBox();
            textBox2 = new TextBox();
            groupBox2 = new GroupBox();
            textBox3 = new TextBox();
            groupBox3 = new GroupBox();
            button2 = new Button();
            label4 = new Label();
            groupBox4 = new GroupBox();
            textBox4 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(55, 30);
            label1.TabIndex = 0;
            label1.Text = "照会";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(73, 21);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 1;
            label2.Text = "inquiry";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 50);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 2;
            label3.Text = "利用者番号";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(97, 47);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(377, 23);
            textBox1.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(480, 46);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "照会開始";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox2);
            groupBox1.Location = new Point(12, 76);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(384, 521);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "過去の履歴";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(6, 22);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Both;
            textBox2.Size = new Size(372, 493);
            textBox2.TabIndex = 6;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBox3);
            groupBox2.Location = new Point(404, 76);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(384, 199);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "現在貸出中の蔵書";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(6, 22);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.ScrollBars = ScrollBars.Both;
            textBox3.Size = new Size(372, 171);
            textBox3.TabIndex = 6;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(button2);
            groupBox3.Controls.Add(label4);
            groupBox3.Location = new Point(404, 486);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(384, 111);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "最終延滞歴";
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 192, 192);
            button2.Location = new Point(6, 73);
            button2.Name = "button2";
            button2.Size = new Size(252, 23);
            button2.TabIndex = 1;
            button2.Text = "この利用者の延滞歴を削除";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(6, 31);
            label4.Name = "label4";
            label4.Size = new Size(109, 30);
            label4.TabIndex = 0;
            label4.Text = "延滞歴なし";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBox4);
            groupBox4.Location = new Point(404, 281);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(384, 199);
            groupBox4.TabIndex = 8;
            groupBox4.TabStop = false;
            groupBox4.Text = "現在予約中の蔵書";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(6, 22);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.ScrollBars = ScrollBars.Both;
            textBox4.Size = new Size(372, 171);
            textBox4.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(561, 50);
            label5.Name = "label5";
            label5.Size = new Size(116, 15);
            label5.TabIndex = 9;
            label5.Text = "現在照会中の利用者:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(674, 50);
            label6.Name = "label6";
            label6.Size = new Size(12, 15);
            label6.TabIndex = 10;
            label6.Text = "-";
            // 
            // _06inquiry
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 609);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "_06inquiry";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "照会";
            Load += _06inquiry_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private Button button1;
        private GroupBox groupBox1;
        private TextBox textBox2;
        private GroupBox groupBox2;
        private TextBox textBox3;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private TextBox textBox4;
        private Label label4;
        private Button button2;
        private Label label5;
        private Label label6;
    }
}