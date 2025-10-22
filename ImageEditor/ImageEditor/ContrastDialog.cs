using System;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class ContrastDialog : Form
    {
        private TrackBar contrastTrackBar;
        private Label valueLabel;
        private Button okButton;
        private Button cancelButton;

        public int ContrastValue => contrastTrackBar.Value;

        public ContrastDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Width = 300;
            Height = 150;
            Text = "Contrast";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            Label contrastLabel = new Label() { Text = "Contrast:", Left = 20, Top = 20, Width = 60 };
            contrastTrackBar = new TrackBar() { Left = 90, Top = 20, Width = 150, Minimum = -50, Maximum = 50, Value = 0 };
            valueLabel = new Label() { Text = "0", Left = 250, Top = 20, Width = 30 };

            okButton = new Button() { Text = "OK", Left = 120, Top = 80, Width = 75, DialogResult = DialogResult.OK };
            cancelButton = new Button() { Text = "Cancel", Left = 200, Top = 80, Width = 75, DialogResult = DialogResult.Cancel };

            contrastTrackBar.ValueChanged += (s, e) => valueLabel.Text = contrastTrackBar.Value.ToString();

            AcceptButton = okButton;
            CancelButton = cancelButton;

            Controls.AddRange(new Control[] { contrastLabel, contrastTrackBar, valueLabel, okButton, cancelButton });
        }
    }
}