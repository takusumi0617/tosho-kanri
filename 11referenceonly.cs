using System.Data;
using System.Text;

namespace 総合図書管理システム新版
{
    public partial class _11referenceonly : Form
    {
        public _11referenceonly()
        {
            InitializeComponent();
        }
        static int changing = 0;
        static string csv = MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\masterdata.aescsv";
        private DataTable table;

        private void Form11_Load(object sender, EventArgs e)
        {
            table = new DataTable("Table");
            table = createDataTable();
            dataGridView1.DataSource = table;
            List<int> columnIndices = new List<int>() { 1, 2, 5, 6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            foreach (int columnIndex in columnIndices.OrderByDescending(x => x))
            {
                if (dataGridView1.Columns.Count > columnIndex)
                {
                    dataGridView1.Columns[columnIndex].Visible = false;
                }
            }
            dataGridView1.Columns["登録番号"].ReadOnly = true;
            dataGridView1.Columns["貸出区分"].ReadOnly = true;
            dataGridView1.Columns["書名"].ReadOnly = true;
            dataGridView1.Columns["貸出区分記号"].DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192);
        }

        private DataTable createDataTable()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            DataTable dt = new DataTable();
            StreamReader streamReader = new StreamReader(csv, Encoding.GetEncoding("shift_jis"));
            string Endata = streamReader.ReadToEnd();
            streamReader.Close();
            string csvString = MainClass.AESDe(Endata);
            // 文字列を行ごとに分割
            StringReader stringReader = new StringReader(csvString);
            string line;
            int rowCount = 0;

            while ((line = stringReader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                // ヘッダー行の処理
                if (rowCount == 0)
                {
                    dt.Columns.Clear();
                    foreach (string header in values)
                    {
                        dt.Columns.Add(header.Trim());
                    }
                }
                else
                {
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < values.Length; i++)
                    {
                        row[i] = values[i].Trim();
                    }
                    dt.Rows.Add(row);
                }
                rowCount++;
            }
            return dt;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // イベントが発生したセルが2列目であることを確認する
            if (e.ColumnIndex == 3)
            {
                // イベントが発生したセルの値を取得する
                string value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                // 対応表に基づいて右隣のセルを変更する
                switch (value)
                {
                    case "1":
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = "一般貸出";
                        break;
                    case "2":
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = "職員限定貸出";
                        break;
                    case "3":
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = "貸出禁止";
                        break;
                    default:
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = "その他";
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void save()
        {
            DialogResult result = MessageBox.Show("変更した内容を保存します。" + "\n" + "よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                return;
            }
            changing = 1;
            string FILE_PATH = MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\masterdata.aescsv";
            string lines = "";

            // ヘッダー行を追加
            StringBuilder header = new StringBuilder();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                header.Append(column.HeaderText).Append(",");
            }
            lines += header.ToString().TrimEnd(',') + "\r\n";

            // データ行を追加（暗号化）
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                StringBuilder line = new StringBuilder();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    // セルの文字列をAESで暗号化して追加
                    line.Append(cell.Value.ToString()).Append(",");
                }
                lines += line.ToString().TrimEnd(',') + "\r\n";
            }

            string stream = MainClass.AESEn(lines.TrimEnd('\n', '\r')).TrimEnd('\n', '\r');

            // ファイルに書き込み
            File.WriteAllText(FILE_PATH, stream);
            MessageBox.Show("保存が完了しました。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            changing = 0;
        }

        private void Form11_FormClosing(object sender, FormClosingEventArgs e)
        {
            save();
        }
    }
}
