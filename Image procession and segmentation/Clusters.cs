using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_procession_and_segmentation
{

    class Clusters
    {
        private int imageHeight;
        private int imageWidth;
        public int numberOfClusters;

        private bool estimatingNumberOfClasters; // Indicates if the program is in "Estimation of clusters number" phase
                                                 // which mean that the program is one step before the "Segmentation" phase.
                                                 // "Estimation of clusters number" will give us the approximate number of clusters (for example X).
                                                 // Meaning than the original picture can be devided into X separate clusters.
        private Bitmap openedImage; // The image that is processed.
        public Bitmap imageAfterEM; // Processed image.

        private int[] clusterTresholds; // Containing the treshold for every cluster.
                    // If the pixel's color is above the treshold it will be belong to the cluster,
                                                                          // otherwise it will not.
        private Histogram histogram; // Histogram of the image.

        private EM_algorithm[] emClusterEstimation; // Array of kMax EM algorithms (EMA).
        // Each instance of EMA will devide the image to specified number of clusters.

        private EM_algorithm EMA;
        private EM_algorithm emForClusterNumEstimation;

        private int kMax = 5;//Arbitrary number of maximum number of clusters.
        //Program using kMax to make estimation of cluster number.
        //Program will divide the image using EM algorithminto 2,3,4,...,kMax clusters 
        //and save the results to furher analysis.


        public double[, ,] likelihood;   // Likelihood matrix[numberOfClusters,imageHeight, imageWidht]
        // In simple words: likelihood will contain kMax copies of the image.
        // For each pixel (i,j) likelihood array will indicates to which cluster this pixel is belongs.
        // For example: likelihood[x,i,j] = 1 or 0 (0-pixel i,j dont belongs to cluster x)
        //                                         (1-pixel i,j belongs to cluster x).

        public Clusters(int n, int h, int w,
                        Histogram imageHistogram,
                        Bitmap source, int iterationNumber,
                        bool doingEstimatioOrNot) //Constructor1 for cluster number estimation.
                             //If you are here it means you are doing cluster number estimation.
        {
            this.emClusterEstimation = new EM_algorithm[this.kMax];
            this.numberOfClusters = n; //The image will dived into "numberOfClusters" clusters.
            this.imageHeight = h;
            this.imageWidth = w;
            this.histogram = imageHistogram; //Histogram of the image.
            this.openedImage = source; //The image that is processed. 
            this.clusterTresholds = new int[this.numberOfClusters]; // See the explanation at the definition

            this.likelihood = new double[this.kMax + 1, this.imageHeight, this.imageWidth]; //must be n not kMax!?!?!!?

            this.estimatingNumberOfClasters = doingEstimatioOrNot; // Indicates if the program doing cluster number estimations

            this.GetClusterColor(this.numberOfClusters);
            this.InitiateLikelihood();

            this.emForClusterNumEstimation = new EM_algorithm(iterationNumber+2, this.openedImage, this.likelihood);         
        }//Constructor1

        public Clusters(int n, int h, int w, Histogram imageHistogram, Bitmap source) //Constructor2 for clustering algorithm.
                              //If you're here it means you are doing an image clusterization to estimated number of clusters.
        {
            this.numberOfClusters = n;
            this.imageHeight = h; 
            this.imageWidth = w;
            this.histogram = imageHistogram;
            this.openedImage = source;
            this.clusterTresholds = new int[this.numberOfClusters];
            this.likelihood = new double[this.kMax+1, this.imageHeight, this.imageWidth];
            this.GetClusterColor(this.numberOfClusters);
            this.InitiateLikelihood();
            /////////////////////////
            //this.EMA = new EM_algorithm(6, this.openedImage, this.likelihood);
            //this.imageAfterEM = this.EMA.run(5);
        }//Constructor2

        public Tuple<double[], double[]> estimateClusterNumber()
        {
            return this.emForClusterNumEstimation.ReturnMeaAndStDeviation();
        }
        private void GetClusterColor(int numberOfClusters)
        {
            float[] imageHistogramAVG;

            if(this.estimatingNumberOfClasters == true)
                imageHistogramAVG = CalculateAverageHistogram(this.histogram.histogramSamples);
            else
                imageHistogramAVG = CalculateAverageHistogram(this.histogram.openedImageHistogramArray);

            this.clusterTresholds[0] = 0;
            int i = 1;
            int treshold = 70;
            int sum = this.imageHeight * this.imageWidth;

            while (numberOfClusters != 1)
            {
                this.clusterTresholds[i] = this.RunOtsu(sum, this.clusterTresholds[i - 1]);
                numberOfClusters--;
                sum = 0;
                if (this.estimatingNumberOfClasters == true) 
                {   // Working with sampled histogram (only 50 colors are randomly presented)
                    for (int k = this.clusterTresholds[i]; k < this.histogram.histogramSamples.Length; k++)
                    {
                        sum = sum + this.histogram.histogramSamples[k];
                    }
                }
                else
                {   // Working with regular histogram (all 256 colors are preseted)
                    for (int k = this.clusterTresholds[i]; k < this.histogram.openedImageHistogramArray.Length; k++)
                    {
                        sum = sum + this.histogram.openedImageHistogramArray[k];
                    }
                }

                i++;
            }
            int maxValue = (int)GetMaxValue(imageHistogramAVG, treshold / 8);

            while (NumOfDesClusters(imageHistogramAVG, maxValue, treshold / 8) < numberOfClusters || maxValue == 0)
            {
                maxValue = maxValue - 1;
            }
            int[] clustersArr = GetClusters(imageHistogramAVG, maxValue, numberOfClusters, treshold / 8);
        }
        private void InitiateLikelihood()
        {
            for (int i = 0; i < this.imageHeight; i++)
            {
                for (int j = 0; j < this.imageWidth; j++)
                {
                    int minDist = 256;
                    int mostFit = 0;
                    for (int c = 1; c < this.numberOfClusters; c++)
                    {
                        if (Math.Abs(((int)this.openedImage.GetPixel(i, j).R) - this.clusterTresholds[c]) < minDist)
                        {
                            minDist = this.clusterTresholds[c];
                            mostFit = c;
                        }
                    }
                    this.likelihood[mostFit, i, j] = 1.0;
                }
            }
        }
        private int GetMaxValue(float[] imageHistogramAVG, int thBottom)
        {
            int max = 0;
            for (int i = thBottom; i < imageHistogramAVG.Length; i++)
            {
                if (imageHistogramAVG[i] > imageHistogramAVG[max])
                    max = i;
            }

            return (int)imageHistogramAVG[max];
        }
        private int NumOfDesClusters(float[] imageHistogramAVG, int thValue, int thBottom)
        {
            int cnt = 0;

            for (int i = thBottom; i < imageHistogramAVG.Length; i++)
            {
                if (imageHistogramAVG[i] > thValue)
                    cnt++;
            }

            return cnt;
        }
        private int[] GetClusters(float[] imageHistogramAVG, int thValue, int cnum, int thBottom)
        {
            int[] clusters = new int[cnum];
            clusters[0] = 0;
            int j = 1;

            for (int i = thBottom; i < imageHistogramAVG.Length && j < cnum; i++)
            {
                if (imageHistogramAVG[i] >= thValue)
                {
                    clusters[j] = i * 8 + 4;
                    j++;
                }
            }

            return clusters;
        }
        private float[] CalculateAverageHistogram(int[] imageHistogram)
        {
            float[] avgArray = new float[32];
            int sumOf8Pixels = 0; //contain sum of pixels for the eight consecutive colors
            //0-7, 8-15, 16-23, ..., 247-255

            for (int i = 0; i < 32; i++)
            {
                for (int j = i * 8; j < i * 8 + 8; j++)
                {
                    //sumOf8Pixels = sumOf8Pixels + imageHistogram[j];
                    sumOf8Pixels = sumOf8Pixels + imageHistogram[j];
                }

                avgArray[i] = sumOf8Pixels / 8;
                sumOf8Pixels = 0;
            }

            return avgArray;
        }
        public int RunOtsu(int total, int bottom)
        {
            int sum = 0;
            for (int i = bottom; i < this.histogram.openedImageHistogramArray.Length; ++i)
                sum += i * this.histogram.openedImageHistogramArray[i];

            int sumB = 0;
            int weightOfBackground = 0;
            int weightOfForeground = 0;

            double meanOfBackground;
            double meanOfForeground;

            double max = 0;     //Hold the max between class variance
            double between = 0; //class variance
            double treshold1 = 0;
            double treshold2 = 0;
            for (int i = bottom; i < this.histogram.openedImageHistogramArray.Length; ++i) //step through all possible thresholds (1 to maximum intensity)
            {
                weightOfBackground += this.histogram.openedImageHistogramArray[i];

                if (weightOfBackground == 0)
                    continue;

                weightOfForeground = total - weightOfBackground;

                if (weightOfForeground == 0)
                    break;

                sumB += i * this.histogram.openedImageHistogramArray[i];
                meanOfBackground = sumB / weightOfBackground;
                meanOfForeground = (sum - sumB) / weightOfForeground;
                between = weightOfBackground * weightOfForeground * Math.Pow(meanOfBackground - meanOfForeground, 2);   //fast approach
                if (between >= max)
                {
                    treshold1 = i;
                    if (between > max)
                    {
                        treshold2 = i;
                    }
                    max = between;
                }
            }
            return (int)((treshold1 + treshold2) / 2);
        }
    }
}
