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
        // Принтиране
        private System.Windows.Forms.Button printButton;
        private Font printFont;
        private StreamReader streamToPrint;

        // Пътища към файловете с бази данни
        string filePath = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "products_db.sql");
        string filePathUsers = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "accounts_db.sql");

        // ПРОСТОТИИТЕ НА ТАЯ:
        /***
        // За четене/запис
        FileStream fs;
        BinaryFormatter bf = new BinaryFormatter();
        ***/
        // Списъка от масиви, който съхванява продуктите

        // ПРОСТОТИИТЕ НА ТАЯ
        /***
        ArrayList ListProj = new ArrayList();
        ***/

        // ГЛАВНИЯТ КОНСТРУКТОР
        public Main()
        {
            InitializeComponent();
            //promotionsDataGridView.Columns[0].HeaderText = "First Column";
            //GenerateData();
            //ShowList();
            //Hope();
            //ReadFromFile();
            //Experimental();
            //Test();
        }

        // НЕЙНИЯ Метод, който генериране данни и ги добавя в DataGridView
        /***
        public void GenerateData()
        {
            // Генериране на тестови данни. 
            // Създава файла StudentProjects.dat

            if (!File.Exists(filePath))
            {
                int n = 2, gr = 57, days = 7;
                string mg = "Информатика",
                       name = "АБВГДЕЖЗИЙКЛМНОПРСТ";
                Random rndIndex = new Random();

                for (int i = 0; i < 40; i++)
                {
                    DateTime d = new DateTime(2013, 04, 10);
                    DateTime d2 = new DateTime();
                    if (i > 20 && i < 35)
                    { 
                        n = 3; 
                        gr = 58; 
                        mg = "Информатика";
                        d = d.AddDays(5); 
                        days = 10; 
                    }

                    if (i > 36)
                    { 
                        n = 4;
                        gr = 58; 
                        mg = "БИС"; 
                        d = d.AddDays(7); 
                        days = 14; 
                    }

                    if ((3000 + i * 2) % 4 == 0) 
                        d2 = d.AddDays(days);

                    // Добавяне на стойности към масива
                    ListProj.Add(new Products
                    {
                        Brand = "Test",
                        Category = "Category",
                        Description = gr.ToString(),
                        InventoryID = i,
                        Price = 2,
                        Promotions = true,
                        Quantity = 3
                    });

                }

                try
                {
                    using (fs = new FileStream(filePath, FileMode.Create)) //Гарантира затварянето на потока;
                        bf.Serialize(fs, ListProj);
                }

                catch
                {
                    MessageBox.Show("Грешка при запис във файл!", "Грешка!");
                }
            }
        }
        ***/

        // EXPERIMENTAL 1: Това не мога да го накарам да работи!
        /***
        public void ReadFromFile()
        {
            string delimeter = "\t";
            string tableName = "BooksTable";
            string fileName = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "bigtest.sql");

            DataSet dataset = new DataSet();
            StreamReader sr = new StreamReader(fileName);

            dataset.Tables.Add(tableName);
            dataset.Tables[tableName].Columns.Add("InventoryID");
            dataset.Tables[tableName].Columns.Add("Brand");
            dataset.Tables[tableName].Columns.Add("Category");
            dataset.Tables[tableName].Columns.Add("Description");
            dataset.Tables[tableName].Columns.Add("Promotions");
            dataset.Tables[tableName].Columns.Add("Quantity");
            dataset.Tables[tableName].Columns.Add("Price");

            string allData = sr.ReadToEnd();
            string[] rows = allData.Split("\r".ToCharArray());

            foreach (string r in rows)
            {
                string[] items = r.Split(delimeter.ToCharArray());
                dataset.Tables[tableName].Rows.Add(items);
            }
            //this.productsDataGridView.DataSource = dataset.Tables[0].DefaultView;
        }
        ***/

        // ЕXPERIMENTAL 2: И това не работи!
        /***
        public void Experimental()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Row No.");
            table.Columns.Add("Col No.");
            table.Columns.Add("Width");
            table.Columns.Add("Height");
            table.Columns.Add("Image URL");
            table.Columns.Add("Description");
            table.Columns.Add("Placeholder");

            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string[] parts = sr.ReadLine().Split('\t');
                    table.Rows.Add(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]);
                }
            }
            productsDataGridView.DataSource = table;
        }
        *****/

        // EXPERIMENTAL 3: Дано стане! Ама не стана!
        /***
        public void Test()
        {
            string[] textData = System.IO.File.ReadAllLines(filePath);
            string[] headers = textData[0].Split('\t');

            //Create and populate DataTable
            DataTable dataTable1 = new DataTable();

            foreach (string header in headers)
                dataTable1.Columns.Add(header, typeof(string), null);
            for (int i = 1; i < textData.Length; i++)
                dataTable1.Rows.Add(textData[i].Split('\t'));

            //Set the DataSource of DataGridView to the DataTable
            promotionsDataGridView.DataSource = dataTable1;
            //Form1.Controls.Add(productsGridView);
            //Form1.ShowDialog();
        }
        ***/

        // EXPERIMENTAL 4: A New Hope!
        /***
        private void Hope()
        {
            string rowValue;
            string[] cellValue;

            if (System.IO.File.Exists(filePath))
            {
                System.IO.StreamReader streamReader = new StreamReader(filePath);

                // Reading header
                rowValue = streamReader.ReadLine();
                cellValue = rowValue.Split(','); 
               
                for (int i = 0; i <= cellValue.Count() - 1; i++)
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.Name = cellValue[i];
                    column.HeaderText = cellValue[i];
                    productsDataGridView.Columns.Add(column);
                }

                // Reading content
                while (streamReader.Peek() != -1)
                {
                    rowValue = streamReader.ReadLine();
                    cellValue = rowValue.Split(',');
                    productsDataGridView.Rows.Add(cellValue);
                }

                streamReader.Close();

            }

            else
            {
                MessageBox.Show("No File is Selected");
            }
        }
        ***/

        // Това ПЪЛНИ DataGrid продукти
        /***
        public void ShowList()
        {

            try
            {
                if (File.Exists(filePath))
                {
                    using (fs = new FileStream(filePath, FileMode.Open))
                    ListProj = (ArrayList)bf.Deserialize(fs);
                }
            }

            catch
            {
                MessageBox.Show("Невъзможно е да се отвори файлът база данни, който ще се зареди в productsDataGridView!");
            }
            
            productsDataGridView.DataSource = ListProj;

        }
        ***/

        // -- Начало методите за принтиране -- //
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
        // -- Край на методите за принтиране -- // 

        // Показване на статистиките отдясно
        public void ShowStatistics()
        {
            // Общо продукти
            //label6.Text = List.Proj.Count.ToString();   // Общо продукти
            //label7.Text = List.Proj.Count.ToString();   // Общо промоции
            //label8.Text = List.Proj.Count.ToString();   // Средна цена на продукт
        }

        //****************************************************************************************************//
        //                                          НАЧАЛО НА GUI ФУНКЦИИ                                     //
        //****************************************************************************************************//

        // При зареждане на цялата форма
        private void Form1_Load(object sender, EventArgs e)
        {
            // Взима стойностите, прочетени от файла (от Products.cs) и ги зарежда в DataGridView
            productsDataGridView.DataSource = Products.LoadUserListFromFile(filePath);

            // Взима стойностите, прочетени от файла (от Products.cs) и ги зарежда в DataGridView na promotions
            promotionsDataGridView.DataSource = Promotions.LoadUserListFromFile(filePath);

            // Центриране на label-ите на двата DataGridView-a
            productsDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            promotionsDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            productsDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            promotionsDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Забраняване на поява на празен ред, който потребителя може да бута в дъното на DGV
            productsDataGridView.AllowUserToAddRows = false;
            promotionsDataGridView.AllowUserToAddRows = false;
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
        }

        // Help > About us: За нас
        private void заНасToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.ShowDialog(this);
        }

        private void свържетеСеСНасToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void promotionsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

    }
}