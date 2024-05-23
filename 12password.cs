using System.Xml;

namespace 総合図書管理システム新版
{
    public partial class _12password : Form
    {
        public _12password()
        {
            InitializeComponent();
        }
        string encryptedXmlFilePath = MainClass.docment() + "\\総合図書管理システム\\System\\config.xml";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == textBox2.Text)
                {
                    if (radioButton1.Checked)
                    {
                        string encryptedXmlString = File.ReadAllText(encryptedXmlFilePath);
                        string decryptedXmlString = MainClass.AESDe(encryptedXmlString);

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(decryptedXmlString);
                        XmlElement rootElement = xmlDoc.DocumentElement;
                        XmlNode row1Node = rootElement.SelectSingleNode("基本情報");
                        XmlNode row2Node = row1Node.SelectSingleNode("管理パスワード");

                        row2Node.InnerText = MainClass.RSAEn(textBox1.Text);

                        string xmlString = xmlDoc.OuterXml;

                        encryptedXmlString = MainClass.AESEn(xmlString);

                        File.WriteAllText(encryptedXmlFilePath, encryptedXmlString);

                        MessageBox.Show("新パスワードに更新されました。", "更新済み", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else if (radioButton2.Checked)
                    {
                        string encryptedXmlString = File.ReadAllText(encryptedXmlFilePath);
                        string decryptedXmlString = MainClass.AESDe(encryptedXmlString);

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(decryptedXmlString);
                        XmlElement rootElement = xmlDoc.DocumentElement;
                        XmlNode row1Node = rootElement.SelectSingleNode("基本情報");
                        XmlNode row2Node = row1Node.SelectSingleNode("一般パスワード");

                        row2Node.InnerText = MainClass.RSAEn(textBox1.Text);

                        string xmlString = xmlDoc.OuterXml;

                        encryptedXmlString = MainClass.AESEn(xmlString);

                        File.WriteAllText(encryptedXmlFilePath, encryptedXmlString);

                        MessageBox.Show("新パスワードに更新されました。", "更新済み", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("①の変更するアカウントを選択してください。", "不明な要素", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("「新パスワード」と「新パスワード(確認用)」が一致しません。", "更新不可", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("致命的なエラーが発生しました。プログラムは直ちに終了します。\r\nエラーコード:CONFIG_FILE_ERROR", "致命的なエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.PasswordChar = '\0';
            }
            else
            {
                textBox1.PasswordChar = '*';
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
    }
}
