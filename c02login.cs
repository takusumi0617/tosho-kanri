using System.Data;
using System.Xml;

namespace 総合図書管理システム新版
{
    internal class c02login
    {
        public static string[] certification(string userid, string password)
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
                XmlNode adminPassNode = row1Node.SelectSingleNode("管理パスワード");
                XmlNode userPassNode = row1Node.SelectSingleNode("一般パスワード");
                XmlNode row3Node = row1Node.SelectSingleNode("anlogin");
                bool anlogin = row3Node.InnerText.Equals("True");
                try
                {
                    string truepass = MainClass.RSADe(adminPassNode.InnerText);
                    string userpass = MainClass.RSADe(userPassNode.InnerText);
                    if (userid == "admin" && password == truepass)
                    {
                        string[] returndata = new string[] { "admin", "success" };
                        return returndata;
                    }
                    else
                    {
                        if ((userid == "user" && password == userpass) || (userid == "" && password == "" && anlogin))
                        {
                            string[] returndata = new string[] { "user", "success" };
                            return returndata;
                        }

                        DataRow[] name = c03process.usernamecall(userid);
                        if (name.Length == 1)
                        {
                            string[] returndata = new string[] { "visitor", "success" };
                            return returndata;
                        }
                        else
                        {
                            string[] returndata = new string[] { "none", "failure" };
                            return returndata;
                        }
                    }
                }
                catch (Exception ae)
                {
                    throw new Exception(ae.Message, ae.InnerException);
                }
            }
            catch
            {
                MessageBox.Show("致命的なエラーが発生しました。プログラムは直ちに終了します。\r\nエラーコード:CONFIG_FILE_ERROR", "致命的なエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return new string[] {"none", "failure"};
            }
        }
    }
}
