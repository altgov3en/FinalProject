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
        public Bitmap openedImage;
        public Bitmap openedImageGrayscaled;
        public Bitmap openedImageEroded;
        public Bitmap openedImageDilatated;

        public Boolean imageWasGrayscaled = false;
        public Boolean imageWasEroded = false;
        public Boolean imageWasDilated = false;

    }
}
