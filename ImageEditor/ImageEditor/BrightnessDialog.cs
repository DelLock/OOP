using System;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class BrightnessDialog : Form
    {
        private TrackBar brightnessTrackBar;
        private Label valueLabel;
        private Button okButton;
        private Button cancelButton;

        public int BrightnessValue => brightnessTrackBar.Value;

        public BrightnessDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Width = 300;
            Height = 150;
            Text = "Яркость";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            Label brightnessLabel = new Label() { Text = "Яркость:", Left = 20, Top = 20, Width = 60 };
            brightnessTrackBar = new TrackBar() { Left = 90, Top = 20, Width = 150, Minimum = -100, Maximum = 100, Value = 0 };
            valueLabel = new Label() { Text = "0", Left = 250, Top = 20, Width = 30 };

            okButton = new Button() { Text = "OK", Left = 120, Top = 80, Width = 75, DialogResult = DialogResult.OK };
            cancelButton = new Button() { Text = "Отмена", Left = 200, Top = 80, Width = 75, DialogResult = DialogResult.Cancel };

            brightnessTrackBar.ValueChanged += (s, e) => valueLabel.Text = brightnessTrackBar.Value.ToString();

            AcceptButton = okButton;
            CancelButton = cancelButton;

            Controls.AddRange(new Control[] { brightnessLabel, brightnessTrackBar, valueLabel, okButton, cancelButton });
        }
    }
}