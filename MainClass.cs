using Microsoft.Win32;
using System.Drawing.Printing;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace 総合図書管理システム新版
{
    internal class MainClass
    {
        private static readonly string Key = "w9jdrb23gd49czdktdfwxrdm2d3787z2";
        private static readonly string IV = "3833h5w93cu876hj";
        private static string dpathtemp = "";
        public static string data_url = "https://api.tosho-kanri.com/data.json";
        public static string docment()
        {
            if (dpathtemp != string.Empty)
            { return dpathtemp; }
            else
            {
                dpathtemp = docmentfromfile();
                return dpathtemp;
            }
        }

        public static string docmentfromfile()
        {
            if (RegistryKeyExists(Registry.CurrentUser, "Software\\TOKI\\tosho", "dpath"))
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\TOKI\\tosho"))
                {
                    return registryKey?.GetValue("dpath").ToString();
                }
            }
            else
            {
                Registry.CurrentUser.CreateSubKey("Software\\TOKI\\tosho");
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\TOKI\\tosho"))
                {
                    key.SetValue("dpath", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TOKI");
                }
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TOKI";
            }
        }

        public static bool RegistryKeyExists(RegistryKey rootKey, string path, string key)
        {
            using (RegistryKey registryKey = rootKey.OpenSubKey(path))
            {
                return registryKey?.GetValue(key) != null;
            }
        }

        public static int csv_creater(string path, string index, bool _lock)
        {
            try
            {
                if (_lock == true)
                {
                    StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("Shift_jis"));
                    writer.Write(AESEn(index));
                    writer.Close();
                }
                else
                {
                    StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("Shift_jis"));
                    writer.Write(index);
                    writer.Close();
                }
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public static string AESEn(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(IV);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText.Replace("\r\n", "\\r\\n").Replace("\r", "\\r").Replace("\n", "\\n"));
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string AESDe(string encryptedText)
        {
            try
            {
                string plain_text;
                // Base64文字列をバイト型配列に変換
                byte[] cipher = Convert.FromBase64String(encryptedText);
                // AESオブジェクトを作成
                using (Aes aes = Aes.Create())
                {
                    // 復号器を作成
                    using ICryptoTransform decryptor =
                        aes.CreateDecryptor(Encoding.UTF8.GetBytes(Key), Encoding.UTF8.GetBytes(IV));
                    // 復号用ストリームを作成
                    using MemoryStream in_stream = new(cipher);
                    // 一気に復号
                    using CryptoStream cs = new(in_stream, decryptor, CryptoStreamMode.Read);
                    using StreamReader sr = new(cs);
                    plain_text = sr.ReadToEnd();
                }
                return plain_text.Replace("\\r\\n", "\r\n");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static string RSADe(string text)
        {
            string textToDecrypt = text;
            string publickey = "51467858";
            string secretkey = "57312895";
            byte[] privatekeyByte = Encoding.UTF8.GetBytes(secretkey);
            byte[] publickeybyte = Encoding.UTF8.GetBytes(publickey);
            byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
            inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
            string decrypted = "";
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                decrypted = encoding.GetString(ms.ToArray());
            }
            return decrypted;
        }

        public static string RSAEn(string text)
        {
            try
            {
                string ToReturn = "";
                string publickey = "51467858";
                string secretkey = "57312895";
                byte[] secretkeyByte = Array.Empty<byte>();
                secretkeyByte = Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = Array.Empty<byte>();
                publickeybyte = Encoding.UTF8.GetBytes(publickey);
                MemoryStream? ms = null;
                CryptoStream? cs = null;
                byte[] inputbyteArray = Encoding.UTF8.GetBytes(text);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static string ExtractJsonData(string json, string key)
        {
            using JsonDocument document = JsonDocument.Parse(json);
            if (document.RootElement.TryGetProperty(key, out JsonElement value))
            {
                return value.GetString();
            }
            else
            {
                throw new ArgumentException($"Key '{key}' not found in JSON data.");
            }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

    public class Printer
    {
        private DataGridView dataGridView;
        private PrintDocument printDocument;
        private int rowIndex = 0;

        public Printer(DataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        public void Print()
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics? graphics = e.Graphics;
            Brush brush = new SolidBrush(dataGridView.ForeColor);
            Font font = dataGridView.Font;
            Font titleFont = new Font(dataGridView.Font.FontFamily, 20, FontStyle.Italic); // 大きなフォントサイズを指定
            int rowHeight = dataGridView.RowTemplate.Height;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            int cellPadding = 5;

            // タイトルを印刷
            string title = "延 滞 者 リ ス ト";
            SizeF titleSize = graphics.MeasureString(title, titleFont);
            float titleX = e.MarginBounds.Left + (e.MarginBounds.Width - titleSize.Width) / 2;
            graphics.DrawString(title, titleFont, brush, titleX, y);

            y += (int)titleSize.Height + cellPadding;

            // 列名を印刷
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                graphics.DrawString(dataGridView.Columns[i].HeaderText, font, brush, x, y);
                x += dataGridView.Columns[i].Width + cellPadding;
            }

            y += rowHeight;

            // DataGridViewの各行を印刷
            while (rowIndex < dataGridView.Rows.Count)
            {
                x = e.MarginBounds.Left;

                for (int col = 0; col < dataGridView.Columns.Count; col++)
                {
                    string? cellValue = dataGridView.Rows[rowIndex].Cells[col].FormattedValue.ToString();
                    graphics.DrawString(cellValue, font, brush, x, y);
                    x += dataGridView.Columns[col].Width + cellPadding;
                }

                y += rowHeight;
                rowIndex++;

                // ページの下端に到達した場合、追加のページがあることを指定
                if (y + rowHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            e.HasMorePages = false; // 追加のページはありません
        }
    }
}
