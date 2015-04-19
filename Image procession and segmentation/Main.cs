using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_procession_and_segmentation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() /*Main program*/
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWindow applicationForm = new MainWindow();
            HistogramWindow grayscaleHistogram = new HistogramWindow();
            
            ImageData openedImageData = new ImageData();
            ImageController OpenedImageController = new ImageController(applicationForm, grayscaleHistogram , openedImageData);
           

            Application.Run(applicationForm); //run the application UI form

        }
    }
}
