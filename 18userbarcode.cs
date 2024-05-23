﻿using System.Data;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Text;
using ZXing;
using ZXing.Common;

namespace 総合図書管理システム新版
{
    public partial class _18userbarcode : Form
    {
        private PrintDocument printDocument;
        private int rowIndex = 0;
        public _18userbarcode()
        {
            InitializeComponent();
            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }
        private const int PaperWidth = 827; // A4用紙の幅（ピクセル）
        private const int PaperHeight = 1169; // A4用紙の高さ（ピクセル）
        private const int BarcodeWidth = 150; // バーコードの幅（ピクセル）
        private const int BarcodeHeight = 50; // バーコードの高さ（ピクセル）
        private const int HorizontalGap = 20; // 横方向のバーコードの間隔（ピクセル）
        private const int VerticalGap = 30; // 縦方向のバーコードの間隔（ピクセル）
        private const int ColumnsPerPage = 4; // 1ページあたりの列数
        private const int RowsPerPage = 10; // 1ページあたりの行数
        private const int PageMargin = 50; // ページの余白（ピクセル）
        private int currentPage = 0;
        private int currentRow = 0;
        private int currentColumn = 0;
        public static List<string[]> stArrayData = new List<string[]>();
        private DataTable table;
        static string docment = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

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

        private void DGVrefresh()
        {
            try
            {
                table = new DataTable("Table");
                ReadCSV(table, true, docment + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv");

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

        private void dataGridViewMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)  // チェックボックス列かつ行が有効な場合
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[0];
                if (checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                {
                    rowIndex = e.RowIndex;  // 選択された行のインデックスを保存する
                }
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            var graphics = e.Graphics;
            var font = new Font("Arial", 9);

            int pageWidth = e.PageBounds.Width;
            int pageHeight = e.PageBounds.Height;

            int barcodeWidthWithGap = BarcodeWidth + HorizontalGap;
            int barcodeHeightWithGap = BarcodeHeight + VerticalGap;

            int availableWidth = pageWidth - 2 * PageMargin;
            int availableHeight = pageHeight - 2 * PageMargin;

            int maxColumns = availableWidth / barcodeWidthWithGap;
            int maxRows = availableHeight / barcodeHeightWithGap;
            int maxBarcodesPerPage = maxColumns * maxRows;

            int totalPages = (int)Math.Ceiling((double)dataGridView1.Rows.Count / maxBarcodesPerPage);

            int startIndex = currentPage * maxBarcodesPerPage;
            int endIndex = Math.Min(startIndex + maxBarcodesPerPage, dataGridView1.Rows.Count);

            int currentBarcodeIndex = startIndex;
            int currentColumnIndex = 0;
            int currentRowIndex = 0;

            for (int i = startIndex; i < endIndex; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("True"))
                    {
                        string barcode = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        string age = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        string clas = dataGridView1.Rows[i].Cells[3].Value.ToString();
                        string number = dataGridView1.Rows[i].Cells[4].Value.ToString();
                        string name = dataGridView1.Rows[i].Cells[5].Value.ToString();
                        string username10;
                        if (name.Length >= 10) { username10 = name.Substring(0, 10); }
                        else { username10 = name; }
                        int x = PageMargin + (currentColumnIndex * barcodeWidthWithGap);
                        int y = PageMargin + (currentRowIndex * barcodeHeightWithGap);

                        BarcodeWriter writer = new BarcodeWriter
                        {
                            Format = BarcodeFormat.CODE_128,
                            Options = new EncodingOptions
                            {
                                Width = BarcodeWidth,
                                Height = BarcodeHeight
                            }
                        };

                        Bitmap barcodeBitmap = writer.Write(barcode);
                        // フォントと文字色の設定
                        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        graphics.DrawString(age + "年" + clas + "組" + number + "番" + username10, font, Brushes.Black, new RectangleF(x - 7, y + 22, barcodeWidthWithGap, barcodeHeightWithGap), stringFormat);
                        graphics.DrawImage(barcodeBitmap, x, y);

                        currentColumnIndex++;
                        if (currentColumnIndex >= maxColumns)
                        {
                            currentColumnIndex = 0;
                            currentRowIndex++;
                        }
                    }

                    currentBarcodeIndex++;
                    if (currentBarcodeIndex >= endIndex || currentBarcodeIndex % maxBarcodesPerPage == 0)
                    {
                        currentPage++;
                        if (currentPage < totalPages)
                        {
                            e.HasMorePages = true;
                            return;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var printDialog = new PrintDialog();
                printDialog.Document = printDocument;
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    currentPage = 0;
                    currentRow = 0;
                    currentColumn = 0;

                    // 印刷を開始
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。\r\n" + ex.Message, "印刷エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form18_Load(object sender, EventArgs e)
        {
            DGVrefresh();
            dataGridView1.Columns["選択"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["利用者番号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["学年"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["クラス"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["番号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["利用者番号"].ReadOnly = true;
            dataGridView1.Columns["学年"].ReadOnly = true;
            dataGridView1.Columns["クラス"].ReadOnly = true;
            dataGridView1.Columns["番号"].ReadOnly = true;
            dataGridView1.Columns["氏名"].ReadOnly = true;
            dataGridView1.Columns["利用者権限レベル"].ReadOnly = true;
            dataGridView1.Columns["利用者権限"].ReadOnly = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells[0] as DataGridViewCheckBoxCell;
                checkBoxCell.Value = true;
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
