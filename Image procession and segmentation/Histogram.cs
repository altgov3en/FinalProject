using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Image_procession_and_segmentation
{
    class Histogram
    {
        public Bitmap openedImageGrayscaled;
        public Bitmap openedImageGraysledSharpened;
        public Bitmap openedImageHistogram;
        public int[] openedImageHistogramArray; //holds the total number of pixel for every color in image (0-255)
        public float[] imagePixelColorProbilityArray; //hold the probability for every pixel to be in specific color (0-255 colors)

        public Histogram(Bitmap source)
        {
            this.openedImageGrayscaled = source;
            this.openedImageHistogramArray = CalculateNumberOfPixelForEachColor();
        } // constructor
        private int[] CalculateNumberOfPixelForEachColor()
        {
            int[] imageHistogram = new int[256]; //2^8=256 colors

            for (int i = 0; i < this.openedImageGrayscaled.Size.Width; i++)//for each column
            {
                for (int j = 0; j < this.openedImageGrayscaled.Size.Height; j++)//for each row
                {
                    Color c = this.openedImageGrayscaled.GetPixel(i, j); //return System.Drawing.Color 
                    // A Color structure represents an ARGB (alpha, red, green, blue) color of the specified pixel.
                    // ARGB values store the alpha transparency, as well as the red, green and blue values. 
                    // These values are stored as bytes which gives them a range of 0 to 255 inclusive.

                    int pixelColor = (int)((c.R + c.G + c.B) / 3);
                    imageHistogram[pixelColor]++;
                }
            }
            return imageHistogram;
        }
        //private float[] CalculateColorProbability()
        //{
        //    float[] colorProbability = new float[256]; //2^8=256 colors
        //    float totalPixelCount = this.openedImageGrayscaled.Height * this.openedImageGrayscaled.Width; //total number of pixel in image

        //    for (int i = 0; i < this.openedImageHistogramArray.Length; i++)
        //    {
        //        colorProbability[i] = (float)(this.openedImageHistogramArray[i]) / totalPixelCount; // Pr(pixel color is X) = (number of pixel colored in color X) / 
        //                                                                                            //                                    (total number of pixels)
        //    }
        //    float sum = colorProbability.Sum(); //total sum of probabilities must converge to 1
        //    return colorProbability;
        //}
        public Bitmap CalculateHistogram(Bitmap src)
        {
            //What is pixel?
            /* Each pixel's color sample has three numerical RGB components (Red, Green, Blue)
             * to represent the color of that tiny pixel area. These three RGB components are
             * three 8-bit numbers for each pixel. Three 8-bit bytes (one byte for each of RGB)
             * is called 24 bit color. Each 8 bit RGB component can have 256 possible values,
             * ranging from 0 to 255. For example, three values like (250, 165, 0), meaning (Red=250, Green=165, Blue=0)
             * to denote one Orange pixel.*/
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            BitmapData data = src.LockBits(new System.Drawing.Rectangle(0, 0, openedImageGrayscaled.Width, openedImageGrayscaled.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

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
                 this.openedImageHistogram = DrawHistogram(histogram);
            }
            src.UnlockBits(data);
            return this.openedImageHistogram;
        }
        public Bitmap DrawHistogram(int[] histogram)
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
            return bmp;
        }
    }
}
