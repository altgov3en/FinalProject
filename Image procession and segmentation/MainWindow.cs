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
        public partial class MainWindow : Form
    {

        //Properties
        public ImageController ImageController 
        {
            get;

            internal set;
            
        }
        public ImageView OpenedImageView
        {
            get;

            internal set;

        }
        //////////////////////////////////////////////
        //////////////////////////////////////////////
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //File->"Open File"
        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenedImageView.openedImage = ImageController.OpenImage(); //Open user specified image for analysis
            pictureBox1.Image = OpenedImageView.openedImage; //Set opened image to main window   
           
        }

        //File->"Save File"
        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageController.SaveImage(OpenedImageView);
        }
       
        //Image Analysis Tools->"Convert To Grayscale"
        private void convertToGrayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenedImageView.openedImageGrayscaled = ImageController.ConvertToGrayscale(OpenedImageView.openedImage);
            pictureBox1.Image = OpenedImageView.openedImageGrayscaled;
        }

        private void imageAnalysisToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
