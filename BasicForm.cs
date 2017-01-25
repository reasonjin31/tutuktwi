using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Windows.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.IO;  

namespace TOOK
{
    public partial class BasicForm : Form
    {
        //변환모드
        SelectMode selectMode = new SelectMode();
        public Mode currentMode { get; set; }
        bool ModeOpen = false;

        //이진화모드
        ThresholdForm thresholdForm = new ThresholdForm();

        public IplImage imgBox {get;set;}
        IplImage beforeErode;
        IplImage beforeNegative;
        string imgName;
        bool isImgOpen;

        #region //* BasicForm() *//
        public BasicForm()
        {
            InitializeComponent();
            Directory.CreateDirectory(@"C:\TookTemp");

            //변환모드 선택 폼
            selectMode.BaseForm = this;
            currentMode = Mode.DEFAULT;
            MoveChildForms();

            //이진화 조정 폼
            thresholdForm.BaseForm = this;
            thresholdForm.TopMost = true;
        }

        private void BasicForm_Load(object sender, EventArgs e)
        {
            PrivateFontCollection Fonts = new PrivateFontCollection();
            Fonts.AddFontFile(@"Library\Font\NANUMBARUNGOTHIC.TTF");

            Font NBG10 = new Font(Fonts.Families[0], 10f);
            Font NBG12 = new Font(Fonts.Families[0], 12f);
            Font NBG14 = new Font(Fonts.Families[0], 14.254f);

            button_Open.Font = NBG12;
            button_Convert.Font = NBG14;
            label_mm.Font = NBG12;
            radioButton_NONE.Font = NBG10;
            radioButton_Polygon.Font = NBG10;
            radioButton_Background.Font = NBG10;
            checkBox_isNegative.Font = NBG10;

        }

        //* BUTTON_EXIT *//
        private void button_EXIT_Click(object sender, EventArgs e)
        {
            Directory.Delete(@"C:\TookTemp", true);
            System.Environment.Exit(-1);
            Application.Exit();
        }

