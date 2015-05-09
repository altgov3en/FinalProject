using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        private Histogram imageHistogram;
        private Clusters imageClusters;

        private MainWindow applicationForm;
        private ImageData OpenedImageData;
        private HistogramWindow grayscaleHistogram;

        public ImageController(MainWindow applicationForm, HistogramWindow grayscaleHistogram, ImageData OpenedImageData) //Constructor
        {
            // TODO: Complete member initialization
            this.applicationForm = applicationForm;
            this.grayscaleHistogram = grayscaleHistogram;
            this.OpenedImageData = OpenedImageData;
            this.applicationForm.Load += new System.EventHandler(this.Form1_Load);
            this.applicationForm.imageAnalysisToolsToolStripMenuItem.Click += new System.EventHandler(this.imageAnalysisToolsToolStripMenuItem_Click);
            this.applicationForm.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            this.applicationForm.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            this.applicationForm.convertToGrayscaleToolStripMenuItem.Click += new System.EventHandler(this.convertToGrayscaleToolStripMenuItem_Click);
            this.applicationForm.erodeTheImageToolStripMenuItem.Click += new System.EventHandler(this.DilatateTheImageToolStripMenuItem_Click);
            this.applicationForm.sharpenTheImageToolStripMenuItem.Click += new System.EventHandler(this.sharpenTheImageToolStripMenuItem_Click);
            this.applicationForm.button1.Click += new System.EventHandler(this.button1_Click);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region "UI Click" handlers

        private void imageAnalysisToolsToolStripMenuItem_Click(object sender, EventArgs e)
        { }

        private void Form1_Load(object sender, EventArgs e)
        { }

        //"Image->Open Image" event
        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenImage(); //Open user specified image for analysis  
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
        }

        //"Image Analysis Tools->Erode the Image" event
        private void DilatateTheImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ErodeGrayscaledImage();
        }

        private void sharpenTheImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SharpenGrayscaledImage();
        }

        //Click->"Show Image Histogram"
        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if(this.OpenedImageData.imageWasSharpened)
                this.grayscaleHistogram.pictureBox1.Image = imageHistogram.CalculateHistogram(this.OpenedImageData.openedImageSharpened); //create and put the histogram of the grayscaled image to picture box
            else
                if (this.OpenedImageData.imageWasEroded)
                    this.grayscaleHistogram.pictureBox1.Image = imageHistogram.CalculateHistogram(this.OpenedImageData.openedImageEroded);
                else
                    if (this.OpenedImageData.imageWasGrayscaled)
                        this.grayscaleHistogram.pictureBox1.Image = imageHistogram.CalculateHistogram(this.OpenedImageData.openedImageGrayscaled);


            grayscaleHistogram.Show(); // show histogram window
            //applicationForm.button1.Visible = false; //make button invisible as histogram is created (change ref. to close button)
            Cursor.Current = Cursors.Arrow;
        }

        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Controller functions
        public void OpenImage()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                OpenedImageData.openedImage = new Bitmap(openDialog.FileName);
                OpenedImageData.imageWasOpened = true; //indicates if user opened the image
                this.applicationForm.pictureBox1.Image = this.OpenedImageData.openedImage; //Set opened image to main window 
            }
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
            Cursor.Current = Cursors.WaitCursor;
            if (OpenedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                OpenedImageData.imageWasGrayscaled = true;
                OpenedImageData.openedImageGrayscaled = grayScaleFilter.Apply(imageToConvert);
                this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageGrayscaled;
                this.imageHistogram = new Histogram(OpenedImageData.openedImageGrayscaled); //create histogram for grayscaled image histogram
                applicationForm.button1.Visible = true; //"Show Image Histogram" button is anabled
                                                        //This image will appear only when the image is grayscaled 
            }
            else
                MessageBox.Show("There is no image to Grayscale.");
            Cursor.Current = Cursors.Arrow;
        }
        public void ErodeGrayscaledImage()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (OpenedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                if (OpenedImageData.imageWasGrayscaled) //checks if the opened image was grayscaled
                {
                    if (OpenedImageData.imageWasEroded) //checks if the grayscaled image was already dilatated
                    //not first time dilatation will dilatate the already dilatated image
                    {
                        this.OpenedImageData.openedImageEroded = this.dilatationFilter.Apply(this.OpenedImageData.openedImageEroded);
                        this.applicationForm.pictureBox1.Image = this.OpenedImageData.openedImageEroded;                       
                    }
                    else
                    {
                        //first time dilatation will dilatate the grayscaled image                
                        OpenedImageData.imageWasEroded = true;
                        this.OpenedImageData.openedImageEroded = this.dilatationFilter.Apply(this.OpenedImageData.openedImageGrayscaled);
                        this.applicationForm.pictureBox1.Image = this.OpenedImageData.openedImageEroded; ;
                    }
                }
                else
                {
                    MessageBox.Show("Please, grayscale the image before applying the Erosion");
                    applicationForm.pictureBox1.Image = OpenedImageData.openedImage;
                }
            }
            else
                MessageBox.Show("There is no image to Erode.");
            Cursor.Current = Cursors.Arrow;
        }
        private void SharpenGrayscaledImage()
        {
            Cursor.Current = Cursors.WaitCursor;
            //this.imageClusters = new Clusters(2, OpenedImageData.openedImage.Height, OpenedImageData.openedImage.Width,
            //                                      this.imageHistogram, this.OpenedImageData.openedImageSharpened);

            if (OpenedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                if (OpenedImageData.imageWasGrayscaled) //checks if the opened image was grayscaled
                {
                    if (OpenedImageData.imageWasSharpened) //checks if the grayscaled image was already sharpened
                    {
                        OpenedImageData.openedImageSharpened = sharpeningFilter.Apply(OpenedImageData.openedImageSharpened);
                        this.imageHistogram.openedImageGraysledSharpened = this.OpenedImageData.openedImageSharpened; //store the image in histogram object
                        this.imageHistogram = new Histogram(OpenedImageData.openedImageSharpened); //create histogram for grayscaled image histogram
                        this.applicationForm.pictureBox1.Image = this.OpenedImageData.openedImageSharpened; //put the picture into picture box
                    }
                    else
                    {
                        //first time dilatation will dilatate the grayscaled image
                        if (OpenedImageData.imageWasEroded)
                            OpenedImageData.openedImageSharpened = sharpeningFilter.Apply(OpenedImageData.openedImageEroded);
                        else
                            OpenedImageData.openedImageSharpened = sharpeningFilter.Apply(OpenedImageData.openedImageGrayscaled);
                        OpenedImageData.imageWasSharpened = true;
                        this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageSharpened;
                    }
                }
                else
                {
                    MessageBox.Show("Please, grayscale the image before applying the Sharpening");
                    applicationForm.pictureBox1.Image = OpenedImageData.openedImage;
                }
            }
            else
                MessageBox.Show("There is no image to Sharp.");
            //this.DrawSeparetedClusters();
            Cursor.Current = Cursors.Arrow;
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DrawSeparetedClusters()
        {
            for (int i = 0; i < this.OpenedImageData.openedImage.Height; i++)
            {
                for (int j = 0; j < this.OpenedImageData.openedImage.Width; j++)
                {
                    if (this.imageClusters.likelihood[0,i,j] == 1)
                    {

                        this.OpenedImageData.openedImage.SetPixel(i, j, Color.White);
                    }
                }
            }
            this.applicationForm.pictureBox1.Image = this.OpenedImageData.openedImage; //Set opened image to main window 
        }
    }
}
