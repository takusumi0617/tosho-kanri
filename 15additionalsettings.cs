using Microsoft.Win32;
using System.Diagnostics;
using System.Xml;

namespace 総合図書管理システム新版
{
    public partial class _15additionalsettings : Form
    {
        public _15additionalsettings()
        {
            InitializeComponent();
        }
        int end = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            try
            {
                string encryptedXmlFilePath = MainClass.docment() + "\\総合図書管理システム\\System\\config.xml";
                string encryptedXmlString = File.ReadAllText(encryptedXmlFilePath);
                string decryptedXmlString = MainClass.AESDe(encryptedXmlString);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(decryptedXmlString);
                XmlElement rootElement = xmlDoc.DocumentElement;
                XmlNode row1Node = rootElement.SelectSingleNode("基本情報");
                XmlNode row2Node = row1Node.SelectSingleNode("anlogin");
                XmlNode noticeNode = row1Node.SelectSingleNode("notice");
                row2Node.InnerText = checkBox1.Checked.ToString();
                if (checkBox2.Checked)
                {
                    noticeNode.InnerText = "1";
                    File.WriteAllText($"{MainClass.docment()}\\総合図書管理システム\\System\\data\\notice.txt", textBox3.Text);
                }
                else
                {
                    noticeNode.InnerText = "0";
                }
                string xmlString = xmlDoc.OuterXml;
                encryptedXmlString = MainClass.AESEn(xmlString);
                File.WriteAllText(encryptedXmlFilePath, encryptedXmlString);

                c03process c03 = new c03process();
                _ = int.TryParse(textBox1.Text, out var result) ? c03.Rentalperiod(true, result) : c03.Rentalperiod(true);
                if (textBox2.Text != MainClass.docment())
                {
                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\TOKI\\tosho"))
                    {
                        key.SetValue("dpath", textBox2.Text);
                    }
                    Directory.CreateDirectory(textBox2.Text);
                    Directory.CreateDirectory(textBox2.Text + "\\総合図書管理システム");
                    Directory.CreateDirectory(textBox2.Text + "\\総合図書管理システム\\System");
                    File.WriteAllText(textBox2.Text + "\\総合図書管理システム\\System\\id.aestxt"
                        , File.ReadAllText(MainClass.docment() + "\\総合図書管理システム\\System\\id.aestxt"));
                }

                MessageBox.Show("設定を保存しました。", "保存完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("致命的なエラーが発生しました。プログラムは直ちに終了します。\r\nエラーコード:CONFIG_FILE_ERROR", "致命的なエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("すべてのデータが削除され、実行した場合、復元することはできません。\r\n本当に実行しますか？", "データに関する重大な警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    string userInput = Microsoft.VisualBasic.Interaction.InputBox("管理者パスワードを入力してください:", "本人確認", "");
                    if (!string.IsNullOrEmpty(userInput))
                    {
                        string path = MainClass.docment() + "\\総合図書管理システム\\System\\config.xml";
                        string encryptedXmlString = File.ReadAllText(path);
                        string decryptedXmlString = MainClass.AESDe(encryptedXmlString);
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(decryptedXmlString);
                        XmlElement rootElement = xmlDoc.DocumentElement;
                        XmlNode row1Node = rootElement.SelectSingleNode("基本情報");
                        XmlNode row2Node = row1Node.SelectSingleNode("管理パスワード");
                        try
                        {
                            string truepass = MainClass.RSADe(row2Node.InnerText);
                            if (truepass == userInput)
                            {
                                try
                                {
                                    string homepath = MainClass.docment() + "\\総合図書管理システム";
                                    DirectoryInfo directoryInfo = new DirectoryInfo(homepath);
                                    RemoveReadonlyAttribute(directoryInfo);
                                    directoryInfo.Delete(true);
                                    MessageBox.Show("データが正常に削除されました。\r\nアプリケーションを再起動します。", "リセット完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    string appPath = Process.GetCurrentProcess().MainModule.FileName;
                                    Process.Start(appPath);
                                    Environment.Exit(0);
                                }
                                catch (UnauthorizedAccessException ex)
                                {
                                    MessageBox.Show("データの削除に必要なファイルのアクセス許可がありません: " + ex.Message, "リセットエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                catch (IOException ex)
                                {
                                    MessageBox.Show("データの削除中にエラーが発生しました: " + ex.Message, "リセットエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("パスワードが違います。", "認証エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ae)
                        {
                            throw new Exception(ae.Message, ae.InnerException);
                        }
                    }
                    else
                    {
                        MessageBox.Show("パスワードが入力されませんでした。", "認証エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("致命的なエラーが発生しました。プログラムは直ちに終了します。\r\nエラーコード:CONFIG_FILE_ERROR", "致命的なエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        public static void RemoveReadonlyAttribute(DirectoryInfo dirInfo)
        {
            //基のフォルダの属性を変更
            if ((dirInfo.Attributes & FileAttributes.ReadOnly) ==
                FileAttributes.ReadOnly)
                dirInfo.Attributes = FileAttributes.Normal;
            //フォルダ内のすべてのファイルの属性を変更
            foreach (FileInfo fi in dirInfo.GetFiles())
                if ((fi.Attributes & FileAttributes.ReadOnly) ==
                    FileAttributes.ReadOnly)
                    fi.Attributes = FileAttributes.Normal;
            //サブフォルダの属性を回帰的に変更
            foreach (DirectoryInfo di in dirInfo.GetDirectories())
                RemoveReadonlyAttribute(di);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("すべての設定がリセットされ、実行した場合、復元することはできません。\r\n本当に実行しますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    string userInput = Microsoft.VisualBasic.Interaction.InputBox("管理者パスワードを入力してください:", "本人確認", "");
                    if (!string.IsNullOrEmpty(userInput))
                    {
                        string path = MainClass.docment() + "\\総合図書管理システム\\System\\config.xml";
                        string encryptedXmlString = File.ReadAllText(path);
                        string decryptedXmlString = MainClass.AESDe(encryptedXmlString);
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(decryptedXmlString);
                        XmlElement rootElement = xmlDoc.DocumentElement;
                        XmlNode row1Node = rootElement.SelectSingleNode("基本情報");
                        XmlNode row2Node = row1Node.SelectSingleNode("管理パスワード");
                        try
                        {
                            string truepass = MainClass.RSADe(row2Node.InnerText);
                            if (truepass == userInput)
                            {
                                try
                                {
                                    string homepath = MainClass.docment() + "\\総合図書管理システム";
                                    DirectoryInfo directoryInfo = new DirectoryInfo(homepath + "\\System");
                                    RemoveReadonlyAttribute(directoryInfo);
                                    directoryInfo.Delete(true);
                                    MessageBox.Show("設定が正常に削除されました。\r\nアプリケーションを再起動します。", "リセット完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    string appPath = Process.GetCurrentProcess().MainModule.FileName;
                                    Process.Start(appPath);
                                    Environment.Exit(0);
                                }
                                catch (IOException ex)
                                {
                                    MessageBox.Show("設定の削除中にエラーが発生しました: " + ex.Message, "リセットエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                catch (UnauthorizedAccessException ex)
                                {
                                    MessageBox.Show("設定の削除に必要なファイルのアクセス許可がありません: " + ex.Message, "リセットエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("パスワードが違います。", "認証エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ae)
                        {
                            throw new Exception(ae.Message, ae.InnerException);
                        }
                    }
                    else
                    {
                        MessageBox.Show("パスワードが入力されませんでした。", "認証エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("致命的なエラーが発生しました。プログラムは直ちに終了します。\r\nエラーコード:CONFIG_FILE_ERROR", "致命的なエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private void _15additionalsettings_Load(object sender, EventArgs e)
        {
            try
            {
                string path = MainClass.docment() + "\\総合図書管理システム\\System\\config.xml";
                string encryptedXmlString = File.ReadAllText(path);
                string decryptedXmlString = MainClass.AESDe(encryptedXmlString);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(decryptedXmlString);
                XmlElement rootElement = xmlDoc.DocumentElement;
                XmlNode row1Node = rootElement.SelectSingleNode("基本情報");
                XmlNode row2Node = row1Node.SelectSingleNode("anlogin");
                XmlNode noticeNode = row1Node.SelectSingleNode("notice");
                checkBox1.Checked = row2Node.InnerText.Equals("True");
                c03process c03 = new c03process();
                textBox1.Text = c03.Rentalperiod(false).ToString();
                textBox2.Text = MainClass.docment();
                string notice_path = $"{MainClass.docment()}\\総合図書管理システム\\System\\data\\notice.txt";
                if (noticeNode.InnerText == "1" && File.Exists(notice_path))
                {
                    textBox3.Enabled = true;
                    checkBox2.Checked = true;
                    textBox3.Text = File.ReadAllText(notice_path);
                }
                end = 0;
            }
            catch
            {
                MessageBox.Show("致命的なエラーが発生しました。プログラムは直ちに終了します。\r\nエラーコード:CONFIG_FILE_ERROR", "致命的なエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private void _15additionalsettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (end != 1)
            {
                end = 1;
                DialogResult d = MessageBox.Show("設定を保存して終了しますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {
                    Save();
                }
                Close();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox3.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
            }
        }
    }
}
