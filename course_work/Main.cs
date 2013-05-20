using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Drawing.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace course_work
{

    public partial class Main : Form
    {

        // --- НАЧАЛО ОБЩИ ДЕФИНИЦИ --- //
        // За принтиране
        private System.Windows.Forms.Button printButton;
        private Font printFont;
        private StreamReader streamToPrint;

        // Пътища към файловете с бази данни
        string filePath = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "products_db.sql");
        string filePathUsers = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "accounts_db.sql");
        // --- КРАЙ ОБЩИ ДЕФИНИЦИ --- //

        // Най-главният конструктор на цялата форма, който зарежда цялата Вселена
        public Main()
        {
            InitializeComponent();
        }

        // --- НАЧАЛО НА МЕТОДИТЕ ЗА ПРИНТИРАНЕ --- //
        // Метод за принтиране - страницата
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            string line = null;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height /
            printFont.GetHeight(ev.Graphics);

            // Print each line of the file. 
            while (count < linesPerPage &&
                  ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count *
                printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page. 
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }

        // Метод за принтиране - още
        private void InitializeComponents()
        {
            this.components = new System.ComponentModel.Container();
            this.printButton = new System.Windows.Forms.Button();

            this.ClientSize = new System.Drawing.Size(504, 381);
            this.Text = "Print Example";

            printButton.ImageAlign =
            System.Drawing.ContentAlignment.MiddleLeft;
            printButton.Location = new System.Drawing.Point(32, 110);
            printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            printButton.TabIndex = 0;
            printButton.Text = "Print the file.";
            printButton.Size = new System.Drawing.Size(136, 40);
            printButton.Click += new System.EventHandler(принтиранеToolStripMenuItem_Click);

            this.Controls.Add(printButton);
        }
        /// --- КРАЙ НА МЕТОДИТЕ ЗА ПРИНТИРАНЕ --- //

        // Показване на статистиките отдясно
        public void LoadStatistics()
        {
            // Общ брой продукти
            int totalProductsAmount = productsDataGridView.Rows.Count; // Много яка опция, която ти дава директно броя на редовете
            label6.Text = Convert.ToString(totalProductsAmount) + " бр";

            // Общ брой промоции
            int totalPromotionsAmount = 0;
            for (int i = 0; i < productsDataGridView.Rows.Count; ++i) // Това е цикъл дето обикаля целия productsDataGridView и гледа дали в колонка промоция (т.е. 4) има >0
            {
                if ( Convert.ToInt32(productsDataGridView.Rows[i].Cells[4].Value) > 0)
                    totalPromotionsAmount++;
            }
            label7.Text = Convert.ToString(totalPromotionsAmount) + " бр";

            // Средна цена на всички продукти
            int averageProductPrice = 0;
            for (int i = 0; i < productsDataGridView.Rows.Count; ++i)
            {
                averageProductPrice += Convert.ToInt32(productsDataGridView.Rows[i].Cells[6].Value);
            }
            averageProductPrice = (averageProductPrice / totalProductsAmount);
            label8.Text = averageProductPrice.ToString() + " лв";

            // Средна цена на всички продукти на промоция
            int averagePromotionPrice = 0;
            for (int i = 0; i < productsDataGridView.Rows.Count; ++i)
            {
                // Събира цените само на промо-продуктите
                if (Convert.ToInt32(productsDataGridView.Rows[i].Cells[4].Value) > 0)
                    averagePromotionPrice += Convert.ToInt32(productsDataGridView.Rows[i].Cells[6].Value);
            }
            averagePromotionPrice = (averagePromotionPrice / totalPromotionsAmount);
            label10.Text = averagePromotionPrice.ToString() + " лв";
        }

        // Взима стойностите, прочетени от файла (от Products.cs) и ги зарежда в DataGridView
        public void LoadProducts()
        {
            
            productsDataGridView.DataSource = Products.LoadUserListFromFile(filePath);
        }

        // Взима стойностите, прочетени от файла (от Products.cs) и ги зарежда в DataGridView na promotions
        public void LoadPromotions()
        {   
            promotionsDataGridView.DataSource = Promotions.LoadUserListFromFile(filePath);
        }

        // Центриране на label-ите на двата DataGridView-a
        public void CenterLabels()
        {
            productsDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            promotionsDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            productsDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            promotionsDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        // Забраняване на поява на празен ред, който потребителя може да бута в дъното на DGV
        public void ForbidEmptyBottomLine()
        {
            productsDataGridView.AllowUserToAddRows = false;
            promotionsDataGridView.AllowUserToAddRows = false;
        }

        // Форматиране на колоните, където има цени да показват "00,00 лв" формат
        public void FormatCurrencyCells()
        {
            productsDataGridView.Columns[6].DefaultCellStyle.Format = "c";
            promotionsDataGridView.Columns[4].DefaultCellStyle.Format = "c";
            promotionsDataGridView.Columns[5].DefaultCellStyle.Format = "c";
            promotionsDataGridView.Columns[6].DefaultCellStyle.Format = "c";
        }

        // Наследяване от productsDataGridView към promotionsDataGridView чрез филтриране само на промоциите
        public void FilterPromotions()
        {
            var results = new List<Products>();

            foreach (DataGridViewRow row in productsDataGridView.Rows)
            {
                if (Convert.ToInt32(row.Cells[4].Value) > 0)
                {
                    var item = row.DataBoundItem as Products;
                    results.Add(item);
                }
            }

            promotionsDataGridView.DataSource = results;
        }

        // Калкулиране на промоциите в promotionsDataGridView:
        public void PromotionCalculate()
        {
            // Отстъпка = 10% от цената
            for (int i = 0; i < productsDataGridView.Rows.Count; ++i)
            {
                promotionsDataGridView.Rows[i].Cells[5].Value = ( 0.10 * Convert.ToDouble(promotionsDataGridView.Rows[i].Cells[4].Value) );
            }

            // Промоционална цена = Цена - Остъпка
            for (int i = 0; i < productsDataGridView.Rows.Count; ++i) 
            {
                promotionsDataGridView.Rows[i].Cells[6].Value = ( Convert.ToDouble(promotionsDataGridView.Rows[i].Cells[4].Value) - 
                                                                  Convert.ToDouble(promotionsDataGridView.Rows[i].Cells[5].Value) );
            }
        }

        //****************************************************************************************************//
        //                                          НАЧАЛО НА GUI ФУНКЦИИ                                     //
        //****************************************************************************************************//
        
        // Методи, които се зареждат при зареждане на цялата форма
        private void Main_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadPromotions();
            LoadStatistics();
            CenterLabels();
            FormatCurrencyCells();
            ForbidEmptyBottomLine();
            FilterPromotions();
            //PromotionCalculate();
        }

        // File > Print: Метод за принтиране
        private void принтиранеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                streamToPrint = new StreamReader(filePath);

                try
                {
                    printFont = new Font("Arial", 10);
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                    pd.Print();
                }

                finally
                {
                    streamToPrint.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // File > Exit: Затваряне на програмата при клик на "Изход"
        private void затвориToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Edit: Редактиране, добавяне, изтриване на данни
        private void редактиранеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Products> pr = (List<Products>)productsDataGridView.DataSource;
            AddEditRemoveEntry editAll = new AddEditRemoveEntry(pr);
            editAll.ShowDialog(this);

            productsDataGridView.DataSource = null;
            productsDataGridView.DataSource = pr;

            LoadStatistics();
        }

        // Help > About us: За нас
        private void заНасToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.ShowDialog(this);
        }

        private void свържетеСеСНасToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //To be continued in afterlife
        }

        private void productsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void productsDataGridView_RowDividerHeightChanged(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сортирайПоToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void promotionsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void помощToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void търсиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            List<Products> pr = (List<Products>)productsDataGridView.DataSource;
            Search searchIt = new Search(pr);
            searchIt.ShowDialog(this);
            productsDataGridView.DataSource = null;
            productsDataGridView.DataSource = pr;
        }

    }
}