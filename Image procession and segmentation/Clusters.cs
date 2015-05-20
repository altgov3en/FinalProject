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
        private Bitmap openedImage;
        public Bitmap imageAfterEM;
        private int[] cl;
        private Histogram histogram;
        private EM_algorithm EMA;

        public double[, ,] likelihood;   //Likelihood matrix



        public Clusters(int n, int h, int w, Histogram imageHistogram, Bitmap source, bool doingEstimatioOrNot) //constructor
        {
            this.numberOfClusters = n;
            this.imageHeight = h;
            this.imageWidth = w;
            this.histogram = imageHistogram;
            this.openedImage = source;
            this.cl = new int[this.numberOfClusters];
            this.likelihood = new double[this.numberOfClusters, this.imageHeight, this.imageWidth];
            this.estimatingNumberOfClasters = doingEstimatioOrNot;

            this.GetClusterColor(this.numberOfClusters);
            this.InitiateLikelihood();

            this.EMA = new EM_algorithm(this.numberOfClusters, this.openedImage, this.likelihood);
            this.imageAfterEM = this.EMA.run(1); //!!!MUST BE FOUND BY BOOK!!!


        }
        private void GetClusterColor(int numberOfClusters)
        {
            float[] imageHistogramAVG;

            if(this.estimatingNumberOfClasters == true)
                imageHistogramAVG = CalculateAverageHistogram(this.histogram.histogramSamples);
            else
                imageHistogramAVG = CalculateAverageHistogram(this.histogram.openedImageHistogramArray);

            this.cl[0] = 0;
            int i = 1;
            int treshold = 70;
            int sum = this.imageHeight * this.imageWidth;

            while (numberOfClusters != 1)
            {
                this.cl[i] = this.RunOtsu(sum, this.cl[i - 1]);
                numberOfClusters--;
                sum = 0;
                for (int k = this.cl[i]; k < this.histogram.openedImageHistogramArray.Length; k++)
                {
                    sum = sum + this.histogram.openedImageHistogramArray[k];
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
                        if (Math.Abs(((int)this.openedImage.GetPixel(i, j).R) - cl[c]) < minDist)
                        {
                            minDist = this.cl[c];
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
