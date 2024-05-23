using System.Data;
using System.Text;

namespace 総合図書管理システム新版
{
    public partial class _13user : Form
    {
        int mode;
        public _13user(int number)
        {
            InitializeComponent();
            mode = number;
        }
        static int changing = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
            Form form14 = new _14useradd();
            form14.ShowDialog();
            Form13_Activated(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            save();
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            DataTable dt = createData();
            dataGridViewDisp(dt);
            if (mode != 1)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                label5.Text = "編集不可";
            }
            else
            {
                label5.Text = "編集可";
            }
        }

        private DataTable createData()
        {
            DataTable dt = createDataTable();
            return dt;
        }

        private DataTable createDataTable()
        {
            string fileName = MainClass.docment() + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            DataTable dt = new DataTable();
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

        private void dataGridViewDisp(DataTable dt)
        {
            //データ行の高さを設定する
            dataGridView1.RowTemplate.Height = 30;
            //dataGridViewにデータをセットする
            dataGridView1.DataSource = dt;
            //行ヘッダーを表示する
            dataGridView1.RowHeadersVisible = true;
            //列ヘッダーを表示する
            dataGridView1.ColumnHeadersVisible = true;
            //列ヘッダーの高さを設定する
            dataGridView1.ColumnHeadersHeight = 30;
            //配置を上下左右の中央に設定する
            dataGridView1.Columns["利用者番号"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["学年"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["クラス"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["番号"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["氏名"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["利用者権限レベル"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["利用者権限"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ユーザが行追加を不可に設定する
            dataGridView1.AllowUserToAddRows = false;
            //ユーザが行削除を不可に設定する
            dataGridView1.AllowUserToDeleteRows = false;
            //複数セルの選択を不可に設定する
            dataGridView1.MultiSelect = false;
            //行単位に選択モードを設定する
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //ユーザが列の幅を変更可に設定する
            dataGridView1.AllowUserToResizeColumns = true;
            //ユーザが行の高さを変更不可に設定する
            dataGridView1.AllowUserToResizeRows = false;
            if (mode == 1)
            {
                dataGridView1.ReadOnly = false;
            }
            else
            {
                dataGridView1.ReadOnly = true;
            }
            //右寄せに設定する
            dataGridView1.Columns["利用者番号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["学年"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["クラス"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["番号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["氏名"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["利用者権限レベル"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["利用者権限"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //選択を解除する
            dataGridView1.ClearSelection();
            //幅を自動調整
            dataGridView1.Columns["利用者番号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["学年"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["クラス"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["番号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["氏名"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["利用者権限レベル"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["利用者権限"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private string quoteCommaCheck(string sCell)
        {
            const string QUOTE = @"""";
            const string COMMA = @",";

            string[] a = new string[] { QUOTE, COMMA };

            if (a.Any(sCell.Contains))
            {
                sCell = sCell.Replace(QUOTE, QUOTE + QUOTE);

                sCell = QUOTE + sCell + QUOTE;
            }
            return sCell;
        }

        private void save()
        {
            changing = 1;
            string FILE_PATH = MainClass.docment() + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv";
            string lines = "";

            if (dataGridView1.RowCount <= 0)
            {
                MessageBox.Show("出力データがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                changing = 0;
                return;
            }

            DialogResult result = MessageBox.Show("変更した内容を保存します。" + "\n" + "よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                changing = 0;
                return;
            }

            // ヘッダー行を追加
            StringBuilder header = new StringBuilder();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                header.Append(column.HeaderText).Append(",");
            }
            lines += header.ToString().Substring(0, header.Length - 1) + "\r\n";

            // データ行を追加（暗号化）
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                StringBuilder line = new StringBuilder();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    // セルの文字列をAESで暗号化して追加
                    line.Append(cell.Value.ToString()).Append(",");
                }
                lines += line.ToString().Substring(0, line.Length - 1) + "\r\n";
            }

            string stream = MainClass.AESEn(lines.TrimEnd('\n', '\r')).TrimEnd('\n', '\r');

            // ファイルに書き込み
            File.WriteAllText(FILE_PATH, stream);
            MessageBox.Show("保存が完了しました。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            changing = 0;
        }

        private void Form13_Activated(object sender, EventArgs e)
        {
            if (changing == 0)
            {
                dataGridView1.Columns.Clear();
                Form13_Load(sender, e);
            }
        }

        private void Form13_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mode == 1)
            {
                save();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            save();
            Form form23 = new _23icid();
            form23.ShowDialog();
            Form13_Activated(sender, e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                if (mode == 1) { button5.Enabled = true; }
                textBox1.Text = selectedRow.Cells[0].Value?.ToString();
                textBox2.Text = selectedRow.Cells[1].Value?.ToString();
                textBox3.Text = selectedRow.Cells[2].Value?.ToString();
                textBox4.Text = selectedRow.Cells[3].Value?.ToString();
                textBox5.Text = selectedRow.Cells[4].Value?.ToString();
                comboBox1.Text = selectedRow.Cells[5].Value?.ToString() + "." + selectedRow.Cells[6].Value?.ToString();
                textBox6.Text = selectedRow.Cells[7].Value?.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0] : null;
            if (selectedRow != null && mode == 1)
            {
                selectedRow.Cells[0].Value = textBox1.Text;
                selectedRow.Cells[1].Value = textBox2.Text;
                selectedRow.Cells[2].Value = textBox3.Text;
                selectedRow.Cells[3].Value = textBox4.Text;
                selectedRow.Cells[4].Value = textBox5.Text;
                string[] usertype = comboBox1.Text.Split('.');
                selectedRow.Cells[5].Value = usertype[0];
                selectedRow.Cells[6].Value = usertype[1];
                selectedRow.Cells[7].Value = textBox6.Text;
                MessageBox.Show("反映しました。", "反映完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("行が選択されていません。", "無効な操作", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
