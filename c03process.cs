using PCSC;
using System.Data;
using System.Text;
using System.Xml;

namespace 総合図書管理システム新版
{
    internal class c03process
    {
        static public string Lending(string usernumber, string booknumber, int lendday)
        {
            string output = "";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            DateTime d = DateTime.Now;
            string savepath = MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\貸出中蔵書";

            //本が予約済みか確認
            Directory.CreateDirectory(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\予約本一覧\\" + booknumber.Replace(" ", ""));
            var fileList2 = Directory.GetFiles(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\予約本一覧\\" + booknumber.Replace(" ", ""), "*.txt");
            bool exist2 = fileList2.Length > 0;
            string reserver = "";
            if (exist2 == true)
            {
                string[] names = Directory.GetFiles(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\予約本一覧\\" + booknumber.Replace(" ", ""), "*.txt");
                foreach (string name in names)
                {
                    reserver += "\r\n" + Path.GetFileNameWithoutExtension(name);
                }
            }
            if (exist2 == true && reserver.Replace("\r\n", "").Contains(usernumber.Replace(" ", "").ToString()) == false)
            {
                MessageBox.Show("この本は現在以下の利用者によって予約されています。" + reserver, "予約された本", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //本が貸出済みか確認
                if (File.Exists(savepath + "\\" + booknumber.Replace(" ", "") + ".txt") == false)
                {
                    //利用者名、書名を照会
                    DataRow[] rowu = usernamecall(usernumber.Replace(" ", ""));
                    DataRow[] rows = booknamecall(booknumber.Replace(" ", ""));
                    if (rows.Length == 1)
                    {   //本がデータ上に1つだけだったコース
                        if (rowu.Length == 1)
                        {   //利用者情報がデータ上に1つだけだったコース
                            //書名と利用者名を照会データから抽出
                            string bookname = rows[0]["書名"].ToString();
                            string username = rowu[0]["氏名"].ToString();
                            //利用者の権限と本の貸出可能権限を抽出
                            int bookpem = int.Parse(rows[0]["貸出区分記号"].ToString());
                            int userpem = int.Parse(rowu[0]["利用者権限レベル"].ToString());
                            //利用者の権限レベル確認
                            if (userpem >= bookpem)
                            {
                                //利用者がほかにも本を借りているか確認
                                Directory.CreateDirectory(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\貸出者一覧\\" + usernumber.Replace(" ", ""));
                                var fileList1 = Directory.GetFiles(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\貸出者一覧\\" + usernumber.Replace(" ", ""), "*.txt");
                                bool exist = fileList1.Length > 0;
                                if (exist == false)
                                {   //利用者がほかに本を借りていないルート
                                    StreamWriter writer = new StreamWriter(savepath + "\\" + booknumber.Replace(" ", "") + ".txt", false);
                                    writer.Write(usernumber.Replace(" ", ""));
                                    writer.Close();
                                    Directory.CreateDirectory(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\統計データ\\" + booknumber.Replace(" ", ""));
                                    //この本が過去誰かに借りられたか確認
                                    if (File.Exists(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\統計データ\\" + usernumber.Replace(" ", "") + "\\userdata.txt") == false)
                                    {   //過去に借りられたことがないルート
                                        StreamWriter writer1 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\統計データ\\" + booknumber.Replace(" ", "") + "\\userdata.txt", false);
                                        writer1.WriteLine("登録番号[" + booknumber.Replace(" ", "") + "]の貸出履歴");
                                        writer1.WriteLine("-------------------------------------");
                                        writer1.Write(d.ToString("[yyyy年MM月dd日(ddd) HH:mm] 利用者ナンバー:") + usernumber.Replace(" ", ""));
                                        writer1.Close();
                                    }
                                    else
                                    {   //過去に借りられたことがあるルート
                                        StreamWriter writer1 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\統計データ\\" + booknumber.Replace(" ", "") + "\\userdata.txt", true);
                                        writer1.Write("\r\n" + d.ToString("[yyyy年MM月dd日(ddd) HH:mm] 利用者ナンバー:") + usernumber.Replace(" ", ""));
                                        writer1.Close();
                                    }
                                    //統計データを保存
                                    if (File.Exists(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\統計データ\\" + usernumber.Replace(" ", "") + "\\bookdata.txt") == false)
                                    {
                                        Directory.CreateDirectory(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\統計データ\\" + usernumber.Replace(" ", ""));
                                        StreamWriter writer2 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\統計データ\\" + usernumber.Replace(" ", "") + "\\bookdata.txt", false);
                                        writer2.WriteLine("利用者番号[" + usernumber.Replace(" ", "") + "]の貸出履歴");
                                        writer2.WriteLine("-------------------------------------");
                                        writer2.Write(d.ToString("[yyyy年MM月dd日(ddd) HH:mm] 登録番号:") + booknumber.Replace(" ", ""));
                                        writer2.Close();
                                    }
                                    else
                                    {
                                        StreamWriter writer2 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\統計データ\\" + usernumber.Replace(" ", "") + "\\bookdata.txt", true);
                                        writer2.Write("\r\n" + d.ToString("[yyyy年MM月dd日(ddd) HH:mm] 登録番号:") + booknumber.Replace(" ", ""));
                                        writer2.Close();
                                    }
                                    //督促状用？
                                    StreamWriter writer3 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\貸出者一覧\\" + usernumber.Replace(" ", "") + "\\" + booknumber.Replace(" ", "") + ".txt", false);
                                    writer3.Write(d.AddDays(lendday).ToString("yyyy/MM/dd HH:mm:ss"));
                                    writer3.Close();
                                    //画面上ログを出力
                                    output += $"\r\n{d.ToString("yyyy/MM/dd-HH:mm:ss")} [貸出] " + usernumber.Replace(" ", "") + "\"" + username + "\"" + " -> " + booknumber.Replace(" ", "") + "\"" + bookname + "\"";
                                }
                                else
                                {   //利用者がほかにも本を借りているルート
                                    DialogResult result = MessageBox.Show($"この利用者にはすでに{fileList1.Length}つの蔵書を貸し出しています。\r\nこのまま貸出処理を続けますか？", "貸出数警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    if (result == DialogResult.Yes)
                                    {
                                        Directory.CreateDirectory(savepath + "\\" + booknumber.Replace(" ", ""));
                                        StreamWriter writer = new StreamWriter(savepath + "\\" + booknumber.Replace(" ", "") + ".txt", false);
                                        writer.Write(usernumber.Replace(" ", ""));
                                        writer.Close();
                                        Directory.CreateDirectory(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\統計データ\\" + booknumber.Replace(" ", ""));
                                        //この本が過去誰かに借りられたか確認
                                        if (File.Exists(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\統計データ\\" + usernumber.Replace(" ", "") + "\\userdata.txt") == false)
                                        {   //過去に借りられたことがないルート
                                            StreamWriter writer1 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\統計データ\\" + booknumber.Replace(" ", "") + "\\userdata.txt", false);
                                            writer1.WriteLine("登録番号[" + booknumber.Replace(" ", "") + "]の貸出履歴");
                                            writer1.WriteLine("-------------------------------------");
                                            writer1.Write(d.ToString("[yyyy年MM月dd日(ddd) HH:mm] 利用者ナンバー:") + usernumber.Replace(" ", ""));
                                            writer1.Close();
                                        }
                                        else
                                        {   //過去に借りられたことがあるルート
                                            StreamWriter writer1 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\統計データ\\" + booknumber.Replace(" ", "") + "\\userdata.txt", true);
                                            writer1.Write("\r\n" + d.ToString("[yyyy年MM月dd日(ddd) HH:mm] 利用者ナンバー:") + usernumber.Replace(" ", ""));
                                            writer1.Close();
                                        }
                                        //統計データを保存
                                        if (File.Exists(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\統計データ\\" + usernumber.Replace(" ", "") + "\\bookdata.txt") == false)
                                        {
                                            Directory.CreateDirectory(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\統計データ\\" + booknumber.Replace(" ", ""));
                                            StreamWriter writer2 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\統計データ\\" + usernumber.Replace(" ", "") + "\\bookdata.txt", false);
                                            writer2.WriteLine("利用者番号[" + usernumber.Replace(" ", "") + "]の貸出履歴");
                                            writer2.WriteLine("-------------------------------------");
                                            writer2.Write(d.ToString("[yyyy年MM月dd日(ddd) HH:mm] 登録番号:") + booknumber.Replace(" ", ""));
                                            writer2.Close();
                                        }
                                        else
                                        {
                                            StreamWriter writer2 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\統計データ\\" + usernumber.Replace(" ", "") + "\\bookdata.txt", true);
                                            writer2.Write("\r\n" + d.ToString("[yyyy年MM月dd日(ddd) HH:mm] 登録番号:") + booknumber.Replace(" ", ""));
                                            writer2.Close();
                                        }
                                        Directory.CreateDirectory(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\貸出者一覧\\" + usernumber.Replace(" ", ""));
                                        StreamWriter writer3 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\貸出者一覧\\" + usernumber.Replace(" ", "") + "\\" + booknumber.Replace(" ", "") + ".txt", false);
                                        writer3.Write(d.AddDays(lendday).ToString("yyyy/MM/dd HH:mm:ss"));
                                        writer3.Close();
                                        output += $"\r\n{d.ToString("yyyy/MM/dd-HH:mm:ss")} [貸出:警告有] " + usernumber.Replace(" ", "") + "\"" + username + "\"" + " -> " + booknumber.Replace(" ", "") + "\"" + bookname + "\"";
                                    }
                                    else
                                    {
                                        return "";
                                    }
                                }
                                {
                                    //督促状用
                                    string bookname10;
                                    if (bookname.Length >= 10) { bookname10 = bookname.Substring(0, 10); }
                                    else { bookname10 = bookname; }
                                    string path = MainClass.docment() + "\\総合図書管理システム\\System\\data\\limit.aescsv";
                                    StreamReader reader = new StreamReader(path, Encoding.GetEncoding("shift_jis"));
                                    string Endata = reader.ReadToEnd();
                                    reader.Close();
                                    string Dedata = MainClass.AESDe(Endata);
                                    Dedata += "\r\n" + booknumber + "," + bookname10 + "," + d.AddDays(lendday).ToString("yyyy/MM/dd HH:mm:ss") + "," + rowu[0]["学年"].ToString() + "," + rowu[0]["クラス"].ToString() + "," + rowu[0]["番号"].ToString() + "," + rowu[0]["氏名"].ToString();
                                    Endata = MainClass.AESEn(Dedata);
                                    StreamWriter writercsv = new StreamWriter(path, false, Encoding.GetEncoding("shift_jis"));
                                    writercsv.Write(Endata);
                                    writercsv.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("この利用者はこの蔵書を借りる十分な権限がありません。\r\n必要な権限:レベル" + bookpem.ToString(), "権限エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (rowu.Length == 0)
                        {
                            MessageBox.Show("この利用者番号は登録されていません。\r\n利用者を登録してから貸出処理を行ってください。", "利用者データエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("この利用者番号は複数の利用者に割り当てられています。\r\n番号の重複を回避してから貸出処理を行ってください。\r\n\r\n重複解消手順:\r\n[保守管理]>[利用者]メニュー>[利用者データ編集]でこの利用者番号の重複を修正してください。", "利用者データエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (rows.Length == 0)
                    {
                        MessageBox.Show("この登録番号は使用されていません。\r\n蔵書を登録してから貸出処理を行ってください。", "蔵書データエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("この登録番号は複数の蔵書に割り当てられています。\r\n番号の重複を回避してから貸出処理を行ってください。\r\n\r\n重複解消手順:\r\n[保守管理]>[蔵書]メニュー>[蔵書データ編集]でこの登録番号の重複を修正してください。", "蔵書データエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("この本はすでに貸し出されています。\r\n返却処理を行ってから貸出処理を行ってください。", "貸出処理エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (exist2 == true && reserver.Replace("\r\n", "").Contains(usernumber.Replace(" ", "").ToString()) == true)
            {
                FileInfo file2 = new FileInfo(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\予約者一覧\\" + usernumber.Replace(" ", "") + "\\" + booknumber.Replace(" ", "") + ".txt");
                if ((file2.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) file2.Attributes = FileAttributes.Normal;
                file2.Delete();
                FileInfo file3 = new FileInfo(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\予約本一覧\\" + booknumber.Replace(" ", "") + "\\" + usernumber.Replace(" ", "") + ".txt");
                if ((file3.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) file3.Attributes = FileAttributes.Normal;
                file3.Delete();
            }
            return output;
        }

        static public string Returning(string booknumber)
        {
            string output = "";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            DateTime d = DateTime.Now;
            string savepath = MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\貸出中蔵書";

            if (File.Exists(savepath + "\\" + booknumber.Replace(" ", "") + ".txt") == true)
            {
                DataRow[] rows = booknamecall(booknumber.Replace(" ", ""));
                DataRow[] rowu;
                if (rows.Length == 1)
                {
                    StreamReader sr = new StreamReader(savepath + "\\" + booknumber.Replace(" ", "") + ".txt");
                    string text = sr.ReadToEnd();
                    sr.Close();
                    FileInfo file2 = new FileInfo(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\貸出者一覧\\" + text + "\\" + booknumber.Replace(" ", "") + ".txt");
                    rowu = usernamecall(text.Replace(" ", ""));
                    string username = rowu[0]["氏名"].ToString();
                    string bookname = rows[0]["書名"].ToString();
                    string bookndc = rows[0]["請求番号"].ToString();
                    StreamReader sr1 = new StreamReader(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\貸出者一覧\\" + text.Replace(" ", "") + "\\" + booknumber.Replace(" ", "") + ".txt");
                    string text1 = sr1.ReadToEnd();
                    sr1.Close();
                    FileInfo file = new FileInfo(savepath + "\\" + booknumber.Replace(" ", "") + ".txt");
                    if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) file.Attributes = FileAttributes.Normal;
                    file.Delete();
                    if ((file2.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) file2.Attributes = FileAttributes.Normal;
                    file2.Delete();
                    bool delay = false;
                    if (0 < d.ToString("yyyy/MM/dd HH:mm:ss").CompareTo(text1))
                    {
                        Directory.CreateDirectory(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\延滞履歴\\" + text.Replace(" ", ""));
                        StreamWriter writer3 = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\利用者データ\\延滞履歴\\" + text.Replace(" ", "") + "\\exist.txt", false);
                        writer3.Write(d.ToString("yyyy/MM/dd HH:mm:ss"));
                        writer3.Close();
                        MessageBox.Show("延滞されていた返却です。", "延滞警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        output += $"\r\n{d.ToString("yyyy/MM/dd-HH:mm:ss")} [返却:延滞あり] {text}\"{username}\" -> {booknumber.Replace(" ", "")}\"{bookname}\"";
                        delay = true;
                    }
                    else
                    {
                        output += $"\r\n{d.ToString("yyyy/MM/dd-HH:mm:ss")} [返却] {text}\"{username}\" -> {booknumber.Replace(" ", "")}\"{bookname}\"";
                    }
                    {
                        string filePath = MainClass.docment() + "\\総合図書管理システム\\System\\data\\historydata.aescsv";
                        StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("shift_jis"));
                        string Endata = reader.ReadToEnd();
                        reader.Close();
                        string Dedata = MainClass.AESDe(Endata);
                        Dedata += "\r\n" + d.ToString("yyyy/MM/dd HH:mm:ss") + "," + text + "," + booknumber + "," + delay.ToString() + "," + bookndc;
                        Endata = MainClass.AESEn(Dedata);
                        StreamWriter history = new StreamWriter(filePath, false, Encoding.GetEncoding("shift_jis"));
                        history.Write(Endata);
                        history.Close();
                    }

                    var fileList1 = Directory.GetFiles(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\予約本一覧\\" + booknumber.Replace(" ", ""), "*.txt");
                    bool exist = fileList1.Length > 0;
                    if (exist == true)
                    {
                        string reserver = "";
                        string[] names = Directory.GetFiles(MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\予約本一覧\\" + booknumber.Replace(" ", ""), "*.txt");
                        foreach (string name in names)
                        {
                            reserver += "\r\n" + Path.GetFileNameWithoutExtension(name);
                        }
                        MessageBox.Show("この本は現在以下の利用者によって予約されています。" + reserver, "予約された本", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    {
                        string csvFilePath = MainClass.docment() + "\\総合図書管理システム\\System\\data\\limit.aescsv";
                        string tempFilePath = MainClass.docment() + "\\総合図書管理システム\\System\\data\\templimit.aescsv";
                        string searchValue = booknumber.ToString().Replace(" ", "");
                        // CSVファイルをAESで復号化
                        StreamReader reader = new StreamReader(csvFilePath);
                        string Endata = reader.ReadToEnd();
                        reader.Close();
                        string decryptedText = MainClass.AESDe(Endata);
                        // 復号化されたCSVデータを処理
                        List<string> lines = decryptedText.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
                        // 列0が指定された文字列Aと一致する行を削除
                        lines.RemoveAll(line => line.Split(',')[0].Trim() == searchValue);
                        // 暗号化キーと初期化ベクトルを使用してデータを再びAESで暗号化
                        string encryptedText = MainClass.AESEn(string.Join(Environment.NewLine, lines));
                        // 暗号化されたデータをファイルに書き込み
                        File.WriteAllText(csvFilePath, encryptedText, Encoding.GetEncoding("Shift_jis"));
                    }
                }
                else if (rows.Length == 0)
                {
                    MessageBox.Show("この登録番号は使用されていません。\r\n蔵書を登録してから貸出処理を行ってください。", "蔵書データエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("この登録番号は複数の蔵書に割り当てられています。\r\n番号の重複を回避してから貸出処理を行ってください。\r\n\r\n重複解消手順:\r\n[保守管理]>[蔵書]メニュー>[蔵書データ編集]でこの登録番号の重複を修正してください。", "蔵書データエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("この本は貸し出されていません。", "返却処理エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return output;
        }

        static public DataRow[] booknamecall(string booknumber)
        {
            string filePath = MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\masterdata.aescsv";
            DataTable dataTable = new DataTable();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                // CSVファイルからテキストを読み取り
                string encryptedText = File.ReadAllText(filePath, Encoding.GetEncoding("shift_jis"));

                // 復号化
                string decryptedText = MainClass.AESDe(encryptedText);

                // 復号化されたテキストをDataTableに読み込む
                using (StringReader reader = new StringReader(decryptedText))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        if (dataTable.Columns.Count == 0)
                        {
                            // 初めての行の場合、列を追加
                            foreach (var value in values)
                            {
                                dataTable.Columns.Add(value.Trim());
                            }
                        }
                        else
                        {
                            // データ行を追加
                            dataTable.Rows.Add(values);
                        }
                    }
                }
                // 指定した列において指定した文字列に一致する行を抽出
                DataRow[] resultRows = dataTable.Select($"{"登録番号"} = '{booknumber}'");
                return resultRows;
            }
            catch (Exception e)
            {
                MessageBox.Show("蔵書データテーブル読み取り時にエラーが発生しました。\r\n" + e.Message, "不明なエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static DataRow[] usernamecall(string usernumber)
        {
            string filePath = MainClass.docment() + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv";
            DataTable dataTable = new DataTable();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                if (File.Exists(filePath) == true)
                {
                    // CSVファイルからテキストを読み取り
                    string encryptedText = File.ReadAllText(filePath, Encoding.GetEncoding("shift_jis"));

                    // 復号化
                    string decryptedText = MainClass.AESDe(encryptedText);

                    // 復号化されたテキストをDataTableに読み込む
                    using (StringReader reader = new StringReader(decryptedText))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] values = line.Split(',');
                            if (dataTable.Columns.Count == 0)
                            {
                                // 初めての行の場合、列を追加
                                foreach (var value in values)
                                {
                                    dataTable.Columns.Add(value.Trim());
                                }
                            }
                            else
                            {
                                // データ行を追加
                                dataTable.Rows.Add(values);
                            }
                        }
                    }
                    // 指定した列において指定した文字列に一致する行を抽出
                    DataRow[] resultRows = dataTable.Select($"{"利用者番号"} = '{usernumber}'");
                    return resultRows;
                }
                else
                {
                    throw new Exception("利用者データファイルが見つかりません。");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("利用者データテーブル読み取り時にエラーが発生しました。\r\n" + e.Message, "不明なエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        static public DataRow[] icidcall(string icid)
        {
            string filePath = MainClass.docment() + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv";
            DataTable dataTable = new DataTable();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                if (File.Exists(filePath) == true)
                {
                    // CSVファイルからテキストを読み取り
                    string encryptedText = File.ReadAllText(filePath, Encoding.GetEncoding("shift_jis"));

                    // 復号化
                    string decryptedText = MainClass.AESDe(encryptedText);

                    // 復号化されたテキストをDataTableに読み込む
                    using (StringReader reader = new StringReader(decryptedText))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] values = line.Split(',');
                            if (dataTable.Columns.Count == 0)
                            {
                                // 初めての行の場合、列を追加
                                foreach (var value in values)
                                {
                                    dataTable.Columns.Add(value.Trim());
                                }
                            }
                            else
                            {
                                // データ行を追加
                                dataTable.Rows.Add(values);
                            }
                        }
                    }
                    // 指定した列において指定した文字列に一致する行を抽出
                    DataRow[] resultRows = dataTable.Select($"{"icid"} = '{icid}'");
                    return resultRows;
                }
                else
                {
                    throw new Exception("利用者データファイルが見つかりません。");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("利用者データテーブル読み取り時にエラーが発生しました。\r\n" + e.Message, "不明なエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public string[] Cardread()
        {
            string cardId = "";
            string log = "";
            IntPtr hContext = IntPtr.Zero;

            // 1. SCardEstablishContext
            log += "***** 1. カード読み取りサービスへ接続 *****\r\n";
            uint ret = Api.SCardEstablishContext(PCSC.Constant.SCARD_SCOPE_USER, IntPtr.Zero, IntPtr.Zero, out hContext);
            if (ret != PCSC.Constant.SCARD_S_SUCCESS)
            {
                string message;
                switch (ret)
                {
                    case PCSC.Constant.SCARD_E_NO_SERVICE:
                        message = "サービスが起動されていません。";
                        break;
                    default:
                        message = "サービスに接続できません。code = " + ret;
                        break;
                }
                log += message;
                return new string[] { "取得失敗", log };
            }
            if (hContext == IntPtr.Zero)
            {
                log += "コンテキストの取得に失敗しました。";
                return new string[] { "取得失敗", log };
            }
            log += "サービスに接続しました。\r\n";

            // 2. SCardListReaders
            log += "***** 2. NFCリーダーと接続 *****\r\n";
            uint pcchReaders = 0;
            // NFCリーダの文字列バッファのサイズを取得
            ret = Api.SCardListReaders(hContext, null, null, ref pcchReaders);
            if (ret != PCSC.Constant.SCARD_S_SUCCESS)
            {
                // 検出失敗
                log += "NFCリーダを確認できません。";
                return new string[] { "取得失敗", log };
            }
            // NFCリーダの文字列を取得
            byte[] mszReaders = new byte[pcchReaders * 2]; // 1文字2byte
            ret = Api.SCardListReaders(hContext, null, mszReaders, ref pcchReaders);
            if (ret != PCSC.Constant.SCARD_S_SUCCESS)
            {
                // 検出失敗
                log += "NFCリーダの取得に失敗しました。";
                return new string[] { "取得失敗", log };
            }
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            string readerNameMultiString = unicodeEncoding.GetString(mszReaders);
            // 認識したNDCリーダの最初の1台を使用
            int nullindex = readerNameMultiString.IndexOf((char)0);
            var readerName = readerNameMultiString.Substring(0, nullindex);
            log += "NFCリーダを検出しました。:" + readerName + "\r\n";

            // 3. SCardConnect
            log += "***** 3. NFCカードと接続 *****\r\n";
            IntPtr hCard = IntPtr.Zero;
            IntPtr activeProtocol = IntPtr.Zero;
            ret = Api.SCardConnect(hContext, readerName, PCSC.Constant.SCARD_SHARE_SHARED, PCSC.Constant.SCARD_PROTOCOL_T1, ref hCard, ref activeProtocol);
            if (ret != PCSC.Constant.SCARD_S_SUCCESS)
            {
                log += "カードに接続できません。code = " + ret;
                return new string[] { "取得失敗", log };
            }
            log += "カードに接続しました。\r\n";

            // 4. SCardTransmit
            log += "***** 4. NFCカードからIDを取得 *****\r\n";
            uint maxRecvDataLen = 256;
            var recvBuffer = new byte[maxRecvDataLen + 2];
            var sendBuffer = new byte[] { 0xff, 0xca, 0x00, 0x00, 0x00 };  // ← IDmを取得するコマンド

            Api.SCARD_IO_REQUEST ioRecv = new Api.SCARD_IO_REQUEST();
            ioRecv.cbPciLength = 255;

            int pcbRecvLength = recvBuffer.Length;
            int cbSendLength = sendBuffer.Length;

            IntPtr handle = Api.LoadLibrary("Winscard.dll");
            IntPtr pci = Api.GetProcAddress(handle, "g_rgSCardT1Pci");
            Api.FreeLibrary(handle);

            ret = Api.SCardTransmit(hCard, pci, sendBuffer, cbSendLength, ioRecv, recvBuffer, ref pcbRecvLength);
            if (ret != PCSC.Constant.SCARD_S_SUCCESS)
            {
                log += "NFCカードへの送信に失敗しました。code = " + ret;
                return new string[] { "取得失敗", log };
            }
            // 受信データからIDmを抽出する
            // recvBuffer = IDm + SW1 + SW2 (SW = StatusWord)
            // SW1 = 0x90 (144) SW1 = 0x00 (0) で正常だが、ここでは見ていない
            cardId = BitConverter.ToString(recvBuffer, 0, pcbRecvLength - 2);
            log += "カードからデータを取得しました。\r\n";
            log += "【IDm】：" + cardId + "\r\n";

            // ##################################################
            // 5. SCardDisconnect
            // ##################################################
            log += "***** 5. NFCカードを切断 *****\r\n";
            ret = Api.SCardDisconnect(hCard, PCSC.Constant.SCARD_LEAVE_CARD);
            if (ret != PCSC.Constant.SCARD_S_SUCCESS)
            {
                log += "NFCカードとの切断に失敗しました。code = " + ret;
                return new string[] { cardId, log };
            }
            log += "カードを切断しました。";
            return new string[] { cardId, log };
        }

        public int Rentalperiod(bool change, int days = 7)
        {
            string path = MainClass.docment() + "\\総合図書管理システム\\System\\config.xml";
            try
            {
                if (change)
                {
                    string encryptedXmlString = File.ReadAllText(path);
                    string decryptedXmlString = MainClass.AESDe(encryptedXmlString);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(decryptedXmlString);
                    XmlElement rootElement = xmlDoc.DocumentElement;
                    XmlNode row1Node = rootElement.SelectSingleNode("基本情報");
                    XmlNode row2Node = row1Node.SelectSingleNode("rentalperiod");
                    row2Node.InnerText = days.ToString();
                    string xmlString = xmlDoc.OuterXml;
                    encryptedXmlString = MainClass.AESEn(xmlString);
                    File.WriteAllText(path, encryptedXmlString);
                    return days;
                }
                else
                {
                    string encryptedXmlString = File.ReadAllText(path);
                    string decryptedXmlString = MainClass.AESDe(encryptedXmlString);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(decryptedXmlString);
                    XmlElement rootElement = xmlDoc.DocumentElement;
                    XmlNode row1Node = rootElement.SelectSingleNode("基本情報");
                    XmlNode row2Node = row1Node.SelectSingleNode("rentalperiod");
                    if (int.TryParse(row2Node.InnerText, out var day))
                    {
                        return day;
                    }
                    else
                    {
                        return 7;
                    }
                }
            }
            catch
            {
                MessageBox.Show("致命的なエラーが発生しました。プログラムは直ちに終了します。\r\nエラーコード:CONFIG_FILE_ERROR", "致命的なエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return 0;
            }
        }
    }
}
