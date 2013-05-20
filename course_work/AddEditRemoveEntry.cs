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
        public AddEditRemoveEntry(List<Products> pr)
        {
            InitializeComponent();
            productsBindingSource.DataSource = pr;
            dataGridView1.DataSource = productsBindingSource;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddEditRemoveEntry_Load(object sender, EventArgs e)
        {
            CenterLabels();
            //productsBindingSource.DataSource = Products.LoadUserListFromFile(filePath);
            //dataGridView1.DataSource = productsBindingSource;
        }
        public void CenterLabels()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void button1_Click(object sender, EventArgs e)
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

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {

        }
    }
}