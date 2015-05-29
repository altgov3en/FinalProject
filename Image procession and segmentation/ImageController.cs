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

        private Histogram openedImageHistogramGrayscaled; // Histogram for grayscaled image
        private Histogram openedImageHistogramEroded;    // Histogran for eroded image
        private Histogram openedImageHistogramSharpened; // Histogram for sharpened image
        
        private Clusters imageClusters;
        private Samples estimatedNumberOfClusters;
        private int numberOfClusters;

        private MainWindow applicationForm;
        private ImageData OpenedImageData;
        private HistogramWindow grayscaleHistogramForm;
        private SegmentedImageWindow segmentedImageForm;

        private int samplingRate = 5; // The program will estimate the number of clusters by sampling
                              // the histogram (of grayscaled and sharpened image) samplingRate times 

        public ImageController(MainWindow applicationForm, HistogramWindow grayscaleHistogramForm,
                               SegmentedImageWindow segmentedImageForm, ImageData OpenedImageData) //Constructor
        {
            // TODO: Complete member initialization
            this.applicationForm = applicationForm;
            this.grayscaleHistogramForm = grayscaleHistogramForm;
            this.segmentedImageForm = segmentedImageForm;
            this.OpenedImageData = OpenedImageData;
            this.applicationForm.Load += new System.EventHandler(this.Form1_Load);
            this.applicationForm.imageAnalysisToolsToolStripMenuItem.Click += new System.EventHandler(this.ImageAnalysisToolsToolStripMenuItem_Click);
            this.applicationForm.openImageToolStripMenuItem.Click += new System.EventHandler(this.OpenImageToolStripMenuItem_Click);
            this.applicationForm.saveImageToolStripMenuItem.Click += new System.EventHandler(this.SaveImageToolStripMenuItem_Click);
            this.applicationForm.convertToGrayscaleToolStripMenuItem.Click += new System.EventHandler(this.ConvertToGrayscaleToolStripMenuItem_Click);
            this.applicationForm.erodeTheImageToolStripMenuItem.Click += new System.EventHandler(this.ErodeTheImageToolStripMenuItem_Click);
            this.applicationForm.sharpenTheImageToolStripMenuItem.Click += new System.EventHandler(this.SharpenTheImageToolStripMenuItem_Click);
            this.applicationForm.showHistogramButton.Click += new System.EventHandler(this.ShowHistogramButton_Click);
            this.applicationForm.showSegImageButton.Click += new System.EventHandler(this.ShowSegImageButton_Click);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region "UI Click" handlers

        private void ImageAnalysisToolsToolStripMenuItem_Click(object sender, EventArgs e)
        { }

        private void Form1_Load(object sender, EventArgs e)
        { }

        //"Image->Open Image" event
        private void OpenImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenImage(); //Open user specified image for analysis  
        }

        //"Image->Save Image" event
        private void SaveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveImage(OpenedImageData);
        }

        //"Image Analysis Tools->Convert To Grayscale" event
        private void ConvertToGrayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ConvertToGrayscale(OpenedImageData.openedImage);
        }

        //"Image Analysis Tools->Erode the Image" event
        private void ErodeTheImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ErodeGrayscaledImage();
        }

        //"Image Analysis Tools->Sharpen the Image" event
        private void SharpenTheImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SharpenGrayscaledImage();
        }

        //Click->"Show Image Histogram"
        private void ShowHistogramButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if(this.OpenedImageData.imageWasSharpened)
                this.grayscaleHistogramForm.pictureBox1.Image = this.openedImageHistogramSharpened.DrawHistogram(this.openedImageHistogramSharpened.openedImageHistogramArray);  
            else
                if (this.OpenedImageData.imageWasEroded)
                    this.grayscaleHistogramForm.pictureBox1.Image = this.openedImageHistogramEroded.DrawHistogram(this.openedImageHistogramEroded.openedImageHistogramArray);
                else
                    if (this.OpenedImageData.imageWasGrayscaled)
                        this.grayscaleHistogramForm.pictureBox1.Image = this.openedImageHistogramGrayscaled.DrawHistogram(this.openedImageHistogramGrayscaled.openedImageHistogramArray);


            this.grayscaleHistogramForm.Show(); // show histogram window
            //applicationForm.button1.Visible = false; //make button invisible as histogram is created (change ref. to close button)
            Cursor.Current = Cursors.Arrow;
        }

        //Click->"Show Segmented Image"
        private void ShowSegImageButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if(this.OpenedImageData.imageWasSharpened == false)
            {
                MessageBox.Show("Cannot display Segmented image.\n-The image must be Sharpened.\n");
                return;
            }


            //this.imageClusters = new Clusters(6, OpenedImageData.openedImage.Height, OpenedImageData.openedImage.Width,
            //                                  this.openedImageHistogramSharpened, this.OpenedImageData.openedImageSharpened, false);
            //this.segmentedImageForm.segmentedImagePBox.Image = this.imageClusters.imageAfterEM;

            //////TEST - draw cumulative sum of pixels
            //this.imageHistogram.CalculateCumulativeSumOfHistogramPeaks();
            //this.grayscaleHistogramForm.pictureBox1.Image = this.imageHistogram.DrawHistogram(this.imageHistogram.sumOfHistogramPeaks);

            this.grayscaleHistogramForm.pictureBox1.Image = this.openedImageHistogramSharpened.DrawHistogram(this.openedImageHistogramSharpened.openedImageHistogramArray);
            this.grayscaleHistogramForm.pictureBox1.Show();

            //this.grayscaleHistogramForm.pictureBox1.Image = this.imageHistogram.DrawHistogram(this.imageHistogram.histogramSamples);
            //this.grayscaleHistogramForm.Show();
            //this.grayscaleHistogramForm.Show();


            this.EstimateTheNumberOfClusters();

            //this.segmentedImageForm.segmentedImagePBox.Image = cl.imageAfterEM;
 
            this.segmentedImageForm.originalImagePBox.Image = this.OpenedImageData.openedImage;
            this.grayscaleHistogramForm.Show();
            //////END TEST

            this.DrawSeparetedClusters();
            this.segmentedImageForm.Show();
            Cursor.Current = Cursors.Arrow;
        }

        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Controller functions
        public void OpenImage() // Only square size images will be accepted
                                // Because our EM clustering algorithm works only with square size images
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                this.OpenedImageData.openedImage = new Bitmap(openDialog.FileName);

                if (this.OpenedImageData.openedImage.Width != this.OpenedImageData.openedImage.Height)
                {
                    MessageBox.Show("Error!\nPlease open square size image");
                    return;
                }
               

                this.OpenedImageData.imageWasOpened = true; //indicates if user opened the image
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
                this.openedImageHistogramGrayscaled = new Histogram(OpenedImageData.openedImageGrayscaled); //create histogram for grayscaled image histogram

                this.applicationForm.showHistogramButton.Visible = true; //"Show Image Histogram" button is anabled                                                        
                this.applicationForm.showSegImageButton.Visible = true; //"Show Segemented Image" button is anabled
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
                    if (OpenedImageData.imageWasEroded) //checks if the grayscaled image was already eroded
                    //not first time erosion, will erode the already eroded image
                    {
                        this.OpenedImageData.openedImageEroded = this.dilatationFilter.Apply(this.OpenedImageData.openedImageEroded);
                        this.applicationForm.pictureBox1.Image = this.OpenedImageData.openedImageEroded;                       
                    }
                    else
                    {
                        //first time ersosion, will erode the grayscaled image                
                        OpenedImageData.imageWasEroded = true;
                        this.OpenedImageData.openedImageEroded = this.dilatationFilter.Apply(this.OpenedImageData.openedImageGrayscaled);
                        this.applicationForm.pictureBox1.Image = this.OpenedImageData.openedImageEroded; ;
                        this.openedImageHistogramEroded = new Histogram(this.OpenedImageData.openedImageEroded);
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

            if (OpenedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                if (OpenedImageData.imageWasGrayscaled) //checks if the opened image was grayscaled
                {
                    if (OpenedImageData.imageWasSharpened) //checks if the grayscaled image was already sharpened
                    {
                        OpenedImageData.openedImageSharpened = sharpeningFilter.Apply(OpenedImageData.openedImageSharpened);
                        this.openedImageHistogramGrayscaled.openedImageGraysledSharpened = this.OpenedImageData.openedImageSharpened; //store the image in histogram object
                        this.openedImageHistogramGrayscaled = new Histogram(OpenedImageData.openedImageSharpened); //create histogram for grayscaled image histogram
                        this.applicationForm.pictureBox1.Image = this.OpenedImageData.openedImageSharpened; //put the picture into picture box
                    }
                    else //first time sharpening, will sharp the grayscaled image
                    {
                        if (OpenedImageData.imageWasEroded)
                            OpenedImageData.openedImageSharpened = sharpeningFilter.Apply(OpenedImageData.openedImageEroded);
                        else
                            OpenedImageData.openedImageSharpened = sharpeningFilter.Apply(OpenedImageData.openedImageGrayscaled);
                        OpenedImageData.imageWasSharpened = true;
                        this.openedImageHistogramSharpened = new Histogram(this.OpenedImageData.openedImageSharpened); //create histogram for sharpened image
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
            Cursor.Current = Cursors.Arrow;
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void EstimateTheNumberOfClusters()
        {
            this.estimatedNumberOfClusters = new Samples(this.openedImageHistogramSharpened,
                                                         this.OpenedImageData.openedImageSharpened,
                                                         this.samplingRate);

            this.estimatedNumberOfClusters.makeEstimationOfClusterNumber();
        }
        private void DrawSeparetedClusters() //!!!MOVE TO CLUSTER CLASS!!!
        {
            //this.OpenedImageData.openedImageSegmented = new Bitmap(this.OpenedImageData.openedImage);

            //Color[] colorArr = { Color.Red, Color.Yellow, Color.Blue, Color.Green, Color.Black };

            //for (int i = 0; i < this.OpenedImageData.openedImageSegmented.Height; i++)
            //{
            //    for (int j = 0; j < this.OpenedImageData.openedImageSegmented.Width; j++)
            //    {
            //        for (int c = 1; c < this.imageClusters.numberOfClusters; c++)
            //        {
            //            if (this.imageClusters.likelihood[c, i, j] == 1)
            //            {

            //                this.OpenedImageData.openedImageSegmented.SetPixel(i, j, colorArr[c]);
            //            }
            //        }
            //    }
            //}
            //this.segmentedImageForm.segmentedImagePBox.Image = this.OpenedImageData.openedImageSegmented;
            //this.segmentedImageForm.segmentedImagePBox.Image = this.imageClusters.imageAfterEM;
            //this.segmentedImageForm.originalImagePBox.Image = this.OpenedImageData.openedImage;

        }
    }
}
