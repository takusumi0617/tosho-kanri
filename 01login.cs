namespace 総合図書管理システム新版
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form about = new _00about();
            about.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] result = c02login.certification(textBox2.Text, textBox3.Text);
            if (result.Contains("admin") && result.Contains("success"))
            {
                Form form2 = new _02Menu("admin");
                form2.ShowDialog();
            }
            else if (result.Contains("user") && result.Contains("success"))
            {
                Form form2 = new _02Menu("user");
                form2.ShowDialog();
            }
            else if (result.Contains("visitor") && result.Contains("success"))
            {
                Form form3 = new _03search(textBox2.Text);
                form3.ShowDialog();
            }
            else if (textBox2.Text == "setup")
            {
                Form form21 = new _21setup();
                form21.ShowDialog();
            }
            else
            {
                MessageBox.Show("ID又はパスワードが違います。", "認証失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            c01ft.dafcreate();
            string notice_path = $"{MainClass.docment()}\\総合図書管理システム\\System\\data\\notice.txt";
            if (c01ft.notice_display() == 1 && File.Exists(notice_path))
            {
                label4.Text = File.ReadAllText(notice_path);
            }
            else
            {
                label4.Text = await c01ft.SConnectAsync();
            }
        }
    }
}