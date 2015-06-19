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
        private ImageData openedImageData;
        private HistogramWindow histogramForm;
        private SegmentedImageWindow segmentedImageForm;

        private int samplingRate = 5; // The program will estimate the number of clusters by sampling
                              // the histogram (of grayscale-eroded-sharpened image) "samplingRate" times 

        public ImageController(MainWindow applicationForm, HistogramWindow histogramForm,
                               SegmentedImageWindow segmentedImageForm, ImageData OpenedImageData) //Constructor
        {
            // TODO: Complete member initialization
            this.applicationForm = applicationForm;
            this.histogramForm = histogramForm;
            this.segmentedImageForm = segmentedImageForm;
            this.openedImageData = OpenedImageData;
            this.applicationForm.Load += new System.EventHandler(this.Form1_Load);
            this.applicationForm.imageAnalysisToolsToolStripMenuItem.Click += new System.EventHandler(this.ImageAnalysisToolsToolStripMenuItem_Click);
            this.applicationForm.openImageToolStripMenuItem.Click += new System.EventHandler(this.OpenImageToolStripMenuItem_Click);
            this.applicationForm.saveImageToolStripMenuItem.Click += new System.EventHandler(this.SaveImageToolStripMenuItem_Click);
            this.applicationForm.convertToGrayscaleToolStripMenuItem.Click += new System.EventHandler(this.ConvertToGrayscaleToolStripMenuItem_Click);
            this.applicationForm.erodeTheImageToolStripMenuItem.Click += new System.EventHandler(this.ErodeTheImageToolStripMenuItem_Click);
            this.applicationForm.sharpenTheImageToolStripMenuItem.Click += new System.EventHandler(this.SharpenTheImageToolStripMenuItem_Click);
            this.applicationForm.runOverallDiagnosisToolStripMenuItem.Click += new System.EventHandler(this.runOverallDiagnosisToolStripMenuItem_Click);
            this.applicationForm.estimateNumberOfClustersToolStripMenuItem.Click += new System.EventHandler(this.estimateNumberOfClustersToolStripMenuItem_Click);
            this.applicationForm.intoEstimatedNumberOfClustersToolStripMenuItem.Click += new System.EventHandler(this.intoEstimatedNumberOfClustersToolStripMenuItem_Click);
            this.applicationForm.intoUserSpecifiesNumberOfClustersToolStripMenuItem.Click += new System.EventHandler(this.intoUserSpecifiesNumberOfClustersToolStripMenuItem_Click);
            this.histogramForm.okButton.Click += new System.EventHandler(this.okButton_Click);

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
            this.SaveImage(openedImageData);
        }
        //"Image Analysis Tools->Convert To Grayscale" event
        private void ConvertToGrayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ConvertToGrayscale(openedImageData.openedImage);
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
        //"Image Analysis Tools -> Run Overall Diagnosis
        private void runOverallDiagnosisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.runOverallDiagnosis();
        }
        //"Image Analysis Tools -> Divide Image To... -> Custom Number Of Clusers event
        private void intoUserSpecifiesNumberOfClustersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showHistogramWindowToSelectDesireNumberOfClusters();
        }
        //"Image Analysis Tools -> Divide Image To... -> Estimated Number Of Clusters event
        private void intoEstimatedNumberOfClustersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.segmentImageToEstimatedNumberOfClusters(); 
        }
        //"Image Analysis Tools -> Estimate Number Of Clusters
        private void estimateNumberOfClustersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.EstimateTheNumberOfClusters();
        }
        //Histogram Window -> OK click event (when user specifies the number of clusters)
        private void okButton_Click(object sender, EventArgs e)
        {
            this.segmentImageToUserSpecifiedNumberOfClusters();
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
                this.openedImageData.openedImage = new Bitmap(openDialog.FileName);

                if (this.openedImageData.openedImage.Width != this.openedImageData.openedImage.Height)
                {
                    MessageBox.Show("Error!\nPlease open square size image");
                    return;
                }
               

                this.openedImageData.imageWasOpened = true; //indicates if user opened the image
                this.applicationForm.pictureBox1.Image = this.openedImageData.openedImage; //Set opened image to main window 
                this.applicationForm.saveImageToolStripMenuItem.Enabled = true;
                this.applicationForm.imageAnalysisToolsToolStripMenuItem.Enabled = true;

                this.applicationForm.openedImageLable.Visible = true;
                this.applicationForm.openedImageLable.Text = "Original Image:";
            }
        }
        public void SaveImage(ImageData ImageView)
        {
            if (openedImageData.imageWasOpened)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (openedImageData.imageWasGrayscaled)
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
            if (openedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                openedImageData.imageWasGrayscaled = true;
                openedImageData.openedImageGrayscaled = grayScaleFilter.Apply(imageToConvert);
                this.applicationForm.pictureBox1.Image = openedImageData.openedImageGrayscaled;
                this.openedImageHistogramGrayscaled = new Histogram(openedImageData.openedImageGrayscaled); //create histogram for grayscaled image histogram

                this.applicationForm.erodeTheImageToolStripMenuItem.Enabled = true;
                this.applicationForm.sharpenTheImageToolStripMenuItem.Enabled = true;


                this.applicationForm.convertToGrayscaleToolStripMenuItem.Text = "Image Already Grayscaled";
                this.applicationForm.convertToGrayscaleToolStripMenuItem.Enabled = false;

                this.applicationForm.openImageToolStripMenuItem.Enabled = false;
                this.applicationForm.openImageToolStripMenuItem.Text = "Open Image (Opened Image Already In Process)";

                this.applicationForm.runOverallDiagnosisToolStripMenuItem.Enabled = true;
                this.applicationForm.estimateNumberOfClustersToolStripMenuItem.Enabled = true;
                this.applicationForm.divideImageToolStripMenuItem.Enabled = true;

                this.applicationForm.openedImageLable.Text = "Original Image:\nGrayscaled";


            }
            else
                MessageBox.Show("There is no image to Grayscale.");

            Cursor.Current = Cursors.Arrow;
        }
        public void ErodeGrayscaledImage()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (openedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                if (openedImageData.imageWasGrayscaled) //checks if the opened image was grayscaled
                {
                    if (openedImageData.imageWasEroded) //checks if the grayscaled image was already eroded
                    //not first time erosion, will erode the already eroded image
                    {
                        this.openedImageData.openedImageEroded = this.dilatationFilter.Apply(this.openedImageData.openedImageEroded);
                        this.applicationForm.pictureBox1.Image = this.openedImageData.openedImageEroded;
                        this.openedImageData.timesEroded++;

                        if (this.openedImageData.imageWasSharpened)
                            this.applicationForm.openedImageLable.Text = "Original Image:\nGrayscaled\nEroded X" + this.openedImageData.timesEroded + " Times\nSharpened X" + this.openedImageData.timesSharpened + " Times";
                        else
                            this.applicationForm.openedImageLable.Text = "Original Image:\nGrayscaled\nEroded X" + this.openedImageData.timesEroded + " Times";
                    }
                    else
                    {
                        //first time ersosion, will erode the grayscaled image                
                        openedImageData.imageWasEroded = true;
                        this.openedImageData.openedImageEroded = this.dilatationFilter.Apply(this.openedImageData.openedImageGrayscaled);
                        this.applicationForm.pictureBox1.Image = this.openedImageData.openedImageEroded; ;
                        this.openedImageHistogramEroded = new Histogram(this.openedImageData.openedImageEroded);

                        this.openedImageData.timesEroded++;

                        if (this.openedImageData.imageWasSharpened)
                            this.applicationForm.openedImageLable.Text = "Original Image:\nGrayscaled\nEroded X" + this.openedImageData.timesEroded + " Times\nSharpened X" + this.openedImageData.timesSharpened + " Times";
                        else
                            this.applicationForm.openedImageLable.Text = "Original Image:\nGrayscaled\nEroded X" + this.openedImageData.timesEroded + " Times";
                    }
                }
                else
                {
                    MessageBox.Show("Please, grayscale the image before applying the Erosion");
                    applicationForm.pictureBox1.Image = openedImageData.openedImage;
                }
            }
            else
                MessageBox.Show("There is no image to Erode.");
            Cursor.Current = Cursors.Arrow;
        }
        private void SharpenGrayscaledImage()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (openedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                if (openedImageData.imageWasGrayscaled) //checks if the opened image was grayscaled
                {
                    if (openedImageData.imageWasSharpened) //checks if the grayscaled image was already sharpened
                    {
                        openedImageData.openedImageSharpened = sharpeningFilter.Apply(openedImageData.openedImageSharpened);
                        this.openedImageHistogramGrayscaled.openedImageGraysledSharpened = this.openedImageData.openedImageSharpened; //store the image in histogram object
                        this.openedImageHistogramGrayscaled = new Histogram(openedImageData.openedImageSharpened); //create histogram for grayscaled image histogram
                        this.applicationForm.pictureBox1.Image = this.openedImageData.openedImageSharpened; //put the picture into picture box
                        this.openedImageData.timesSharpened++;

                        if(this.openedImageData.imageWasEroded)
                            this.applicationForm.openedImageLable.Text = "Original Image:\nGrayscaled\nEroded X" + this.openedImageData.timesEroded + " Times\nSharpened X" + this.openedImageData.timesSharpened + " Times";
                        else
                            this.applicationForm.openedImageLable.Text = "Original Image:\nGrayscaled\nSharpened X" + this.openedImageData.timesSharpened + " Times";
                    }
                    else //first time sharpening, will sharp the grayscaled image
                    {
                        if (openedImageData.imageWasEroded)
                            openedImageData.openedImageSharpened = sharpeningFilter.Apply(openedImageData.openedImageEroded);
                        else
                            openedImageData.openedImageSharpened = sharpeningFilter.Apply(openedImageData.openedImageGrayscaled);
                        openedImageData.imageWasSharpened = true;
                        this.openedImageHistogramSharpened = new Histogram(this.openedImageData.openedImageSharpened); //create histogram for sharpened image
                        this.applicationForm.pictureBox1.Image = openedImageData.openedImageSharpened;

                        this.openedImageData.timesSharpened++;

                        if (this.openedImageData.imageWasEroded)
                            this.applicationForm.openedImageLable.Text = "Original Image:\nGrayscaled\nEroded X" + this.openedImageData.timesEroded + " Times\nSharpened X" + this.openedImageData.timesSharpened + " Times";
                        else
                            this.applicationForm.openedImageLable.Text = "Original Image:\nGrayscaled\nSharpened X" + this.openedImageData.timesSharpened + " Times";
                    }
                }
                else
                {
                    MessageBox.Show("Please, grayscale the image before applying the Sharpening");
                    applicationForm.pictureBox1.Image = openedImageData.openedImage;
                }
            }
            else
                MessageBox.Show("There is no image to Sharp.");
            Cursor.Current = Cursors.Arrow;
        }
        private void EstimateTheNumberOfClusters()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.applicationForm.erodeTheImageToolStripMenuItem.Enabled = false;
            this.applicationForm.sharpenTheImageToolStripMenuItem.Enabled = false;

            if (this.openedImageData.imageWasSharpened)           
                this.estimatedNumberOfClusters = new Samples(this.openedImageHistogramSharpened,
                                                             this.openedImageData.openedImageSharpened,
                                                             this.samplingRate);            
            else
            {
                if (this.openedImageData.imageWasEroded)
                    this.estimatedNumberOfClusters = new Samples(this.openedImageHistogramEroded,
                                                                 this.openedImageData.openedImageEroded,
                                                                 this.samplingRate);
                else
                    if (this.openedImageData.imageWasGrayscaled)
                        this.estimatedNumberOfClusters = new Samples(this.openedImageHistogramGrayscaled,
                                                                     this.openedImageData.openedImageGrayscaled,
                                                                     this.samplingRate);
            }

            this.numberOfClusters = this.estimatedNumberOfClusters.makeEstimationOfClusterNumber();

            this.applicationForm.intoEstimatedNumberOfClustersToolStripMenuItem.Text = "Estimated Number Of Clusters\n(Estimated Number Of Clusters: " + this.numberOfClusters + ")";
            this.applicationForm.intoEstimatedNumberOfClustersToolStripMenuItem.Enabled = true;
            this.applicationForm.estimateNumberOfClustersToolStripMenuItem.Enabled = false;
            this.applicationForm.estimateNumberOfClustersToolStripMenuItem.Text = "Estimate Number Of Clusters (Already Estimated)";

            Cursor.Current = Cursors.Arrow;
         }
        private void showHistogramWindowToSelectDesireNumberOfClusters()
        {
            this.applicationForm.erodeTheImageToolStripMenuItem.Enabled = false;
            this.applicationForm.sharpenTheImageToolStripMenuItem.Enabled = false;



            if (this.openedImageData.imageWasSharpened)
                this.histogramForm.histogramPicture.Image = this.openedImageHistogramSharpened.DrawHistogram(this.openedImageHistogramSharpened.openedImageHistogramArray);
            else
                if (this.openedImageData.imageWasEroded)

                    this.histogramForm.histogramPicture.Image = this.openedImageHistogramEroded.DrawHistogram(this.openedImageHistogramEroded.openedImageHistogramArray);
                else
                    if (this.openedImageData.imageWasGrayscaled)
                        this.histogramForm.histogramPicture.Image = this.openedImageHistogramGrayscaled.DrawHistogram(this.openedImageHistogramGrayscaled.openedImageHistogramArray);

            this.histogramForm.Show();
        }
        private void segmentImageToUserSpecifiedNumberOfClusters()
        {
            Cursor.Current = Cursors.WaitCursor;
            Int32.TryParse(this.histogramForm.numberOfClustersTextBox.Text, out this.numberOfClusters);
            this.histogramForm.Close();

            if (this.openedImageData.imageWasSharpened)
            {
                this.segmentedImageForm.imageHistogrm.Image = this.openedImageHistogramSharpened.DrawHistogram(this.openedImageHistogramSharpened.openedImageHistogramArray);
                this.segmentedImageForm.imageHistogrm.Show();
                this.segmentedImageForm.segmentedImageLable.Text = "Image Segmented Into " + this.numberOfClusters + " Clusters";
                this.imageClusters = new Clusters(this.numberOfClusters, this.openedImageData.openedImage.Height,
                                                  this.openedImageData.openedImage.Width, this.openedImageHistogramSharpened,
                                                  this.openedImageData.openedImageSharpened, false);
            }
            else
                if (this.openedImageData.imageWasEroded)
                {
                    this.segmentedImageForm.imageHistogrm.Image = this.openedImageHistogramEroded.DrawHistogram(this.openedImageHistogramEroded.openedImageHistogramArray);
                    this.segmentedImageForm.imageHistogrm.Show();
                    this.segmentedImageForm.segmentedImageLable.Text = "Image Segmented Into " + this.numberOfClusters + " Clusters";
                    this.imageClusters = new Clusters(this.numberOfClusters, this.openedImageData.openedImage.Height,
                                                      this.openedImageData.openedImage.Width, this.openedImageHistogramEroded,
                                                      this.openedImageData.openedImageEroded, false);

                }
                else
                    if (this.openedImageData.imageWasGrayscaled)
                    {
                        this.segmentedImageForm.imageHistogrm.Image = this.openedImageHistogramGrayscaled.DrawHistogram(this.openedImageHistogramGrayscaled.openedImageHistogramArray);
                        this.segmentedImageForm.imageHistogrm.Show();
                        this.segmentedImageForm.segmentedImageLable.Text = "Image Segmented Into " + this.numberOfClusters + " Clusters";
                        this.imageClusters = new Clusters(this.numberOfClusters, this.openedImageData.openedImage.Height,
                                                          this.openedImageData.openedImage.Width, this.openedImageHistogramGrayscaled,
                                                          this.openedImageData.openedImageGrayscaled, false);
                    }

            this.segmentedImageForm.segmentedImagePBox.Image = this.imageClusters.imageAfterEM;
            this.segmentedImageForm.originalImagePBox.Image = this.openedImageData.openedImage;

            this.segmentedImageForm.Show();

            Cursor.Current = Cursors.Arrow;

        }
        private void segmentImageToEstimatedNumberOfClusters()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (this.openedImageData.imageWasSharpened)
            {
                this.segmentedImageForm.imageHistogrm.Image = this.openedImageHistogramSharpened.DrawHistogram(this.openedImageHistogramSharpened.openedImageHistogramArray);
                this.segmentedImageForm.imageHistogrm.Show();
                this.segmentedImageForm.segmentedImageLable.Text = "Image Segmented Into " + this.numberOfClusters + " Clusters";
                this.imageClusters = new Clusters(this.numberOfClusters, this.openedImageData.openedImage.Height,
                                                  this.openedImageData.openedImage.Width, this.openedImageHistogramSharpened,
                                                  this.openedImageData.openedImageSharpened, false);
            }
            else
                if (this.openedImageData.imageWasEroded)
                {
                    this.segmentedImageForm.imageHistogrm.Image = this.openedImageHistogramEroded.DrawHistogram(this.openedImageHistogramEroded.openedImageHistogramArray);
                    this.segmentedImageForm.imageHistogrm.Show();
                    this.segmentedImageForm.segmentedImageLable.Text = "Image Segmented Into " + this.numberOfClusters + " Clusters";
                    this.imageClusters = new Clusters(this.numberOfClusters, this.openedImageData.openedImage.Height,
                                                      this.openedImageData.openedImage.Width, this.openedImageHistogramEroded,
                                                      this.openedImageData.openedImageEroded, false);

                }
                else
                    if (this.openedImageData.imageWasGrayscaled)
                    {
                        this.segmentedImageForm.imageHistogrm.Image = this.openedImageHistogramGrayscaled.DrawHistogram(this.openedImageHistogramGrayscaled.openedImageHistogramArray);
                        this.segmentedImageForm.imageHistogrm.Show();
                        this.segmentedImageForm.segmentedImageLable.Text = "Image Segmented Into " + this.numberOfClusters + " Clusters";
                        this.imageClusters = new Clusters(this.numberOfClusters, this.openedImageData.openedImage.Height,
                                                          this.openedImageData.openedImage.Width, this.openedImageHistogramGrayscaled,
                                                          this.openedImageData.openedImageGrayscaled, false);
                    }

            this.segmentedImageForm.segmentedImagePBox.Image = this.imageClusters.imageAfterEM;
            this.segmentedImageForm.originalImagePBox.Image = this.openedImageData.openedImage;

            this.segmentedImageForm.Show();

            Cursor.Current = Cursors.Arrow;
        }
        private void runOverallDiagnosis()
        {
            Cursor.Current = Cursors.WaitCursor;


            if (this.openedImageData.imageWasSharpened)
            {
                this.segmentedImageForm.imageHistogrm.Image = this.openedImageHistogramSharpened.DrawHistogram(this.openedImageHistogramSharpened.openedImageHistogramArray);
                this.segmentedImageForm.imageHistogrm.Show();
                this.EstimateTheNumberOfClusters();
                this.segmentedImageForm.segmentedImageLable.Text = "Image Segmented Into " + this.numberOfClusters + " Clusters";
                this.imageClusters = new Clusters(this.numberOfClusters, this.openedImageData.openedImage.Height,
                                                  this.openedImageData.openedImage.Width, this.openedImageHistogramSharpened,
                                                  this.openedImageData.openedImageSharpened, false);
            }
            else
                if (this.openedImageData.imageWasEroded)
                {
                    this.segmentedImageForm.imageHistogrm.Image = this.openedImageHistogramEroded.DrawHistogram(this.openedImageHistogramEroded.openedImageHistogramArray);
                    this.segmentedImageForm.imageHistogrm.Show();
                    this.EstimateTheNumberOfClusters();
                    this.segmentedImageForm.segmentedImageLable.Text = "Image Segmented Into " + this.numberOfClusters + " Clusters";
                    this.imageClusters = new Clusters(this.numberOfClusters, this.openedImageData.openedImage.Height,
                                                      this.openedImageData.openedImage.Width, this.openedImageHistogramEroded,
                                                      this.openedImageData.openedImageEroded, false);

                }
                else
                    if (this.openedImageData.imageWasGrayscaled)
                    {
                        this.segmentedImageForm.imageHistogrm.Image = this.openedImageHistogramGrayscaled.DrawHistogram(this.openedImageHistogramGrayscaled.openedImageHistogramArray);
                        this.segmentedImageForm.imageHistogrm.Show();
                        this.EstimateTheNumberOfClusters();
                        this.segmentedImageForm.segmentedImageLable.Text = "Image Segmented Into " + this.numberOfClusters + " Clusters";
                        this.imageClusters = new Clusters(this.numberOfClusters, this.openedImageData.openedImage.Height,
                                                          this.openedImageData.openedImage.Width, this.openedImageHistogramGrayscaled,
                                                          this.openedImageData.openedImageGrayscaled, false);
                    }

            this.segmentedImageForm.segmentedImagePBox.Image = this.imageClusters.imageAfterEM;
            this.segmentedImageForm.originalImagePBox.Image = this.openedImageData.openedImage;

            this.segmentedImageForm.Show();

            Cursor.Current = Cursors.Arrow;
        }
        #endregion


    }
}
