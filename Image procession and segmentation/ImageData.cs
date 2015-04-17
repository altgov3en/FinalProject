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
    public class ImageData //rename image meta data
    {
        public Bitmap openedImage;           //stores the opened image
        public Bitmap openedImageGrayscaled; //stores the grayscaled image
        public Bitmap openedImageEroded;     //stores the eroded image
        public Bitmap openedImageDilatated;  //stores the eroded image
        public Bitmap openedImageSharpened;  //stores the sharpened image

        public Boolean imageWasOpened = false;      //indicates if the image was opened
        public Boolean imageWasGrayscaled = false;  //indicates if the image was grayscaled
        public Boolean imageWasEroded = false;      //indicates if the image was eroded
        public Boolean imageWasDilatated = false;   //indicates if the image was dilatated
        public Boolean imageWasSharpened = false;    //indicates if the image was sharpened

    }
}
