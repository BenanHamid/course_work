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
using course_work.Properties;

namespace course_work
{
    public partial class Login : Form
    {
        // Само проверява за грешки
        ErrorProvider err = new ErrorProvider();

        // Главен конструктор
        public Login()
        {
            InitializeComponent();
        }

        // Това е за Remember me?
        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Remember == true)
            {
                textBox1.Text = Settings.Default.Password;
                textBox2.Text = Settings.Default.Username;
                checkBox1.Checked = Settings.Default.Remember;
            }
        }

        // Бутон "ВЛЕЗ" - Тук са всички проверки за логина
        private void button1_Click(object sender, EventArgs e)
        {
            Login LoginForm = new Login();
            
            // Дефинираме потребителското име и паролата
            if (Properties.Settings.Default.Remember == true)
            {
                Settings.Default.Password = textBox1.Text;
                Settings.Default.Username = textBox2.Text;
                Settings.Default.Save();
            }

            // Търси в accounts_db.sql дали има такава комбинация от потребител и парола
            string filePath = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "accounts_db.sql");
            StreamReader tr = new StreamReader(filePath);
     
            // Това е същинската проверка дали има валидна комбинация от потребител и парола
            int flag = 0; // Флаг за съвпадение на въведени user/pass и намерени в DB user/pass
            while (tr.Peek() >= 0)
            {
                // Четене на потребител и хеширана парола от accounts_db.sql
                string tab = "\t";
                string user = tr.ReadLine();
                string getUser = user.Substring(0, user.IndexOf(tab));
                string getPass = user.Substring(user.IndexOf(tab) + 2, user.Length - user.IndexOf(tab) - 2);

                // Тук криптираме "чистата" парола, която е въведена в поле "Password" във SHA-512
                string passwordHolder = textBox2.Text; // Налага се да го помним в допълнителна променлива, защото иначе постоянно модифицираме съдържанито на password box-а ("дългата" хеширана парола се появява в password box-a)
                passwordHolder = EncryptSHA512(passwordHolder);

                // Проверяваме дали имаме съвпадение на въведената и хеширана току-що парола и потребител със потребител и хеш от accounts_db.sql
                if (getPass == passwordHolder && getUser == textBox1.Text)
                {
                    flag = 0;
                    err.SetError(textBox1, ""); // Чисти error-а
                    err.SetError(textBox2, "");
                    MessageBox.Show("Успешен вход в системата!", "Влизане в системата");
                    Main forma = new Main();
                    this.Hide();
                    forma.ShowDialog(this);
                    this.Close();
                }
            }

            // Грешна комбинация потребител и парола
            if(flag == 0)
            {
                err.SetError(textBox2, "Невалидна комбинация от потребителско име и парола!");
                MessageBox.Show("Невалидна комбинация от потребителско име и парола!", "Влизане в системата");
                Main forma = new Main();
            }
        }

        // Бутон "Remember Me"
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Settings.Default.Remember = true;
            }
            else
            {
                Settings.Default.Remember = false;
            }
            Settings.Default.Save();
        }

        // NOT USED: MD5
        //public static string MD5Hash(string text)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    //compute hash from the bytes of text
        //    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
        //    //get hash result after compute it
        //    byte[] result = md5.Hash;
        //    StringBuilder strBuilder = new StringBuilder();
        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        //change it into 2 hexadecimal digits
        //        //for each byte
        //        strBuilder.Append(result[i].ToString("x2"));
        //    }
        //
        //    return strBuilder.ToString();
        //}

        // NOT USED: Криптиране със SHA-512
        public static string GetCrypt(string text)
        {
            string hash = "";
            SHA512 alg = SHA512.Create();
            byte[] result = alg.ComputeHash(Encoding.UTF8.GetBytes(text));
            hash = Encoding.UTF8.GetString(result);
            return hash;
        }

        // Бутон за създаване на нов акаунт. Отваря CreateNewAccount.cs
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateNewAccount create = new CreateNewAccount();
            create.ShowDialog(this);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
         
    }
}