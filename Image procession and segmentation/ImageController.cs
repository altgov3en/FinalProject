using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;

namespace Image_procession_and_segmentation
{

    public class ImageController
    {
        private Bitmap openedImage;
        private IFilter grayScaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
        private Erosion erosionFilter = new Erosion();


        public bool imageWasGrayscaled = false;
        public bool imageWasEroded = false;

        private MainWindow applicationForm;
        private ImageController OpenedImageController;
        private ImageData OpenedImageData;

        public ImageController(MainWindow applicationForm, ImageData OpenedImageData) //constructor
        {
            // TODO: Complete member initialization
            this.applicationForm = applicationForm;
            this.OpenedImageData = OpenedImageData;
            this.applicationForm.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            this.applicationForm.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            this.applicationForm.convertToGrayscaleToolStripMenuItem.Click += new System.EventHandler(this.convertToGrayscaleToolStripMenuItem_Click);
            this.applicationForm.erodeImageToolStripMenuItem.Click += new System.EventHandler(this.erodeImageToolStripMenuItem_Click);
        }

        private void erodeImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenedImageData.openedImageEroded = this.ErodeGrayscaledImage(OpenedImageData.openedImageGrayscaled);
            this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageEroded;
        }

        private void convertToGrayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenedImageData.openedImageGrayscaled = this.ConvertToGrayscale(OpenedImageData.openedImage);
            this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageGrayscaled;
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveImage(OpenedImageData);
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenedImageData.openedImage = this.OpenImage(); //Open user specified image for analysis
            this.applicationForm.pictureBox1.Image = OpenedImageData.openedImage; //Set opened image to main window   
        }

        public Bitmap OpenImage()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if(openDialog.ShowDialog() == DialogResult.OK)
            {
                openedImage = new Bitmap(openDialog.FileName);
            }

            return openedImage;
        }

        public void SaveImage(ImageData ImageView)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (imageWasGrayscaled)
                    ImageView.openedImageGrayscaled.Save(saveDialog.FileName);
                else
                    ImageView.openedImage.Save(saveDialog.FileName);
            }   

        }

        public Bitmap ConvertToGrayscale(Bitmap imageToConvert)
        {
            this.imageWasGrayscaled = true;
            return grayScaleFilter.Apply(imageToConvert);
        }
        
        public Bitmap ErodeGrayscaledImage(Bitmap imageToErode) 
        {
            
            return erosionFilter.Apply(imageToErode);
        }
    }
}
