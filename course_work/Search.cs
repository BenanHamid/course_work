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
            CenterLabels();
            NotSelectable();
        }

        public void CenterLabels()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        //Метод с който търся
        public void SearchMe()
        {
            var results = new List<Products>();
            string searchValue = textBox1.Text;
            int flag = 1;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(searchValue) || row.Cells[2].Value.ToString().Equals(searchValue)
                        || row.Cells[4].Value.ToString().Equals(searchValue))
                    {
                        var item = row.DataBoundItem as Products;
                        results.Add(item);
                        flag = 0;
                        //textBox1.Text = "";
                    }
                }

            }
            catch
            {
                if (flag == 1)
                {
                    MessageBox.Show("Няма намерени данни");
                    textBox1.Text = "";
                }
            }

            dataGridView1.DataSource = results;
        }

        // Прави клетките Read-Only
        public void NotSelectable()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.ReadOnly = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Викам метода за търсенето
            SearchMe();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //Бутонз за изчистване
        private void button2_Click_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = productsBindingSource;
            textBox1.Text = "";
        }
    }
}
