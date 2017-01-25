using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOOK
{
    public partial class SelectMode : Form
    {
        public BasicForm BaseForm { get; set; }

        public SelectMode()
        {
            InitializeComponent();
        }

        private void Select_Stamp_Click(object sender, EventArgs e)
        {
            BaseForm.currentMode = Mode.STAMP;
            BaseForm.ModeImageChange();
            Hide();
        }

        private void Select_Default_Click(object sender, EventArgs e)
        {
            BaseForm.currentMode = Mode.DEFAULT;
            BaseForm.ModeImageChange();
            Hide();
        }

        private void Select_Ring_Click(object sender, EventArgs e)
        {
            BaseForm.currentMode = Mode.RING;
            BaseForm.ModeImageChange();
            Hide();
        }

        private void Select_Dot_Click(object sender, EventArgs e)
        {
            BaseForm.currentMode = Mode.DOT;
            BaseForm.ModeImageChange();
            Hide();
        }
    }
}
