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
    public partial class AboutUs : Form
    {
        public AboutUs()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void AboutUs_Load(object sender, EventArgs e)
        {
            Font font = new Font("Times New Roman", 13.0f);
            /*this.Font = font;
            label1.Font = new Font(label1.Font, label1.Font.Style | FontStyle.Bold | FontStyle.Underline);
            label2.Font = new Font(label1.Font, label2.Font.Style | FontStyle.Bold | FontStyle.Underline);*/
        }
    }
}
