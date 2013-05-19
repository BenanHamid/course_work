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
    public partial class Search : Form
    {
        public Search(List<Products> pr)
        {
            InitializeComponent();
            productsBindingSource.DataSource = pr;
            dataGridView1.DataSource = productsBindingSource;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Search_Load(object sender, EventArgs e)
        {

        }
        //Метод с който търся
        public void Searcher()
        {
            string lookFor;
            lookFor = textBox1.Text;
            int flag = 1, i = 0, y = 0;

            for (i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                for (y = 0; y < dataGridView1.Columns.Count; y++)
                {
                    if (lookFor == dataGridView1.Rows[i].Cells[y].Value.ToString())
                    {
                        MessageBox.Show(dataGridView1.Rows[i].Cells[y].Value.ToString());
                    }

                }

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Викам метода за търсенето
            Searcher();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
// test promeni