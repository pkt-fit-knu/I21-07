using System;
using System.Drawing;

namespace ImageToHistogram
{
    static internal class BitmapExtensions
    {
        public static Bitmap Resize(this Bitmap specifiedBitmap, int newWidth, int newHeight)
        {
            Bitmap resizedTemp = new Bitmap(newWidth, newHeight, specifiedBitmap.PixelFormat);

            double XFactor = (double)specifiedBitmap.Width / newWidth;
            double YFactor = (double)specifiedBitmap.Height / newHeight;

            for (int x = 0; x < resizedTemp.Width; ++x)
            {
                for (int y = 0; y < resizedTemp.Height; ++y)
                {
                    resizedTemp.SetPixel(x, y, specifiedBitmap.GetPixel((int)(Math.Floor(x * XFactor)), (int)(Math.Floor(y * YFactor))));
                }
            }

            return resizedTemp;
        }
    }
}
