namespace 総合図書管理システム新版
{
    public partial class _06inquiry : Form
    {
        public _06inquiry()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Replace(" ", "") != "")
            {
                if (c03process.usernamecall(textBox1.Text).Length >= 1)
                {
                    //履歴表示
                    if (File.Exists(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\統計データ\\" + textBox1.Text.Replace(" ", "") + "\\bookdata.txt") == true)
                    {
                        StreamReader sr = new StreamReader(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\統計データ\\" + textBox1.Text.Replace(" ", "") + "\\bookdata.txt");
                        string text = sr.ReadToEnd();
                        sr.Close();
                        textBox2.Text = text;
                    }
                    else
                    {
                        textBox2.Text = "履歴なし";
                    }
                    //現在貸出中蔵書表示
                    try
                    {
                        textBox3.Text = "";
                        string[] names = Directory.GetFiles(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\貸出者一覧\\" + textBox1.Text.Replace(" ", ""), "*.txt");
                        foreach (string name in names)
                        {
                            textBox3.AppendText(Path.GetFileNameWithoutExtension(name) + "\r\n");
                        }
                        if (textBox3.Text == "")
                        {
                            textBox3.Text = "貸出図書なし";
                        }
                    }
                    catch { textBox3.Text = "貸出図書なし"; }
                    //予約表示
                    try
                    {
                        var fileList1 = Directory.GetFiles(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\予約者一覧\\" + textBox1.Text.Replace(" ", ""), "*.txt");
                        bool exist = fileList1.Length > 0;
                        if (exist == true)
                        {
                            string reserved = "";
                            string[] names = Directory.GetFiles(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\予約者一覧\\" + textBox1.Text.Replace(" ", ""), "*.txt");
                            foreach (string name in names)
                            {
                                reserved += Path.GetFileNameWithoutExtension(name) + "\r\n";
                            }
                            textBox4.Text = reserved.TrimEnd('\r', '\n');
                        }
                        else
                        {
                            textBox4.Text = "予約中蔵書なし";
                        }
                    }
                    catch
                    {
                        textBox4.Text = "予約中蔵書なし";
                    }

                    //延滞表示
                    if (File.Exists(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\延滞履歴\\" + textBox1.Text.Replace(" ", "") + "\\exist.txt") == true)
                    {
                        StreamReader sr = new StreamReader(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\延滞履歴\\" + textBox1.Text.Replace(" ", "") + "\\exist.txt");
                        string text = sr.ReadToEnd();
                        sr.Close();
                        label4.Text = "延滞歴あり(" + text + ")";
                        label4.ForeColor = Color.Red;
                        button2.Enabled = true;
                    }
                    else
                    {
                        label4.Text = "延滞なし";
                        label4.ForeColor = Color.FromName("ControlText");
                        button2.Enabled = false;
                    }

                    label6.Text = textBox1.Text;
                }
                else
                {
                    MessageBox.Show("この利用者番号に一致する利用者が存在しません。", "データなし", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("利用者番号を入力してください。", "処理エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _06inquiry_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label4.Text != "延滞なし")
            {
                FileInfo file = new FileInfo(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\延滞履歴\\" + label6.Text.Replace(" ", "") + "\\exist.txt");
                if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) file.Attributes = FileAttributes.Normal;
                file.Delete();
                MessageBox.Show("このユーザーの延滞履歴を削除しました。", "履歴削除", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("このユーザーには延滞履歴がありません。", "削除エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            button1_Click(sender, e);
        }
    }
}
