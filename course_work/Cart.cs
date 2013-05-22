using System;
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
    public partial class Cart : Form
    {

        // Пътища към файловете с бази данни
        string filePath = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "products_db.sql");
        string filePathUsers = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "accounts_db.sql");

        public Cart()
        {
            InitializeComponent();
            LoadProducts();
        }

        public void LoadProducts()
        {

            productsDataGridView.DataSource = Products.LoadUserListFromFile(filePath);
        }

        // Loading bar
        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            progressBar1.Visible = true;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Cart_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void productsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void productsDataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
