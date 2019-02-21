using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROI
{
    public partial class frmOptions : Form
    {
        public static Color colorOfPoints;
        public static Color colorOfLines;
        public static int sizeOfPoints;
        public static int sizeOfLines;
        public frmOptions()
        {
            InitializeComponent();
        }

        private void lblColorPoints_Click(object sender, EventArgs e)
        {

            if (colorDialogPoints.ShowDialog() == DialogResult.OK)
            {
                lblColorPoints.BackColor = colorDialogPoints.Color;
            }
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            lblColorPoints.BackColor = frmOptions.colorOfPoints;
            lblColorLines.BackColor = frmOptions.colorOfLines;
            txtSizeOfLines.Text = frmOptions.sizeOfLines.ToString();
            txtSizeOfPoints.Text = frmOptions.sizeOfPoints.ToString();
        }

        private void lblColorLines_Click(object sender, EventArgs e)
        {
            if (colorDialogLines.ShowDialog() == DialogResult.OK)
            {
                lblColorLines.BackColor = colorDialogLines.Color;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            bool result = true;
            result = int.TryParse(txtSizeOfPoints.Text, out sizeOfPoints);
            if (!result)
            {
                labelStatus.Text = "The input for size should be integer.";
                return;
            }
            result = int.TryParse(txtSizeOfLines.Text, out sizeOfLines);
            if (!result)
            {
                labelStatus.Text = "The input for size should be integer.";
                return;
            }
            colorOfLines = lblColorLines.BackColor;
            colorOfPoints = lblColorPoints.BackColor;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Color defaultColor = new Color();
            defaultColor = Color.FromArgb(100, 100, 100);

            frmOptions.colorOfLines = defaultColor;
            frmOptions.colorOfPoints = defaultColor;
            frmOptions.sizeOfLines = 1;
            frmOptions.sizeOfPoints = 5;
            txtSizeOfPoints.Text = "5";
            txtSizeOfLines.Text = "1";
            lblColorLines.BackColor = defaultColor;
            lblColorPoints.BackColor = defaultColor;
        }
    }
}
