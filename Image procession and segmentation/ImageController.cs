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
        private bool imageWasGrayscaled = false;

        public Bitmap OpenImage()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if(openDialog.ShowDialog() == DialogResult.OK)
            {
                openedImage = new Bitmap(openDialog.FileName);
            }

            return openedImage;
        }

        public void SaveImage(ImageView ImageView)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (imageWasGrayscaled)
                    ImageView.openedImageGrayscaled.Save(saveDialog.FileName);
                else
                    ImageView.openedImage.Save(saveDialog.FileName);
            }   

        }

        public Bitmap ConvertToGrayscale(Bitmap imageToConvert)
        {
            this.imageWasGrayscaled = true;
            return grayScaleFilter.Apply(imageToConvert);;
        }
    }
}
