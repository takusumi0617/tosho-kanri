using System.Data;
using System.Text;

namespace 総合図書管理システム新版
{
    public partial class _16reminder : Form
    {
        public _16reminder()
        {
            InitializeComponent();
        }

        private void _16reminder_Load(object sender, EventArgs e)
        {
            Limtable_Load();
        }

        private void Limtable_Load()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string csvFilePath = MainClass.docment() + "\\総合図書管理システム\\System\\data\\limit.aescsv";
            DataTable dataTable = new DataTable();

            // CSVファイルを一括で復号化
            string encryptedContent = File.ReadAllText(csvFilePath, Encoding.GetEncoding("shift_jis"));
            string decryptedContent = MainClass.AESDe(encryptedContent);

            // ヘッダ行を読み取り、列を作成
            string[] lines = decryptedContent.Split('\n');
            if (lines.Length > 0)
            {
                string headerLine = lines[0].Trim();
                string[] headers = headerLine.Split(',');
                foreach (string header in headers)
                {
                    dataTable.Columns.Add(header);
                }
            }

            // 残りの行を読み取り、DataTableに追加
            for (int i = 1; i < lines.Length; i++)
            {
                string decryptedLine = lines[i].Trim();
                if (!string.IsNullOrEmpty(decryptedLine))
                {
                    string[] values = decryptedLine.Split(',');
                    dataTable.Rows.Add(values);
                }
            }

            // 期限が現在の日時より古い行だけを抽出
            DateTime currentDate = DateTime.Now;
            DataView dv = dataTable.DefaultView;
            dv.RowFilter = $"期限 < '{currentDate.ToString("yyyy/MM/dd HH:mm:ss")}'";

            // DataGridViewにデータをバインド
            dataGridView1.DataSource = dv.ToTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Printer printer = new Printer(dataGridView1);
            printer.Print();
        }
    }
}
