using System.Text;

namespace 総合図書管理システム新版
{
    public partial class _23icid : Form
    {
        public _23icid()
        {
            InitializeComponent();
        }

        static string docment = MainClass.docment();

        private void button1_Click(object sender, EventArgs e)
        {
            c03process c03 = new c03process();
            string[] cardreaddata = c03.Cardread();
            if (cardreaddata[0] != "")
            {
                label5.Text = cardreaddata[0];
            }
            else
            {
                label5.Text = "読取失敗";
            }
            if (cardreaddata[1] != "")
            {
                MessageBox.Show(cardreaddata[1], "カード通信ログ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (label5.Text != "(未読み取り)" && label5.Text != "読取失敗")
            {
                string filePath = docment + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv";
                string searchValue = textBox1.Text;
                string replacementValue = label5.Text;

                StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("Shift_jis"));
                string Endata = sr.ReadToEnd();
                sr.Close();
                string Dedata = MainClass.AESDe(Endata);

                string[] lines = Dedata.Split(Environment.NewLine);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] columns = lines[i].Split(',');
                    if (columns[0] == searchValue)
                    {
                        columns[7] = replacementValue;
                    }
                    lines[i] = string.Join(",", columns);
                }

                // 変更を保存
                File.WriteAllText(filePath, MainClass.AESEn(String.Join("\r\n", lines)), Encoding.GetEncoding("Shift_jis"));
                MessageBox.Show("データを登録しました。\r\n利用者番号:" + searchValue + "\r\nICカード番号:" + replacementValue, "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
            else
            {
                MessageBox.Show("カードを読み取ってください。", "登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
