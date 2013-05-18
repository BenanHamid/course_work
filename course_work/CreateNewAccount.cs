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

namespace course_work
{
    public partial class CreateNewAccount : Form
    {

        // Само проверява за грешки
        ErrorProvider err = new ErrorProvider();

        public CreateNewAccount()
        {
            InitializeComponent();
        }

        private void CreateForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Започване на четене

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
            sw.WriteLine(textBox1.Text + "\t" + textBox2.Text);
            sw.Dispose();
            
            this.Close();
            //}

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
