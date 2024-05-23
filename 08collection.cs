using System.Data;
using System.Text;

namespace 総合図書管理システム新版
{
    public partial class _08collection : Form
    {
        int mode;
        public _08collection(int number)
        {
            InitializeComponent();
            mode = number;
        }
        static int changing = 0;

        private void _08collection_Load(object sender, EventArgs e)
        {
            DataTable dt = createDataTable();
            dataGridViewDisp(dt);
            if (mode != 1)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                label5.Text = "編集不可";
            }
            else
            {
                label5.Text = "編集可";
            }
        }

        private DataTable createDataTable()
        {
            string fileName = MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\masterdata.aescsv";
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


        public static int CountLines(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0; // 空の文字列の場合は行数は0とします
            }
            int lineCount = 1; // 初めの行は必ず存在するため、初期値は1
            foreach (char c in input)
            {
                if (c == '\n')
                {
                    lineCount++;
                }
            }
            return lineCount;
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
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //選択を解除する
            dataGridView1.ClearSelection();
            //幅を自動調節
            dataGridView1.Columns["登録番号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["資料区分記号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["資料区分"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["貸出区分記号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["貸出区分"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["請求番号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["受入日"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["ISBN13"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["書名"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["書名カタカナ"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["著者名"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["著者名カタカナ"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["内容"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["件名"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["出版年"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["出版社"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["ページ数"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["大きさ"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["金額"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["受入先"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["費目記号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["費目"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["所在記号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["所在"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["備考"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void save()
        {
            changing = 1;

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            button3_Click(sender, e);
            Form form9 = new _09collectionadd();
            form9.ShowDialog();
            _08collection_Activated(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            save();
        }

        private void _08collection_Activated(object sender, EventArgs e)
        {
            if (changing == 0)
            {
                dataGridView1.Columns.Clear();
                _08collection_Load(sender, e);
            }
        }

        private void _08collection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mode == 1)
            {
                save();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                if (mode == 1) { button4.Enabled = true; }
                textBox1.Text = selectedRow.Cells[0].Value?.ToString();
                comboBox1.Text = selectedRow.Cells[1].Value?.ToString() + "." + selectedRow.Cells[2].Value.ToString();
                comboBox2.Text = selectedRow.Cells[3].Value?.ToString() + "." + selectedRow.Cells[4].Value.ToString();
                textBox2.Text = selectedRow.Cells[5].Value?.ToString();
                textBox3.Text = selectedRow.Cells[6].Value?.ToString();
                textBox4.Text = selectedRow.Cells[7].Value?.ToString();
                textBox5.Text = selectedRow.Cells[8].Value?.ToString();
                textBox6.Text = selectedRow.Cells[9].Value?.ToString();
                textBox8.Text = selectedRow.Cells[10].Value?.ToString();
                textBox9.Text = selectedRow.Cells[11].Value?.ToString();
                textBox10.Text = selectedRow.Cells[12].Value?.ToString();
                textBox11.Text = selectedRow.Cells[13].Value?.ToString();
                textBox12.Text = selectedRow.Cells[14].Value?.ToString();
                textBox13.Text = selectedRow.Cells[15].Value?.ToString();
                textBox14.Text = selectedRow.Cells[16].Value?.ToString();
                string[] size = selectedRow.Cells[17].Value?.ToString().Replace("cm", "").Split("*");
                textBox15.Text = size[0];
                textBox16.Text = size[1];
                textBox17.Text = selectedRow.Cells[18].Value?.ToString();
                textBox19.Text = selectedRow.Cells[19].Value?.ToString();
                comboBox4.Text = selectedRow.Cells[20].Value?.ToString() + "." + selectedRow.Cells[21].Value?.ToString();
                comboBox5.Text = selectedRow.Cells[22].Value?.ToString() + "." + selectedRow.Cells[23].Value?.ToString();
                textBox18.Text = selectedRow.Cells[24].Value?.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0] : null;
            if (selectedRow != null && mode == 1)
            {
                selectedRow.Cells[0].Value = textBox1.Text;
                string[] objecttype = comboBox1.Text.Split(".");
                selectedRow.Cells[1].Value = objecttype[0];
                selectedRow.Cells[2].Value = objecttype[1];
                string[] rendingtype = comboBox2.Text.Split(".");
                selectedRow.Cells[3].Value = rendingtype[0];
                selectedRow.Cells[4].Value = rendingtype[1];
                selectedRow.Cells[5].Value = textBox2.Text;
                selectedRow.Cells[6].Value = textBox3.Text;
                selectedRow.Cells[7].Value = textBox4.Text;
                selectedRow.Cells[8].Value = textBox5.Text;
                selectedRow.Cells[9].Value = textBox6.Text;
                selectedRow.Cells[10].Value = textBox8.Text;
                selectedRow.Cells[11].Value = textBox9.Text;
                selectedRow.Cells[12].Value = textBox10.Text;
                selectedRow.Cells[13].Value = textBox11.Text;
                selectedRow.Cells[14].Value = textBox12.Text;
                selectedRow.Cells[15].Value = textBox13.Text;
                selectedRow.Cells[16].Value = textBox14.Text;
                selectedRow.Cells[17].Value = textBox15.Text + "cm*" + textBox16.Text + "cm";
                selectedRow.Cells[18].Value = textBox17.Text;
                selectedRow.Cells[19].Value = textBox19.Text;
                string[] pay = comboBox4.Text.Split('.');
                selectedRow.Cells[20].Value = pay[0];
                selectedRow.Cells[21].Value = pay[1];
                string[] place = comboBox5.Text.Split(".");
                selectedRow.Cells[22].Value = place[0];
                selectedRow.Cells[23].Value = place[1];
                selectedRow.Cells[24].Value = textBox18.Text;
                MessageBox.Show("反映しました。", "反映完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("行が選択されていません。", "無効な操作", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
