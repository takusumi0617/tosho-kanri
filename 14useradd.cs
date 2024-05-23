using System.Text;

namespace 総合図書管理システム新版
{
    public partial class _14useradd : Form
    {
        public _14useradd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (checkBox1.Checked == true)
            {
                textBox2.Text = "0";
                textBox3.Text = "0";
                textBox4.Text = textBox1.Text;
            }
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox1.Text != "")
            {
                string Age = "0";
                string Class = "0";
                string Number = "0";
                string pernum = "4";
                string pernam = "その他";
                if (checkBox1.Checked == true)
                {
                    Age = "0";
                    Class = "0";
                    Number = textBox1.Text;
                }
                else
                {
                    Age = textBox2.Text;
                    Class = textBox3.Text;
                    Number = textBox4.Text;
                }
                if (comboBox1.Text == "1.一般、生徒ユーザー") { pernum = "1"; pernam = "一般、生徒ユーザー"; }
                else if (comboBox1.Text == "2.職員ユーザー") { pernum = "2"; pernam = "職員ユーザー"; }
                else if (comboBox1.Text == "3.管理者ユーザー") { pernum = "3"; pernam = "管理者ユーザー"; }
                else { pernum = "4"; pernam = "その他"; }
                string data = textBox1.Text + "," + Age + "," + Class + "," + Number + "," + textBox5.Text + "," + pernum + "," + pernam + ",";
                string path = MainClass.docment() + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv";
                StreamReader reader = new StreamReader(path, Encoding.GetEncoding("shift_jis"));
                string Endata = reader.ReadToEnd();
                reader.Close();
                string Dedata = MainClass.AESDe(Endata);
                Dedata += "\r\n" + data;
                StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("shift_jis"));
                writer.Write(MainClass.AESEn(Dedata));
                writer.Close();
                MessageBox.Show("以下の利用者を保存しました。\r\n氏名:" + textBox5.Text, "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                comboBox1.Text = "1.一般、生徒ユーザー";
            }
            else
            {
                MessageBox.Show("すべての項目を入力してください。", "登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = MainClass.docment() + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv";
            if (File.Exists(filePath))
            {
                string lines1 = File.ReadAllText(filePath);
                string[] lines = MainClass.AESDe(lines1).Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                if (lines.Length > 0)
                {
                    string lastLine = lines[lines.Length - 1];
                    string[] columns = lastLine.Split(',');
                    if (columns.Length > 0 && int.TryParse(columns[0], out int value))
                    {
                        int result = value + 1;
                        textBox1.Text = result.ToString().PadLeft(6, '0');
                    }
                    else
                    {
                        MessageBox.Show("データから読み取ることができませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    int result = 1;
                    textBox1.Text = result.ToString().PadLeft(6, '0');
                }
            }
            else
            {
                MessageBox.Show("利用者データベースが所定のフォルダに存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "") { textBox2.Text = "00"; }
            if (textBox3.Text == "") { textBox3.Text = "00"; }
            if (textBox4.Text == "") { textBox4.Text = "00"; }
            if (int.TryParse(textBox2.Text, out _))
            {
                if (int.TryParse(textBox3.Text, out _))
                {
                    if (int.TryParse(textBox4.Text, out _))
                    {
                        int currentYear = DateTime.Now.Year;
                        int currentFiscalYear;
                        if (DateTime.Now.Month >= 4)
                        {
                            currentFiscalYear = (currentYear % 100) - (int.Parse(textBox2.Text) - 1);
                        }
                        else
                        {
                            currentFiscalYear = ((currentYear - 1) % 100) - (int.Parse(textBox2.Text) - 1);
                        }
                        string number = currentFiscalYear.ToString() + textBox3.Text.PadLeft(2, '0') + textBox4.Text.PadLeft(2, '0');
                        textBox1.Text = number;
                    }
                    else
                    {
                        MessageBox.Show("番号に数字以外の文字が含まれています。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("クラスに数字以外の文字が含まれています。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("学年に数字以外の文字が含まれています。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string inputFile = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "ファイルを選択してください";
            openFileDialog.Filter = "CSVファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputFile = openFileDialog.FileName;
            }

            string outputFile = MainClass.docment() + "\\総合図書管理システム\\利用者データ\\masterdata.aescsv";
            List<Student> students = ReadCsv(inputFile);
            if (students != null)
            {
                AddDataToCsv(outputFile, students);
                MessageBox.Show("データが正常に追加されました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        static List<Student> ReadCsv(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                List<Student> students = new List<Student>();
                using (var reader = new StreamReader(filePath, Encoding.GetEncoding("Shift_jis")))
                {
                    string line;
                    bool isFirstLine = true;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue;
                        }

                        string[] fields = line.Split(',');
                        int grade = int.Parse(fields[0]);
                        int @class = int.Parse(fields[1]);
                        int number = int.Parse(fields[2]);
                        string name = fields[3];
                        int currentYear = DateTime.Now.Year;
                        int currentFiscalYear;
                        if (DateTime.Now.Month >= 4)
                        {
                            currentFiscalYear = (currentYear % 100) - grade + 1;
                        }
                        else
                        {
                            currentFiscalYear = ((currentYear - 1) % 100) - grade + 1;
                        }
                        int studentNumber = currentFiscalYear * 10000 + @class * 100 + number;

                        students.Add(new Student
                        {
                            StudentNumber = studentNumber,
                            Grade = grade,
                            Class = @class,
                            Number = number,
                            Name = name
                        });
                    }
                }

                return students;
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        static void AddDataToCsv(string filePath, List<Student> students)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("shift_jis"));
                string Endata = reader.ReadToEnd();
                reader.Close();
                string Dedata = MainClass.AESDe(Endata);
                foreach (var student in students)
                {
                    Dedata += $"\r\n{student.StudentNumber},{student.Grade},{student.Class},{student.Number},{student.Name},1,一般、生徒ユーザー,";
                }
                StreamWriter writer = new StreamWriter(filePath, false, Encoding.GetEncoding("shift_jis"));
                writer.Write(MainClass.AESEn(Dedata));
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("データベースの変更中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存場所を選択してください";
            saveFileDialog.InitialDirectory = MainClass.docment();
            saveFileDialog.Filter = "CSVファイル|*.csv|すべてのファイル|*.*";
            saveFileDialog.FileName = "template.csv";
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string selectedPath = saveFileDialog.FileName;
                StreamWriter writer = new StreamWriter(selectedPath, false, Encoding.GetEncoding("Shift_jis"));
                writer.Write("学年,クラス,番号,氏名");
                writer.Close();
            }
        }
    }
    class Student
    {
        public int StudentNumber { get; set; }
        public int Grade { get; set; }
        public int Class { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
    }
}
