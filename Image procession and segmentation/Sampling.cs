using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_procession_and_segmentation
{
    class Sampling
    {
        private Histogram imageHistogram;
        private Random randomized;
        public int[] sumOfHistogramPeaks; //contain the cumulative pixel count for each color
        public int[] histogramSamples; //contain 50 randomized samples of image histogram


        public Sampling(Histogram imageHistogram)
        {
            this.imageHistogram = imageHistogram;
            this.histogramSamples = new int[50];
            this.randomized = new Random();

            this.GetHistogramSamples();
            this.CalculateCumulativeSumOfHistogramPeaks();
        }

        public void CalculateCumulativeSumOfHistogramPeaks() // calculates the cumulative pixel count for each color
        // for example: SumOfHistogramPeaks[i] = Sum(imageHistogram[0] to imageHistogram[i])
        {
            this.sumOfHistogramPeaks = new int[256];
            this.sumOfHistogramPeaks[0] = this.imageHistogram.openedImageHistogramArray[0];

            for (int i = 1; i < this.imageHistogram.openedImageHistogramArray.Length; i++)
                this.sumOfHistogramPeaks[i] = this.sumOfHistogramPeaks[i - 1] + this.imageHistogram.openedImageHistogramArray[i];
        }

        public void GetHistogramSamples()
        {
            for (int i = 0; i < 50; i++ )
                this.histogramSamples[i] = this.imageHistogram.openedImageHistogramArray[randomized.Next(0, 255)];          
        }


    }
}
