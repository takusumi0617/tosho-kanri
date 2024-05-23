using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Xml.Linq;

namespace 総合図書管理システム新版
{
    public partial class _09collectionadd : Form
    {
        private int countDownSeconds = 20;
        private System.Windows.Forms.Timer timer;
        public _09collectionadd()
        {
            InitializeComponent();
            timer = new System.Windows.Forms.Timer
            {
                Interval = 1000 // 1 second
            };
            timer.Tick += Timer_Tick;
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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string lib1;
            string lib2;
            switch (comboBox1.Text)
            {
                case "1.図書":
                    lib1 = "1";
                    lib2 = "図書";
                    break;
                case "2.CD":
                    lib1 = "2";
                    lib2 = "CD";
                    break;
                case "3.デジタルデータ":
                    lib1 = "3";
                    lib2 = "デジタルデータ";
                    break;
                default:
                    lib1 = "4";
                    lib2 = "その他";
                    break;
            }

            string lend1;
            string lend2;
            switch (comboBox2.Text)
            {
                case "1.一般貸出":
                    lend1 = "1";
                    lend2 = "一般貸出";
                    break;
                case "2.職員限定貸出":
                    lend1 = "2";
                    lend2 = "職員限定貸出";
                    break;
                case "3.貸出禁止":
                    lend1 = "3";
                    lend2 = "貸出禁止";
                    break;
                default:
                    lend1 = "4";
                    lend2 = "その他";
                    break;
            }

            string by1;
            string by2;
            switch (comboBox4.Text)
            {
                case "1.県費":
                    by1 = "1";
                    by2 = "県費";
                    break;
                case "2.市区町村費":
                    by1 = "2";
                    by2 = "市区町村費";
                    break;
                case "3.団費":
                    by1 = "3";
                    by2 = "団費";
                    break;
                case "4.委員会費":
                    by1 = "4";
                    by2 = "委員会費";
                    break;
                case "5.生徒会費":
                    by1 = "5";
                    by2 = "生徒会費";
                    break;
                case "6.寄贈":
                    by1 = "6";
                    by2 = "寄贈";
                    break;
                default:
                    by1 = "7";
                    by2 = "その他";
                    break;
            }

            string where1;
            string where2;
            switch (comboBox5.Text)
            {
                case "1.図書室":
                    where1 = "1";
                    where2 = "図書室";
                    break;
                case "2.閲覧室":
                    where1 = "2";
                    where2 = "閲覧室";
                    break;
                case "3.司書室":
                    where1 = "3";
                    where2 = "司書室";
                    break;
                case "4.書庫":
                    where1 = "4";
                    where2 = "書庫";
                    break;
                default:
                    where1 = "5";
                    where2 = "その他";
                    break;
            }

            string data = "\r\n" + textBox1.Text + "," + lib1 + "," + lib2 + "," + lend1 + "," + lend2 + "," + textBox2.Text + "," + textBox3.Text + "," + textBox4.Text + "," + textBox5.Text + "," + textBox6.Text + "," + textBox8.Text + "," + textBox9.Text + "," + textBox10.Text + "," + textBox11.Text + "," + textBox12.Text + "," + textBox13.Text + "," + textBox14.Text + "," + textBox15.Text + "cm*" + textBox16.Text + "cm," + textBox17.Text + "," + textBox19.Text + "," + by1 + "," + by2 + "," + where1 + "," + where2 + "," + textBox18.Text;
            string path = MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\masterdata.aescsv";
            StreamReader reader = new StreamReader(path, Encoding.GetEncoding("shift_jis"));
            string Endata = reader.ReadToEnd();
            reader.Close();
            string Dedata = MainClass.AESDe(Endata);
            Dedata += data;
            StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("shift_jis"));
            writer.Write(MainClass.AESEn(Dedata));
            writer.Close();
            MessageBox.Show("以下のデータを保存しました。\r\n書名:" + textBox5.Text + "\r\nISBN:" + textBox4.Text, "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            textBox3.Text = d.ToString("yyyy/MM/dd");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (countDownSeconds >= 0)
            {
                if (textBox7.TextLength == 13 || textBox7.TextLength == 10)
                {
                    try
                    {
                        await DownloadXmlFile("https://iss.ndl.go.jp/api/opensearch?dpid=iss-ndl-opac&isbn=" + textBox7.Text, MainClass.docment() + "\\総合図書管理システム\\xml\\未加工\\b.xml");
                    }
                    catch
                    {
                        MessageBox.Show("データの取得に失敗しました。", "通信エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        countDownSeconds = 20;
                        timer.Start();
                        return;
                    }

                    StreamReader sr = new StreamReader(MainClass.docment() + "\\総合図書管理システム\\xml\\未加工\\b.xml");
                    string text = sr.ReadToEnd();
                    sr.Close();
                    text = text.Replace("xsi:", "");
                    text = text.Replace("dc:", "");
                    text = text.Replace("dcndl:", "");
                    StreamWriter writer = new StreamWriter(MainClass.docment() + "\\総合図書管理システム\\xml\\未加工\\b.xml", false);
                    writer.Write(text);
                    writer.Close();

                    // XMLファイルを読み込む
                    XDocument doc = XDocument.Load(MainClass.docment() + "\\総合図書管理システム\\xml\\未加工\\b.xml");

                    // 辞書を作成する
                    Dictionary<int, string> titles = new Dictionary<int, string>();
                    int indexA = 0;
                    {
                        foreach (XElement titleA in doc.Descendants("item"))
                        {
                            titles.Add(indexA, titleA.Element("title").Value);
                            indexA++;
                        }
                    }

                    // 辞書を作成する
                    Dictionary<int, string> titlekes = new Dictionary<int, string>();
                    int indexB = 0;
                    {
                        foreach (XElement titleB in doc.Descendants("titleTranscription"))
                        {
                            titlekes.Add(indexB, titleB.Value);
                            indexB++;
                        }
                    }

                    Dictionary<int, string> ndcA = new Dictionary<int, string>();
                    int ndcB = 0;
                    foreach (XElement item in doc.Descendants("item"))
                    {
                        XElement? subject = item.Descendants("subject").FirstOrDefault(x => (string?)x.Attribute("type") == "NDC10");
                        if (subject != null)
                        {
                            string value = subject.Value;
                            ndcA.Add(ndcB, value);
                            ndcB++;
                        }
                        else
                        {
                            XElement? subject1 = item.Descendants("subject").FirstOrDefault(x => (string?)x.Attribute("type") == "NDC9");
                            if (subject1 != null)
                            {
                                string value = subject1.Value;
                                ndcA.Add(ndcB, value);
                                ndcB++;
                            }
                        }
                    }

                    // 辞書を作成する
                    Dictionary<int, string> creators = new Dictionary<int, string>();
                    int creatorA = 0;
                    {
                        foreach (XElement A in doc.Descendants("creator"))
                        {
                            creators.Add(creatorA, A.Value);
                            creatorA++;
                        }
                    }

                    // 辞書を作成する
                    Dictionary<int, string> creatorsB = new Dictionary<int, string>();
                    int creatorB = 0;
                    {
                        foreach (XElement A in doc.Descendants("creatorTranscription"))
                        {
                            creatorsB.Add(creatorB, A.Value);
                            creatorB++;
                        }
                    }

                    // 辞書を作成する
                    Dictionary<int, string> publisher = new Dictionary<int, string>();
                    int publisherA = 0;
                    {
                        foreach (XElement A in doc.Descendants("publisher"))
                        {
                            publisher.Add(publisherA, A.Value);
                            publisherA++;
                        }
                    }

                    // 辞書を作成する
                    Dictionary<int, string> price = new Dictionary<int, string>();
                    int priceA = 0;
                    {
                        foreach (XElement A in doc.Descendants("price"))
                        {
                            price.Add(priceA, A.Value);
                            priceA++;
                        }
                    }

                    // 辞書を作成する
                    Dictionary<int, string> date = new Dictionary<int, string>();
                    int dateA = 0;
                    {
                        foreach (XElement A in doc.Descendants("date"))
                        {
                            try
                            {
                                if (double.TryParse(A.Value.TrimEnd(' '), out double originalValue))
                                {
                                    // 小数点を繰り下げ
                                    double roundedDownValue = Math.Floor(originalValue);

                                    // 結果を文字列に変換して返す
                                    date.Add(dateA, roundedDownValue.ToString());
                                }
                                dateA++;
                            }
                            catch { }
                        }
                    }

                    try
                    {
                        textBox8.Text = creators[0].Replace(",", "");
                    }
                    catch { }
                    try
                    {
                        textBox9.Text = creatorsB[0].Replace(",", "");
                    }
                    catch { }
                    try
                    {
                        textBox2.Text = ndcA[0].Replace(",", "");
                    }
                    catch { }
                    try
                    {
                        textBox5.Text = titles[0].Replace(",", "");
                    }
                    catch { }
                    try
                    {
                        textBox6.Text = titlekes[0].Replace(",", "");
                    }
                    catch { }
                    try
                    {
                        textBox13.Text = publisher[0].Replace(",", "");
                    }
                    catch { }
                    try
                    {
                        textBox17.Text = price[0].Replace(",", "").Replace("円", "");
                    }
                    catch { }
                    try
                    {
                        textBox12.Text = date[0].Replace(",", "");
                    }
                    catch { }
                    textBox4.Text = textBox7.Text;
                    textBox7.Text = "";
                    DateTime d = DateTime.Now;
                    textBox18.Text = $"国立国会図書館書誌データ({d:yyyy年MM月dd日}現在)をもとに登録";

                    countDownSeconds = 20;
                    timer.Start();

                    MessageBox.Show("ISBN変換が完了しました", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ISBNの桁数が正しくありません。", "ISBNエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("短期間にISBN検索を複数回行うことはできません。\r\nあと" + countDownSeconds.ToString() + "秒後にもう一度行ってください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _09collectionadd_Load(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string filePath = MainClass.docment() + "\\総合図書管理システム\\蔵書データ\\masterdata.aescsv";
            if (File.Exists(filePath))
            {
                string Endata = File.ReadAllText(filePath, Encoding.GetEncoding("Shift_JIS"));
                string Dedata = MainClass.AESDe(Endata);
                double maxNumber = 0;
                using (StringReader reader = new StringReader(Dedata))
                using (TextFieldParser parser = new TextFieldParser(reader))
                {
                    parser.Delimiters = new string[] { "," };

                    while (!parser.EndOfData)
                    {
                        string[]? fields = parser.ReadFields();

                        if (fields != null && fields.Length > 0)
                        {
                            if (double.TryParse(fields[0], out double currentNumber))
                            {
                                maxNumber = Math.Max(maxNumber, currentNumber);
                            }
                        }
                    }
                }
                textBox1.Text = (maxNumber + 1).ToString();
            }
            else
            {
                textBox1.Text = "100001";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                FileName = "default.html",
                InitialDirectory = MainClass.docment() + "\\総合図書管理システム\\xml\\未加工\\",
                Filter = "XMLファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*",
                FilterIndex = 1,
                //タイトルを設定する
                Title = "開くファイルを選択してください",
                RestoreDirectory = true,
                CheckFileExists = true,
                CheckPathExists = true
            };

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(ofd.FileName);
                XDocument doc = XDocument.Load(ofd.FileName);

                // 辞書を作成する
                Dictionary<int, string> titles = new Dictionary<int, string>();
                int indexA = 0;
                {
                    foreach (XElement titleA in doc.Descendants("item"))
                    {
                        titles.Add(indexA, titleA.Element("title").Value);
                        indexA++;
                    }
                }

                // 辞書を作成する
                Dictionary<int, string> titlekes = new Dictionary<int, string>();
                int indexB = 0;
                {
                    foreach (XElement titleB in doc.Descendants("titleTranscription"))
                    {
                        titlekes.Add(indexB, titleB.Value);
                        indexB++;
                    }
                }

                Dictionary<int, string> ndcA = new Dictionary<int, string>();
                int ndcB = 0;
                foreach (XElement item in doc.Descendants("item"))
                {
                    XElement? subject = item.Descendants("subject").FirstOrDefault(x => (string?)x.Attribute("type") == "NDC10");
                    if (subject != null)
                    {
                        string value = subject.Value;
                        ndcA.Add(ndcB, value);
                        ndcB++;
                    }
                    else
                    {
                        XElement? subject1 = item.Descendants("subject").FirstOrDefault(x => (string?)x.Attribute("type") == "NDC9");
                        if (subject1 != null)
                        {
                            string value = subject1.Value;
                            ndcA.Add(ndcB, value);
                            ndcB++;
                        }
                    }
                }

                Dictionary<int, string> isbnA = new Dictionary<int, string>();
                int isbnB = 0;
                foreach (XElement item in doc.Descendants("item"))
                {
                    XElement? subject = item.Descendants("identifier").FirstOrDefault(x => (string?)x.Attribute("type") == "ISBN");
                    if (subject != null)
                    {
                        string value = subject.Value;
                        isbnA.Add(isbnB, value);
                        isbnB++;
                    }
                }

                // 辞書を作成する
                Dictionary<int, string> creators = new Dictionary<int, string>();
                int creatorA = 0;
                {
                    foreach (XElement A in doc.Descendants("creator"))
                    {
                        creators.Add(creatorA, A.Value);
                        creatorA++;
                    }
                }

                // 辞書を作成する
                Dictionary<int, string> creatorsB = new Dictionary<int, string>();
                int creatorB = 0;
                {
                    foreach (XElement A in doc.Descendants("creatorTranscription"))
                    {
                        creatorsB.Add(creatorB, A.Value);
                        creatorB++;
                    }
                }

                // 辞書を作成する
                Dictionary<int, string> publisher = new Dictionary<int, string>();
                int publisherA = 0;
                {
                    foreach (XElement A in doc.Descendants("publisher"))
                    {
                        publisher.Add(publisherA, A.Value);
                        publisherA++;
                    }
                }

                // 辞書を作成する
                Dictionary<int, string> price = new Dictionary<int, string>();
                int priceA = 0;
                {
                    foreach (XElement A in doc.Descendants("price"))
                    {
                        price.Add(priceA, A.Value);
                        priceA++;
                    }
                }

                // 辞書を作成する
                Dictionary<int, string> date = new Dictionary<int, string>();
                int dateA = 0;
                {
                    foreach (XElement A in doc.Descendants("date"))
                    {
                        try
                        {
                            if (double.TryParse(A.Value.TrimEnd(' '), out double originalValue))
                            {
                                // 小数点を繰り下げ
                                double roundedDownValue = Math.Floor(originalValue);

                                // 結果を文字列に変換して返す
                                date.Add(dateA, roundedDownValue.ToString());
                            }
                            dateA++;
                        }
                        catch { }
                    }
                }

                try
                {
                    textBox8.Text = creators[0].Replace(",", "");
                }
                catch { }
                try
                {
                    textBox9.Text = creatorsB[0].Replace(",", "");
                }
                catch { }
                try
                {
                    textBox2.Text = ndcA[0].Replace(",", "");
                }
                catch { }
                try
                {
                    textBox5.Text = titles[0].Replace(",", "");
                }
                catch { }
                try
                {
                    textBox6.Text = titlekes[0].Replace(",", "");
                }
                catch { }
                try
                {
                    textBox13.Text = publisher[0].Replace(",", "");
                }
                catch { }
                try
                {
                    textBox17.Text = price[0].Replace(",", "").Replace("円", "");
                }
                catch { }
                try
                {
                    textBox12.Text = date[0].Replace(",", "");
                }
                catch { }
                try
                {
                    textBox4.Text = isbnA[0].Replace(",", "");
                }
                catch { }
                button2_Click(sender, e);
                DateTime d = DateTime.Now;
                textBox18.Text = $"国立国会図書館書誌データ({d:yyyy年MM月dd日}現在)をもとに登録";
                //MessageBox.Show("ISBN変換が完了しました", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(sender, e);
            }
        }

        static async Task DownloadXmlFile(string url, string filePath)
        {
            using HttpClient httpClient = new HttpClient();
            // HTTP GETリクエストを送信し、応答を取得
            HttpResponseMessage response = await httpClient.GetAsync(url);

            // HTTPステータスコードが成功（200番台）であるか確認
            response.EnsureSuccessStatusCode();

            // XMLデータを取得
            string xmlData = await response.Content.ReadAsStringAsync();

            // XMLデータをファイルに保存
            File.WriteAllText(filePath, xmlData);
        }
    }
}
