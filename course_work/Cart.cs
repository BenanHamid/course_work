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
        
        // Инициализация
        public Cart()
        {
            InitializeComponent();
        }

        // Зареждане на формата: Тук са методите, които реално се използват във формата
        private void Cart_Load(object sender, EventArgs e)
        {
            LoadProducts();
            ForbidEmptyBottomLine();
            CenterLabels();
            PromotionCalculate();
            FormatCurrencyCells();
        }

        // Зареждане на DGV
        public void LoadProducts()
        {
            buyDataGridView.DataSource = Products.LoadUserListFromFile(filePath);
        }

        // Забраняване на поява на празен ред, който потребителя може да бута в дъното на DGV
        public void ForbidEmptyBottomLine()
        {
            buyDataGridView.AllowUserToAddRows = false;
        }

        // Центриране на label-ите на двата DataGridView-a
        public void CenterLabels()
        {
            buyDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            buyDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        // Продуктите на промоция трябва да са с 10% остъпка
        public void PromotionCalculate()
        {
            // Отстъпка = 10% от цената
            for (int i = 0; i < buyDataGridView.Rows.Count; ++i)
            {
                if(Convert.ToInt32(buyDataGridView.Rows[i].Cells[3].Value) > 0 )
                    buyDataGridView.Rows[i].Cells[4].Value = (0.90 * Convert.ToDouble(buyDataGridView.Rows[i].Cells[4].Value));
            }
        }

        // Форматиране на колоните, където има цени да показват "00,00 лв" формат
        public void FormatCurrencyCells()
        {
            buyDataGridView.Columns[4].DefaultCellStyle.Format = "c";
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
