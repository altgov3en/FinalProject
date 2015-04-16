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
        private Dilatation dilatationFilter = new Dilatation();

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
            this.applicationForm.erodeTheImageToolStripMenuItem.Click += new System.EventHandler(this.erodeTheImageToolStripMenuItem_Click);
        }

        private void erodeTheImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenedImageData.openedImageDilatated = this.DilatateGrayscaledImage();
            this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageDilatated;
        }

        private void erodeImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenedImageData.openedImageEroded = this.ErodeGrayscaledImage();
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
                OpenedImageData.imageWasOpened = true; //indicates if user opened the image
            }

            return openedImage;
        }

        public void SaveImage(ImageData ImageView)
        {
            if (OpenedImageData.imageWasOpened)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (OpenedImageData.imageWasGrayscaled)
                        ImageView.openedImageGrayscaled.Save(saveDialog.FileName);
                    else
                        ImageView.openedImage.Save(saveDialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("There is no image to save.\n");
            }            

        }

        public Bitmap ConvertToGrayscale(Bitmap imageToConvert)
        {
            OpenedImageData.imageWasGrayscaled = true;
            return grayScaleFilter.Apply(imageToConvert);
        }
        
        public Bitmap ErodeGrayscaledImage() 
        {
            if (OpenedImageData.imageWasOpened)
            {
                if (OpenedImageData.imageWasGrayscaled)
                {
                    if (OpenedImageData.imageWasEroded)
                        return erosionFilter.Apply(OpenedImageData.openedImageEroded);
                    else
                    {
                        OpenedImageData.imageWasEroded = true;
                        return erosionFilter.Apply(OpenedImageData.openedImageGrayscaled);

                    }
                }
                else
                {
                    MessageBox.Show("Please, grayscale the image before applying the Dilatation");
                    return OpenedImageData.openedImage;
                }
            }
            else
            {
                MessageBox.Show("There is no image to Dilatate.");
                return null;
            }
        }
    
        public Bitmap DilatateGrayscaledImage()
        {
            if (OpenedImageData.imageWasOpened)
            {
                if (OpenedImageData.imageWasGrayscaled)
                {
                    if (OpenedImageData.imageWasDilated)
                        return dilatationFilter.Apply(OpenedImageData.openedImageDilatated);
                    else
                    {
                        OpenedImageData.imageWasDilated = true;
                        return dilatationFilter.Apply(OpenedImageData.openedImageGrayscaled);

                    }
                }
                else
                {
                    MessageBox.Show("Please, grayscale the image before applying the Erosion");
                    return OpenedImageData.openedImage;
                }
            }
            else
            {
                MessageBox.Show("There is no image to Erode.");
                return null;
            }
        }
    }
}
