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
