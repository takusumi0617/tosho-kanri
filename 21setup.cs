using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace 総合図書管理システム新版
{
    public partial class _21setup : Form
    {
        private int countDownSeconds = 0;
        private System.Windows.Forms.Timer timer;
        int licensed = 0;
        public _21setup()
        {
            InitializeComponent();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            licensed = 0;
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                textBox2.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                textBox1.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            countDownSeconds--;
            if (countDownSeconds == 0)
            {
                timer.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (countDownSeconds == 0)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        // PHPスクリプトのURLを指定
                        string phpUrl = "https://api.tosho-kanri.com/license.php?id=" + textBox1.Text;

                        // HTTPリクエストを同期的に送信し、応答を取得
                        HttpResponseMessage response = client.GetAsync(phpUrl).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            // 応答からBoolean値を取得
                            bool result = false;
                            if (response.Content.ReadAsStringAsync().Result == "true") { result = true; }
                            else { result = false; }

                            if (result)
                            {
                                try
                                {
                                    File.WriteAllText(MainClass.docment() + "\\総合図書管理システム\\System\\id.aestxt", MainClass.AESEn(textBox1.Text));
                                }
                                catch
                                {
                                    MessageBox.Show("ライセンス認証には成功しましたが、キーの保存に失敗しました。\r\nサポートにお問い合わせください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Environment.Exit(0);
                                }
                                finally
                                {
                                    File.WriteAllText(MainClass.docment() + "\\総合図書管理システム\\System\\setup.txt", "1");
                                }
                                // 取得したBoolean値を使用
                                progressBar1.Value = 33;
                                label3.Text = "33％";
                                MessageBox.Show("ライセンス認証に成功しました。", "認証完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                panel1.Visible = false;
                                panel2.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("ライセンス認証に失敗しました。ライセンスキーが無効です。\r\nエラーコード: invalid license code", "認証失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("HTTPリクエストが失敗しました。エラーコード: http" + response.StatusCode, "認証失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("認証のためインターネットに接続した際、エラーが発生しました。\r\nエラーコード: network exception\r\n" + ex.Message, "認証エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                countDownSeconds = 60;
                timer.Start();
            }
            else
            {
                MessageBox.Show("一定時間内に連続してライセンス認証を試行することはできません。\r\nあと" + countDownSeconds.ToString() + "秒お待ちください。\r\nエラーコード: too many authentication requests", "連続試行攻撃の防止", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            if (textBox2.TextLength == 14)
            {
                if (double.TryParse(textBox2.Text, out var num))
                {
                    double max = 53900009999999 + double.Parse(dt.ToString("yy") + dt.ToString("MM")) * 10000000;
                    double min = 53900001000001 + double.Parse(dt.AddMonths(-1).ToString("yy") + dt.AddMonths(-1).ToString("MM")) * 10000000;
                    if (min <= num && num <= max)
                    {
                        File.WriteAllText(MainClass.docment() + "\\総合図書管理システム\\System\\setup.txt", "1");
                        File.WriteAllText(MainClass.docment() + "\\総合図書管理システム\\System\\id.aestxt", MainClass.AESEn("offline"));
                        progressBar1.Value = 33;
                        label3.Text = "33％";
                        MessageBox.Show("ライセンス認証に成功しました。", "認証完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        panel1.Visible = false;
                        panel2.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("オフラインキーが間違っています。", "キー検証エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("オフラインキー入力ボックスに不正な文字列が含まれています。", "キー検証エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text == textBox4.Text && textBox5.Text == textBox6.Text)
                {
                    string encryptedXmlFilePath = MainClass.docment() + "\\総合図書管理システム\\System\\config.xml";
                    if (!File.Exists(encryptedXmlFilePath))
                    {
                        string writer = "";
                        writer += "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n";
                        writer += "<管理ページ>\r\n";
                        writer += " <基本情報>\r\n";
                        writer += "  <管理パスワード>4GeQUtDhHZ7FRFV8o1/eRg==</管理パスワード>\r\n";
                        writer += "  <一般パスワード>4GeQUtDhHZ7FRFV8o1/eRg==</一般パスワード>\r\n";
                        writer += "  <anlogin>False</anlogin>\r\n";
                        writer += "  <rentalperiod>7</rentalperiod>\r\n";
                        writer += "  <notice>0</notice>\r\n";
                        writer += " </基本情報>\r\n";
                        writer += "</管理ページ>";
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(writer);
                        string xmlString = xmlDoc.OuterXml;
                        string encryptedXmlString = MainClass.AESEn(xmlString);
                        File.WriteAllText(encryptedXmlFilePath, encryptedXmlString);
                    }
                    {
                        string encryptedXmlString = File.ReadAllText(encryptedXmlFilePath);
                        string decryptedXmlString = MainClass.AESDe(encryptedXmlString);

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(decryptedXmlString);
                        XmlElement rootElement = xmlDoc.DocumentElement;
                        XmlNode row1Node = rootElement.SelectSingleNode("基本情報");
                        XmlNode row2Node1 = row1Node.SelectSingleNode("管理パスワード");
                        XmlNode row2Node2 = row1Node.SelectSingleNode("一般パスワード");
                        row2Node1.InnerText = MainClass.RSAEn(textBox3.Text);
                        row2Node2.InnerText = MainClass.RSAEn(textBox6.Text);
                        string xmlString = xmlDoc.OuterXml;
                        encryptedXmlString = MainClass.AESEn(xmlString);
                        File.WriteAllText(encryptedXmlFilePath, encryptedXmlString);
                        File.WriteAllText(MainClass.docment() + "\\総合図書管理システム\\System\\setup.txt", "2");
                        progressBar1.Value = 66;
                        label3.Text = "66％";
                        panel2.Visible = false;
                        panel3.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("パスワードと確認用パスワードが一致しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("致命的なエラーが発生しました。プログラムは直ちに終了します。\r\nエラーコード:CONFIG_FILE_ERROR", "致命的なエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void _21setup_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (licensed != 1)
            {
                Environment.Exit(0);
            }
        }

        static void InstallCertificate(string certificateFilePath, StoreName storeName)
        {
            // X509証明書ストアを開く
            X509Store store = new X509Store(storeName, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadWrite);
                // 証明書を作成し、ストアに追加
                X509Certificate2 certificate = new X509Certificate2(certificateFilePath);
                store.Add(certificate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("証明書のインストールでエラーが発生しました。\r\nMessage:" + ex.Message, "証明書インストールエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // ストアを閉じる
                store.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            X509Certificate2 certificate = new X509Certificate2("ca.crt");
            X509Certificate2UI.DisplayCertificate(certificate);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            X509Certificate2 certificate = new X509Certificate2("codeica.crt");
            X509Certificate2UI.DisplayCertificate(certificate);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                InstallCertificate("ca.crt", StoreName.Root);
                InstallCertificate("codeica.crt", StoreName.CertificateAuthority);
                File.Delete(MainClass.docment() + "\\総合図書管理システム\\System\\setup.txt");
                panel3.Visible = false;
                progressBar1.Value = 100;
                label3.Text = "100％";
                licensed = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"インストール中にエラーが発生しましたが、セットアップを続けます。\r\nエラーメッセージ:{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void _21setup_Load(object sender, EventArgs e)
        {

        }
    }
}
