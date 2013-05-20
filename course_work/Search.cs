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
            string searchValue = textBox1.Text;
            
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[2].Value.ToString().Equals(searchValue))
                        {
                            row.Selected = true;
                        }
                    }
            }
            catch 
            {
               
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