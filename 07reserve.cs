using System.Data;

namespace 総合図書管理システム新版
{
    public partial class _07reserve : Form
    {
        public _07reserve()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;

            DataRow[] rowu = c03process.usernamecall(textBox1.Text.Replace(" ", ""));
            DataRow[] rows = c03process.booknamecall(textBox2.Text.Replace(" ", ""));
            if (rows.Length == 1 && rowu.Length == 1)
            {
                Directory.CreateDirectory(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\予約者一覧\\" + textBox1.Text.Replace(" ", ""));
                StreamWriter writer = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\予約者一覧\\" + textBox1.Text.Replace(" ", "") + "\\" + textBox2.Text.Replace(" ", "") + ".txt", false);
                writer.Write(d.ToString("yyyy/MM/dd HH:mm:ss"));
                writer.Close();
                Directory.CreateDirectory(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\予約本一覧\\" + textBox2.Text.Replace(" ", ""));
                StreamWriter writer2 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\予約本一覧\\" + textBox2.Text.Replace(" ", "") + "\\" + textBox1.Text.Replace(" ", "") + ".txt", false);
                writer2.Write(d.ToString("yyyy/MM/dd HH:mm:ss"));
                writer2.Close();
                MessageBox.Show("予約を完了しました。", "予約完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("入力された利用者情報と蔵書のデータが見つかりませんでした。", "データ不一致", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "照会")
            {
                string directoryPath = MainClass.docment() + "\\総合図書管理システム\\利用者データ\\予約者一覧\\" + textBox3.Text.Replace(" ", "");
                if (Directory.Exists(directoryPath))
                {
                    string[] txtFiles = Directory.GetFiles(directoryPath, "*.txt");
                    if (txtFiles.Length > 0)
                    {
                        foreach (string txtFile in txtFiles)
                        {
                            listBox1.Items.Add(Path.GetFileNameWithoutExtension(txtFile));
                        }
                        button2.Text = "操作中止";
                        textBox3.Enabled = false;
                        button3.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("この利用者の予約情報はありませんでした。", "予約なし", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("入力された利用者情報が見つかりませんでした。", "データ不一致", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("キャンセル操作を中止します。", "操作中止", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button2.Text = "照会";
                listBox1.Items.Clear();
                textBox3.Enabled = true;
                button3.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button2.Text == "操作中止")
            {
                string directoryPath = MainClass.docment() + "\\総合図書管理システム\\利用者データ\\予約者一覧\\" + textBox3.Text.Replace(" ", "");
                // 選択されたアイテムを取得
                string selectedFileName = listBox1.SelectedItem?.ToString();

                if (selectedFileName != null)
                {
                    // 完全なファイルパスを構築
                    string filePath = Path.Combine(directoryPath, selectedFileName + ".txt");
                    // ファイルが存在するか確認
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            File.Delete(filePath);
                            MessageBox.Show("予約情報が削除されました。", "削除成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            button2.Text = "照会";
                            textBox3.Enabled = true;
                            textBox3.Text = "";
                            button3.Enabled = false;
                            listBox1.Items.Clear();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"予約情報の削除中にエラーが発生しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("指定された予約情報は存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("削除する予約情報を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
