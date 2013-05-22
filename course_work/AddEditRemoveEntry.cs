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
    public partial class AddEditRemoveEntry : Form
    {
        string filePath = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "products_db.sql");
        
        // Главен конструктор
        public AddEditRemoveEntry(List<Products> pr)
        {
            InitializeComponent();
            productsBindingSource.DataSource = pr;
            dataGridView1.DataSource = productsBindingSource;
        }

        // При зареждане на формата
        private void AddEditRemoveEntry_Load(object sender, EventArgs e)
        {
            CenterLabels();
           // Validator();

            //productsBindingSource.DataSource = Products.LoadUserListFromFile(filePath);
            //dataGridView1.DataSource = productsBindingSource;
        }

        //validator za kletkite proverqva dali ima ne6to v tqh i ne te puska napred ako ne go izpulni6
        public void Validator()
        {
              //this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            
        }

   
        
        // Центриране на labels
        public void CenterLabels()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        //  Бутон: Записване на промените във файла с базата данни
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TextWriter tw = new StreamWriter(filePath);
                for (int x = 0; x < dataGridView1.Rows.Count - 1; x++)
                {
                    for (int y = 0; y < dataGridView1.Columns.Count; y++)
                    {
                        tw.Write(dataGridView1.Rows[x].Cells[y].Value);
                        tw.Write('\t');
                    }
                    tw.WriteLine();
                }
                tw.Close();
                MessageBox.Show("Успешен запис на данните");
            }
            catch
            {
                MessageBox.Show("Неуспешен запис на данните");
            }
        }

        //****************************************************************************************************//
        //                                          ПРАЗНИ МЕТОДИ                                             //
        //****************************************************************************************************//
        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems_1(object sender, EventArgs e)
        {

        }
    }
}