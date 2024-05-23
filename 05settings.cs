using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace 総合図書管理システム新版
{
    public partial class _05settings : Form
    {
        public _05settings()
        {
            InitializeComponent();
        }
        string latesturl = "";

        private void _05settings_Load(object sender, EventArgs e)
        {
            string packet = MainClass.docment() + "\\総合図書管理システム\\System\\data\\data.json";
            try
            {
                StreamReader sr = new StreamReader(packet, Encoding.UTF8);
                string str = sr.ReadToEnd();
                sr.Close();
                string json = str;
                if (!string.IsNullOrEmpty(json))
                {
                    string latest = ExtractJsonData(json, "latest");
                    string apiUrl = ExtractJsonData(json, "url");
                    string emergency = ExtractJsonData(json, "emergency");
                    Version versionC = Assembly.GetEntryAssembly().GetName().Version;
                    Version versionI = new Version(latest);
                    int comparisonResult = versionC.CompareTo(versionI);
                    textBox1.Text = "";
                    if (comparisonResult < 0)
                    {
                        textBox1.AppendText($"更新が必要です。実行中: {versionC}  最新版: {versionI}\r\n\r\n");
                        textBox1.AppendText(emergency);
                        button17.Enabled = true;
                        latesturl = apiUrl;
                    }
                    else if (comparisonResult > 0)
                    {
                        textBox1.AppendText($"現在リリース版より新しいバージョンが実行されています。ver{versionC}\r\n\r\n");
                        textBox1.AppendText(emergency);
                        button17.Enabled = false;
                    }
                    else
                    {
                        textBox1.AppendText($"最新版が実行されています。ver{versionC}\r\n\r\n");
                        textBox1.AppendText(emergency);
                        button17.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = "サーバーとの通信中にエラーが発生しました。\r\nインターネット接続をお確かめください。\r\n";
                textBox1.AppendText(ex.Message);
            }
        }

        static string ExtractJsonData(string json, string key)
        {
            using (JsonDocument document = JsonDocument.Parse(json))
            {
                if (document.RootElement.TryGetProperty(key, out JsonElement value))
                {
                    return value.GetString();
                }
                else
                {
                    throw new ArgumentException("");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form8 = new _08collection(1);
            form8.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form8 = new _08collection(0);
            form8.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form11 = new _11referenceonly();
            form11.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form form12 = new _12password();
            form12.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form form13 = new _13user(1);
            form13.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form form13 = new _13user(0);
            form13.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form form15 = new _15additionalsettings();
            form15.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form form16 = new _16reminder();
            form16.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form form17 = new _17barcodeprint();
            form17.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form form18 = new _18userbarcode();
            form18.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("バックアップ先には別PCなどの外部機器が推奨されます。\r\nまた、個人情報の管理、記憶装置の紛失には十分ご注意ください。\r\n情報漏洩等に関して当ソフトウェア開発会社は一切責任を負いません。\r\n続行しますか？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    DateTime dt = DateTime.Now;
                    sfd.FileName = dt.ToString("yyyy-MM-dd-HH-mm-ss") + ".bkup";
                    sfd.InitialDirectory = @"C:\";
                    sfd.Filter = "バックアップファイル(*.bkup)|*.bkup";
                    sfd.FilterIndex = 1;
                    sfd.Title = "バックアップの保存先を選択してください";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string sourceFile = MainClass.docment() + "\\総合図書管理システム";
                        string zipFilePath = sfd.FileName;
                        ZipFile.CreateFromDirectory(sourceFile, zipFilePath);
                    }
                    MessageBox.Show("バックアップが完了しました。ファイルの取り扱いに注意してください。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("エラーが発生しました。\r\n" + ex.Message + "\r\nバックアップは完了していません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("バックアップ時の状態に復元され、現在のデータは削除されます。\r\n続行しますか？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.FileName = "";
                    ofd.InitialDirectory = @"C:\";
                    ofd.Filter = "バックアップファイル(*.bkup)|*.bkup|すべてのファイル(*.*)|*.*";
                    ofd.FilterIndex = 1;
                    ofd.Title = "バックアップファイルを選択してください";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string sourceFile = MainClass.docment() + "\\総合図書管理システム";
                        // ファイルを上書きコピー
                        ZipFile.ExtractToDirectory(ofd.FileName, sourceFile, true);
                        MessageBox.Show("復元が完了しました。再起動します。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string appPath = Process.GetCurrentProcess().MainModule.FileName;
                        Process.Start(appPath);
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。\r\n" + ex.Message + "\r\n復元は完了していません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form form19 = new _19bookremove();
            form19.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form form20 = new _20userremove();
            form20.ShowDialog();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Process.Start(latesturl);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Form form22 = new _22statistics();
            form22.ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Form form24 = new _24inspection();
            form24.ShowDialog();
        }
    }
}
