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
            int flag = 0;
            while (tr.Peek() >= 0)
            {
                string tab = "\t";
                string user = tr.ReadLine();
                string getUser = user.Substring(0, user.IndexOf(tab));
                //MessageBox.Show(getUser);
                string getPass = user.Substring(user.IndexOf(tab) + 1, user.Length - user.IndexOf(tab) - 1);
                //MessageBox.Show(getPass);

                if (getPass == textBox2.Text && getUser == textBox1.Text)
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

        // Бутон "Направи нов акаунт"
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateNewAccount create = new CreateNewAccount();
            create.ShowDialog(this);
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
         
    }
}