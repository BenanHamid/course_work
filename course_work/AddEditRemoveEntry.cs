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
    public partial class AddEditRemoveEntry : Form
    {
        string filePath = string.Format("{0}/databases/{1}", AppDomain.CurrentDomain.BaseDirectory, "products_db.sql");
        public AddEditRemoveEntry()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddEditRemoveEntry_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Products.LoadUserListFromFile(filePath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = new BindingList<object>();
            // dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            // DataTable dtFromGrid = new DataTable();
            //dtFromGrid = dataGridView1.DataSource as DataTable;
            try
            {
                //BindingList<Products> bindingList = new BindingList<Products>();
                //dataGridView1.DataSource = bindingList;
                //bindingList.Remove(
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                //
            }
            catch
            {
                MessageBox.Show("err");
            }
            // dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            
        }
    }
}
