using System.Data;
using System.Text;

namespace 総合図書管理システム新版
{
    public partial class _22statistics : Form
    {
        public _22statistics()
        {
            InitializeComponent();
        }
        DataTable dataTable = new DataTable();
        string document = MainClass.docment();

        private void Form22_Load(object sender, EventArgs e)
        {

        }

        private void dataTable_Reload(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart_set("分類番号", "冊数");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart_set("クラス", "冊数");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart_set("貸出者ID", "冊数");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            chart_set("蔵書ID", "回数");
        }

        private void chart_set(string type, string unit)
        {
            // CSVファイルからデータを読み取り、分類番号のカウントを行う
            Dictionary<string, int> classificationCounts = new Dictionary<string, int>();

            StreamReader sr = new StreamReader(document + "\\総合図書管理システム\\System\\data\\historydata.aescsv", Encoding.GetEncoding("shift_jis"));
            string csvrowdata = sr.ReadToEnd();
            sr.Close();
            string csvdata = MainClass.AESDe(csvrowdata);
            using (StringReader reader = new StringReader(csvdata))
            {
                string headerLine = reader.ReadLine(); // ヘッダー行を読み飛ばす
                while (reader.Peek() > -1)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(',');
                    if (parts.Length >= 5)
                    {
                        string classificationNumber = "";
                        if (type != "クラス")
                        {
                            classificationNumber = parts[FindIndex(headerLine, type)].Trim();
                        }
                        else
                        {
                            int a = int.Parse(parts[FindIndex(headerLine, "貸出者ID")].Trim()) / 100;
                            classificationNumber = a.ToString();
                        }
                        if (!classificationCounts.ContainsKey(classificationNumber))
                        {
                            classificationCounts[classificationNumber] = 0;
                        }
                        classificationCounts[classificationNumber]++;
                    }
                }
            }

            // ソートされたリストを作成
            List<KeyValuePair<string, int>> items = classificationCounts.ToList();
            items.Sort((x, y) => string.Compare(x.Key, y.Key, StringComparison.Ordinal));

            formsPlot1.Plot.Clear();
            var keyArray = items.SelectMany(dict => dict.Key).Distinct().ToArray();

            if (items.Count > 0)
            {
                string[]? keydata = new string[items.Count];
                int[] valuedata = new int[items.Count];
                double[]? length = new double[items.Count];
                int length1 = 0;
                foreach (var kvp in items)
                {
                    length1++;
                    keydata[length1 - 1] += kvp.Key;
                    valuedata[length1 - 1] += kvp.Value;
                    length[length1 - 1] += length1;
                }
                double[] doublevalue = Array.ConvertAll(valuedata, e => (double)e);
                formsPlot1.Plot.Title(type + "別 統計データ");
                formsPlot1.Plot.AddBar(doublevalue, length);
                formsPlot1.Plot.XTicks(length, keydata);
                formsPlot1.Plot.SetViewLimits(0, length.Max() + 1, 0, valuedata.Max());
                formsPlot1.Plot.XLabel("ID");
                formsPlot1.Plot.YLabel("数");
                formsPlot1.Refresh();
            }
        }

        static int FindIndex(string A, string B)
        {
            string[] elements = A.Split(',');
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == B)
                {
                    return i;
                }
            }
            return -1; // 見つからなかった場合
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG ファイル|*.png|すべてのファイル|*.*";
                sfd.Title = "グラフを保存";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // 選択されたファイルのパスを取得
                    string filePath = sfd.FileName;
                    // グラフを指定されたファイル形式で保存
                    formsPlot1.Plot.SaveFig(filePath);
                }
            }
        }
    }
}
