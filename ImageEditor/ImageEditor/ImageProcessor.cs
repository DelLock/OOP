using System;
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
            Bitmap rotated = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(rotated))
            {
                g.TranslateTransform(image.Width / 2, image.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-image.Width / 2, -image.Height / 2);
                g.DrawImage(image, new Point(0, 0));
            }

            return rotated;
        }

        public static Bitmap ApplySepia(Bitmap image)
        {
            Bitmap result = new Bitmap(image);

            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    Color pixel = result.GetPixel(x, y);

                    int tr = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int tg = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int tb = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);

                    tr = Math.Min(255, tr);
                    tg = Math.Min(255, tg);
                    tb = Math.Min(255, tb);

                    result.SetPixel(x, y, Color.FromArgb(tr, tg, tb));
                }
            }

            return result;
        }

        public static Bitmap ApplyInvert(Bitmap image)
        {
            Bitmap result = new Bitmap(image);

            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    Color pixel = result.GetPixel(x, y);
                    result.SetPixel(x, y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }

            return result;
        }

        public static Bitmap ApplyGrayscale(Bitmap image)
        {
            Bitmap result = new Bitmap(image);

            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    Color pixel = result.GetPixel(x, y);
                    int gray = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                    result.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }

            return result;
        }

        public static Bitmap ApplyContrast(Bitmap image, int contrast)
        {
            Bitmap result = new Bitmap(image);
            double contrastFactor = (100.0 + contrast) / 100.0;
            contrastFactor *= contrastFactor;

            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    Color pixel = result.GetPixel(x, y);

                    double r = pixel.R / 255.0;
                    r -= 0.5;
                    r *= contrastFactor;
                    r += 0.5;
                    r *= 255;
                    r = Math.Max(0, Math.Min(255, r));

                    double g = pixel.G / 255.0;
                    g -= 0.5;
                    g *= contrastFactor;
                    g += 0.5;
                    g *= 255;
                    g = Math.Max(0, Math.Min(255, g));

                    double b = pixel.B / 255.0;
                    b -= 0.5;
                    b *= contrastFactor;
                    b += 0.5;
                    b *= 255;
                    b = Math.Max(0, Math.Min(255, b));

                    result.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                }
            }

            return result;
        }

        public static Bitmap ApplySaturation(Bitmap image, int saturation)
        {
            Bitmap result = new Bitmap(image);
            double saturationFactor = saturation / 100.0;

            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    Color pixel = result.GetPixel(x, y);

                    // Convert to HSL
                    double r = pixel.R / 255.0;
                    double g = pixel.G / 255.0;
                    double b = pixel.B / 255.0;

                    double max = Math.Max(r, Math.Max(g, b));
                    double min = Math.Min(r, Math.Min(g, b));
                    double h, s, l = (max + min) / 2.0;

                    if (max == min)
                    {
                        h = s = 0; // achromatic
                    }
                    else
                    {
                        double d = max - min;
                        s = l > 0.5 ? d / (2 - max - min) : d / (max + min);

                        if (max == r) h = (g - b) / d + (g < b ? 6 : 0);
                        else if (max == g) h = (b - r) / d + 2;
                        else h = (r - g) / d + 4;

                        h /= 6;
                    }

                    // Adjust saturation
                    s = Math.Max(0, Math.Min(1, s * (1 + saturationFactor)));

                    // Convert back to RGB
                    if (s == 0)
                    {
                        r = g = b = l;
                    }
                    else
                    {
                        double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                        double p = 2 * l - q;

                        r = HueToRGB(p, q, h + 1.0 / 3);
                        g = HueToRGB(p, q, h);
                        b = HueToRGB(p, q, h - 1.0 / 3);
                    }

                    int red = (int)(r * 255);
                    int green = (int)(g * 255);
                    int blue = (int)(b * 255);

                    red = Math.Max(0, Math.Min(255, red));
                    green = Math.Max(0, Math.Min(255, green));
                    blue = Math.Max(0, Math.Min(255, blue));

                    result.SetPixel(x, y, Color.FromArgb(red, green, blue));
                }
            }

            return result;
        }

        private static double HueToRGB(double p, double q, double t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (t < 1.0 / 6) return p + (q - p) * 6 * t;
            if (t < 1.0 / 2) return q;
            if (t < 2.0 / 3) return p + (q - p) * (2.0 / 3 - t) * 6;
            return p;
        }

        public static Bitmap ApplyVintage(Bitmap image)
        {
            Bitmap result = new Bitmap(image);

            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    Color pixel = result.GetPixel(x, y);

                    // Apply vintage effect (yellowish tint)
                    int r = (int)(pixel.R * 0.9);
                    int g = (int)(pixel.G * 0.8);
                    int b = (int)(pixel.B * 0.6);

                    // Add some noise for vintage look
                    Random rand = new Random((x * y) + x + y);
                    r = Math.Max(0, Math.Min(255, r + rand.Next(-10, 10)));
                    g = Math.Max(0, Math.Min(255, g + rand.Next(-10, 10)));
                    b = Math.Max(0, Math.Min(255, b + rand.Next(-10, 10)));

                    result.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return result;
        }

        public static Bitmap ApplyCool(Bitmap image)
        {
            Bitmap result = new Bitmap(image);

            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    Color pixel = result.GetPixel(x, y);

                    // Cool filter - increase blue, decrease red
                    int r = Math.Max(0, pixel.R - 10);
                    int g = pixel.G;
                    int b = Math.Min(255, pixel.B + 10);

                    result.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return result;
        }

        public static Bitmap ApplyWarm(Bitmap image)
        {
            Bitmap result = new Bitmap(image);

            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    Color pixel = result.GetPixel(x, y);

                    // Warm filter - increase red and yellow tones
                    int r = Math.Min(255, pixel.R + 10);
                    int g = Math.Min(255, pixel.G + 5);
                    int b = Math.Max(0, pixel.B - 5);

                    result.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return result;
        }
    }
}