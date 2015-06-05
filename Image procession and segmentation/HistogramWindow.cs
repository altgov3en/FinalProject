using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_procession_and_segmentation
{
    public partial class HistogramWindow : Form
    {
        public HistogramWindow()
        {
            InitializeComponent();
        }
        int i;

        private void HistogramWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide(); 
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.textBox1.Clear();
            if (e.KeyChar != '2' && e.KeyChar != '3' && e.KeyChar != '4' && e.KeyChar != '5' && e.KeyChar != '6' && e.KeyChar != '7')
           {

                this.okButton.Enabled = false;
                this.okButton.Text = "Enter Number";
            }
            else
            {
                this.okButton.Enabled = true;
                this.okButton.Text = "Divide to " + e.KeyChar + " Segments";
            }

                
        }
    }
}
