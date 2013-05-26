using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace course_work
{
    public partial class CreateNewAccount : Form
    {
        // Само проверява за грешки
        ErrorProvider err = new ErrorProvider();

        // Инициализация - Файл
        public CreateNewAccount()
        {
            InitializeComponent();
        }

        // Инициализация - GUI
        private void CreateForm_Load(object sender, EventArgs e)
        {

        }

        // Създаване на нов акаунт
        private void button1_Click(object sender, EventArgs e)
        {
            //if (!File.Exists("account_db.sql"))
            //{
            string filePath = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "accounts_db.sql");
            //System.IO.TextWriter tw = new System.IO.StreamWriter("accounts_db.sql");
            //tw.WriteLine(textBox1.Text + "\t" + textBox2.Text);
            //tw.WriteLine();
            //tw.Close();
            //tw.Dispose();
            FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            textBox2.Text = EncryptSHA512(textBox2.Text); // Рекурсивно криптиране с SHA-512, муафака
            sw.WriteLine(textBox1.Text + "\t" + textBox2.Text);
            sw.Dispose();
            this.Close();
            //}
        }

        // Криптиране със SHA-512
        public void Encryption1()
        {
            string encryptedPassword = textBox2.Text;
            SHA512 alg = SHA512.Create();
            //encryptedPassword = alg.ComputeHash();
            //byte[] result = alg.ComputeHash(Encoding.UTF8.GetBytes(textBox2.Text));
            //encryptedPassword = Encoding.Unicode.GetString(result);
            textBox2.Text = encryptedPassword;
            MessageBox.Show(encryptedPassword);
        }

        // Крипитане със SHA-512 v2
        public void Encryption2()
        {
            byte[] data = new byte[2];
            byte[] result;
            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);
        }

        // SHA-512 v3
        public static string GetSHA512(string path)
        {
            byte[] HashValue, MessageBytes = File.ReadAllBytes(path);
            SHA512Managed SHhash = new SHA512Managed();
            string strHex = "";

            HashValue = SHhash.ComputeHash(MessageBytes);
            foreach (byte b in HashValue)
            {
                strHex += String.Format("{0:x2}", b);
            }
            return strHex;
        }

        // Криптиране на string със алгоритъм SHA-512 bit
        public static string EncryptSHA512(string unencryptedString)
        {
            return BitConverter.ToString(new SHA512CryptoServiceProvider().
                                         ComputeHash(Encoding.Default.GetBytes(unencryptedString))).
                                         Replace("-", String.Empty).ToUpper();
        }

        //****************************************************************************************************//
        //                                          ПРАЗНИ МЕТОДИ                                             //
        //****************************************************************************************************//

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
