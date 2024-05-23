using System.Data;

namespace 総合図書管理システム新版
{
    public partial class _02Menu : Form
    {
        public string usertype = "";
        public _02Menu(string type)
        {
            InitializeComponent();
            if (type == "user")
            {
                usertype = "一般ユーザー";
            }
            else if (type == "admin")
            {
                usertype = "管理者";
            }
        }
        public int lendday = 7;
        private static string userid = "";
        DateTime d = DateTime.Now;

        private void _02Menu_Load(object sender, EventArgs e)
        {
            timer1.Interval = 3000;
            c03process c03 = new c03process();
            lendday = c03.Rentalperiod(false);
            label13.Text = lendday.ToString();
            textBox1.Focus();
            Lend();
            label12.Text = usertype;
            timer1.Enabled = true;
            timer1_Tick(sender, e);
            textBox2.BackColor = SystemColors.Control;
            textBox3.BackColor = SystemColors.Control;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = d.ToString("yyyy年MM月dd日(ddd)");
            lendday = int.Parse(label13.Text);
            label7.Text = d.AddDays(lendday).ToString("yyyy年MM月dd日(ddd)");
        }

        private void Lend()
        {
            groupBox1.BackColor = Color.FromArgb(255, 77, 88);
            label1.Text = "貸出";
            label2.Text = "利用者ID";
            label3.Text = "lend";
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            userid = String.Empty;
        }
        private void Return()
        {
            groupBox1.BackColor = Color.FromArgb(66, 215, 245);
            label1.Text = "返却";
            label2.Text = "書籍ID";
            label3.Text = "return";
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            userid = String.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Replace(" ", "") != "")
            {
                if (label1.Text == "貸出")
                {
                    if (label2.Text == "利用者ID")
                    {
                        DataRow[] rowu = c03process.usernamecall(textBox1.Text.Replace(" ", ""));
                        if (rowu != null)
                        {
                            if (rowu.Length == 1)
                            {
                                userid = textBox1.Text;
                                label2.Text = "書籍ID";
                                textBox1.Text = String.Empty;
                                string data = "";
                                if (File.Exists(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\統計データ\\" + userid.Replace(" ", "") + "\\bookdata.txt") == true)
                                {
                                    StreamReader sr = new StreamReader(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\統計データ\\" + userid.Replace(" ", "") + "\\bookdata.txt");
                                    string text = sr.ReadToEnd();
                                    sr.Close();
                                    data = text;
                                }

                                if (File.Exists(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\延滞履歴\\" + userid.Replace(" ", "") + "\\exist.txt") == true)
                                {
                                    StreamReader sr = new StreamReader(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\延滞履歴\\" + userid.Replace(" ", "") + "\\exist.txt");
                                    string text = sr.ReadToEnd();
                                    sr.Close();
                                    string late = text;
                                    textBox2.Text = "過去に延滞履歴(" + late + ")あり\r\n\r\n" + data;
                                }
                                else
                                {
                                    textBox2.Text = "延滞なし\r\n\r\n" + data;
                                }
                            }
                            else
                            {
                                MessageBox.Show("この利用者番号は登録されていません。\r\n利用者を登録してから貸出処理を行ってください。", "利用者データエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("この利用者番号は登録されていません。\r\n利用者を登録してから貸出処理を行ってください。", "利用者データエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        textBox3.Text += c03process.Lending(userid, textBox1.Text, lendday);
                        label2.Text = "利用者ID";
                        textBox1.Text = String.Empty;
                        textBox2.Text = String.Empty;
                    }
                }
                else
                {
                    textBox3.Text += c03process.Returning(textBox1.Text);
                    textBox1.Text = String.Empty;
                    textBox2.Text = String.Empty;
                }
            }
            else
            {
                MessageBox.Show("番号を入力してください。", "処理エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _03search form3 = new _03search();
            form3.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (usertype == "管理者")
            {
                _06inquiry form6 = new _06inquiry();
                form6.ShowDialog();
            }
            else
            {
                MessageBox.Show("管理者ユーザーのみ開くことができます。", "権限エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _04message form4 = new _04message(usertype);
            form4.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (usertype == "管理者")
            {
                _05settings form5 = new _05settings();
                form5.ShowDialog();
            }
            else
            {
                MessageBox.Show("管理者ユーザーのみ開くことができます。", "権限エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _07reserve form7 = new _07reserve();
            form7.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var processclass = new c03process();
            MessageBox.Show("カードの読み取りを開始します。\r\nカードをリーダーにかざしてください。", "読み取りの開始", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string[] cardreaddata = processclass.Cardread();
            string cardid;
            if (cardreaddata[0] != "")
            {
                cardid = cardreaddata[0];
            }
            else
            {
                if (cardreaddata[1] != "")
                {
                    MessageBox.Show(cardreaddata[1], "カード通信ログ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                MessageBox.Show("カードの読み取りに失敗しました。");
                return;
            }
            DataRow[] id = c03process.icidcall(cardid);
            if (cardid != null && id != null)
            {
                if (id.Length == 1)
                {
                    textBox1.Text = id[0]["利用者番号"].ToString();
                    button1_Click(sender, e);
                }
                else if (id.Length == 0)
                {
                    MessageBox.Show("このカードに一致する利用者データがありません。");
                }
                else if (id.Length > 1)
                {
                    MessageBox.Show("データが重複しています。");
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                textBox1.Text = String.Empty;
                if (label1.Text == "貸出") Return();
                else Lend();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            if (label1.Text == "貸出") Return();
            else Lend();
        }

        private void _02Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textBox3.Text != "＝＝ログ開始＝＝")
            {
                textBox3.Text += "\r\n＝＝ログ終了＝＝";
                DateTime dt = DateTime.Now;
                StreamWriter sw = new StreamWriter($"{MainClass.docment()}\\総合図書管理システム\\System\\log\\{dt.ToString("yyyyMMddHHmmss")}.log");
                sw.Write(textBox3.Text);
                sw.Close();
            }
        }
    }
}
