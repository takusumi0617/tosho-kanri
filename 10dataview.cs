using System.Data;

namespace 総合図書管理システム新版
{
    public partial class _10dataview : Form
    {
        static string number;
        string usernumber = "";
        public _10dataview(string datanumber, string user = "")
        {
            InitializeComponent();
            number = datanumber;
            usernumber = user;
        }

        private void _10dataview_Load(object sender, EventArgs e)
        {
            DataRow[] rows = c03process.booknamecall(number.Replace(" ", ""));
            label2.Text = "登録番号:" + rows[0]["登録番号"].ToString();
            label3.Text = "資料区分:" + rows[0]["資料区分記号"].ToString() + "." + rows[0]["資料区分"].ToString();
            label4.Text = "貸出区分:" + rows[0]["貸出区分記号"].ToString() + "." + rows[0]["貸出区分"].ToString();
            label5.Text = "分類番号:" + rows[0]["請求番号"].ToString();
            label6.Text = "受入れ日:" + rows[0]["受入日"].ToString();
            label7.Text = "ISBN番号:" + rows[0]["ISBN13"].ToString();
            label8.Text = "書名　　:" + rows[0]["書名"].ToString() + " , " + rows[0]["書名カタカナ"].ToString();
            label9.Text = "著者名　:" + rows[0]["著者名"].ToString() + " , " + rows[0]["著者名カタカナ"].ToString();
            label10.Text = "内容　　:" + rows[0]["内容"].ToString() + " , " + rows[0]["件名"].ToString();
            label11.Text = "出版情報:" + rows[0]["出版年"].ToString() + " , " + rows[0]["出版社"].ToString();
            label12.Text = "書籍情報:" + rows[0]["ページ数"].ToString() + "p , " + rows[0]["大きさ"].ToString() + " , " + rows[0]["金額"].ToString() + "円";
            label13.Text = "所在情報:" + rows[0]["受入先"].ToString() + " , " + rows[0]["所在記号"].ToString() + "." + rows[0]["所在"].ToString();
            label14.Text = "費目情報:" + rows[0]["費目記号"].ToString() + "." + rows[0]["費目"].ToString();
            label15.Text = "備考　　:" + rows[0]["備考"].ToString();
            if (usernumber != "")
            {
                textBox1.Text = usernumber;
                textBox1.Enabled = false;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            DataRow[] rowu = c03process.usernamecall(textBox1.Text.Replace(" ", ""));
            if (rowu.Length == 1)
            {
                Directory.CreateDirectory(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\予約者一覧\\" + textBox1.Text.Replace(" ", ""));
                StreamWriter writer = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\予約者一覧\\" + textBox1.Text.Replace(" ", "") + "\\" + number.Replace(" ", "") + ".txt", false);
                writer.Write(d.ToString("yyyy/MM/dd HH:mm:ss"));
                writer.Close();
                Directory.CreateDirectory(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\予約本一覧\\" + number.Replace(" ", ""));
                StreamWriter writer2 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\予約本一覧\\" + number.Replace(" ", "") + "\\" + textBox1.Text.Replace(" ", "") + ".txt", false);
                writer2.Write(d.ToString("yyyy/MM/dd HH:mm:ss"));
                writer2.Close();
                MessageBox.Show("予約を完了しました。", "予約完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button1.Enabled = false;
            }
            else
            {
                MessageBox.Show("利用者情報が登録されていないか、重複して登録されています。", "照会エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
