using System;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class SaturationDialog : Form
    {
        private TrackBar saturationTrackBar;
        private Label valueLabel;
        private Button okButton;
        private Button cancelButton;

        public int SaturationValue => saturationTrackBar.Value;

        public SaturationDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Width = 300;
            Height = 150;
            Text = "Saturation";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            Label saturationLabel = new Label() { Text = "Saturation:", Left = 20, Top = 20, Width = 60 };
            saturationTrackBar = new TrackBar() { Left = 90, Top = 20, Width = 150, Minimum = -100, Maximum = 100, Value = 0 };
            valueLabel = new Label() { Text = "0", Left = 250, Top = 20, Width = 30 };

            okButton = new Button() { Text = "OK", Left = 120, Top = 80, Width = 75, DialogResult = DialogResult.OK };
            cancelButton = new Button() { Text = "Cancel", Left = 200, Top = 80, Width = 75, DialogResult = DialogResult.Cancel };

            saturationTrackBar.ValueChanged += (s, e) => valueLabel.Text = saturationTrackBar.Value.ToString();

            AcceptButton = okButton;
            CancelButton = cancelButton;

            Controls.AddRange(new Control[] { saturationLabel, saturationTrackBar, valueLabel, okButton, cancelButton });
        }
    }
}