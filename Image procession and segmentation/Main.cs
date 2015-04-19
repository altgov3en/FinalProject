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
            
            ImageData openedImageData = new ImageData();
            applicationForm.pictureBox2.Hide(); 
            ImageController OpenedImageController = new ImageController(applicationForm, openedImageData);
           

            Application.Run(applicationForm); //run the application UI form

        }
    }
}
