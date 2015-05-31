using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_procession_and_segmentation
{
    class EM_algorithm
    {
        private long pixels;    // Number of pixels in the image
        private int clusters;   // Number of clusters
        private Bitmap image;   // Original Image
        private double[, ,] likelihood;   //Likelihood matrix
        public double[] standartDeviation;  // How much pixels scatterd from the cluster's mean
        private double[] mean;  //Mean values of each cluster
        public const double NORM = 0.159154943;  // 1/sqrt(2*PI)^2
        private int numberOfEmInterations = 5; // EM is not running until convergence. EM is running specified number of iterations

        public EM_algorithm(int clusters, Bitmap image, double[, ,] likelihoodArr)
        {
            this.clusters = clusters;
            this.image = image;
            this.pixels = image.Height * image.Width; //total number of pixels
            standartDeviation = new double[clusters]; //sigma
            mean = new double[clusters]; //meuw
            likelihood = likelihoodArr; //3 dimentional matrix 
        }

        private void maximizationStep()
        {
            for (int c = 0; c < clusters; c++)
            {
                double sum = 0.0;
                double meanSum = 0.0;
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        sum += likelihood[c, i, j];
                        meanSum += likelihood[c, i, j] * image.GetPixel(i, j).R;
                    }
                }

                standartDeviation[c] = sum / (image.Height * image.Width);

                if (meanSum == 0.0 && sum == 0.0)
                    mean[c] = 0.0;
                else
                    mean[c] = meanSum / sum;
            }
        }

        //Compute likelihood
        private void expectationStep()
        {
            double normdist;

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    for (int c = 0; c < clusters; c++)
                    {
                        normdist = NORM * Math.Exp(-((image.GetPixel(i, j).R - mean[c]) * (image.GetPixel(i, j).R - mean[c])) / 2.0);
                        likelihood[c, i, j] = normdist * standartDeviation[c] / totalnormdist(image.GetPixel(i, j).R);
                    }
                }
            }
        }

        private double totalnormdist(int pixel)
        {
            double totalnormdist = 0.0;
            for (int c = 0; c < clusters; c++)
            {
                totalnormdist += standartDeviation[c] * NORM * Math.Exp(-((pixel - mean[c]) * (pixel - mean[c])) / 2.0);
            }
            return totalnormdist;
        }

        //Associate each pixel merely to the cluster with the highest likelihood
        private void preventScatter()
        {
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    int maxCluster = findMax(i, j);

                    for (int c = 0; c < clusters; c++)
                    {
                        if (maxCluster == c)
                            likelihood[c, i, j] = 1.0;
                        else
                            likelihood[c, i, j] = 0.0;
                    }
                }
            }
        }

        //Find the cluster with the maximum likelihood for a specific pixel (i,j)
        private int findMax(int row, int col)
        {
            double max = 0.0;
            int cluster = 0;
            for (int c = 0; c < clusters; c++)
            {
                if (likelihood[c, row, col] > max)
                {
                    max = likelihood[c, row, col];
                    cluster = c;
                }
            }
            return cluster;
        }

        //Calculates the average value of a cluster
        private double averageValue(int cluster)
        {
            double avg = 0.0;
            int numOfPixels = 0;
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    if (likelihood[cluster, i, j] == 1.0)
                    {
                        avg = avg + image.GetPixel(i, j).R;
                        numOfPixels += 1;

                    }
                }
            }
            return avg / (double)numOfPixels;
        }

        //Update the color of each pixel to the average color of the it's cluster
        private Bitmap updatePixels()
        {
            Color[] colorArr = { /*Color.Red, Color.Yellow, Color.Blue,*/ Color.Green, Color.Black, Color.Purple , Color.Coral, Color.DarkOrange, Color.AliceBlue};

            Bitmap temp = new Bitmap(image);
            for (int c = 0; c < clusters; c++)
            {
                double avg = averageValue(c);
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        if (likelihood[c, i, j] == 1.0)
                        {
                            //temp.SetPixel(i, j, Color.FromArgb(255, (int)avg, (int)avg, (int)avg));
                            temp.SetPixel(i, j, colorArr[c]);

                        }
                    }
                }
            }
            return temp;
        }

        //Run the algorithm
        public Bitmap run(int iterations)
        {
            if (iterations == -1)
            {
                double[, ,] prevLikelihood = new double[clusters, image.Height, image.Width];
                double mismatch = 0;
                double epsilon = 1;

                while (epsilon > 0.01)
                {
                    for (int i = 0; i < image.Height; i++)
                    {
                        for (int j = 0; j < image.Width; j++)
                        {
                            for (int c = 1; c < clusters; c++)
                            {
                                prevLikelihood[c, i, j] = likelihood[c, i, j];
                            }
                        }
                    }
                    maximizationStep();
                    expectationStep();
                    preventScatter();

                    for (int i = 0; i < image.Height; i++)
                    {
                        for (int j = 0; j < image.Width; j++)
                        {
                            for (int c = 1; c < clusters; c++)
                            {
                                if (prevLikelihood[c, i, j] != likelihood[c, i, j])
                                {
                                    mismatch = mismatch + 1.0;
                                }
                            }
                        }
                    }
                    epsilon = (mismatch / 2) / pixels;
                    mismatch = 0;
                }
            }

            else
            {
                //printLikelihood("initialized");
                for (int i = 0; i < iterations; i++)
                {
                    maximizationStep();
                    expectationStep();
                    preventScatter();

                }

            }
            //printLikelihood("beforePrevent");
            //preventScatter();
            Bitmap result = updatePixels();
            return result;
        }

        public Tuple<double[],double[]> ReturnMeaAndStDeviation()
        {
            this.run(this.numberOfEmInterations);
            return Tuple.Create(this.mean, this.standartDeviation);
        }
    }
}
