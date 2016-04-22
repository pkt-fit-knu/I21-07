using System.Drawing;

namespace LaplacianSharper
{
    static class LaplacianSharper
    {
        private static int[,] pixelArray;

        private static int width;
        private static int height;

        public static Bitmap SharpImage(Bitmap bitmap, Mask mask)
        {
            GetIntensities(bitmap);

            SubstractImage(ImposeMask(mask));

            ModifyImage();

            return CreateBitmap();
        }

        private static void GetIntensities(Bitmap bitmap)
        {
            pixelArray = new int[bitmap.Width + 4, bitmap.Height + 4];

            width = pixelArray.GetLength(0);
            height = pixelArray.GetLength(1);

            for (int x = 2; x < width - 2; x++)
            {
                for (int y = 2; y < height - 2; y++)
                {
                    pixelArray[x, y] = (int)(RGBConstants.RedMultiplier * bitmap.GetPixel(x - 2, y - 2).R +
                                             RGBConstants.GreenMultiplier * bitmap.GetPixel(x - 2, y - 2).G +
                                             RGBConstants.BlueMultiplier * bitmap.GetPixel(x - 2, y - 2).B);
                }
            }
        }

        private static int[,] ImposeMask(Mask mask)
        {
            int[,] imageWithMask = new int[width, height];

            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    imageWithMask[x, y] = pixelArray[x - 1, y - 1] * mask.GetCell(0, 0) +
                                          pixelArray[x, y - 1] * mask.GetCell(1, 0) +
                                          pixelArray[x + 1, y - 1] * mask.GetCell(2, 0) +
                                          pixelArray[x - 1, y] * mask.GetCell(0, 1) +
                                          pixelArray[x, y] * mask.GetCell(1, 1) +
                                          pixelArray[x + 1, y] * mask.GetCell(2, 1) +
                                          pixelArray[x - 1, y + 1] * mask.GetCell(0, 2) +
                                          pixelArray[x, y + 1] * mask.GetCell(1, 2) +
                                          pixelArray[x + 1, y + 1] * mask.GetCell(2, 2);
                }
            }

            return imageWithMask;
        }

        private static void SubstractImage(int[,] imageWithMask)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    pixelArray[x, y] -= imageWithMask[x, y];
                }
            }
        }

        private static void ModifyImage()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (pixelArray[x, y] < 0)
                    {
                        pixelArray[x, y] = 0;
                    }

                    if (pixelArray[x, y] > 255)
                    {
                        pixelArray[x, y] = 255;
                    }
                }
            }
        }

        private static Bitmap CreateBitmap()
        {
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int color = pixelArray[x, y];

                    bitmap.SetPixel(x, y, Color.FromArgb(color, color, color));
                }
            }

            return bitmap;
        }
    }
}