        //숫자만입력()
        private void textBox_SetZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region //* 폼 이동() *//
        Point mousePoint;
        private void BasicForm_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        private void BasicForm_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X),
                    this.Top - (mousePoint.Y - e.Y));
            }
        }

        private void BasicForm_Move(object sender, EventArgs e)
        {
            MoveChildForms();
        }

        //* MOVE CHILDFORMS *//
        private void MoveChildForms()
        {
            selectMode.Location = new Point(Location.X, Location.Y + Height + 5);
            thresholdForm.Location = new Point(Location.X+90, Location.Y+60);
        }

        #endregion
        

        //* BUTTON_OPEN *//
        private void button_Open_Click(object sender, EventArgs e)
        {
            string filterName = "IMAGE(*.jpg, *.gif,*.jpeg, *.jpe, *.bmp, *.png) | *.jpg; *.gif; *.jpeg; *.jpe; *.bmp; *.png|";
            filterName = filterName + "Jpeg File(*.jpg)|*.jpg|Bitmap File(*.bmp)|*.bmp|Gif FIle(*.gif)|*.gif|PNG Files(*.png)|*.png";

            openFileDialog1.Title = "투디가툭튀어나오다";
            openFileDialog1.Filter = filterName;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imgName = openFileDialog1.FileName;
                isImgOpen = true;
                checkBox_isNegative.Enabled = true;
                trackBar_Bold.Enabled = true;
                button_Threshold.Enabled = true;
                trackBar_Bold.Value = 0;
                loadImage(openFileDialog1.FileName);
            }
            else
            {
                return;
            }
        }

        //* LOAD IMAGE *//
        private void loadImage(string filename)
        {
            checkBox_isNegative.Checked = false;
            bool checkSize = false;
            IplImage temp = new IplImage(filename, LoadMode.GrayScale);

            // Check Image Size...
            do
            {
                if (temp.Width > 1000 && temp.Height > 1000)
                {
                    int w = temp.Width / 2, h = temp.Height / 2;
                    imgBox = new IplImage(w, h, temp.Depth, temp.NChannels);
                    temp.Resize(imgBox);
                }
                else
                {
                    imgBox = temp;
                    checkSize = true;
                }
                temp = imgBox;
            } while (!checkSize);

            //imgBox.Erode(imgBox);   // 보류
            beforeErode = imgBox.Clone();
            thresholdForm.srcImage = imgBox.Clone();
            pictureBox.ImageIpl = imgBox;
            temp = null;
        }


        #region << CONVERT >>

        //* BUTTON_CONVERT *//
        private void button_Convert_Click(object sender, EventArgs e)
        {
            IplImage tempImgBox;
            Took3D.SET_Z = Int32.Parse(textBox_SetZ.Text);
            if (isImgOpen == true)
            {

                switch (currentMode)
                {
                    // <<DEFAULT>>
                    case Mode.DEFAULT:
                        // 흰색 윤곽선 추가.
                        tempImgBox = new IplImage(imgBox.Width + 10, imgBox.Height + 10, BitDepth.U8, 1);
                        imgBox.CopyMakeBorder(tempImgBox, new CvPoint(5, 5), BorderType.Constant, CvScalar.ScalarAll(0xFF));

                        // 모델링
                        Took3D.START(tempImgBox);

                        if (radioButton_Polygon.Checked == true)
                        {
                            bool checkBottomMode = Took3D.bottomPolygon();
                            if (checkBottomMode == false)
                                radioButton_Background.Checked = true;

                            Took3D.binarySTL_Bottom();
                        }
                        else if (radioButton_Background.Checked == true)
                        {
                            Took3D.bottomBackground();
                            Took3D.binarySTL_BackBottom();
                        }
                        else
                        {
                            Took3D.binarySTL();
                        }
                        break;

                    // <<STAMP>>
                    case Mode.STAMP:
                        tempImgBox = mode_Stamp(imgBox);    
                        Took3D.START(tempImgBox);
                        Took3D.stampModeling();
                        Took3D.binarySTL_BackBottom();

                        break;

                    // <<DOT>>
                    case Mode.DOT:
                        // 1. 크기조정
                        tempImgBox = Dot_Resize(imgBox);

                        // 2. 이미지 확대 ( 도트 원형 유지 )
                        IplImage dot_tmp = new IplImage(tempImgBox.Width * 10, tempImgBox.Height * 10, tempImgBox.Depth, tempImgBox.NChannels);
                        tempImgBox.Resize(dot_tmp, Interpolation.Cubic);
                        tempImgBox.Dispose();

                        // 3. 도트 이미지 생성
                        tempImgBox = mode_Dot(dot_tmp);
                        dot_tmp.Dispose();

                        // 4. 흰색윤곽추가
                        dot_tmp = new IplImage(tempImgBox.Width + 4, tempImgBox.Height + 4, tempImgBox.Depth, tempImgBox.NChannels);
                        tempImgBox.CopyMakeBorder(dot_tmp, new CvPoint(2, 2), BorderType.Constant, CvScalar.ScalarAll(0xFF));
                        tempImgBox = dot_tmp;

                        // 5. 이진화 시작
                        Took3D.START(tempImgBox);
                        Took3D.binarySTL();
                        break;


                    // <<RING>>
                    case Mode.RING:
                        tempImgBox = mode_Ring(imgBox);
                        Took3D.START(tempImgBox);
                        Took3D.binarySTL();
                        break;


                    default:
                        break;
                }

                pictureBox.ImageIpl = Took3D.resultImage;

                //Console.WriteLine("minX : {0} minY : {1} maxX : {2} maxY : {3}", Took3D.minX, Took3D.minY, Took3D.maxX, Took3D.maxY);

                // 파일 저장 처리
                saveFileDialog1.Filter = "STL File(*.stl)|*.stl";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileInfo file = new FileInfo(@"C:\TookTemp\output");
                    if (file.Exists)
                    {
                        File.Copy(@"C:\TookTemp\output", saveFileDialog1.FileName, true);
                        MessageBox.Show("완료");
                    }
                    
                }
                //tempImgBox.Dispose();
            }
            else
            {
                MessageBox.Show("이미지를 선택해주세요!");
            }
        }



        //* MODE : STAMP *//
        private IplImage mode_Stamp(IplImage srcImg)
        {
            // 1. 객체추출
            Took3D.checkSize(srcImg);
            int minX = Took3D.minX, minY = Took3D.minY;
            int maxX = Took3D.maxX - minX, maxY = Took3D.maxY - minY;

            srcImg.SetROI(new CvRect(minX, minY, maxX + 1, maxY + 1));
            IplImage src = new IplImage(maxX + 1, maxY + 1, srcImg.Depth, srcImg.NChannels);
            srcImg.Copy(src);

            // 2. 도장이미지 생성
            IplImage stampImg = new IplImage(200, 200, src.Depth, src.NChannels);
            stampImg.Set(CvScalar.ScalarAll(255));

            // 3. 도장이미지 크기조정
            int roi_width = 175;
            int roi_height = 175;

            IplImage gr_hole;
            int setHeight = 0, setWidth = 0;

            if (src.Width > src.Height)
            {
                setWidth = roi_width;
                setHeight = (roi_width * src.Height) / src.Width;

                if (setHeight > roi_height)
                {
                    setHeight = roi_height;
                    setWidth = (roi_height * setWidth) / setHeight;
                }
                gr_hole = new IplImage(setWidth, setHeight, src.Depth, src.NChannels);
            }
            else if (src.Width < src.Height)
            {
                setHeight = roi_height;
                setWidth = (roi_height * src.Width) / src.Height;

                if (setWidth > roi_width)
                {
                    setWidth = roi_width;
                    setHeight = (roi_width * setHeight) / setWidth;
                }

                gr_hole = new IplImage(setWidth, setHeight, src.Depth, src.NChannels);
            }
            else
            {
                setHeight = roi_height;
                setWidth = (roi_height * src.Width) / src.Height;
                gr_hole = new IplImage(setWidth, setHeight, src.Depth, src.NChannels);
            }
            src.Resize(gr_hole, Interpolation.Cubic);

            // 4. 위치 조정
            int mid_X = (200 / 2) - (gr_hole.Width / 2);
            int mid_Y = (200 / 2) - (gr_hole.Height / 2);
            stampImg.SetROI(mid_X, mid_Y, gr_hole.Width, gr_hole.Height);

            // 5. 삽입
            gr_hole.Copy(stampImg);

            // 6. 메모리 정리
            srcImg.ResetROI();
            stampImg.ResetROI();
            gr_hole.Dispose();
            src.Dispose();

            return stampImg;
        }


        //* MODE : DOT *//
        private IplImage mode_Dot(IplImage srcImg)
        {
            IplImage srcImage = srcImg;
            srcImage.Threshold(srcImage, 150, 255, ThresholdType.Otsu);

            //점이 찍어질 까만 이미지
            IplImage dotImage = Cv.CreateImage(Cv.GetSize(srcImage), (BitDepth)CvConst.IPL_DEPTH_8U, 1);
            Cv.Set(dotImage, new CvScalar(0, 0, 0));

            //흑백이진화 된 원래 이미지에서 지정 픽셀이 검은색이면 흰 원을 그린다.
            double r;

            // 비율조정
            int ratio;

            if (srcImg.Height > srcImg.Width)
                ratio = srcImg.Width;
            else
                ratio = srcImg.Height;

            // DRAW DOT
            for (int Y = 0; Y < srcImg.Height; Y += (ratio / 40))
            {
                for (int X = 0; X < srcImg.Width; X += (ratio / 40))
                {
                    r = Cv.GetReal2D(srcImg, Y, X);

                    if (r == 0)
                    {
                        Cv.DrawCircle(dotImage, new CvPoint(X, Y), (int)(0.008 * ratio), Cv.RGB(255, 255, 255), Cv.FILLED);
                    }
                }
            }

            // 이미지 축소
            IplImage dst_dotImgae = new IplImage(dotImage.Width / 10, dotImage.Height / 10, dotImage.Depth, dotImage.NChannels);
            dotImage.Resize(dst_dotImgae);
            dotImage.Smooth(dotImage, SmoothType.Gaussian);
            dotImage.Dispose();

            return dst_dotImgae;
        }
        private IplImage Dot_Resize(IplImage srcImg)
        {
            // 1. 객체추출
            Took3D.checkSize(srcImg);
            int minX = Took3D.minX, minY = Took3D.minY;
            int maxX = Took3D.maxX - minX, maxY = Took3D.maxY - minY;

            srcImg.SetROI(new CvRect(minX, minY, maxX, maxY));
            IplImage src = new IplImage(maxX, maxY, srcImg.Depth, srcImg.NChannels);
            srcImg.Copy(src);

            // 2. 도트 밑판 생성
            IplImage dotBackImg = new IplImage(500, 500, src.Depth, src.NChannels);
            dotBackImg.Set(CvScalar.ScalarAll(255));

            // 3. 도트이미지 크기조정
            int roi_width = 470;
            int roi_height = 470;

            IplImage temp;
            int setHeight = 0, setWidth = 0;

            if (src.Width > src.Height)
            {
                setWidth = roi_width;
                setHeight = (roi_width * src.Height) / src.Width;

                if (setHeight > roi_height)
                {
                    setHeight = roi_height;
                    setWidth = (roi_height * setWidth) / setHeight;
                }
                temp = new IplImage(setWidth, setHeight, src.Depth, src.NChannels);
            }
            else if (src.Width < src.Height)
            {
                setHeight = roi_height;
                setWidth = (roi_height * src.Width) / src.Height;

                if (setWidth > roi_width)
                {
                    setWidth = roi_width;
                    setHeight = (roi_width * setHeight) / setWidth;
                }

                temp = new IplImage(setWidth, setHeight, src.Depth, src.NChannels);
            }
            else
            {
                setHeight = roi_height;
                setWidth = (roi_height * src.Width) / src.Height;
                temp = new IplImage(setWidth, setHeight, src.Depth, src.NChannels);
            }
            src.Resize(temp, Interpolation.Cubic);

            // 4. 위치 조정
            int mid_X = (500 / 2) - (temp.Width / 2);
            int mid_Y = (500 / 2) - (temp.Height / 2);
            dotBackImg.SetROI(mid_X, mid_Y, temp.Width, temp.Height);

            // 5. 삽입
            temp.Copy(dotBackImg);

            // 6. 메모리 정리
            srcImg.ResetROI();
            dotBackImg.ResetROI();
            temp.Dispose();
            src.Dispose();

            return dotBackImg;
        }


        //* MODE : RING *//
        private IplImage mode_Ring(IplImage srcImg)
        {
            // 1. 링(고리) 생성
            IplImage temp = Properties.Resources.gr.ToIplImage();
            IplImage gr = new IplImage(temp.Size, srcImg.Depth, srcImg.NChannels);
            temp.CvtColor(gr, ColorConversion.BgrToGray);
            temp.Dispose(); //해제


            // 2. 객체추출
            Took3D.checkSize(srcImg);
            int minX = Took3D.minX, minY = Took3D.minY;
            int maxX = Took3D.maxX - minX, maxY = Took3D.maxY - minY;

            srcImg.SetROI(new CvRect(minX, minY, maxX, maxY));
            IplImage src = new IplImage(maxX, maxY, srcImg.Depth, srcImg.NChannels);
            srcImg.Copy(src);


            // 3. 이미지 변환
            Took3D.negativeImg(src);


            // 4. 이미지 크기 조정
            IplImage gr_hole;

            int roi_X = 37;
            int roi_Y = 226;
            int roi_width = 204;
            int roi_height = 175;

            //int X_wid = 240;
            //int Y_hei = 379;

            int setHeight = 0, setWidth = 0;

            if (src.Width > src.Height)
            {
                setWidth = roi_width;
                setHeight = (roi_width * src.Height) / src.Width;

                if (setHeight > roi_height)
                {
                    setHeight = roi_height;
                    setWidth = (roi_height * setWidth) / setHeight;
                }
                gr_hole = new IplImage(setWidth, setHeight, src.Depth, src.NChannels);
            }
            else if (src.Width < src.Height)
            {
                setHeight = roi_height;
                setWidth = (roi_height * src.Width) / src.Height;

                if (setWidth > roi_width)
                {
                    setWidth = roi_width;
                    setHeight = (roi_width * setHeight) / setWidth;
                }

                gr_hole = new IplImage(setWidth, setHeight, src.Depth, src.NChannels);
            }
            else
            {
                setHeight = roi_height;
                setWidth = (roi_height * src.Width) / src.Height;
                gr_hole = new IplImage(setWidth, setHeight, src.Depth, src.NChannels);
            }
            src.Resize(gr_hole, Interpolation.Cubic);


            //5. 위치 조정
            int mid_X = (roi_width / 2) - (gr_hole.Width / 2);
            gr.SetROI(roi_X + mid_X, roi_Y, gr_hole.Width, gr_hole.Height);


            //6. 합성
            gr_hole.Copy(gr);


            //7. 메모리 정리
            gr.ResetROI();
            srcImg.ResetROI();
            gr_hole.Dispose();
            src.Dispose();

            return gr;
        }

        #endregion
        

        //* CHECKBOX _ Negative *//
        private void checkBox_isNegative_CheckedChanged(object sender, EventArgs e)
        {
            Took3D.negativeImg(imgBox);
            imgBox.Copy(beforeErode);
            pictureBox.ImageIpl = imgBox;
        }

        //* TrackBar _ Erode *//
        private void trackBar_Erode_Scroll(object sender, EventArgs e)
        {
            IplConvKernel element = new IplConvKernel(3,3,1,1,ElementShape.Rect,null);

            if (checkBox_isNegative.Checked == false)
            {
                if (trackBar_Bold.Value == 1)
                {
                    beforeErode.Erode(imgBox, element, 1);
                    pictureBox.ImageIpl = imgBox;
                }
                else if (trackBar_Bold.Value == 2)
                {
                    beforeErode.Erode(imgBox, element, 2);
                    pictureBox.ImageIpl = imgBox;
                }
                else if (trackBar_Bold.Value == 3)
                {
                    beforeErode.Erode(imgBox, element, 3);
                    pictureBox.ImageIpl = imgBox;
                }
                else if (trackBar_Bold.Value == 4)
                {
                    beforeErode.Erode(imgBox, element, 4);
                    pictureBox.ImageIpl = imgBox;
                }
                else if (trackBar_Bold.Value == 5)
                {
                    beforeErode.Erode(imgBox, element, 5);
                    pictureBox.ImageIpl = imgBox;
                }
                else
                {
                    beforeErode.Copy(imgBox);
                    pictureBox.ImageIpl = imgBox;
                }
            }
            else if (checkBox_isNegative.Checked == true)
            {
                if (trackBar_Bold.Value == 1)
                {
                    beforeErode.Dilate(imgBox, element, 1);
                    pictureBox.ImageIpl = imgBox;
                }
                else if (trackBar_Bold.Value == 2)
                {
                    beforeErode.Dilate(imgBox, element, 2);
                    pictureBox.ImageIpl = imgBox;
                }
                else if (trackBar_Bold.Value == 3)
                {
                    beforeErode.Dilate(imgBox, element, 3);
                    pictureBox.ImageIpl = imgBox;
                }
                else if (trackBar_Bold.Value == 4)
                {
                    beforeErode.Dilate(imgBox, element, 4);
                    pictureBox.ImageIpl = imgBox;
                }
                else if (trackBar_Bold.Value == 5)
                {
                    beforeErode.Dilate(imgBox, element, 5);
                    pictureBox.ImageIpl = imgBox;
                }
                else
                {
                    beforeErode.Copy(imgBox);
                    pictureBox.ImageIpl = imgBox;
                }
            }
        }


        #region <<MODE CHANGE>>

        //* Mode Change *//
        private void button_Mode_Click(object sender, EventArgs e)
        {
            if (ModeOpen == false)
            {
                selectMode.Show(this);
                ModeOpen = true;
            }
            else
            {
                selectMode.Hide();
                ModeOpen = false;
            }
        }

        //* 버튼클릭, 그림 변경 *//
        public void ModeImageChange()
        {
            if (currentMode == Mode.DEFAULT)
            {
                button_Mode.Image = Properties.Resources.bc_mode;
                ModeOpen = false;
            }
            else if (currentMode == Mode.STAMP)
            {
                button_Mode.Image = Properties.Resources.stamp_mode;
                ModeOpen = false;
                textBox_SetZ.Text = "75";
            }
            else if (currentMode == Mode.DOT)
            {
                button_Mode.Image = Properties.Resources.dot_mode;
                ModeOpen = false;
                textBox_SetZ.Text = "10";
            }
            else if (currentMode == Mode.RING)
            {
                button_Mode.Image = Properties.Resources.ring_mode;
                ModeOpen = false;
                textBox_SetZ.Text = "10";
            }

            //상태변경
            if (currentMode == Mode.DEFAULT)
            {
                textBox_SetZ.Enabled = true;
                radioButton_NONE.Enabled = true;
                radioButton_Polygon.Enabled = true;
                radioButton_Background.Enabled = true;
            }
            else
            {
                textBox_SetZ.Enabled = false;
                radioButton_NONE.Enabled = false;
                radioButton_Polygon.Enabled = false;
                radioButton_Background.Enabled = false;
            }
        }

        #endregion


        //* 이진화조정 *//
        private void button_Threshold_Click(object sender, EventArgs e)
        {
            IplConvKernel element = new IplConvKernel(3,3,1,1,ElementShape.Rect,null);

            if (checkBox_isNegative.Checked == false)
                beforeErode.Erode(thresholdForm.srcImage, element, trackBar_Bold.Value);
            else
                beforeErode.Dilate(thresholdForm.srcImage, element, trackBar_Bold.Value);

           
            thresholdForm.Show(this);
        }

        public void changePictureBox(IplImage dst)
        {
            pictureBox.ImageIpl = dst;
        }
    }
}
