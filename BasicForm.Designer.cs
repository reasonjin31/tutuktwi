using System.Drawing.Text;

namespace TOOK
{
    partial class BasicForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BasicForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBox_isNegative = new System.Windows.Forms.CheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.trackBar_Bold = new System.Windows.Forms.TrackBar();
            this.textBox_SetZ = new System.Windows.Forms.TextBox();
            this.label_mm = new System.Windows.Forms.Label();
            this.radioButton_NONE = new System.Windows.Forms.RadioButton();
            this.radioButton_Polygon = new System.Windows.Forms.RadioButton();
            this.radioButton_Background = new System.Windows.Forms.RadioButton();
            this.button_Mode = new System.Windows.Forms.Button();
            this.pictureBox = new OpenCvSharp.UserInterface.PictureBoxIpl();
            this.button_Open = new System.Windows.Forms.Button();
            this.button_Convert = new System.Windows.Forms.Button();
            this.button_EXIT = new System.Windows.Forms.Button();
            this.pictureBox_Base = new OpenCvSharp.UserInterface.PictureBoxIpl();
            this.button_Threshold = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Bold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Base)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBox_isNegative
            // 
            this.checkBox_isNegative.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_isNegative.AutoSize = true;
            this.checkBox_isNegative.Enabled = false;
            this.checkBox_isNegative.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.checkBox_isNegative.FlatAppearance.BorderSize = 0;
            this.checkBox_isNegative.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_isNegative.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_isNegative.ForeColor = System.Drawing.Color.Transparent;
            this.checkBox_isNegative.Image = ((System.Drawing.Image)(resources.GetObject("checkBox_isNegative.Image")));
            this.checkBox_isNegative.Location = new System.Drawing.Point(512, 633);
            this.checkBox_isNegative.Name = "checkBox_isNegative";
            this.checkBox_isNegative.Size = new System.Drawing.Size(42, 38);
            this.checkBox_isNegative.TabIndex = 7;
            this.checkBox_isNegative.UseVisualStyleBackColor = true;
            this.checkBox_isNegative.CheckedChanged += new System.EventHandler(this.checkBox_isNegative_CheckedChanged);
            // 
            // trackBar_Bold
            // 
            this.trackBar_Bold.Enabled = false;
            this.trackBar_Bold.LargeChange = 1;
            this.trackBar_Bold.Location = new System.Drawing.Point(89, 557);
            this.trackBar_Bold.Maximum = 5;
            this.trackBar_Bold.Name = "trackBar_Bold";
            this.trackBar_Bold.Size = new System.Drawing.Size(465, 45);
            this.trackBar_Bold.TabIndex = 8;
            this.trackBar_Bold.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_Bold.Scroll += new System.EventHandler(this.trackBar_Erode_Scroll);
            // 
            // textBox_SetZ
            // 
            this.textBox_SetZ.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_SetZ.Location = new System.Drawing.Point(96, 608);
            this.textBox_SetZ.MaxLength = 3;
            this.textBox_SetZ.Name = "textBox_SetZ";
            this.textBox_SetZ.Size = new System.Drawing.Size(41, 27);
            this.textBox_SetZ.TabIndex = 10;
            this.textBox_SetZ.Text = "30";
            this.textBox_SetZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_SetZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_SetZ_KeyPress);
            // 
            // label_mm
            // 
            this.label_mm.AutoSize = true;
            this.label_mm.Font = new System.Drawing.Font("나눔바른고딕", 12F);
            this.label_mm.Location = new System.Drawing.Point(138, 617);
            this.label_mm.Name = "label_mm";
            this.label_mm.Size = new System.Drawing.Size(37, 19);
            this.label_mm.TabIndex = 12;
            this.label_mm.Text = "mm";
            // 
            // radioButton_NONE
            // 
            this.radioButton_NONE.AutoSize = true;
            this.radioButton_NONE.Checked = true;
            this.radioButton_NONE.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radioButton_NONE.Location = new System.Drawing.Point(96, 652);
            this.radioButton_NONE.Name = "radioButton_NONE";
            this.radioButton_NONE.Size = new System.Drawing.Size(61, 19);
            this.radioButton_NONE.TabIndex = 14;
            this.radioButton_NONE.TabStop = true;
            this.radioButton_NONE.Text = "NONE";
            this.radioButton_NONE.UseVisualStyleBackColor = true;
            // 
            // radioButton_Polygon
            // 
            this.radioButton_Polygon.AutoSize = true;
            this.radioButton_Polygon.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radioButton_Polygon.Location = new System.Drawing.Point(161, 652);
            this.radioButton_Polygon.Name = "radioButton_Polygon";
            this.radioButton_Polygon.Size = new System.Drawing.Size(85, 19);
            this.radioButton_Polygon.TabIndex = 15;
            this.radioButton_Polygon.Text = "POLYGON";
            this.radioButton_Polygon.UseVisualStyleBackColor = true;
            // 
            // radioButton_Background
            // 
            this.radioButton_Background.AutoSize = true;
            this.radioButton_Background.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radioButton_Background.Location = new System.Drawing.Point(250, 652);
            this.radioButton_Background.Name = "radioButton_Background";
            this.radioButton_Background.Size = new System.Drawing.Size(109, 19);
            this.radioButton_Background.TabIndex = 16;
            this.radioButton_Background.Text = "BACKGROUND";
            this.radioButton_Background.UseVisualStyleBackColor = true;
            // 
            // button_Mode
            // 
            this.button_Mode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.button_Mode.FlatAppearance.BorderSize = 0;
            this.button_Mode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Mode.Image = ((System.Drawing.Image)(resources.GetObject("button_Mode.Image")));
            this.button_Mode.Location = new System.Drawing.Point(20, 541);
            this.button_Mode.Name = "button_Mode";
            this.button_Mode.Size = new System.Drawing.Size(70, 145);
            this.button_Mode.TabIndex = 17;
            this.button_Mode.UseVisualStyleBackColor = true;
            this.button_Mode.Click += new System.EventHandler(this.button_Mode_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(30, 50);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(723, 480);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            // 
            // button_Open
            // 
            this.button_Open.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.button_Open.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_Open.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.button_Open.FlatAppearance.BorderSize = 0;
            this.button_Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Open.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Open.ForeColor = System.Drawing.Color.White;
            this.button_Open.Image = ((System.Drawing.Image)(resources.GetObject("button_Open.Image")));
            this.button_Open.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Open.Location = new System.Drawing.Point(611, 550);
            this.button_Open.Name = "button_Open";
            this.button_Open.Size = new System.Drawing.Size(152, 52);
            this.button_Open.TabIndex = 4;
            this.button_Open.Text = "IMAGE OPEN";
            this.button_Open.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.button_Open.UseVisualStyleBackColor = false;
            this.button_Open.Click += new System.EventHandler(this.button_Open_Click);
            // 
            // button_Convert
            // 
            this.button_Convert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.button_Convert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_Convert.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.button_Convert.FlatAppearance.BorderSize = 0;
            this.button_Convert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Convert.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Convert.ForeColor = System.Drawing.Color.Transparent;
            this.button_Convert.Image = ((System.Drawing.Image)(resources.GetObject("button_Convert.Image")));
            this.button_Convert.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button_Convert.Location = new System.Drawing.Point(593, 618);
            this.button_Convert.Name = "button_Convert";
            this.button_Convert.Size = new System.Drawing.Size(170, 67);
            this.button_Convert.TabIndex = 4;
            this.button_Convert.Text = "CONVERT";
            this.button_Convert.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.button_Convert.UseVisualStyleBackColor = false;
            this.button_Convert.Click += new System.EventHandler(this.button_Convert_Click);
            // 
            // button_EXIT
            // 
            this.button_EXIT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.button_EXIT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_EXIT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.button_EXIT.FlatAppearance.BorderSize = 0;
            this.button_EXIT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_EXIT.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_EXIT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button_EXIT.Image = ((System.Drawing.Image)(resources.GetObject("button_EXIT.Image")));
            this.button_EXIT.Location = new System.Drawing.Point(747, 3);
            this.button_EXIT.Name = "button_EXIT";
            this.button_EXIT.Size = new System.Drawing.Size(35, 30);
            this.button_EXIT.TabIndex = 3;
            this.button_EXIT.Text = " ";
            this.button_EXIT.UseVisualStyleBackColor = false;
            this.button_EXIT.Click += new System.EventHandler(this.button_EXIT_Click);
            // 
            // pictureBox_Base
            // 
            this.pictureBox_Base.BackColor = System.Drawing.Color.White;
            this.pictureBox_Base.Location = new System.Drawing.Point(20, 40);
            this.pictureBox_Base.Name = "pictureBox_Base";
            this.pictureBox_Base.Size = new System.Drawing.Size(743, 500);
            this.pictureBox_Base.TabIndex = 6;
            this.pictureBox_Base.TabStop = false;
            // 
            // button_Threshold
            // 
            this.button_Threshold.Enabled = false;
            this.button_Threshold.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.button_Threshold.FlatAppearance.BorderSize = 0;
            this.button_Threshold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Threshold.Image = ((System.Drawing.Image)(resources.GetObject("button_Threshold.Image")));
            this.button_Threshold.Location = new System.Drawing.Point(460, 633);
            this.button_Threshold.Name = "button_Threshold";
            this.button_Threshold.Size = new System.Drawing.Size(46, 39);
            this.button_Threshold.TabIndex = 18;
            this.button_Threshold.UseVisualStyleBackColor = true;
            this.button_Threshold.Click += new System.EventHandler(this.button_Threshold_Click);
            // 
            // BasicForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this.ClientSize = new System.Drawing.Size(784, 700);
            this.Controls.Add(this.button_Threshold);
            this.Controls.Add(this.button_Mode);
            this.Controls.Add(this.radioButton_Background);
            this.Controls.Add(this.radioButton_Polygon);
            this.Controls.Add(this.radioButton_NONE);
            this.Controls.Add(this.label_mm);
            this.Controls.Add(this.textBox_SetZ);
            this.Controls.Add(this.trackBar_Bold);
            this.Controls.Add(this.checkBox_isNegative);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.button_Open);
            this.Controls.Add(this.button_Convert);
            this.Controls.Add(this.button_EXIT);
            this.Controls.Add(this.pictureBox_Base);
            this.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BasicForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "투디가툭튀어나오다";
            this.Load += new System.EventHandler(this.BasicForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BasicForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BasicForm_MouseMove);
            this.Move += new System.EventHandler(this.BasicForm_Move);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Bold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Base)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_EXIT;
        private System.Windows.Forms.Button button_Convert;
        private OpenCvSharp.UserInterface.PictureBoxIpl pictureBox;
        private OpenCvSharp.UserInterface.PictureBoxIpl pictureBox_Base;
        private System.Windows.Forms.Button button_Open;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox checkBox_isNegative;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TrackBar trackBar_Bold;
        private System.Windows.Forms.TextBox textBox_SetZ;
        private System.Windows.Forms.Label label_mm;
        private System.Windows.Forms.RadioButton radioButton_NONE;
        private System.Windows.Forms.RadioButton radioButton_Polygon;
        private System.Windows.Forms.RadioButton radioButton_Background;
        private System.Windows.Forms.Button button_Mode;
        private System.Windows.Forms.Button button_Threshold;       
    }
}

