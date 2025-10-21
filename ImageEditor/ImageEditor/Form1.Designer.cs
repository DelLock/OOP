namespace ImageEditor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            imageToolStripMenuItem = new ToolStripMenuItem();
            rotateToolStripMenuItem = new ToolStripMenuItem();
            rotate90ToolStripMenuItem = new ToolStripMenuItem();
            rotate180ToolStripMenuItem = new ToolStripMenuItem();
            rotate270ToolStripMenuItem = new ToolStripMenuItem();
            cropToolStripMenuItem = new ToolStripMenuItem();
            filtersToolStripMenuItem = new ToolStripMenuItem();
            brightnessToolStripMenuItem = new ToolStripMenuItem();
            blurToolStripMenuItem = new ToolStripMenuItem();
            sepiaToolStripMenuItem = new ToolStripMenuItem();
            invertToolStripMenuItem = new ToolStripMenuItem();
            grayscaleToolStripMenuItem = new ToolStripMenuItem();
            drawingToolStripMenuItem = new ToolStripMenuItem();
            pencilToolStripMenuItem = new ToolStripMenuItem();
            eraserToolStripMenuItem = new ToolStripMenuItem();
            colorToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            increasePenToolStripMenuItem = new ToolStripMenuItem();
            decreasePenToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            zoomInToolStripMenuItem = new ToolStripMenuItem();
            zoomOutToolStripMenuItem = new ToolStripMenuItem();
            resetZoomToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            newToolStripButton = new ToolStripButton();
            openToolStripButton = new ToolStripButton();
            saveToolStripButton = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            pencilToolStripButton = new ToolStripButton();
            eraserToolStripButton = new ToolStripButton();
            colorToolStripButton = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            zoomInToolStripButton = new ToolStripButton();
            zoomOutToolStripButton = new ToolStripButton();
            resetZoomToolStripButton = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            pictureBox1 = new PictureBox();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            panel1 = new Panel();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, imageToolStripMenuItem, filtersToolStripMenuItem, drawingToolStripMenuItem, viewToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(875, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new Size(146, 22);
            newToolStripMenuItem.Text = "&New";
            newToolStripMenuItem.Click += NewFile_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(146, 22);
            openToolStripMenuItem.Text = "&Open";
            openToolStripMenuItem.Click += OpenFile_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(146, 22);
            saveToolStripMenuItem.Text = "&Save";
            saveToolStripMenuItem.Click += SaveFile_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(146, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += Exit_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new Size(180, 22);
            undoToolStripMenuItem.Text = "&Undo";
            undoToolStripMenuItem.Click += Undo_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
            redoToolStripMenuItem.Size = new Size(180, 22);
            redoToolStripMenuItem.Text = "&Redo";
            redoToolStripMenuItem.Click += Redo_Click;
            // 
            // imageToolStripMenuItem
            // 
            imageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { rotateToolStripMenuItem, cropToolStripMenuItem });
            imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            imageToolStripMenuItem.Size = new Size(52, 20);
            imageToolStripMenuItem.Text = "&Image";
            // 
            // rotateToolStripMenuItem
            // 
            rotateToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { rotate90ToolStripMenuItem, rotate180ToolStripMenuItem, rotate270ToolStripMenuItem });
            rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            rotateToolStripMenuItem.Size = new Size(180, 22);
            rotateToolStripMenuItem.Text = "&Rotate";
            // 
            // rotate90ToolStripMenuItem
            // 
            rotate90ToolStripMenuItem.Name = "rotate90ToolStripMenuItem";
            rotate90ToolStripMenuItem.Size = new Size(97, 22);
            rotate90ToolStripMenuItem.Text = "90°";
            rotate90ToolStripMenuItem.Click += Rotate90_Click;
            // 
            // rotate180ToolStripMenuItem
            // 
            rotate180ToolStripMenuItem.Name = "rotate180ToolStripMenuItem";
            rotate180ToolStripMenuItem.Size = new Size(97, 22);
            rotate180ToolStripMenuItem.Text = "180°";
            rotate180ToolStripMenuItem.Click += Rotate180_Click;
            // 
            // rotate270ToolStripMenuItem
            // 
            rotate270ToolStripMenuItem.Name = "rotate270ToolStripMenuItem";
            rotate270ToolStripMenuItem.Size = new Size(97, 22);
            rotate270ToolStripMenuItem.Text = "270°";
            rotate270ToolStripMenuItem.Click += Rotate270_Click;
            // 
            // cropToolStripMenuItem
            // 
            cropToolStripMenuItem.Name = "cropToolStripMenuItem";
            cropToolStripMenuItem.Size = new Size(180, 22);
            cropToolStripMenuItem.Text = "&Crop";
            cropToolStripMenuItem.Click += Crop_Click;
            // 
            // filtersToolStripMenuItem
            // 
            filtersToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { brightnessToolStripMenuItem, blurToolStripMenuItem, sepiaToolStripMenuItem, invertToolStripMenuItem, grayscaleToolStripMenuItem });
            filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            filtersToolStripMenuItem.Size = new Size(50, 20);
            filtersToolStripMenuItem.Text = "&Filters";
            // 
            // brightnessToolStripMenuItem
            // 
            brightnessToolStripMenuItem.Name = "brightnessToolStripMenuItem";
            brightnessToolStripMenuItem.Size = new Size(180, 22);
            brightnessToolStripMenuItem.Text = "&Brightness";
            brightnessToolStripMenuItem.Click += BrightnessFilter_Click;
            // 
            // blurToolStripMenuItem
            // 
            blurToolStripMenuItem.Name = "blurToolStripMenuItem";
            blurToolStripMenuItem.Size = new Size(180, 22);
            blurToolStripMenuItem.Text = "B&lur";
            blurToolStripMenuItem.Click += BlurFilter_Click;
            // 
            // sepiaToolStripMenuItem
            // 
            sepiaToolStripMenuItem.Name = "sepiaToolStripMenuItem";
            sepiaToolStripMenuItem.Size = new Size(180, 22);
            sepiaToolStripMenuItem.Text = "&Sepia";
            sepiaToolStripMenuItem.Click += SepiaFilter_Click;
            // 
            // invertToolStripMenuItem
            // 
            invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            invertToolStripMenuItem.Size = new Size(180, 22);
            invertToolStripMenuItem.Text = "&Invert";
            invertToolStripMenuItem.Click += InvertFilter_Click;
            // 
            // grayscaleToolStripMenuItem
            // 
            grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            grayscaleToolStripMenuItem.Size = new Size(180, 22);
            grayscaleToolStripMenuItem.Text = "&Grayscale";
            grayscaleToolStripMenuItem.Click += GrayscaleFilter_Click;
            // 
            // drawingToolStripMenuItem
            // 
            drawingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { pencilToolStripMenuItem, eraserToolStripMenuItem, colorToolStripMenuItem, toolStripSeparator1, increasePenToolStripMenuItem, decreasePenToolStripMenuItem });
            drawingToolStripMenuItem.Name = "drawingToolStripMenuItem";
            drawingToolStripMenuItem.Size = new Size(63, 20);
            drawingToolStripMenuItem.Text = "&Drawing";
            // 
            // pencilToolStripMenuItem
            // 
            pencilToolStripMenuItem.Name = "pencilToolStripMenuItem";
            pencilToolStripMenuItem.Size = new Size(180, 22);
            pencilToolStripMenuItem.Text = "&Pencil";
            pencilToolStripMenuItem.Click += PencilTool_Click;
            // 
            // eraserToolStripMenuItem
            // 
            eraserToolStripMenuItem.Name = "eraserToolStripMenuItem";
            eraserToolStripMenuItem.Size = new Size(180, 22);
            eraserToolStripMenuItem.Text = "&Eraser";
            eraserToolStripMenuItem.Click += EraserTool_Click;
            // 
            // colorToolStripMenuItem
            // 
            colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            colorToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            colorToolStripMenuItem.Size = new Size(180, 22);
            colorToolStripMenuItem.Text = "&Color";
            colorToolStripMenuItem.Click += ColorTool_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // increasePenToolStripMenuItem
            // 
            increasePenToolStripMenuItem.Name = "increasePenToolStripMenuItem";
            increasePenToolStripMenuItem.Size = new Size(180, 22);
            increasePenToolStripMenuItem.Text = "&Increase Pen";
            increasePenToolStripMenuItem.Click += IncreasePenTool_Click;
            // 
            // decreasePenToolStripMenuItem
            // 
            decreasePenToolStripMenuItem.Name = "decreasePenToolStripMenuItem";
            decreasePenToolStripMenuItem.Size = new Size(180, 22);
            decreasePenToolStripMenuItem.Text = "&Decrease Pen";
            decreasePenToolStripMenuItem.Click += DecreasePenTool_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { zoomInToolStripMenuItem, zoomOutToolStripMenuItem, resetZoomToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "&View";
            // 
            // zoomInToolStripMenuItem
            // 
            zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            zoomInToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Add;
            zoomInToolStripMenuItem.Size = new Size(207, 22);
            zoomInToolStripMenuItem.Text = "Zoom &In";
            zoomInToolStripMenuItem.Click += ZoomInTool_Click;
            // 
            // zoomOutToolStripMenuItem
            // 
            zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            zoomOutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Subtract;
            zoomOutToolStripMenuItem.Size = new Size(207, 22);
            zoomOutToolStripMenuItem.Text = "Zoom &Out";
            zoomOutToolStripMenuItem.Click += ZoomOutTool_Click;
            // 
            // resetZoomToolStripMenuItem
            // 
            resetZoomToolStripMenuItem.Name = "resetZoomToolStripMenuItem";
            resetZoomToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D0;
            resetZoomToolStripMenuItem.Size = new Size(207, 22);
            resetZoomToolStripMenuItem.Text = "&Reset Zoom";
            resetZoomToolStripMenuItem.Click += ResetZoomTool_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { newToolStripButton, openToolStripButton, saveToolStripButton, toolStripSeparator2, pencilToolStripButton, eraserToolStripButton, colorToolStripButton, toolStripSeparator3, zoomInToolStripButton, zoomOutToolStripButton, resetZoomToolStripButton });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(875, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            newToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newToolStripButton.ImageTransparentColor = Color.Magenta;
            newToolStripButton.Name = "newToolStripButton";
            newToolStripButton.Size = new Size(23, 22);
            newToolStripButton.Text = "New";
            newToolStripButton.Click += NewFile_Click;
            // 
            // openToolStripButton
            // 
            openToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openToolStripButton.ImageTransparentColor = Color.Magenta;
            openToolStripButton.Name = "openToolStripButton";
            openToolStripButton.Size = new Size(23, 22);
            openToolStripButton.Text = "Open";
            openToolStripButton.Click += OpenFile_Click;
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveToolStripButton.ImageTransparentColor = Color.Magenta;
            saveToolStripButton.Name = "saveToolStripButton";
            saveToolStripButton.Size = new Size(23, 22);
            saveToolStripButton.Text = "Save";
            saveToolStripButton.Click += SaveFile_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // pencilToolStripButton
            // 
            pencilToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            pencilToolStripButton.ImageTransparentColor = Color.Magenta;
            pencilToolStripButton.Name = "pencilToolStripButton";
            pencilToolStripButton.Size = new Size(23, 22);
            pencilToolStripButton.Text = "Pencil";
            pencilToolStripButton.Click += PencilTool_Click;
            // 
            // eraserToolStripButton
            // 
            eraserToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            eraserToolStripButton.ImageTransparentColor = Color.Magenta;
            eraserToolStripButton.Name = "eraserToolStripButton";
            eraserToolStripButton.Size = new Size(23, 22);
            eraserToolStripButton.Text = "Eraser";
            eraserToolStripButton.Click += EraserTool_Click;
            // 
            // colorToolStripButton
            // 
            colorToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            colorToolStripButton.ImageTransparentColor = Color.Magenta;
            colorToolStripButton.Name = "colorToolStripButton";
            colorToolStripButton.Size = new Size(23, 22);
            colorToolStripButton.Text = "Color";
            colorToolStripButton.Click += ColorTool_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // zoomInToolStripButton
            // 
            zoomInToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            zoomInToolStripButton.ImageTransparentColor = Color.Magenta;
            zoomInToolStripButton.Name = "zoomInToolStripButton";
            zoomInToolStripButton.Size = new Size(23, 22);
            zoomInToolStripButton.Text = "Zoom In";
            zoomInToolStripButton.Click += ZoomInTool_Click;
            // 
            // zoomOutToolStripButton
            // 
            zoomOutToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            zoomOutToolStripButton.ImageTransparentColor = Color.Magenta;
            zoomOutToolStripButton.Name = "zoomOutToolStripButton";
            zoomOutToolStripButton.Size = new Size(23, 22);
            zoomOutToolStripButton.Text = "Zoom Out";
            zoomOutToolStripButton.Click += ZoomOutTool_Click;
            // 
            // resetZoomToolStripButton
            // 
            resetZoomToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resetZoomToolStripButton.ImageTransparentColor = Color.Magenta;
            resetZoomToolStripButton.Name = "resetZoomToolStripButton";
            resetZoomToolStripButton.Size = new Size(23, 22);
            resetZoomToolStripButton.Text = "Reset Zoom";
            resetZoomToolStripButton.Click += ResetZoomTool_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 12, 0);
            statusStrip1.Size = new Size(875, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(114, 17);
            toolStripStatusLabel1.Text = "Pencil | Zoom: 100%";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(3, -2);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(700, 450);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            pictureBox1.MouseWheel += pictureBox1_MouseWheel;
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif";
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 49);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(875, 379);
            panel1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 450);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Image Editor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem imageToolStripMenuItem;
        private ToolStripMenuItem rotateToolStripMenuItem;
        private ToolStripMenuItem rotate90ToolStripMenuItem;
        private ToolStripMenuItem rotate180ToolStripMenuItem;
        private ToolStripMenuItem rotate270ToolStripMenuItem;
        private ToolStripMenuItem cropToolStripMenuItem;
        private ToolStripMenuItem filtersToolStripMenuItem;
        private ToolStripMenuItem brightnessToolStripMenuItem;
        private ToolStripMenuItem blurToolStripMenuItem;
        private ToolStripMenuItem sepiaToolStripMenuItem;
        private ToolStripMenuItem invertToolStripMenuItem;
        private ToolStripMenuItem grayscaleToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton saveToolStripButton;
        private ToolStripButton openToolStripButton;
        private StatusStrip statusStrip1;
        private PictureBox pictureBox1;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private Panel panel1;
        private ToolStripMenuItem drawingToolStripMenuItem;
        private ToolStripMenuItem pencilToolStripMenuItem;
        private ToolStripMenuItem eraserToolStripMenuItem;
        private ToolStripMenuItem colorToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem increasePenToolStripMenuItem;
        private ToolStripMenuItem decreasePenToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem zoomInToolStripMenuItem;
        private ToolStripMenuItem zoomOutToolStripMenuItem;
        private ToolStripMenuItem resetZoomToolStripMenuItem;
        private ToolStripButton newToolStripButton;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton pencilToolStripButton;
        private ToolStripButton eraserToolStripButton;
        private ToolStripButton colorToolStripButton;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton zoomInToolStripButton;
        private ToolStripButton zoomOutToolStripButton;
        private ToolStripButton resetZoomToolStripButton;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripMenuItem redoToolStripMenuItem;
    }
}