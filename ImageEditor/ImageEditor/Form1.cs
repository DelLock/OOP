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
        private bool isEraser;
        private float zoomFactor = 1.0f;
        private Point panOffset = Point.Empty;
        private bool isPanning;
        private Point panStart;

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
            isEraser = false;
            zoomFactor = 1.0f;
            panOffset = Point.Empty;

            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            KeyPreview = true;
            KeyDown += MainForm_KeyDown;

            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            string toolStatus = isEraser ? "Eraser" : "Pencil";
            string zoomStatus = $"Zoom: {(zoomFactor * 100):F0}%";
            toolStripStatusLabel1.Text = $"{toolStatus} | {zoomStatus} | Pen: {penWidth}px";
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
            zoomFactor = 1.0f;
            panOffset = Point.Empty;
            UpdateImageDisplay();
            UpdateStatusBar();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveCurrentState();
                    currentImage = new Bitmap(openFileDialog1.FileName);
                    originalImage = new Bitmap(currentImage);
                    zoomFactor = 1.0f;
                    panOffset = Point.Empty;
                    UpdateImageDisplay();
                    UpdateStatusBar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving image: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                UpdateImageDisplay();
                UpdateStatusBar();
            }
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(new Bitmap(currentImage));
                currentImage = redoStack.Pop();
                UpdateImageDisplay();
                UpdateStatusBar();
            }
        }

        private void RotateImage(float angle)
        {
            if (currentImage == null) return;

            SaveCurrentState();

            Bitmap rotated = ImageProcessor.RotateImage(currentImage, angle);
            currentImage = rotated;
            zoomFactor = 1.0f;
            panOffset = Point.Empty;
            UpdateImageDisplay();
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
                isEraser = false;
            }
            else
            {
                currentTool = "Pencil";
            }
            UpdateStatusBar();
        }

        private void BrightnessFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            using (BrightnessDialog dialog = new BrightnessDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SaveCurrentState();
                    currentImage = ImageProcessor.ApplyBrightness(currentImage, dialog.BrightnessValue);
                    UpdateImageDisplay();
                }
            }
        }

        private void BlurFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            SaveCurrentState();
            currentImage = ImageProcessor.ApplyGaussianBlur(currentImage);
            UpdateImageDisplay();
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
            UpdateImageDisplay();
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
            UpdateImageDisplay();
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
            UpdateImageDisplay();
        }

        private void GrayscaleFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            SaveCurrentState();
            ApplyGrayscale();
        }

        // Drawing and Zooming Methods
        private void EnablePencil()
        {
            currentTool = "Pencil";
            isEraser = false;
            UpdateStatusBar();
        }

        private void EnableEraser()
        {
            currentTool = "Pencil";
            isEraser = true;
            UpdateStatusBar();
        }

        private void ChangeColor()
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = drawingColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    drawingColor = colorDialog.Color;
                    UpdateStatusBar();
                }
            }
        }

        private void IncreasePenSize()
        {
            penWidth = Math.Min(50, penWidth + 1);
            UpdateStatusBar();
        }

        private void DecreasePenSize()
        {
            penWidth = Math.Max(1, penWidth - 1);
            UpdateStatusBar();
        }

        private void ZoomIn()
        {
            zoomFactor *= 1.2f;
            if (zoomFactor > 5.0f) zoomFactor = 5.0f;
            UpdateImageDisplay();
            UpdateStatusBar();
        }

        private void ZoomOut()
        {
            zoomFactor /= 1.2f;
            if (zoomFactor < 0.1f) zoomFactor = 0.1f;
            UpdateImageDisplay();
            UpdateStatusBar();
        }

        private void ResetZoom()
        {
            zoomFactor = 1.0f;
            panOffset = Point.Empty;
            UpdateImageDisplay();
            UpdateStatusBar();
        }

        private void UpdateImageDisplay()
        {
            if (currentImage == null) return;

            if (Math.Abs(zoomFactor - 1.0f) < 0.01f)
            {
                pictureBox1.Image = currentImage;
                pictureBox1.Size = currentImage.Size;
            }
            else
            {
                int newWidth = (int)(currentImage.Width * zoomFactor);
                int newHeight = (int)(currentImage.Height * zoomFactor);

                Bitmap scaledImage = new Bitmap(newWidth, newHeight);
                using (Graphics g = Graphics.FromImage(scaledImage))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(currentImage, 0, 0, newWidth, newHeight);
                }
                pictureBox1.Image = scaledImage;
                pictureBox1.Size = scaledImage.Size;
            }
            pictureBox1.Invalidate();
        }

        private Point ConvertMouseCoordinates(Point mousePos)
        {
            if (currentImage == null || pictureBox1.Image == null)
                return mousePos;

            float scaleX = (float)currentImage.Width / pictureBox1.Image.Width;
            float scaleY = (float)currentImage.Height / pictureBox1.Image.Height;

            int x = (int)(mousePos.X * scaleX);
            int y = (int)(mousePos.Y * scaleY);

            x = Math.Max(0, Math.Min(currentImage.Width - 1, x));
            y = Math.Max(0, Math.Min(currentImage.Height - 1, y));

            return new Point(x, y);
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
                    previousPoint = ConvertMouseCoordinates(e.Location);
                    SaveCurrentState();
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                isPanning = true;
                panStart = e.Location;
                pictureBox1.Cursor = Cursors.SizeAll;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentImage == null) return;

            if (isPanning && e.Button == MouseButtons.Middle)
            {
                int deltaX = e.X - panStart.X;
                int deltaY = e.Y - panStart.Y;

                // Update scroll position
                panel1.AutoScrollPosition = new Point(
                    -panel1.AutoScrollPosition.X - deltaX,
                    -panel1.AutoScrollPosition.Y - deltaY);

                panStart = e.Location;
            }
            else if (currentTool == "Crop" && e.Button == MouseButtons.Left)
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
                Point currentPoint = ConvertMouseCoordinates(e.Location);

                using (Graphics g = Graphics.FromImage(currentImage))
                {
                    Color drawColor = isEraser ? Color.White : drawingColor;
                    using (Pen pen = new Pen(drawColor, penWidth))
                    {
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.DrawLine(pen, previousPoint, currentPoint);
                    }
                }

                previousPoint = currentPoint;
                UpdateImageDisplay();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;

            if (e.Button == MouseButtons.Middle)
            {
                isPanning = false;
                pictureBox1.Cursor = Cursors.Default;
            }

            if (currentTool == "Crop" && !cropRectangle.IsEmpty && cropRectangle.Width > 0 && cropRectangle.Height > 0)
            {
                SaveCurrentState();
                ApplyCrop();
            }
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                if (e.Delta > 0)
                    ZoomIn();
                else
                    ZoomOut();
            }
        }

        private void ApplyCrop()
        {
            if (cropRectangle.Width > 0 && cropRectangle.Height > 0)
            {
                try
                {
                    // Получаем реальные координаты на исходном изображении
                    Point sourceLocation = ConvertMouseCoordinates(cropRectangle.Location);

                    // Вычисляем реальные размеры области обрезки
                    float scaleX = (float)currentImage.Width / pictureBox1.Image.Width;
                    float scaleY = (float)currentImage.Height / pictureBox1.Image.Height;

                    int sourceWidth = (int)(cropRectangle.Width * scaleX);
                    int sourceHeight = (int)(cropRectangle.Height * scaleY);

                    // Ограничиваем размеры областью изображения
                    sourceWidth = Math.Min(sourceWidth, currentImage.Width - sourceLocation.X);
                    sourceHeight = Math.Min(sourceHeight, currentImage.Height - sourceLocation.Y);

                    if (sourceWidth > 0 && sourceHeight > 0)
                    {
                        // Создаем прямоугольник для обрезки
                        Rectangle sourceRect = new Rectangle(sourceLocation.X, sourceLocation.Y, sourceWidth, sourceHeight);

                        Bitmap cropped = new Bitmap(sourceRect.Width, sourceRect.Height);
                        using (Graphics g = Graphics.FromImage(cropped))
                        {
                            g.DrawImage(currentImage, new Rectangle(0, 0, cropped.Width, cropped.Height),
                                       sourceRect, GraphicsUnit.Pixel);
                        }

                        currentImage = cropped;
                        cropRectangle = Rectangle.Empty;
                        zoomFactor = 1.0f;
                        panOffset = Point.Empty;
                        UpdateImageDisplay();
                        currentTool = "Pencil";
                        UpdateStatusBar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during crop: {ex.Message}", "Crop Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                Redo_Click(sender, e);
            }
            else if (e.KeyCode == Keys.P)
            {
                EnablePencil();
            }
            else if (e.KeyCode == Keys.E)
            {
                EnableEraser();
            }
            else if (e.Control && e.KeyCode == Keys.Add)
            {
                ZoomIn();
            }
            else if (e.Control && e.KeyCode == Keys.Subtract)
            {
                ZoomOut();
            }
            else if (e.Control && e.KeyCode == Keys.D0)
            {
                ResetZoom();
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                ChangeColor();
            }
            else if (e.KeyCode == Keys.Oemplus)
            {
                IncreasePenSize();
            }
            else if (e.KeyCode == Keys.OemMinus)
            {
                DecreasePenSize();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFile_Click(sender, e);
        }

        // Menu item handlers for new drawing tools
        private void PencilTool_Click(object sender, EventArgs e)
        {
            EnablePencil();
        }

        private void EraserTool_Click(object sender, EventArgs e)
        {
            EnableEraser();
        }

        private void ColorTool_Click(object sender, EventArgs e)
        {
            ChangeColor();
        }

        private void IncreasePenTool_Click(object sender, EventArgs e)
        {
            IncreasePenSize();
        }

        private void DecreasePenTool_Click(object sender, EventArgs e)
        {
            DecreasePenSize();
        }

        private void ZoomInTool_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void ZoomOutTool_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        private void ResetZoomTool_Click(object sender, EventArgs e)
        {
            ResetZoom();
        }
    }
}