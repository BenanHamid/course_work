﻿using System;
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
        //
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
                        || row.Cells[3].Value.ToString().Equals(searchValue))
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



        //Метод с който търся в момента не бачка
        /*public void Searcher()
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
                MessageBox.Show("Няма намерени данни !");
            }
            

        }*/

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

        private void button2_Click_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = productsBindingSource;
            textBox1.Text = "";
        }
    }
}
// test promeni