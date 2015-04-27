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
            this.applicationForm.erodeImageToolStripMenuItem.Click += new System.EventHandler(this.erodeImageToolStripMenuItem_Click);
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
            //this.DilatateGrayscaledImage();
            this.NEWerodeGrayscaledImage();
        }

        //"Image Analysis Tools->Dilatate the Image" event
        private void erodeImageToolStripMenuItem_Click(object sender, EventArgs e)
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
            this.calculateHistogram(); //create histogram of the grayscaled image
            applicationForm.button1.Visible = false; //make button invisible as histogram is created (change ref. to close button)
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
                this.applicationForm.pictureBox1.Image = OpenedImageData.openedImage; //Set opened image to main window 
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
            if (OpenedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                OpenedImageData.imageWasGrayscaled = true;
                OpenedImageData.openedImageGrayscaled = grayScaleFilter.Apply(imageToConvert);
                this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageGrayscaled;
                applicationForm.button1.Visible = true; //"Show Image Histogram" button is anabled
                                                        //This image will appear only when the image is grayscaled 
            }
            else
                MessageBox.Show("There is no image to Grayscale.");
        }
        public void ErodeGrayscaledImage()  //kjkjkljl
        {
            if (OpenedImageData.imageWasOpened) //checks if user opened any image to be analyzed
            {
                if (OpenedImageData.imageWasGrayscaled) //checks if the opened image was grayscaled
                {
                    if (OpenedImageData.imageWasEroded)//checks if the grayscaled image was already eroded
                    //not first time erosion will erode the already eroded image
                    {
                        OpenedImageData.openedImageEroded = erosionFilter.Apply(OpenedImageData.openedImageEroded);
                        this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageEroded;
                    }
                    else
                    {
                        //first time erosion will erode the grayscaled image 
                        OpenedImageData.imageWasEroded = true;
                        OpenedImageData.openedImageEroded = erosionFilter.Apply(OpenedImageData.openedImageGrayscaled);
                        this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageEroded;

                    }
                }
                else
                {
                    MessageBox.Show("Please, grayscale the image before applying the Dilatation");
                    applicationForm.pictureBox1.Image = OpenedImageData.openedImage;
                }
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
                    {
                        OpenedImageData.openedImageDilatated = dilatationFilter.Apply(OpenedImageData.openedImageDilatated);
                        this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageDilatated;
                    }
                    else
                    {
                        //first time dilatation will dilatate the grayscaled image                
                        OpenedImageData.imageWasDilatated = true;
                        OpenedImageData.openedImageDilatated = dilatationFilter.Apply(OpenedImageData.openedImageGrayscaled);
                        this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageDilatated;

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
                        if(OpenedImageData.imageWasEroded)
                            OpenedImageData.openedImageSharpened = sharpeningFilter.Apply(OpenedImageData.openedImageEroded);
                        else
                            if(OpenedImageData.imageWasDilatated)
                                OpenedImageData.openedImageSharpened = sharpeningFilter.Apply(OpenedImageData.openedImageDilatated);
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
        }
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void NEWerodeGrayscaledImage()
        {
            BitmapData sourceData = OpenedImageData.openedImageGrayscaled.LockBits(new System.Drawing.Rectangle(0, 0, OpenedImageData.openedImage.Width, OpenedImageData.openedImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            
            OpenedImageData.openedImageEroded = new Bitmap(sourceData.Width, sourceData.Height);
            BitmapData destData = OpenedImageData.openedImageEroded.LockBits(new System.Drawing.Rectangle(0, 0, sourceData.Width, sourceData.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
         
            unsafe
            {
                byte* sourcePtr = (byte*)sourceData.Scan0;
                byte* destPtr = (byte*)destData.Scan0;

                int blueValue = 0;
                int greenValue = 0;
                int redValue = 0;

                for (int i = 0; i < sourceData.Height - 3; i++)//for each row
                {
                    for (int j = 0; j < sourceData.Width - 3; j++)//for each column
                    {
                        blueValue += sourcePtr[0];
                        blueValue += sourcePtr[3];
                        blueValue += sourcePtr[6];
                        greenValue += sourcePtr[1];
                        greenValue += sourcePtr[4];
                        greenValue += sourcePtr[7];
                        redValue += sourcePtr[2];
                        redValue += sourcePtr[5];
                        redValue += sourcePtr[8];

                        sourcePtr += sourceData.Width;
                        blueValue += sourcePtr[0];
                        blueValue += sourcePtr[6];
                        greenValue += sourcePtr[1];
                        greenValue += sourcePtr[7];
                        redValue += sourcePtr[2];
                        redValue += sourcePtr[8];

                        sourcePtr += sourceData.Width;
                        blueValue += sourcePtr[0];
                        blueValue += sourcePtr[3];
                        blueValue += sourcePtr[6];
                        greenValue += sourcePtr[1];
                        greenValue += sourcePtr[4];
                        greenValue += sourcePtr[7];
                        redValue += sourcePtr[2];
                        redValue += sourcePtr[5];
                        redValue += sourcePtr[8];

                        blueValue /= 9;
                        greenValue /= 9;
                        redValue /= 9;

                        destPtr[0] = (byte)blueValue;
                        destPtr[1] = (byte)greenValue;
                        destPtr[2] = (byte)redValue;

                        sourcePtr -= 2 * sourceData.Width;
                        sourcePtr += 3;

                        destPtr += 3;
                    }
                }
            }
            OpenedImageData.openedImageEroded.UnlockBits(destData);
            this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageEroded;
        }

        public void calculateHistogram()
        {
            //What is pixel?
            /* Each pixel's color sample has three numerical RGB components (Red, Green, Blue)
             * to represent the color of that tiny pixel area. These three RGB components are
             * three 8-bit numbers for each pixel. Three 8-bit bytes (one byte for each of RGB)
             * is called 24 bit color. Each 8 bit RGB component can have 256 possible values,
             * ranging from 0 to 255. For example, three values like (250, 165, 0), meaning (Red=250, Green=165, Blue=0)
             * to denote one Orange pixel.*/
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            BitmapData data = OpenedImageData.openedImageGrayscaled.LockBits(new System.Drawing.Rectangle(0, 0, OpenedImageData.openedImage.Width, OpenedImageData.openedImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                //BitmapData.Scan0 gets or sets the address of the first pixel data in the bitmap.
                //This can also be thought of as the first scan line in the bitmap.
                byte* ptr = (byte*)data.Scan0;
  
                //BitmapData.Width gets the width, in pixels, of this Image.
                //BitmapData.Stride gets the number of bytes (rounded up to a four-byte boundary)
                //                                             taken to store one row of an image.
                //
                int remain = data.Stride - data.Width * 3; //Difference between rounded up to four bytes image width 
                                                           //               and actual image widht.
                //Example
                //For given 4x5 pixels image:
                //
                //|B|G|R| |B|G|R| |B|G|R| |B|G|R|
                //|B|G|R| |B|G|R| |B|G|R| |B|G|R|
                //|B|G|R| |B|G|R| |B|G|R| |B|G|R|
                //|B|G|R| |B|G|R| |B|G|R| |B|G|R|
                //|B|G|R| |B|G|R| |B|G|R| |B|G|R|
                //
                //BitmapData.Width = 4 pixel (int value)
                //BitmapData.Stride = (3+1)*4 Bytes (int value)


                int[] histogram = new int[256]; //2^8 = 256 shades of gray
                                                //histogram[i] holds the number of pixels for each shade 'i'

                for (int i = 0; i < histogram.Length; i++) //each nuber for pixels for shade i initialize to zero 
                    histogram[i] = 0;

                for (int i = 0; i < data.Height; i++)//for each row
                {
                    for (int j = 0; j < data.Width; j++)//for each column
                    {
                        int mean = ptr[0] + ptr[1] + ptr[2]; //sum 3 pixel's color values 
                                                             //(each pixel represented with three 8 bit (Byte) values)

                        mean /= 3; //calculate the pixel color or shade

                        histogram[mean]++; //incriment the pixel counter for shade 'mean'
                        ptr += 3;          //go to next pixel
                    }

                    ptr += remain; //this will actualy take the pointer to the new image row
                }
                drawHistogram(histogram);
            }
            OpenedImageData.openedImageGrayscaled.UnlockBits(data);
        }
        public void drawHistogram(int[] histogram)
        {
            Bitmap bmp = new Bitmap(histogram.Length + 10, 310); //new blank bitmap image
            BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* ptr = (byte*)data.Scan0; //see explanations about Scan0 at calculateHistogram() method
                int remain = data.Stride - data.Width * 3; //see explanations about Stride at calculateHistogram() method

                //Color blank image in black
                for (int i = 0; i < data.Height; i++)//for each row
                {
                    for (int j = 0; j < data.Width; j++)//for each column
                    {
                        ptr[0] = ptr[1] = ptr[2] = 0; //Blue=0,Red=0 and Green=0 gets us black color
                        ptr += 3; //go to next image pixel
                    }
                    ptr += remain; //this will actualy take the pointer to the new image row
                }

                int max = 0;
                for (int i = 0; i < histogram.Length; i++) //finds the maximum in array
                    if (max < histogram[i])
                        max = histogram[i];

                //Draw histogram columns on blank black image
                for (int i = 0; i < histogram.Length; i++)
                {
                    ptr = (byte*)data.Scan0;//see explanations about hte Scan0 at calculateHistogram() method
                    ptr += data.Stride * (305) + (i + 5) * 3; //go to the bottom of the image and i+5 pixels to left

                    int length = (int)(1.0 * histogram[i] * 300 / max); //histogram column height

                    for (int j = 0; j < length; j++) //draw the column of the height 'lenght'
                    {
                        ptr[0] = 0;
                        ptr[1] = ptr[2] = 255;
                        ptr -= data.Stride; //go up exactly one row (staying at the same pixel but one row above)
                    }
                }
            }

            bmp.UnlockBits(data);

            grayscaleHistogram.pictureBox1.Image = bmp; //put the Histogram image into the picture box (in the window)
            grayscaleHistogram.Show(); //show the window on the screen
        }
    }

}
