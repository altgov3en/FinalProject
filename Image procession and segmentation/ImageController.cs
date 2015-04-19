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
        private IFilter grayScaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
        private Erosion erosionFilter = new Erosion();
        private Dilatation dilatationFilter = new Dilatation();
        private Sharpen sharpeningFilter = new Sharpen();

        private MainWindow applicationForm;
        private ImageData OpenedImageData;

        public ImageController(MainWindow applicationForm, ImageData OpenedImageData) //Constructor
        {
            // TODO: Complete member initialization
            this.applicationForm = applicationForm;
            this.OpenedImageData = OpenedImageData;
            this.applicationForm.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            this.applicationForm.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            this.applicationForm.convertToGrayscaleToolStripMenuItem.Click += new System.EventHandler(this.convertToGrayscaleToolStripMenuItem_Click);
            this.applicationForm.erodeImageToolStripMenuItem.Click += new System.EventHandler(this.erodeImageToolStripMenuItem_Click);
            this.applicationForm.erodeTheImageToolStripMenuItem.Click += new System.EventHandler(this.erodeTheImageToolStripMenuItem_Click);
            this.applicationForm.sharpenTheImageToolStripMenuItem.Click += new System.EventHandler(this.sharpenTheImageToolStripMenuItem_Click);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region "UI Click" handlers
        //"Image->Open Image" event
        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenedImageData.openedImage = this.OpenImage(); //Open user specified image for analysis
            this.applicationForm.pictureBox1.Image = OpenedImageData.openedImage; //Set opened image to main window   
        }

        //"Image->Save Image" event
        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveImage(OpenedImageData);
        }

        //"Image Analysis Tools->Convert To Grayscale" event
        private void convertToGrayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ConvertToGrayscale(OpenedImageData.openedImage);
            this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageGrayscaled;
        }

        //"Image Analysis Tools->Erode the Image" event
        private void erodeTheImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DilatateGrayscaledImage();
            this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageDilatated;
        }

        //"Image Analysis Tools->Dilatate the Image" event
        private void erodeImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ErodeGrayscaledImage();
            this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageEroded;
        }
        private void sharpenTheImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SharpenGrayscaledImage();
            this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageSharpened;
        }

        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Controller functions
        public Bitmap OpenImage()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                OpenedImageData.openedImage = new Bitmap(openDialog.FileName);
                OpenedImageData.imageWasOpened = true; //indicates if user opened the image
            }

            return OpenedImageData.openedImage;
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
                MessageBox.Show("There is no image to Save.\n");
            }

        }
        public void ConvertToGrayscale(Bitmap imageToConvert)
        {
            if (OpenedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                OpenedImageData.imageWasGrayscaled = true;
                OpenedImageData.openedImageGrayscaled = grayScaleFilter.Apply(imageToConvert);

                //image statistics
                OpenedImageData.openedImageStatistics = new AForge.Imaging.ImageStatistics(OpenedImageData.openedImageGrayscaled);
            }
            else
            {
                MessageBox.Show("There is no image to Grayscale.");
            }
        }
        public void ErodeGrayscaledImage()
        {
            if (OpenedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                if (OpenedImageData.imageWasGrayscaled) //checks if the opened image was grayscaled
                {
                    if (OpenedImageData.imageWasEroded)//checks if the grayscaled image was already eroded
                        //not first time erosion will erode the already eroded image
                        OpenedImageData.openedImageEroded = erosionFilter.Apply(OpenedImageData.openedImageEroded);
                    else
                    {
                        //first time erosion will erode the grayscaled image 
                        OpenedImageData.imageWasEroded = true;
                        OpenedImageData.openedImageEroded = erosionFilter.Apply(OpenedImageData.openedImageGrayscaled);

                    }
                }
                else
                    MessageBox.Show("Please, grayscale the image before applying the Dilatation");
            }
            else
                MessageBox.Show("There is no image to Dilatate.");
  
        }
        public void DilatateGrayscaledImage()
        {
            if (OpenedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                if (OpenedImageData.imageWasGrayscaled) //checks if the opened image was grayscaled
                {
                    if (OpenedImageData.imageWasDilatated) //checks if the grayscaled image was already dilatated
                        //not first time dilatation will dilatate the already dilatated image
                        OpenedImageData.openedImageDilatated = dilatationFilter.Apply(OpenedImageData.openedImageDilatated); 
                    else
                    {
                        //first time dilatation will dilatate the grayscaled image                
                        OpenedImageData.imageWasDilatated = true;
                        OpenedImageData.openedImageDilatated = dilatationFilter.Apply(OpenedImageData.openedImageGrayscaled);

                    }
                }
                else
                    MessageBox.Show("Please, grayscale the image before applying the Erosion");
            }
            else
                MessageBox.Show("There is no image to Erode.");
        }

        private void SharpenGrayscaledImage()
        {
            if (OpenedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                if (OpenedImageData.imageWasGrayscaled) //checks if the opened image was grayscaled
                {
                    if (OpenedImageData.imageWasSharpened) //checks if the grayscaled image was already sharpened
                        MessageBox.Show("This image is already sharpened.");
                    else
                    {
                        //first time dilatation will dilatate the grayscaled image                
                        OpenedImageData.imageWasSharpened = true;
                        OpenedImageData.openedImageSharpened = sharpeningFilter.Apply(OpenedImageData.openedImageGrayscaled);
                    }

                }
                else
                    MessageBox.Show("Please, grayscale the image before applying the Sharpening");
            }
            else
                MessageBox.Show("There is no image to Sharp.");
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
