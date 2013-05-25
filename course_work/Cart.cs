using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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
        string filePathOrders = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "orders_db.sql");

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

        // Метод, който проверява дали потребителя е приел условията за поръчка
        public bool CheckUserAcceptTOS()
        {
            bool userAcceptTOS = false;
            if (checkBox1.Checked)
                userAcceptTOS = true;
            return userAcceptTOS;
        }

        // Изпращане на поръчка
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                // Проверяваме дали потребителя е приел условията за използване
                CheckUserAcceptTOS();
                if (CheckUserAcceptTOS() == false)
                {
                    MessageBox.Show("Не сте приели условията за обслужване!");
                }

                // Запазваме данните на потребителя
                string name = textBox1.Text;
                string family = textBox2.Text;
                string subjectType = Convert.ToString(comboBox1.Items[this.comboBox1.SelectedIndex]);
                string ID = textBox3.Text;
                string city = Convert.ToString(comboBox3.Items[this.comboBox3.SelectedIndex]);
                string address = textBox4.Text;
                string telephone = textBox5.Text;
                string paymentMethod = "";
                if (radioButton1.Checked)
                    paymentMethod = "Наложен платеж";
                if (radioButton2.Checked)
                    paymentMethod = "Банков път";

                // Записваме данние на потребителя във orders_db.sql
                TextWriter tw = new StreamWriter(filePathOrders, true);
                {
                    tw.Write(name);
                    tw.Write('\t');
                    tw.Write(family);
                    tw.Write('\t');
                    tw.Write(subjectType);
                    tw.Write('\t');
                    tw.Write(ID);
                    tw.Write('\t');
                    tw.Write(city);
                    tw.Write('\t');
                    tw.Write(address);
                    tw.Write('\t');
                    tw.Write(telephone);
                    tw.Write('\t');
                    tw.Write(paymentMethod);
                    tw.WriteLine();
                    tw.Close();
                }

                if (CheckUserAcceptTOS() == true)
                {
                    MessageBox.Show("Поръчката ви беше изпратена успешно!");
                }
            }

            catch
            {
                CheckUserAcceptTOS();
                if (CheckUserAcceptTOS() == true)
                MessageBox.Show("Неуспешен запис на данните");
            }

            // Анимация за зареждане. Не прави нищо, само краси
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            progressBar1.Visible = true;
        }

        //****************************************************************************************************//
        //                                          ПРАЗНИ МЕТОДИ                                             //
        //****************************************************************************************************//

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
