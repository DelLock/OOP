using System.Drawing;
using System.Drawing.Imaging;

namespace ImageEditor
{
    public static class ImageProcessor
    {
        public static Bitmap ApplyBrightness(Bitmap image, int brightness)
        {
            Bitmap result = new Bitmap(image);
            BitmapData bmpData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height),
                                               ImageLockMode.ReadWrite, result.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * result.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int counter = 0; counter < rgbValues.Length; counter++)
            {
                int value = rgbValues[counter] + brightness;
                rgbValues[counter] = (byte)Math.Max(0, Math.Min(255, value));
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            result.UnlockBits(bmpData);

            return result;
        }

        public static Bitmap ApplyGaussianBlur(Bitmap image, int radius = 2)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);

            int size = radius * 2 + 1;
            double[,] kernel = CreateGaussianKernel(size, radius / 3.0);

            for (int x = radius; x < image.Width - radius; x++)
            {
                for (int y = radius; y < image.Height - radius; y++)
                {
                    double r = 0, g = 0, b = 0;

                    for (int i = -radius; i <= radius; i++)
                    {
                        for (int j = -radius; j <= radius; j++)
                        {
                            Color pixel = image.GetPixel(x + i, y + j);
                            double weight = kernel[i + radius, j + radius];

                            r += pixel.R * weight;
                            g += pixel.G * weight;
                            b += pixel.B * weight;
                        }
                    }

                    result.SetPixel(x, y, Color.FromArgb(
                        (int)Math.Max(0, Math.Min(255, r)),
                        (int)Math.Max(0, Math.Min(255, g)),
                        (int)Math.Max(0, Math.Min(255, b))
                    ));
                }
            }

            return result;
        }

        private static double[,] CreateGaussianKernel(int size, double sigma)
        {
            double[,] kernel = new double[size, size];
            double sum = 0.0;
            int half = size / 2;

            for (int x = -half; x <= half; x++)
            {
                for (int y = -half; y <= half; y++)
                {
                    kernel[x + half, y + half] = Math.Exp(-(x * x + y * y) / (2 * sigma * sigma));
                    sum += kernel[x + half, y + half];
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    kernel[i, j] /= sum;
                }
            }

            return kernel;
        }

        public static Bitmap RotateImage(Bitmap image, float angle)
        {
            Bitmap rotated = new Bitmap(image.Height, image.Width);

            using (Graphics g = Graphics.FromImage(rotated))
            {
                g.TranslateTransform(rotated.Width / 2, rotated.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-image.Width / 2, -image.Height / 2);
                g.DrawImage(image, Point.Empty);
            }

            return rotated;
        }
    }
}