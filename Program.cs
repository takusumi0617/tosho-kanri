namespace 総合図書管理システム新版
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }

    /// <summary>
    /// ダブルバッファリングを有効にしたDataGridViewコントロール
    /// </summary>
    public class DoubleBufferingDataGridView : System.Windows.Forms.DataGridView
    {
        public DoubleBufferingDataGridView()
        {
            this.DoubleBuffered = true;
        }
    }
}