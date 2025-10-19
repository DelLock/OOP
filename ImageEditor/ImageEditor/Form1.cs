using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class Form1 : Form
    {
        private Bitmap currentImage;
        private Bitmap originalImage;
        private Stack<Bitmap> undoStack;
        private Stack<Bitmap> redoStack;
        private bool isDrawing;
        private Point previousPoint;
        private Color drawingColor;
        private int penWidth;
        private Rectangle cropRectangle;
        private bool isCropping;
        private string currentTool;

        public Form1()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            undoStack = new Stack<Bitmap>();
            redoStack = new Stack<Bitmap>();
            drawingColor = Color.Black;
            penWidth = 3;
            currentTool = "Pencil";

            KeyPreview = true;
            KeyDown += MainForm_KeyDown;
        }


        private void Redo_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(new Bitmap(currentImage));
                currentImage = redoStack.Pop();
                pictureBox1.Image = currentImage;
                pictureBox1.Invalidate();
            }
        }
        private void SaveCurrentState()
        {
            if (currentImage != null)
            {
                undoStack.Push(new Bitmap(currentImage));
                redoStack.Clear();
            }
        }

        private void NewFile_Click(object sender, EventArgs e)
        {
            SaveCurrentState();
            currentImage = new Bitmap(800, 600);
            using (Graphics g = Graphics.FromImage(currentImage))
            {
                g.Clear(Color.White);
            }
            originalImage = new Bitmap(currentImage);
            pictureBox1.Image = currentImage;
            pictureBox1.Invalidate();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveCurrentState();
                currentImage = new Bitmap(openFileDialog1.FileName);
                originalImage = new Bitmap(currentImage);
                pictureBox1.Image = currentImage;
                pictureBox1.Size = currentImage.Size;
            }
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageFormat format = ImageFormat.Png;
                switch (Path.GetExtension(saveFileDialog1.FileName).ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }

                currentImage.Save(saveFileDialog1.FileName, format);
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                redoStack.Push(new Bitmap(currentImage));
                currentImage = undoStack.Pop();
                pictureBox1.Image = currentImage;
                pictureBox1.Invalidate();
            }
        }

        private void RotateImage(float angle)
        {
            if (currentImage == null) return;

            SaveCurrentState();

            Bitmap rotated = new Bitmap(currentImage.Height, currentImage.Width);
            using (Graphics g = Graphics.FromImage(rotated))
            {
                g.TranslateTransform(rotated.Width / 2, rotated.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-currentImage.Width / 2, -currentImage.Height / 2);
                g.DrawImage(currentImage, Point.Empty);
            }

            currentImage = rotated;
            pictureBox1.Image = currentImage;
            pictureBox1.Size = currentImage.Size;
        }

        private void Rotate90_Click(object sender, EventArgs e)
        {
            RotateImage(90);
        }

        private void Rotate180_Click(object sender, EventArgs e)
        {
            RotateImage(180);
        }

        private void Rotate270_Click(object sender, EventArgs e)
        {
            RotateImage(270);
        }

        private void Crop_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            isCropping = !isCropping;
            if (isCropping)
            {
                currentTool = "Crop";
            }
            else
            {
                currentTool = "Pencil";
            }
        }

        private void ApplyBrightness(int brightness)
        {
            if (currentImage == null) return;

            Bitmap tempImage = new Bitmap(currentImage);
            BitmapData bmpData = tempImage.LockBits(new Rectangle(0, 0, tempImage.Width, tempImage.Height),
                                                   ImageLockMode.ReadWrite, tempImage.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * tempImage.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int counter = 0; counter < rgbValues.Length; counter++)
            {
                int value = rgbValues[counter] + brightness;
                rgbValues[counter] = (byte)Math.Max(0, Math.Min(255, value));
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            tempImage.UnlockBits(bmpData);

            currentImage = tempImage;
            pictureBox1.Image = currentImage;
        }

        private void BrightnessFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            using (BrightnessDialog dialog = new BrightnessDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SaveCurrentState();
                    ApplyBrightness(dialog.BrightnessValue);
                }
            }
        }

        private void ApplyBlur()
        {
            if (currentImage == null) return;

            Bitmap blurred = new Bitmap(currentImage.Width, currentImage.Height);

            for (int x = 1; x < currentImage.Width - 1; x++)
            {
                for (int y = 1; y < currentImage.Height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    int count = 0;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            Color pixel = currentImage.GetPixel(x + i, y + j);
                            r += pixel.R;
                            g += pixel.G;
                            b += pixel.B;
                            count++;
                        }
                    }

                    r /= count;
                    g /= count;
                    b /= count;

                    blurred.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            currentImage = blurred;
            pictureBox1.Image = currentImage;
        }

        private void BlurFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            SaveCurrentState();
            ApplyBlur();
        }

        private void ApplySepia()
        {
            if (currentImage == null) return;

            Bitmap tempImage = new Bitmap(currentImage);

            for (int x = 0; x < tempImage.Width; x++)
            {
                for (int y = 0; y < tempImage.Height; y++)
                {
                    Color pixel = tempImage.GetPixel(x, y);

                    int tr = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int tg = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int tb = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);

                    tr = Math.Min(255, tr);
                    tg = Math.Min(255, tg);
                    tb = Math.Min(255, tb);

                    tempImage.SetPixel(x, y, Color.FromArgb(tr, tg, tb));
                }
            }

            currentImage = tempImage;
            pictureBox1.Image = currentImage;
        }

        private void SepiaFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            SaveCurrentState();
            ApplySepia();
        }

        private void ApplyInvert()
        {
            if (currentImage == null) return;

            Bitmap tempImage = new Bitmap(currentImage);

            for (int x = 0; x < tempImage.Width; x++)
            {
                for (int y = 0; y < tempImage.Height; y++)
                {
                    Color pixel = tempImage.GetPixel(x, y);
                    tempImage.SetPixel(x, y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }

            currentImage = tempImage;
            pictureBox1.Image = currentImage;
        }

        private void InvertFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            SaveCurrentState();
            ApplyInvert();
        }

        private void ApplyGrayscale()
        {
            if (currentImage == null) return;

            Bitmap tempImage = new Bitmap(currentImage);

            for (int x = 0; x < tempImage.Width; x++)
            {
                for (int y = 0; y < tempImage.Height; y++)
                {
                    Color pixel = tempImage.GetPixel(x, y);
                    int gray = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                    tempImage.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }

            currentImage = tempImage;
            pictureBox1.Image = currentImage;
        }

        private void GrayscaleFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            SaveCurrentState();
            ApplyGrayscale();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentImage == null) return;

            if (e.Button == MouseButtons.Left)
            {
                if (currentTool == "Crop")
                {
                    cropRectangle = new Rectangle(e.Location, Size.Empty);
                }
                else if (currentTool == "Pencil")
                {
                    isDrawing = true;
                    previousPoint = e.Location;
                    SaveCurrentState();
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentImage == null) return;

            if (currentTool == "Crop" && e.Button == MouseButtons.Left)
            {
                int x = Math.Min(cropRectangle.X, e.X);
                int y = Math.Min(cropRectangle.Y, e.Y);
                int width = Math.Abs(cropRectangle.X - e.X);
                int height = Math.Abs(cropRectangle.Y - e.Y);

                cropRectangle = new Rectangle(x, y, width, height);
                pictureBox1.Invalidate();
            }
            else if (currentTool == "Pencil" && isDrawing && e.Button == MouseButtons.Left)
            {
                using (Graphics g = Graphics.FromImage(currentImage))
                {
                    using (Pen pen = new Pen(drawingColor, penWidth))
                    {
                        g.DrawLine(pen, previousPoint, e.Location);
                    }
                }

                previousPoint = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;

            if (currentTool == "Crop" && !cropRectangle.IsEmpty && cropRectangle.Width > 0 && cropRectangle.Height > 0)
            {
                SaveCurrentState();
                ApplyCrop();
            }
        }

        private void ApplyCrop()
        {
            if (cropRectangle.Width > 0 && cropRectangle.Height > 0)
            {
                Bitmap cropped = new Bitmap(cropRectangle.Width, cropRectangle.Height);
                using (Graphics g = Graphics.FromImage(cropped))
                {
                    g.DrawImage(currentImage, new Rectangle(0, 0, cropped.Width, cropped.Height),
                               cropRectangle, GraphicsUnit.Pixel);
                }
                currentImage = cropped;
                cropRectangle = Rectangle.Empty;
                pictureBox1.Image = currentImage;
                pictureBox1.Size = currentImage.Size;
                currentTool = "Pencil";
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (currentTool == "Crop" && !cropRectangle.IsEmpty)
            {
                using (Pen cropPen = new Pen(Color.Red, 2))
                {
                    cropPen.DashStyle = DashStyle.Dash;
                    e.Graphics.DrawRectangle(cropPen, cropRectangle);
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                Undo_Click(sender, e);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFile_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFile_Click(sender, e);
        }
    }
}