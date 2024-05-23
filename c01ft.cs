using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using System.Reflection;
using System.Text;
using System.Xml;

namespace 総合図書管理システム新版
{
    internal class c01ft
    {
        public static void dafcreate()
        {
            string docment = MainClass.docment();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                Directory.CreateDirectory(docment + "\\総合図書管理システム");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\System");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\xml");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\xml\\未加工");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\System\\data");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\System\\log");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\蔵書データ");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\蔵書データ\\蔵書点検データ");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\蔵書データ\\貸出中蔵書");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\蔵書データ\\統計データ");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\蔵書データ\\予約本一覧");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\利用者データ");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\利用者データ\\貸出者一覧");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\利用者データ\\予約者一覧");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\利用者データ\\統計データ");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\利用者データ\\延滞履歴");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\伝言データ");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\伝言データ\\AA");
                Directory.CreateDirectory(docment + "\\総合図書管理システム\\伝言データ\\AU");
                if (!File.Exists(docment + "\\総合図書管理システム\\System\\id.aestxt"))
                {
                    Form form = new _21setup();
                    form.ShowDialog();
                }
                if (!File.Exists(docment + "\\総合図書管理システム\\System\\config.xml"))
                {
                    string path = docment + "\\総合図書管理システム\\System\\config.xml";
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
                    File.WriteAllText(path, encryptedXmlString);
                }
                if (!File.Exists(docment + "\\総合図書管理システム\\蔵書データ\\masterdata.aescsv")) { MainClass.csv_creater(docment + "\\総合図書管理システム\\蔵書データ\\masterdata.aescsv", "登録番号,資料区分記号,資料区分,貸出区分記号,貸出区分,請求番号,受入日,ISBN13,書名,書名カタカナ,著者名,著者名カタカナ,内容,件名,出版年,出版社,ページ数,大きさ,金額,受入先,費目記号,費目,所在記号,所在,備考", true); }
                if (!File.Exists(docment + "\\総合図書管理システム\\蔵書データ\\historydata.aescsv")) { MainClass.csv_creater(docment + "\\総合図書管理システム\\蔵書データ\\historydata.aescsv", "貸出日時,返却日時,貸出者ID,蔵書ID,延滞有無,資料区分,分類番号", true); }
                if (!File.Exists(docment + "\\総合図書管理システム\\蔵書データ\\deleteddata.aescsv")) { MainClass.csv_creater(docment + "\\総合図書管理システム\\蔵書データ\\deleteddata.aescsv", "除籍理由,登録番号,資料区分記号,資料区分,貸出区分記号,貸出区分,請求番号,受入日,ISBN13,書名,書名カタカナ,著者名,著者名カタカナ,内容,件名,出版年,出版社,ページ数,大きさ,金額,受入先,費目記号,費目,所在記号,所在,備考", true); }
                if (!File.Exists(docment + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv")) { MainClass.csv_creater(docment + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv", "利用者番号,学年,クラス,番号,氏名,利用者権限レベル,利用者権限,icid", true); }
                if (!File.Exists(docment + "\\総合図書管理システム\\System\\data\\limit.aescsv")) { MainClass.csv_creater(docment + "\\総合図書管理システム\\System\\data\\limit.aescsv", "本番号,図書名,期限,年,組,番号,氏名", true); }
                if (!File.Exists(docment + "\\総合図書管理システム\\System\\data\\historydata.aescsv")) { MainClass.csv_creater(docment + "\\総合図書管理システム\\System\\data\\historydata.aescsv", "返却日時,貸出者ID,蔵書ID,延滞有無,分類番号", true); }
                if (!File.Exists(docment + "\\総合図書管理システム\\伝言データ\\AA\\message.aestxt"))
                {
                    StreamWriter _writer = new StreamWriter(docment + "\\総合図書管理システム\\伝言データ\\AA\\message.aestxt", false, Encoding.GetEncoding("Shift_jis"));
                    _writer.Write("");
                    _writer.Close();
                }
                if (!File.Exists(docment + "\\総合図書管理システム\\伝言データ\\AU\\message.aestxt"))
                {
                    StreamWriter _writer = new StreamWriter(docment + "\\総合図書管理システム\\伝言データ\\AU\\message.aestxt", false, Encoding.GetEncoding("Shift_jis"));
                    _writer.Write("");
                    _writer.Close();
                }
                if (!File.Exists(docment + "\\総合図書管理システム\\警告このフォルダを操作しないでください。.txt"))
                {
                    StreamWriter _writer = new StreamWriter(docment + "\\総合図書管理システム\\警告このフォルダを操作しないでください。.txt", false, Encoding.GetEncoding("Shift_jis"));
                    _writer.WriteLine("このフォルダは総合図書管理システムの重要なデータが保存されています。");
                    _writer.WriteLine("万が一手動で操作した場合、データが破損したり、プログラムが正常に動作しなくなる可能性があります。");
                    _writer.WriteLine("プログラムに異常が発生しても\"絶対に\"これらのファイルをテキストエディターなどで操作せず、サポートにお問い合わせください。");
                    _writer.Write("サポート窓口:https://forms.gle/9ieTcsMWJW4dwVcF6");
                    _writer.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static int notice_display()
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
                XmlNode noticeNode = row1Node.SelectSingleNode("notice");
                int notice;
                if (int.TryParse(noticeNode.InnerText, out notice))
                {
                    return notice;
                }
                else
                {
                    MessageBox.Show("「お知らせ」表示欄の設定取得に失敗しました。\r\nアプリバージョン情報を表示します。", "取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return 0;
                }
            }
            catch
            {
                MessageBox.Show("致命的なエラーが発生しました。プログラムは直ちに終了します。\r\nエラーコード:CONFIG_FILE_ERROR", "致命的なエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return 0;
            }
        }

        public static async Task<string> SConnectAsync()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string returndata = "";
            int temp = 1;
            string packet = MainClass.docment() + "\\総合図書管理システム\\System\\data\\data.json";
            if (File.Exists(packet))
            {
                DateTime lastModified = File.GetLastWriteTime(packet);
                TimeSpan timeSinceModified = DateTime.Now - lastModified;
                bool renewed = timeSinceModified.TotalDays < 1;

                if (renewed)
                {
                    temp = 0;
                }
                else
                {
                    temp = 1;
                }
            }
            else
            {
                temp = 1;
            }
            if (temp == 1)
            {
                try
                {
                    string license_id = MainClass.AESDe(File.ReadAllText(MainClass.docment() + "\\総合図書管理システム\\System\\id.aestxt"));
                    if (license_id != "offline")
                    {
                        string result = await SendHttpRequest("https://api.tosho-kanri.com/license_regular.php?id=" + license_id);
                        if (result.Trim().Equals("false", StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("ライセンス認証サーバーがこのキーを無効と判断しました。\r\n正当なIDを入力していた場合はサポートにお問い合わせください。\r\nライセンスキー:" + license_id,
                                "無効なライセンスキー",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            try
                            {
                                File.Delete(MainClass.docment() + "\\総合図書管理システム\\System\\id.aestxt");
                            }
                            catch { }
                            Application.Restart();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("ライセンスキーが見つかりません。\r\nもう一度認証しなおしてください。",
                                "無効なライセンスキー",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    try
                    {
                        File.Delete(MainClass.docment() + "\\総合図書管理システム\\System\\id.aestxt");
                    }
                    catch { }
                    Application.Restart();
                }

                string url = MainClass.data_url;
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        // JSONファイルをダウンロード
                        HttpResponseMessage response = client.GetAsync(url).Result; // 同期的な呼び出し

                        // 応答が成功したかどうかを確認
                        response.EnsureSuccessStatusCode();

                        // ダウンロードしたJSONを保存
                        using (FileStream fileStream = new FileStream(packet, FileMode.Create))
                        {
                            response.Content.CopyToAsync(fileStream).Wait();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("エラーが発生しました。\r\nインターネット接続をお確かめください。\r\n" + ex.Message, "通信エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            try
            {
                StreamReader sr = new StreamReader(packet, Encoding.GetEncoding("UTF-8"));
                string str = sr.ReadToEnd();
                sr.Close();
                string json = str;
                if (!string.IsNullOrEmpty(json))
                {
                    string latest = MainClass.ExtractJsonData(json, "latest");
                    string apiUrl = MainClass.ExtractJsonData(json, "url");
                    string emergency = MainClass.ExtractJsonData(json, "emergency");
                    Version versionC = Assembly.GetEntryAssembly().GetName().Version;
                    Version versionI = new Version(latest);
                    int comparisonResult = versionC.CompareTo(versionI);
                    if (comparisonResult < 0)
                    {
                        returndata = $"新しい更新データがあります。設定/保守画面から更新してください。実行中: {versionC}  最新版: {versionI}\r\n";
                        returndata += "\r\n" + emergency;
                        MessageBox.Show($"新しいバージョンがリリースされました。\r\n保守画面からダウンロードしてください。\r\n新バージョン:{versionI}");
                    }
                    else if (comparisonResult > 0)
                    {
                        returndata = $"現在正式リリース版より新しいバージョンが実行されています。ver{versionC}\r\n";
                        returndata += "\r\n" + emergency;
                    }
                    else
                    {
                        returndata = $"最新版が実行されています。ver{versionC}\r\n";
                        returndata += "\r\n" + emergency;
                    }
                }
            }
            catch (Exception ex)
            {
                returndata = "サーバーとの通信中にエラーが発生しました。\r\nインターネット接続をお確かめください。\r\n" + ex.Message;
            }
            return returndata;
        }

        static async Task<string> SendHttpRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // HTTP GETリクエストを送信し、レスポンスを取得
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    // レスポンスの内容を文字列として取得
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTPリクエストエラー: {ex.Message}");
                    // ここにエラー時の処理を追加
                    return string.Empty;
                }
            }
        }
    }
}
