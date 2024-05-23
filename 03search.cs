using System.Data;
using System.Text;

namespace 総合図書管理システム新版
{
    public partial class _03search : Form
    {
        string usernumber = "";

        public _03search(string user = "")
        {
            InitializeComponent();
            usernumber = user;
        }

        public List<string[]> stArrayData = new List<string[]>();
        private DataTable? table;

        private void _03search_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add(new DataGridViewButtonColumn() { Name = "操作", Text = "開く", Width = 50, UseColumnTextForButtonValue = true });
            DGVrefresh();
        }

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
                string searchString = textBox4.Text;
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

        private void dataGridViewMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // クリックした行のインデックスを取得
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex != 0)
                {
                    return;
                }
                string? data = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                Form form10 = new _10dataview(data, usernumber);
                form10.ShowDialog();
            }
        }

        private void DGVrefresh()
        {
            try
            {
                table = new DataTable("Table");
                ReadCSV(table, true, MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\masterdata.aescsv");

                LoadDataIntoDataGridView(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadDataIntoDataGridView(DataTable dataList)
        {
            // データを読み込む
            dataGridView1.DataSource = dataList;
        }

        private static void ReadCSV(DataTable dt, bool hasHeader, string fileName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            StreamReader streamReader = new StreamReader(fileName, Encoding.GetEncoding("shift_jis"));
            string Endata = streamReader.ReadToEnd();
            streamReader.Close();
            string csvString = MainClass.AESDe(Endata);
            // 文字列を行ごとに分割
            StringReader stringReader = new StringReader(csvString);
            string? line;
            int rowCount = 0;

            while ((line = stringReader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                // ヘッダー行の処理
                if (rowCount == 0 && hasHeader)
                {
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
        }
    }
}
