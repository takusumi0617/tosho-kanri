using System.Data;
using System.Text;

namespace 総合図書管理システム新版
{
    public partial class _20userremove : Form
    {
        public _20userremove()
        {
            InitializeComponent();
        }

        private DataTable table;

        private void button1_Click(object sender, EventArgs e)
        {
            DGVrefresh();
            List<DataGridViewRow> rowsToDelete = new List<DataGridViewRow>();
            if (textBox1.Text != "")
            {
                DataGridViewRowCollection rows = dataGridView1.Rows;
                int columnIndex = dataGridView1.Columns["氏名"].Index;
                string searchString = textBox1.Text;
                foreach (DataGridViewRow row in rows)
                {
                    if (row.Cells[columnIndex].Value != null && row.Cells[columnIndex].Value.ToString().Contains(searchString))
                    {

                    }
                    else
                    {
                        rowsToDelete.Add(row);
                    }
                }
            }

            if (textBox3.Text != "")
            {
                DataGridViewRowCollection rows = dataGridView1.Rows;
                int columnIndex = dataGridView1.Columns["利用者番号"].Index;
                string searchString = textBox3.Text;
                foreach (DataGridViewRow row in rows)
                {
                    if (row.Cells[columnIndex].Value != null && row.Cells[columnIndex].Value.ToString().Equals(searchString))
                    {

                    }
                    else
                    {
                        rowsToDelete.Add(row);
                    }
                }
            }

            foreach (DataGridViewRow row in rowsToDelete)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void DGVrefresh()
        {
            try
            {
                table = new DataTable("Table");
                ReadCSV(table, true, MainClass.docment() + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv");

                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ReadCSV(DataTable dt, bool hasHeader, string fileName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            StreamReader streamReader = new StreamReader(fileName, Encoding.GetEncoding("shift_jis"));
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
                if (rowCount == 0 && hasHeader)
                {
                    dt.Columns.Add("選択", typeof(bool));
                    foreach (string header in values)
                    {
                        dt.Columns.Add(header.Trim());
                    }
                }
                else
                {
                    DataRow row = dt.NewRow();
                    row["選択"] = false;
                    for (int i = 0; i < values.Length; i++)
                    {
                        row[i + 1] = values[i].Trim();
                    }
                    dt.Rows.Add(row);
                }
                rowCount++;
            }
        }

        private void Form19_Load(object sender, EventArgs e)
        {
            DGVrefresh();

            dataGridView1.Columns["選択"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["利用者番号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["学年"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["クラス"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["番号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["氏名"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["利用者権限レベル"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["利用者権限"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["利用者番号"].ReadOnly = true;
            dataGridView1.Columns["学年"].ReadOnly = true;
            dataGridView1.Columns["クラス"].ReadOnly = true;
            dataGridView1.Columns["番号"].ReadOnly = true;
            dataGridView1.Columns["氏名"].ReadOnly = true;
            dataGridView1.Columns["利用者権限レベル"].ReadOnly = true;
            dataGridView1.Columns["利用者権限"].ReadOnly = true;
            dataGridView1.Columns["選択"].DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                string csvFilePath = MainClass.docment() + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv";

                List<string> selectedRegistrationNumbers = new List<string>();

                // チェックボックスがTrueの行の登録番号を取得
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["選択"].Value != null && (bool)row.Cells["選択"].Value)
                    {
                        string registrationNumber = row.Cells["利用者番号"].Value.ToString();
                        selectedRegistrationNumbers.Add(registrationNumber);
                    }
                }

                if (selectedRegistrationNumbers.Count > 0)
                {
                    // CSVファイルを読み込んでデータを変更
                    DataTable dataTable = new DataTable();
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        StreamReader streamReader = new StreamReader(csvFilePath, Encoding.GetEncoding("shift_jis"));
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
                                foreach (string header in values)
                                {
                                    dataTable.Columns.Add(header.Trim());
                                }
                            }
                            else
                            {
                                DataRow row = dataTable.NewRow();
                                for (int i = 0; i < values.Length; i++)
                                {
                                    row[i] = values[i].Trim();
                                }
                                string registrationNumber = values[0]; // 登録番号の列の位置に応じて修正してください
                                if (!selectedRegistrationNumbers.Contains(registrationNumber))
                                {
                                    dataTable.Rows.Add(row);
                                }
                            }
                            rowCount++;
                        }
                    }

                    {
                        // DataTableをCSVに変換
                        StringBuilder csvData = new StringBuilder();
                        csvData.AppendLine(string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(col => col.ColumnName)));
                        foreach (DataRow row in dataTable.Rows)
                        {
                            csvData.AppendLine(string.Join(",", row.ItemArray));
                        }

                        // CSVデータをAESで暗号化
                        string encryptedData = MainClass.AESEn(csvData.ToString().TrimEnd('\n', '\r'));

                        // 暗号化されたデータをファイルに書き込み
                        File.WriteAllText(csvFilePath, encryptedData, Encoding.GetEncoding("shift_jis"));
                    }
                }
                MessageBox.Show("正常に登録解除されました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Columns.Clear();
                Form19_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("処理中にエラーが発生しました。\r\n" + ex.Message, "解除エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("本当にすべて選択しますか？選択された状態の利用者は登録解除ボタンを押すと登録解除されます。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                    checkBoxCell.Value = true;
                }
            }
        }
    }
}
