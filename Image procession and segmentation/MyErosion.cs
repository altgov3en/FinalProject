using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_procession_and_segmentation
{
    class MyErosion
    {
        //This method applies 3x3 filter on sourceImage
        public void MyErosionFilter(Bitmap sourceImage)
        {
            //BitmapData sourceData = sourceImage.LockBits(new System.Drawing.Rectangle(0, 0, OpenedImageData.openedImage.Width, OpenedImageData.openedImage.Height), 
            //                                             ImageLockMode.ReadWrite,
            //                                             PixelFormat.Format24bppRgb);

            //OpenedImageData.openedImageEroded = new Bitmap(sourceData.Width, sourceData.Height);
            //BitmapData destData = OpenedImageData.openedImageEroded.LockBits(new System.Drawing.Rectangle(0, 0, sourceData.Width, sourceData.Height),
            //                                                                 ImageLockMode.ReadWrite,
            //                                                                 PixelFormat.Format24bppRgb);

            //unsafe
            //{
            //    byte* sourcePtr = (byte*)sourceData.Scan0;
            //    byte* destPtr = (byte*)destData.Scan0;

            //    int blueValue = 0;
            //    int greenValue = 0;
            //    int redValue = 0;

            //    for (int i = 0; i < sourceData.Height - 3; i++)//for each row
            //    {
            //        for (int j = 0; j < sourceData.Width - 3; j++)//for each column
            //        {
            //            blueValue += sourcePtr[0];
            //            blueValue += sourcePtr[3];
            //            blueValue += sourcePtr[6];
            //            greenValue += sourcePtr[1];
            //            greenValue += sourcePtr[4];
            //            greenValue += sourcePtr[7];
            //            redValue += sourcePtr[2];
            //            redValue += sourcePtr[5];
            //            redValue += sourcePtr[8];

            //            sourcePtr += sourceData.Width*3;
            //            blueValue += sourcePtr[0];
            //            //blueValue += sourcePtr[3];
            //            blueValue += sourcePtr[6];
            //            greenValue += sourcePtr[1];
            //            //greenValue += sourcePtr[4];
            //            greenValue += sourcePtr[7];
            //            redValue += sourcePtr[2];
            //            //redValue += sourcePtr[5];
            //            redValue += sourcePtr[8];

            //            sourcePtr += sourceData.Width*3;
            //            blueValue += sourcePtr[0];
            //            blueValue += sourcePtr[3];
            //            blueValue += sourcePtr[6];
            //            greenValue += sourcePtr[1];
            //            greenValue += sourcePtr[4];
            //            greenValue += sourcePtr[7];
            //            redValue += sourcePtr[2];
            //            redValue += sourcePtr[5];
            //            redValue += sourcePtr[8];

            //            blueValue /= 9;
            //            greenValue /= 9;
            //            redValue /= 9;

            //            destPtr[0] = (byte)blueValue;
            //            destPtr[1] = (byte)greenValue;
            //            destPtr[2] = (byte)redValue;

            //            sourcePtr -= 2 * sourceData.Width*3;
            //            sourcePtr += 3;

            //            destPtr += 3;
            //        }
            //    }
            //}
            //OpenedImageData.openedImageEroded.UnlockBits(destData);
            //this.applicationForm.pictureBox1.Image = OpenedImageData.openedImageEroded; 
        }
    }
}
