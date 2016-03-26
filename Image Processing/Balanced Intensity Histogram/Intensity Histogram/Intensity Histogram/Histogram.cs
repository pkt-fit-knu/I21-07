using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace ImageToHistogram
{
    internal class Histogram
    {
        private int[] intensities;

        public Histogram()
        {
            this.intensities = new int[256];
        }

        public void SetIntensities(Bitmap image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    int intensity = (int)(RGBConstants.RedMultiplier * image.GetPixel(x, y).R +
                                          RGBConstants.GreenMultiplier * image.GetPixel(x, y).G +
                                          RGBConstants.BlueMultiplier * image.GetPixel(x, y).B);

                    this.intensities[intensity]++;
                }
            }
        }

        public Bitmap BalanceImage(Bitmap image)
        {
            int originalRedBrightness;
            int originalGreenBrightness;
            int originalBlueBrightness;

            double newRedBrightness;
            double newGreenBrightness;
            double newBlueBrightness;

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    originalRedBrightness = image.GetPixel(x, y).R;
                    newRedBrightness = 0;

                    for (int i = 0; i <= originalRedBrightness; i++)
                    {
                        newRedBrightness += (double)this.intensities[i] / (image.Width * image.Height);
                    }

                    newRedBrightness *= 255;

                    originalGreenBrightness = image.GetPixel(x, y).G;
                    newGreenBrightness = 0;

                    for (int i = 0; i <= originalGreenBrightness; i++)
                    {
                        newGreenBrightness += (double)this.intensities[i] / (image.Width * image.Height);
                    }

                    newGreenBrightness *= 255;

                    originalBlueBrightness = image.GetPixel(x, y).B;
                    newBlueBrightness = 0;

                    for (int i = 0; i <= originalBlueBrightness; i++)
                    {
                        newBlueBrightness += (double)this.intensities[i] / (image.Width * image.Height);
                    }

                    newBlueBrightness *= 255;

                    image.SetPixel(x, y, Color.FromArgb((int)newRedBrightness, (int)newGreenBrightness, (int)newBlueBrightness));
                }
            }

            return image;
        }

        public void DrawHistogram(Chart chart)
        {
            chart.Series["Intensity"].Points.Clear();

            for (int i = 0; i < this.intensities.Length; i++)
            {
                DataPoint dataPoint = new DataPoint();

                dataPoint.SetValueXY(i, this.intensities[i]);

                chart.Series["Intensity"].Points.Add(dataPoint);
            }
        }
    }
}
