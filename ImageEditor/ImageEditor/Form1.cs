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
        private Rectangle selectionRectangle;
        private bool isCropping;
        private bool isSelecting;
        private string currentTool;
        private bool isEraser;
        private float zoomFactor = 1.0f;
        private bool isPanning;
        private Point panStart;
        private Bitmap selectedArea;
        private Point selectionStart;
        private bool hasSelection;

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

            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            KeyPreview = true;
            KeyDown += MainForm_KeyDown;

            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            string toolStatus = currentTool;
            if (currentTool == "Pencil")
                toolStatus = isEraser ? "Eraser" : "Pencil";

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

        private void Copy_Click(object sender, EventArgs e)
        {
            if (hasSelection && selectedArea != null)
            {
                Clipboard.SetImage(selectedArea);
            }
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                SaveCurrentState();
                Image clipboardImage = Clipboard.GetImage();
                using (Graphics g = Graphics.FromImage(currentImage))
                {
                    g.DrawImage(clipboardImage, new Point(10, 10));
                }
                UpdateImageDisplay();
            }
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            if (hasSelection && selectedArea != null)
            {
                SaveCurrentState();
                Clipboard.SetImage(selectedArea);

                using (Graphics g = Graphics.FromImage(currentImage))
                {
                    using (Brush brush = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(brush, selectionRectangle);
                    }
                }

                hasSelection = false;
                selectedArea = null;
                UpdateImageDisplay();
            }
        }
        private void RotateImage(float angle)
        {
            if (currentImage == null) return;

            SaveCurrentState();

            Bitmap rotated = ImageProcessor.RotateImage(currentImage, angle);
            currentImage = rotated;
            zoomFactor = 1.0f;
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
                isSelecting = false;
            }
            else
            {
                currentTool = "Pencil";
            }
            UpdateStatusBar();
        }

        private void ApplyCrop()
        {
            if (cropRectangle.Width > 0 && cropRectangle.Height > 0)
            {
                try
                {
                    Point sourceLocation = ConvertMouseCoordinates(cropRectangle.Location);

                    float scaleX = (float)currentImage.Width / pictureBox1.Image.Width;
                    float scaleY = (float)currentImage.Height / pictureBox1.Image.Height;

                    int sourceWidth = (int)(cropRectangle.Width * scaleX);
                    int sourceHeight = (int)(cropRectangle.Height * scaleY);

                    sourceWidth = Math.Min(sourceWidth, currentImage.Width - sourceLocation.X);
                    sourceHeight = Math.Min(sourceHeight, currentImage.Height - sourceLocation.Y);

                    if (sourceWidth > 0 && sourceHeight > 0)
                    {
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
                        UpdateImageDisplay();
                        currentTool = "Pencil";
                        isCropping = false;
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
        private void Select_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            isSelecting = !isSelecting;
            if (isSelecting)
            {
                currentTool = "Select";
                isCropping = false;
                isEraser = false;
            }
            else
            {
                currentTool = "Pencil";
                hasSelection = false;
                selectedArea = null;
            }
            UpdateStatusBar();
        }

        private void ApplySelection()
        {
            if (selectionRectangle.Width > 0 && selectionRectangle.Height > 0)
            {
                try
                {
                    Point sourceLocation = ConvertMouseCoordinates(selectionRectangle.Location);

                    float scaleX = (float)currentImage.Width / pictureBox1.Image.Width;
                    float scaleY = (float)currentImage.Height / pictureBox1.Image.Height;

                    int sourceWidth = (int)(selectionRectangle.Width * scaleX);
                    int sourceHeight = (int)(selectionRectangle.Height * scaleY);

                    sourceWidth = Math.Min(sourceWidth, currentImage.Width - sourceLocation.X);
                    sourceHeight = Math.Min(sourceHeight, currentImage.Height - sourceLocation.Y);

                    if (sourceWidth > 0 && sourceHeight > 0)
                    {
                        Rectangle sourceRect = new Rectangle(sourceLocation.X, sourceLocation.Y, sourceWidth, sourceHeight);
                        selectedArea = new Bitmap(sourceRect.Width, sourceRect.Height);

                        using (Graphics g = Graphics.FromImage(selectedArea))
                        {
                            g.DrawImage(currentImage, new Rectangle(0, 0, selectedArea.Width, selectedArea.Height),
                                       sourceRect, GraphicsUnit.Pixel);
                        }

                        hasSelection = true;
                        selectionRectangle = Rectangle.Empty;
                        UpdateImageDisplay();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during selection: {ex.Message}", "Selection Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void SepiaFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            SaveCurrentState();
            currentImage = ImageProcessor.ApplySepia(currentImage);
            UpdateImageDisplay();
        }

        private void InvertFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            SaveCurrentState();
            currentImage = ImageProcessor.ApplyInvert(currentImage);
            UpdateImageDisplay();
        }

        private void GrayscaleFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            SaveCurrentState();
            currentImage = ImageProcessor.ApplyGrayscale(currentImage);
            UpdateImageDisplay();
        }

        private void ContrastFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            using (ContrastDialog dialog = new ContrastDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SaveCurrentState();
                    currentImage = ImageProcessor.ApplyContrast(currentImage, dialog.ContrastValue);
                    UpdateImageDisplay();
                }
            }
        }

        private void SaturationFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            using (SaturationDialog dialog = new SaturationDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SaveCurrentState();
                    currentImage = ImageProcessor.ApplySaturation(currentImage, dialog.SaturationValue);
                    UpdateImageDisplay();
                }
            }
        }

        private void VintageFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            SaveCurrentState();
            currentImage = ImageProcessor.ApplyVintage(currentImage);
            UpdateImageDisplay();
        }

        private void CoolFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            SaveCurrentState();
            currentImage = ImageProcessor.ApplyCool(currentImage);
            UpdateImageDisplay();
        }

        private void WarmFilter_Click(object sender, EventArgs e)
        {
            if (currentImage == null) return;

            SaveCurrentState();
            currentImage = ImageProcessor.ApplyWarm(currentImage);
            UpdateImageDisplay();
        }
        private void EnablePencil()
        {
            currentTool = "Pencil";
            isEraser = false;
            isCropping = false;
            isSelecting = false;
            UpdateStatusBar();
        }

        private void EnableEraser()
        {
            currentTool = "Pencil";
            isEraser = true;
            isCropping = false;
            isSelecting = false;
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
                else if (currentTool == "Select")
                {
                    selectionRectangle = new Rectangle(e.Location, Size.Empty);
                    selectionStart = e.Location;
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
            else if (currentTool == "Select" && e.Button == MouseButtons.Left)
            {
                int x = Math.Min(selectionStart.X, e.X);
                int y = Math.Min(selectionStart.Y, e.Y);
                int width = Math.Abs(selectionStart.X - e.X);
                int height = Math.Abs(selectionStart.Y - e.Y);

                selectionRectangle = new Rectangle(x, y, width, height);
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
            else if (currentTool == "Select" && !selectionRectangle.IsEmpty && selectionRectangle.Width > 0 && selectionRectangle.Height > 0)
            {
                ApplySelection();
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
            else if (currentTool == "Select" && !selectionRectangle.IsEmpty)
            {
                using (Pen selectPen = new Pen(Color.Blue, 2))
                {
                    selectPen.DashStyle = DashStyle.Dash;
                    e.Graphics.DrawRectangle(selectPen, selectionRectangle);
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
            else if (e.Control && e.KeyCode == Keys.N)
            {
                NewFile_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                OpenFile_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                SaveFile_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                Copy_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                Paste_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                Cut_Click(sender, e);
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
            else if (e.KeyCode == Keys.Oemplus)
            {
                IncreasePenSize();
            }
            else if (e.KeyCode == Keys.OemMinus)
            {
                DecreasePenSize();
            }
            else if (e.KeyCode == Keys.Delete && hasSelection)
            {
                SaveCurrentState();
                using (Graphics g = Graphics.FromImage(currentImage))
                {
                    using (Brush brush = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(brush, selectionRectangle);
                    }
                }
                hasSelection = false;
                selectedArea = null;
                UpdateImageDisplay();
            }
        }

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

        private void SelectTool_Click(object sender, EventArgs e)
        {
            Select_Click(sender, e);
        }
    }
}