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
        string filePath = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "accounts_db.sql"); // Взима ДИНАМИЧНО директорията на файла, т.е. е relative path, което е универсално за всяка машина, на която се изпълнява програмата

        // Инициализация - Файл
        public CreateNewAccount()
        {
            InitializeComponent();
        }

        // Инициализация - GUI
        private void CreateForm_Load(object sender, EventArgs e)
        {

        }

        // Бутон "Създаване": Създаване на нов акаунт - Тук е целият алгоритъм и проверки за създаването на нов акаунт
        private void button1_Click(object sender, EventArgs e)
        {
            // Първо проверяваме дали файла е празен - няма смисъл празен файл да се проверява за потребителите...
            CheckFileEmpty();

            // Ако файлът е празен
            if (CheckFileEmpty() == true)
                CreateNewUser();

            // Ако файлът не е празен
            else
            {
                // Проверяваме дали потребителя съществува във файла
                CheckUserExists();

                // Ако потребителя съществува: ГРЕШКА!
                if (CheckUserExists() == true)
                {
                    err.SetError(textBox1, ""); // Чисти error-а
                    err.SetError(textBox2, "");
                    MessageBox.Show("Потребителят вече съществува!", "Неуспешна регистрация");
                }

                // Ако потребителя не съществува: Създаваме го!
                else
                    CreateNewUser();
            }
        }

        // Връща дали файла е празен
        public bool CheckFileEmpty()
        {
            bool fileEmpty = false;
            if (new FileInfo(filePath).Length == 0)
                fileEmpty = true;
            return fileEmpty;
        }

        // Връща дали вече има такъв потребител
        public bool CheckUserExists()
        {
            bool UserExists = false;
            string filePath = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "accounts_db.sql");
            StreamReader sr = new StreamReader(filePath);

            // Търсим дали имаме такъв потребител в accounts_db.sql
            while (sr.Peek() >= 0)
            {
                string tab = "\t";
                string user = sr.ReadLine();
                string getUser = user.Substring(0, user.IndexOf(tab));

                // Съществува вече потребител с такова име
                if (getUser == textBox1.Text)
                {
                    sr.Dispose();
                    this.Close();
                    UserExists = true;
                    break;
                }
                else
                    // Стигнали сме до края на файла и не сме намерили такъв потребител
                    UserExists = false;
            }
            sr.Dispose(); // Освобождаваме четеца, а и самият файл (за да могат и други процеси и методи да го използват :))
            return UserExists;
        }

        // Метод, който създава нов потребител
        private void CreateNewUser()
        {
            // Писане с файлов поток
            FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            // Създаване на нов акаунт
            textBox2.Text = EncryptSHA512(textBox2.Text); // Рекурсивно криптиране с SHA-512, муафака
            sw.WriteLine(textBox1.Text + "\t" + "\t" + textBox2.Text);
            sw.Dispose();
            MessageBox.Show("Потребител " + textBox1.Text + " беше успешно създаден!", "Успешна регистрация!");
            this.Close();
        }

        // NOT USED: Криптиране със SHA-512
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

        // NOT USED:  Крипитане със SHA-512 v2
        public void Encryption2()
        {
            byte[] data = new byte[2];
            byte[] result;
            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);
        }

        // NOT USED:  SHA-512 v3
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
