using System.Data;
using System.Text;

namespace 総合図書管理システム新版
{
    public partial class _19bookremove : Form
    {
        public _19bookremove()
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
                int columnIndex = dataGridView1.Columns["書名"].Index;
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


            if (textBox2.Text != "")
            {
                DataGridViewRowCollection rows = dataGridView1.Rows;
                int columnIndex = dataGridView1.Columns["内容"].Index;
                string searchString = textBox2.Text;
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
                int columnIndex = dataGridView1.Columns["登録番号"].Index;
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

            if (textBox4.Text != "")
            {
                DataGridViewRowCollection rows = dataGridView1.Rows;
                int columnIndex = dataGridView1.Columns["著者名"].Index;
                string searchString = textBox3.Text;
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
                ReadCSV(table, true, MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\masterdata.aescsv");

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
                    dt.Columns.Add("除籍理由", typeof(string));
                    foreach (string header in values)
                    {
                        dt.Columns.Add(header.Trim());
                    }
                }
                else
                {
                    DataRow row = dt.NewRow();
                    row["選択"] = false;
                    row["除籍理由"] = "";
                    for (int i = 0; i < values.Length; i++)
                    {
                        row[i + 2] = values[i].Trim();
                    }
                    dt.Rows.Add(row);
                }
                rowCount++;
            }
        }

        private void Form19_Load(object sender, EventArgs e)
        {
            DGVrefresh();
            List<int> columnIndices = new List<int>() { 3, 4, 5, 6, 7, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 23, 24, 25, 26 };
            foreach (int columnIndex in columnIndices.OrderByDescending(x => x))
            {
                if (dataGridView1.Columns.Count > columnIndex)
                {
                    dataGridView1.Columns[columnIndex].Visible = false;
                }
            }

            dataGridView1.Columns["選択"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["登録番号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["受入日"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["ISBN13"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["金額"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["登録番号"].ReadOnly = true;
            dataGridView1.Columns["受入日"].ReadOnly = true;
            dataGridView1.Columns["ISBN13"].ReadOnly = true;
            dataGridView1.Columns["書名"].ReadOnly = true;
            dataGridView1.Columns["書名カタカナ"].ReadOnly = true;
            dataGridView1.Columns["金額"].ReadOnly = true;
            dataGridView1.Columns["選択"].DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192);
            dataGridView1.Columns["除籍理由"].DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                string csvFilePath = MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\masterdata.aescsv";
                string csvDFilePath = MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\deleteddata.aescsv";
                List<string> selectedRegistrationNumbers = new List<string>();
                List<string?[]> deletedData = new List<string?[]>();

                // チェックボックスがTrueの行の登録番号を取得
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["選択"].Value != null && (bool)row.Cells["選択"].Value)
                    {
                        string registrationNumber = row.Cells["登録番号"].Value.ToString();
                        selectedRegistrationNumbers.Add(registrationNumber);

                        string?[] rowData = row.Cells.Cast<DataGridViewCell>()
                            .Skip(1) // 登録番号以外の列のデータを保存する場合、適宜修正してください
                            .Select(cell => cell.Value.ToString())
                            .ToArray();
                        deletedData.Add(rowData);
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

                    {
                        // DataTableをCSVに変換
                        StringBuilder csvData = new StringBuilder();

                        StreamReader sr = new StreamReader(csvDFilePath, Encoding.GetEncoding("shift_jis"));
                        string Endata = sr.ReadToEnd();
                        sr.Close();
                        csvData.AppendLine(MainClass.AESDe(Endata));

                        foreach (string?[] rowData in deletedData)
                        {
                            csvData.AppendLine(string.Join(",", rowData));
                        }
                        // CSVデータをAESで暗号化
                        string encryptedData = MainClass.AESEn(csvData.ToString().TrimEnd('\n', '\r'));

                        // 暗号化されたデータをファイルに書き込み
                        StreamWriter sw = new StreamWriter(csvDFilePath, false, Encoding.GetEncoding("shift_jis"));
                        sw.Write(encryptedData);
                        sw.Close();
                    }
                }
                MessageBox.Show("正常に除籍されました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Columns.Clear();
                Form19_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("処理中にエラーが発生しました。\r\n" + ex.Message, "除籍エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("本当にすべて選択しますか？選択された状態の蔵書は除籍ボタンを押すと除籍されます。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                    checkBoxCell.Value = true;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                checkBoxCell.Value = false;
            }
        }
    }
}
