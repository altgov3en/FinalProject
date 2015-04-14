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
            applicationForm.ImageController = new ImageController(); //Intialize image controller
            applicationForm.OpenedImageView = new ImageView(); //Initialize image view
            

            Application.Run(applicationForm); //run the application form

        }
    }
}
