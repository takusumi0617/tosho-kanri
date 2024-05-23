using System.Data;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace 総合図書管理システム新版
{
    public partial class _24inspection : Form
    {
        DataTable original;
        DataTable check;
        DataTable lend;
        DataTable unknown;
        DataTable delete;
        int num = 0;

        public _24inspection()
        {
            InitializeComponent();
            original = new DataTable();
            check = new DataTable();
            lend = new DataTable();
            unknown = new DataTable();
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSVファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*",
                Title = "CSVファイルを選択してください"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string csvFilePath = openFileDialog.FileName;
                try
                {
                    using (StreamReader reader = new StreamReader(csvFilePath))
                    {
                        reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            string[] fields = reader.ReadLine().Split(',');

                            // DataTableに行を追加
                            DataRow row = check.NewRow();
                            row[0] = fields[0];
                            row[1] = c03process.booknamecall(fields[0])[0]["書名"].ToString();
                            check.Rows.Add(row);
                        }
                    }
                    MessageBox.Show("CSVファイルの読み込みが完了しました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button2_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"CSVファイル読み取り時にエラーが発生しました。\r\n{ex.Message}", "CSV読み取りエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listview_setting(check);
            label6.Text = "蔵書点検でチェックした蔵書一覧";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable lendrow = new DataTable();
            aescsv_load(MainClass.docment() + "\\総合図書管理システム\\System\\data\\limit.aescsv", lendrow);
            lend.Clear();
            lend = lendrow.Clone();
            //貸出リストにもチェック済みリストにも存在しているデータ(返却処理していない不正返却)
            var differences = check.AsEnumerable().Intersect(lendrow.AsEnumerable(), DataRowComparer.Default);
            foreach (DataRow row in differences)
            {
                lend.ImportRow(row);
            }
            listview_setting(lend);
            label6.Text = "返却処理されずに返却された蔵書一覧";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable lendrow = new DataTable();
            aescsv_load(MainClass.docment() + "\\総合図書管理システム\\System\\data\\limit.aescsv", lendrow);
            unknown.Clear();
            unknown = lendrow.Clone();

            var differences = original.AsEnumerable().Except(check.AsEnumerable(), DataRowComparer.Default)
                                                     .Except(lendrow.AsEnumerable(), DataRowComparer.Default);
            foreach (DataRow row in differences)
            {
                unknown.ImportRow(row);
            }
            listview_setting(unknown);
            label6.Text = "貸出処理されずに貸出された蔵書一覧";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable lendrow = new DataTable();
            aescsv_load(MainClass.docment() + "\\総合図書管理システム\\System\\data\\limit.aescsv", lendrow);
            {
                lend.Clear();
                lend = lendrow.Clone();
                //貸出リストにもチェック済みリストにも存在しているデータ(返却処理していない不正返却)
                var differences = check.AsEnumerable().Intersect(lendrow.AsEnumerable(), DataRowComparer.Default);
                foreach (DataRow row in differences)
                { lend.ImportRow(row); }
            }
            {
                unknown.Clear();
                unknown = lendrow.Clone();
                var differences = original.AsEnumerable().Except(check.AsEnumerable(), DataRowComparer.Default)
                                                         .Except(lendrow.AsEnumerable(), DataRowComparer.Default);
                foreach (DataRow row in differences)
                { unknown.ImportRow(row); }
            }
            DataTable uniondata = new DataTable();
            uniondata = original.Clone();
            var union = lend.AsEnumerable().Union(unknown.AsEnumerable(), DataRowComparer.Default);
            foreach (DataRow row in union)
            {
                uniondata.ImportRow(row);
            }
            listview_setting(uniondata);
            label6.Text = "不正返却 + 不正貸出 蔵書一覧";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //checkにデータ追加
                if (c03process.booknamecall(textBox1.Text).Count() > 0)
                {
                    check.Rows.Add(textBox1.Text, c03process.booknamecall(textBox1.Text)[0]["書名"].ToString());
                    textBox1.Text = string.Empty;
                    button2_Click(sender, e);
                }
            }
        }

        private void _24inspection_Load(object sender, EventArgs e)
        {
            string progress_path = $"{MainClass.docment()}\\総合図書管理システム\\System\\data\\inspection.aescsv";
            if (File.Exists(progress_path))
            {
                aescsv_load(progress_path, check);
                File.Delete(progress_path);
            }
            else
            {
                check.Columns.Add("蔵書番号");
                check.Columns.Add("蔵書名");
            }
            aescsv_load($"{MainClass.docment()}\\総合図書管理システム\\蔵書データ\\masterdata.aescsv", original);
            lend.Columns.Add("蔵書名");
            unknown.Columns.Add("蔵書番号");
            unknown.Columns.Add("蔵書名");
        }

        void aescsv_load(string path, DataTable dt)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("shift_jis"));
            string Endata = streamReader.ReadToEnd();
            streamReader.Close();
            string csvString = MainClass.AESDe(Endata);
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
                    dt.Columns.Add("蔵書番号");
                    dt.Columns.Add("蔵書名");
                }
                else
                {
                    DataRow row = dt.NewRow();
                    row[0] = values[0].Trim();
                    row[1] = c03process.booknamecall(values[0].Trim())[0]["書名"].ToString();
                    dt.Rows.Add(row);
                }
                rowCount++;
            }
        }

        void listview_setting(DataTable dt)
        {
            listView1.Clear();
            listView1.Columns.Add("蔵書番号", 100);
            listView1.Columns.Add("蔵書名", 250);
            listView1.View = View.Details;
            foreach (DataRow row in dt.Rows)
            {
                if (row != null)
                {
                    ListViewItem item = new ListViewItem(row[0].ToString());
                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        item.SubItems.Add(row[i].ToString());
                    }
                    listView1.Items.Add(item);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("蔵書点検で発見された蔵書すべてを表示します。", "ヘルプ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("蔵書点検で「貸出中」にもかかわらず発見された蔵書を表示します。\r\n※この蔵書は返却処理されていません。\r\n(返却処理せず無断で返却されています。)", "ヘルプ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("蔵書点検で「貸出中」でないのに発見できなかった蔵書を表示します。\r\n※この蔵書は貸出処理されていません。\r\n(貸出処理せず無断で持ち出されています。)", "ヘルプ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("左二つの「貸出中」「不明」のデータをまとめて表示します。", "ヘルプ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dr = MessageBox.Show("1列目に「点検した蔵書の\"蔵書番号\"」を記録します。それ以外に指定はありません。\r\n「OK」を選択するとひな形を保存できます。", "書式詳細", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "保存場所を選択してください",
                    InitialDirectory = MainClass.docment(),
                    Filter = "CSVファイル|*.csv|すべてのファイル|*.*",
                    FileName = "template.csv"
                };
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string selectedPath = saveFileDialog.FileName;
                    StreamWriter writer = new StreamWriter(selectedPath, false, Encoding.GetEncoding("Shift_jis"));
                    writer.Write("蔵書番号 (↓この向きにデータを追加)");
                    writer.Close();
                }
            }
        }

        private void _24inspection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (num == 0)
            {
                if (MessageBox.Show("進捗を保存しますか？", "途中終了", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    StringBuilder data = new StringBuilder();

                    // 列名を追加
                    foreach (DataColumn column in check.Columns)
                    {
                        data.Append(column.ColumnName);
                        data.Append(",");
                    }
                    data.Length--;
                    data.AppendLine();

                    // データを追加
                    foreach (DataRow row in check.Rows)
                    {
                        for (int i = 0; i < check.Columns.Count; i++)
                        {
                            data.Append(row[i]);
                            data.Append(",");
                        }
                        data.Length--;
                        data.AppendLine();
                    }
                    MainClass.csv_creater($"{MainClass.docment()}\\総合図書管理システム\\System\\data\\inspection.aescsv", data.ToString(), true);
                }
                else
                {
                    if (MessageBox.Show("本当に進捗を破棄しますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        StringBuilder data = new StringBuilder();

                        // 列名を追加
                        foreach (DataColumn column in check.Columns)
                        {
                            data.Append(column.ColumnName);
                            data.Append(",");
                        }
                        data.Length--;
                        data.AppendLine();

                        // データを追加
                        foreach (DataRow row in check.Rows)
                        {
                            for (int i = 0; i < check.Columns.Count; i++)
                            {
                                data.Append(row[i]);
                                data.Append(",");
                            }
                            data.Length--;
                            data.AppendLine();
                        }
                        MainClass.csv_creater($"{MainClass.docment()}\\総合図書管理システム\\System\\data\\inspection.aescsv", data.ToString(), true);
                    }
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                groupBox1.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                groupBox2.Enabled = true;
            }
            else
            {
                groupBox2.Enabled = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (csv_exist_check(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\蔵書点検データ"))
            {

                if (checkBox1.Checked && csv_exist_check(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\蔵書点検データ"))
                {
                    delete = new DataTable();
                    delete.Columns.Add("蔵書番号", typeof(string));
                    delete.Columns.Add("蔵書名", typeof(string));

                    // Load CSV files from specified directory
                    string directoryPath = MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\蔵書点検データ"; // Change this to your directory path
                    DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
                    var latestCsvFiles = directoryInfo.GetFiles("*.csv")
                        .OrderByDescending(f => f.CreationTime)
                        .Take(5);

                    DataTable[] dataTables = new DataTable[5];

                    // Read data from each CSV file into dataTables and compare with dataTableA
                    int i = 0;
                    foreach (var file in latestCsvFiles)
                    {
                        dataTables[i] = ReadCsvFileToDataTable(file.FullName);

                        // Compare data with dataTableA
                        foreach (DataRow row in dataTables[i].Rows)
                        {
                            //checkデータテーブルベースの動作なので不明図書がすり抜ける
                            if (!DataTableContainsRow(original, row))
                            {
                                // Add row to dataTableB if it doesn't exist in dataTableA
                                if (delete.Columns.Count == 0)
                                {
                                    // Create columns in dataTableB based on the structure of dataTables
                                    foreach (DataColumn column in dataTables[i].Columns)
                                    {
                                        delete.Columns.Add(column.ColumnName, column.DataType);
                                    }
                                }
                                delete.ImportRow(row);
                            }
                        }
                        i++;
                    }
                }
                if (checkBox4.Checked)
                {
                    data_print("除籍", delete);
                }
                if (checkBox5.Checked)
                {
                    data_print("除籍", delete);
                }
            }
            if (checkBox6.Checked)
            {
                data_print("不明", unknown);
            }
            if (checkBox3.Checked)
            {
                data_print("不明", unknown);
            }
            if (check.Rows.Count <= 0)
            {
                MessageBox.Show("出力データがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime dt = DateTime.Now;
            string FILE_PATH = MainClass.docment() + $"\\総合図書管理システム\\蔵書データ\\蔵書点検データ\\{dt.ToString("yyyyMMddHHmmss")}.csv";
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (StreamWriter writer = new StreamWriter(FILE_PATH, false))
            {
                // ヘッダーを書き込む
                foreach (DataColumn column in check.Columns)
                {
                    writer.Write(column.ColumnName);
                    if (column.Ordinal < check.Columns.Count - 1)
                    {
                        writer.Write(",");
                    }
                }
                writer.WriteLine();

                // データを書き込む
                foreach (DataRow row in check.Rows)
                {
                    for (int i = 0; i < check.Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString());
                        if (i < check.Columns.Count - 1)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine();
                }
            }
            num = 1;
            Close();
        }

        static bool csv_exist_check(string directoryPath)
        {
            try
            {
                // 指定されたディレクトリ内のすべてのCSVファイルを取得
                string[] csvFiles = Directory.GetFiles(directoryPath, "*.csv");

                // CSVファイルの数が5以上かどうかをチェック
                return csvFiles.Length >= 5;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"エラーが発生しました: {ex.Message}");
                return false;
            }
        }

        private void data_print(string name, DataTable dataTable1)
        {
            //name 不明、除籍
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (s, ev) =>
            {
                // Draw the title at the center
                Font titleFont = new Font("Arial", 16, FontStyle.Bold);
                string title = $"全{name}図書一覧";
                SizeF titleSize = ev.Graphics.MeasureString(title, titleFont);
                ev.Graphics.DrawString(title, titleFont, Brushes.Black, (ev.PageBounds.Width - titleSize.Width) / 2, 100);

                // Draw data from dataTable1
                Font dataFont = new Font("Arial", 12);
                float yPos = 150;
                if (dataTable1.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable1.Rows.Count; i++) // Assuming to print only first two rows
                    {
                        string rowData = $"{dataTable1.Rows[i][0]} - {dataTable1.Rows[i][1]}";
                        ev.Graphics.DrawString(rowData, dataFont, Brushes.Black, 100, yPos);
                        yPos += dataFont.GetHeight();
                    }
                }
            };

            // Select printer
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = pd;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            button2_Click(sender, e);
            button3_Click(sender, e);
            button4_Click(sender, e);
            button5_Click(sender, e);
        }

        static DataTable ReadCsvFileToDataTable(string filePath)
        {
            DataTable dataTable = new DataTable();
            using (StreamReader reader = new StreamReader(filePath))
            {
                bool isFirstRow = true;
                while (!reader.EndOfStream)
                {
                    string[] data = reader.ReadLine().Split(',');
                    if (isFirstRow)
                    {
                        foreach (string header in data)
                        {
                            dataTable.Columns.Add(header.Trim());
                        }
                        isFirstRow = false;
                    }
                    else
                    {
                        DataRow row = dataTable.NewRow();
                        for (int i = 0; i < data.Length; i++)
                        {
                            row[i] = data[i].Trim();
                        }
                        dataTable.Rows.Add(row);
                    }
                }
            }
            return dataTable;
        }

        static bool DataTableContainsRow(DataTable dataTable, DataRow row)
        {
            foreach (DataRow existingRow in dataTable.Rows)
            {
                if (DataRowEquals(existingRow, row))
                {
                    return true;
                }
            }
            return false;
        }

        static bool DataRowEquals(DataRow row1, DataRow row2)
        {
            for (int i = 0; i < row1.ItemArray.Length; i++)
            {
                if (!row1[i].Equals(row2[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
