using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenCvSharp;

namespace TOOK
{
    public partial class ThresholdForm : Form
    {
        public BasicForm BaseForm { get; set; }
        public IplImage srcImage { get; set; }
        public IplImage dstImage { get; set; }
        public int setThreshold { get; set; }
        

        public ThresholdForm()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            srcImage.Threshold(dstImage, trackBar1.Value, 255, ThresholdType.Binary);
            pictureBoxIpl1.ImageIpl = dstImage;
        }

        private void ThresholdForm_Load(object sender, EventArgs e)
        {
            dstImage = new IplImage(srcImage.Size, srcImage.Depth, srcImage.NChannels);
            srcImage.Threshold(dstImage, trackBar1.Value, 255, ThresholdType.Binary);
            pictureBoxIpl1.ImageIpl = dstImage;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            BaseForm.imgBox = dstImage;
            BaseForm.changePictureBox(BaseForm.imgBox);
            Hide();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void button_auto_Click(object sender, EventArgs e)
        {
            srcImage.Threshold(dstImage, trackBar1.Value, 255, ThresholdType.Otsu);
            BaseForm.imgBox = dstImage;
            BaseForm.changePictureBox(BaseForm.imgBox);
            Hide();
        }

        private void ThresholdForm_Activated(object sender, EventArgs e)
        {
            dstImage = new IplImage(srcImage.Size, srcImage.Depth, srcImage.NChannels);
            srcImage.Threshold(dstImage, trackBar1.Value, 255, ThresholdType.Binary);
            pictureBoxIpl1.ImageIpl = dstImage;
        }
    }
}
