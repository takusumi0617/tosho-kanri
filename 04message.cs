using System.Text;

namespace 総合図書管理システム新版
{
    public partial class _04message : Form
    {
        private string usertype = "";
        public _04message(string usertypename)
        {
            InitializeComponent();
            usertype = usertypename;
        }

        static string messagepath = MainClass.docment() + "\\総合図書管理システム\\伝言データ";
        static string roomname;
        static string rowmessage;
        static string newmessage;

        private void _04message_Load(object sender, EventArgs e)
        {
            label4.Text = usertype;
            if (usertype != "管理者")
            {
                button1.Enabled = false;
            }
            textBox1.BackColor = SystemColors.Control;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (usertype == "管理者")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                button4.Enabled = true;
                textBox2.Enabled = true;

                StreamReader sr = new StreamReader(messagepath + "\\AA\\message.aestxt", Encoding.GetEncoding("shift_jis"));
                rowmessage = MainClass.AESDe(sr.ReadToEnd());
                sr.Close();
                newmessage = rowmessage;
                textBox1.Text = newmessage;
                roomname = "AA";
                button1.BackColor = Color.FromArgb(255, 128, 128);
                button2.BackColor = SystemColors.Control;
                button2.UseVisualStyleBackColor = true;
            }
            else
            {
                MessageBox.Show("アクセス権限がありません。管理者のアカウントで再ログインしてください。", "権限エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            textBox1.Text = "";
            textBox2.Text = "";
            button4.Enabled = true;
            textBox2.Enabled = true;

            StreamReader sr = new StreamReader(messagepath + "\\AU\\message.aestxt", Encoding.GetEncoding("shift_jis"));
            rowmessage = MainClass.AESDe(sr.ReadToEnd());
            sr.Close();
            newmessage = rowmessage;
            textBox1.Text = newmessage;
            roomname = "AU";
            button1.BackColor = SystemColors.Control;
            button1.UseVisualStyleBackColor = true;
            button2.BackColor = Color.FromArgb(255, 128, 128);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (textBox2.Text != "")
            {
                DateTime dt = DateTime.Now;
                if (roomname == "AA")
                {
                    string Endata = MainClass.AESEn(textBox1.Text + "\r\n" + dt.ToString("[yyyy/MM/dd HH:mm:ss]") + textBox2.Text);
                    StreamWriter writer = new StreamWriter(messagepath + "\\AA\\message.aestxt", false, Encoding.GetEncoding("shift_jis"));
                    writer.Write(Endata);
                    writer.Close();
                    textBox2.Text = "";
                }
                else if (roomname == "AU")
                {
                    string Endata = MainClass.AESEn(textBox1.Text + "\r\n" + dt.ToString("[yyyy/MM/dd HH:mm:ss]") + textBox2.Text);
                    StreamWriter writer = new StreamWriter(messagepath + "\\AU\\message.aestxt", false, Encoding.GetEncoding("shift_jis"));
                    writer.Write(Endata);
                    writer.Close();
                    textBox2.Text = "";
                }
                else
                {
                    MessageBox.Show("左からルームを選択してください。", "ルーム選択", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                StreamReader sr = new StreamReader(messagepath + "\\" + roomname + "\\message.aestxt", Encoding.GetEncoding("shift_jis"));
                rowmessage = MainClass.AESDe(sr.ReadToEnd());
                sr.Close();
                textBox1.Text = rowmessage;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            DialogResult d = MessageBox.Show("本当にこの履歴を削除しますか。\r\n削除後、復元することはできません。", "履歴削除の警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (d == DialogResult.Yes)
            {
                DateTime dt = DateTime.Now;
                if (roomname == "AA")
                {
                    StreamWriter writer = new StreamWriter(messagepath + "\\AA\\message.aestxt", false, Encoding.GetEncoding("shift_jis"));
                    string Endata = MainClass.AESEn(dt.ToString("このルームは[yyyy/MM/dd HH:mm:ss]に履歴が削除されました。") + "\r\n----------------------------------");
                    writer.Write(Endata);
                    writer.Close();
                    textBox1.Text = dt.ToString("このルームは[yyyy/MM/dd HH:mm:ss]に履歴が削除されました。\r\n");
                    textBox1.AppendText("----------------------------------");
                    textBox2.Text = "";
                }
                else if (roomname == "AU")
                {
                    StreamWriter writer = new StreamWriter(messagepath + "\\AU\\message.aestxt", false, Encoding.GetEncoding("shift_jis"));
                    string Endata = MainClass.AESEn(dt.ToString("このルームは[yyyy/MM/dd HH:mm:ss]に履歴が削除されました。") + "\r\n----------------------------------");
                    writer.Write(Endata);
                    writer.Close();
                    textBox1.Text = dt.ToString("このルームは[yyyy/MM/dd HH:mm:ss]に履歴が削除されました。\r\n");
                    textBox1.AppendText("----------------------------------");
                    textBox2.Text = "";
                }
                else
                {
                    MessageBox.Show("左からルームを選択してください。", "ルーム選択", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
