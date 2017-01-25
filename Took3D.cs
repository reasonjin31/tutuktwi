using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenCvSharp;

namespace TOOK
{
    static class Took3D
    {
        public static int SET_Z { get; set; }
        public static int SET_BOTTOM_Z = 10;

        public static IplImage srcImage;
        public static IplImage resultImage;

        public static int imageWidth;
        public static int imageHeight;

        public static int minX, minY, maxX, maxY;

        public static Queue<Triangle> TRI; 
        public static Queue<Triangle> BOTTOM;


        //* Image를 통해 삼각화 시작 *//
        public static void START(IplImage imageName)
        {
            // 영상이진화 //
            srcImage = imageName;
            Cv.Flip(srcImage);
            Cv.Threshold(srcImage, srcImage, 150, 255, ThresholdType.Otsu);

            // 삼각화 //
            triangulate();


            // 화면출력 //
            //Cv.Flip(srcImage);
            resultImage = srcImage.Clone();
            Cv.SetImageROI(resultImage, new CvRect(minX, minY, (maxX-minX), (maxY-minY)));
            Cv.Flip(resultImage);
        }

        //* 삼각화, 좌표추출 *//
        private static void triangulate()
        {
            TRI = new Queue<Triangle>();
            minX = srcImage.Width-1; 
            minY = srcImage.Height-1; 
            maxX = 0; 
            maxY = 0;

            unsafe
            {
                // 픽셀 별 높이(Z) 설정
                int[,] Z = new int[srcImage.Width, srcImage.Height];
                for (int Y = 0; Y < srcImage.Height; Y++)
                {
                    for (int X = 0; X < srcImage.Width; X++)
                    {
                        if (srcImage.ImageDataPtr[Y * srcImage.WidthStep + X] == 0)
                        {
                            Z[X, Y] = SET_Z;
                            if (X > maxX)
                                maxX = X;
          
                            if (Y > maxY)
                                maxY = Y;

                            if (X > 0 && X < minX)
                                minX = X;

                            if (Y > 0 && Y < minY)
                                minY = Y;
                        }
                        else
                            Z[X, Y] = 0;
                    }//END:FOR(Z)
                }//END:FOR(Y)

                //Check Line
                bool[,] lineOK = new bool[(srcImage.Width + 2), (srcImage.Height + 2)];

                // (X, Y, Z) 설정
                for (int Y = 1; Y < srcImage.Height - 2; Y++)
                {
                    for (int X = 1; X < srcImage.Width - 2; X++)
                    {
                        if (Z[X, Y] > 0 || Z[X + 1, Y] > 0 || Z[X, Y + 1] > 0 || Z[X + 1, Y + 1] > 0)
                        {
                            // 오른쪽직각삼각형 만들기
                            if (Z[X, Y + 1] == 0)
                            {
                                Triangle try1 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y,
                                    Z1 = Z[X, Y],
                                    X2 = X + 1,
                                    Y2 = Y + 1,
                                    Z2 = Z[X + 1, Y + 1],
                                    X3 = X,
                                    Y3 = Y + 1,
                                    Z3 = Z[X, Y + 1]
                                };

                                Triangle try2 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y,
                                    Z1 = Z[X, Y],
                                    X2 = X + 1,
                                    Y2 = Y,
                                    Z2 = Z[X + 1, Y],
                                    X3 = X + 1,
                                    Y3 = Y + 1,
                                    Z3 = Z[X + 1, Y + 1]
                                };
                                TRI.Enqueue(try1);
                                TRI.Enqueue(try2);
                            }
                            // 왼쪽직각삼각형 만들기
                            else if (Z[X + 1, Y + 1] == 0)
                            {
                                Triangle try1 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y,
                                    Z1 = Z[X, Y],
                                    X2 = X + 1,
                                    Y2 = Y,
                                    Z2 = Z[X + 1, Y],
                                    X3 = X,
                                    Y3 = Y + 1,
                                    Z3 = Z[X, Y + 1]
                                };

                                Triangle try2 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y + 1,
                                    Z1 = Z[X, Y + 1],
                                    X2 = X + 1,
                                    Y2 = Y,
                                    Z2 = Z[X + 1, Y],
                                    X3 = X + 1,
                                    Y3 = Y + 1,
                                    Z3 = Z[X + 1, Y + 1]
                                };
                                TRI.Enqueue(try1);
                                TRI.Enqueue(try2);
                            }
                            //
                            else if (Z[X, Y] == 0)
                            {
                                Triangle try1 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y,
                                    Z1 = Z[X, Y],
                                    X2 = X + 1,
                                    Y2 = Y,
                                    Z2 = Z[X + 1, Y],
                                    X3 = X,
                                    Y3 = Y + 1,
                                    Z3 = Z[X, Y + 1]
                                };
                                Triangle try2 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y + 1,
                                    Z1 = Z[X, Y + 1],
                                    X2 = X + 1,
                                    Y2 = Y,
                                    Z2 = Z[X + 1, Y],
                                    X3 = X + 1,
                                    Y3 = Y + 1,
                                    Z3 = Z[X + 1, Y + 1]
                                };
                                TRI.Enqueue(try1);
                                TRI.Enqueue(try2);
                            }
                            //
                            else if (Z[X + 1, Y] == 0)
                            {
                                Triangle try1 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y,
                                    Z1 = Z[X, Y],
                                    X2 = X + 1,
                                    Y2 = Y + 1,
                                    Z2 = Z[X + 1, Y + 1],
                                    X3 = X,
                                    Y3 = Y + 1,
                                    Z3 = Z[X, Y + 1]
                                };
                                Triangle try2 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y,
                                    Z1 = Z[X, Y],
                                    X2 = X + 1,
                                    Y2 = Y,
                                    Z2 = Z[X + 1, Y],
                                    X3 = X + 1,
                                    Y3 = Y + 1,
                                    Z3 = Z[X + 1, Y + 1]
                                };
                                TRI.Enqueue(try1);
                                TRI.Enqueue(try2);
                            }
                            //Check Line
                            else
                            {
                                lineOK[X, Y] = true;
                            }
                        }//END:IF
                    }//END:FOR(X) _ XYZ

                    // Line Triangulate
                    int pxInLine = 0;
                    List<int> arr = new List<int>();
                    List<int> arr2 = new List<int>();

                    for (int X = 1; X < srcImage.Width - 2; X++)
                    {
                        if (Z[X, Y] > 0 || Z[X + 1, Y] > 0 || Z[X, Y + 1] > 0 || Z[X + 1, Y + 1] > 0)
                        {
                            if (lineOK[X, Y])
                                arr.Add(X);
                        }
                    }

                    if(arr.Count>0)
                    {
                        arr2.Add(arr[0]);
                        for(int f=0; f<arr.Count-1 ;f++)
                        {
                            if (arr[f]+1 != arr[f+1])
                            {
                                arr2.Add(arr[f]);
                                arr2.Add(arr[f + 1]);
                                pxInLine++;
                            }
                        }

                        arr2.Add(arr[arr.Count-1]);//arr2 어레이엔 6,8,13,15가 들어가게됨
                        pxInLine=pxInLine+1;

                        int n = 0;
                        for (int h = 1; h <= pxInLine; h++)
                        {
                            Triangle try1 = new Triangle()
                            {
                                X1 = arr2[n],
                                Y1 = Y,
                                Z1 = Z[arr2[n], Y],
                                X2 = arr2[n+1]+1,
                                Y2 = Y + 1,
                                Z2 = Z[arr2[n+1]+1, Y + 1],
                                X3 = arr2[n],
                                Y3 = Y + 1,
                                Z3 = Z[arr2[n], Y + 1]
                            };

                            Triangle try2 = new Triangle()
                            {
                                X1 = arr2[n],
                                Y1 = Y,
                                Z1 = Z[arr2[n], Y],
                                X2 = arr2[n + 1]+1,
                                Y2 = Y,
                                Z2 = Z[arr2[n + 1]+ 1, Y],
                                X3 = arr2[n + 1]+1,
                                Y3 = Y + 1,
                                Z3 = Z[arr2[n + 1]+1, Y + 1]
                            };
                            TRI.Enqueue(try1);
                            TRI.Enqueue(try2);
                            
                            n = n + 2;
                        }
                    }//END:LINE_FOR(X)
                }//END:FOR(Y) _ XYZ
            }//END:unsafe
        }

        //* 삼각화, 좌표추출 _ Bottom Polygon *//
        private static void triangulate(IplImage btImage)
        {
            BOTTOM = new Queue<Triangle>();

            unsafe
            {
                // 픽셀 별 높이(Z) 설정
                int[,] Z = new int[btImage.Width, btImage.Height];
                for (int Y = 0; Y < btImage.Height; Y++)
                {
                    for (int X = 0; X < btImage.Width; X++)
                    {
                        if (btImage.ImageDataPtr[Y * btImage.WidthStep + X] == 0)
                            Z[X, Y] = SET_BOTTOM_Z;
                        else
                            Z[X, Y] = 0;
                    }//END:FOR(Z)
                }//END:FOR(Y)

                //Check Line
                bool[,] lineOK = new bool[(btImage.Width + 2), (btImage.Height + 2)];

                // (X, Y, Z) 설정
                for (int Y = 1; Y < btImage.Height - 2; Y++)
                {
                    for (int X = 1; X < btImage.Width - 2; X++)
                    {
                        if (Z[X, Y] > 0 || Z[X + 1, Y] > 0 || Z[X, Y + 1] > 0 || Z[X + 1, Y + 1] > 0)
                        {
                            // 오른쪽직각삼각형 만들기
                            if (Z[X, Y + 1] == 0)
                            {
                                Triangle try1 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y,
                                    Z1 = Z[X, Y],
                                    X2 = X + 1,
                                    Y2 = Y + 1,
                                    Z2 = Z[X + 1, Y + 1],
                                    X3 = X,
                                    Y3 = Y + 1,
                                    Z3 = Z[X, Y + 1]
                                };

                                Triangle try2 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y,
                                    Z1 = Z[X, Y],
                                    X2 = X + 1,
                                    Y2 = Y,
                                    Z2 = Z[X + 1, Y],
                                    X3 = X + 1,
                                    Y3 = Y + 1,
                                    Z3 = Z[X + 1, Y + 1]
                                };
                                BOTTOM.Enqueue(try1);
                                BOTTOM.Enqueue(try2);
                            }
                            // 왼쪽직각삼각형 만들기
                            else if (Z[X + 1, Y + 1] == 0)
                            {
                                Triangle try1 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y,
                                    Z1 = Z[X, Y],
                                    X2 = X + 1,
                                    Y2 = Y,
                                    Z2 = Z[X + 1, Y],
                                    X3 = X,
                                    Y3 = Y + 1,
                                    Z3 = Z[X, Y + 1]
                                };

                                Triangle try2 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y + 1,
                                    Z1 = Z[X, Y + 1],
                                    X2 = X + 1,
                                    Y2 = Y,
                                    Z2 = Z[X + 1, Y],
                                    X3 = X + 1,
                                    Y3 = Y + 1,
                                    Z3 = Z[X + 1, Y + 1]
                                };
                                BOTTOM.Enqueue(try1);
                                BOTTOM.Enqueue(try2);
                            }
                            //
                            else if (Z[X, Y] == 0)
                            {
                                Triangle try1 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y,
                                    Z1 = Z[X, Y],
                                    X2 = X + 1,
                                    Y2 = Y,
                                    Z2 = Z[X + 1, Y],
                                    X3 = X,
                                    Y3 = Y + 1,
                                    Z3 = Z[X, Y + 1]
                                };
                                Triangle try2 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y + 1,
                                    Z1 = Z[X, Y + 1],
                                    X2 = X + 1,
                                    Y2 = Y,
                                    Z2 = Z[X + 1, Y],
                                    X3 = X + 1,
                                    Y3 = Y + 1,
                                    Z3 = Z[X + 1, Y + 1]
                                };
                                BOTTOM.Enqueue(try1);
                                BOTTOM.Enqueue(try2);
                            }
                            //
                            else if (Z[X + 1, Y] == 0)
                            {
                                Triangle try1 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y,
                                    Z1 = Z[X, Y],
                                    X2 = X + 1,
                                    Y2 = Y + 1,
                                    Z2 = Z[X + 1, Y + 1],
                                    X3 = X,
                                    Y3 = Y + 1,
                                    Z3 = Z[X, Y + 1]
                                };
                                Triangle try2 = new Triangle()
                                {
                                    X1 = X,
                                    Y1 = Y,
                                    Z1 = Z[X, Y],
                                    X2 = X + 1,
                                    Y2 = Y,
                                    Z2 = Z[X + 1, Y],
                                    X3 = X + 1,
                                    Y3 = Y + 1,
                                    Z3 = Z[X + 1, Y + 1]
                                };
                                BOTTOM.Enqueue(try1);
                                BOTTOM.Enqueue(try2);
                            }
                            //Check Line
                            else
                            {
                                lineOK[X, Y] = true;
                            }
                        }//END:IF
                    }//END:FOR(X) _ XYZ

                    // Line Triangulate
                    int pxInLine = 0;
                    List<int> arr = new List<int>();
                    List<int> arr2 = new List<int>();

                    for (int X = 1; X < btImage.Width - 2; X++)
                    {
                        if (Z[X, Y] > 0 || Z[X + 1, Y] > 0 || Z[X, Y + 1] > 0 || Z[X + 1, Y + 1] > 0)
                        {
                            if (lineOK[X, Y])
                                arr.Add(X);
                        }
                    }

                    if (arr.Count > 0)
                    {
                        arr2.Add(arr[0]);
                        for (int f = 0; f < arr.Count - 1; f++)
                        {
                            if (arr[f] + 1 != arr[f + 1])
                            {
                                arr2.Add(arr[f]);
                                arr2.Add(arr[f + 1]);
                                pxInLine++;
                            }
                        }

                        arr2.Add(arr[arr.Count - 1]);//arr2 어레이엔 6,8,13,15가 들어가게됨
                        pxInLine = pxInLine + 1;

                        int n = 0;
                        for (int h = 1; h <= pxInLine; h++)
                        {
                            Triangle try1 = new Triangle()
                            {
                                X1 = arr2[n],
                                Y1 = Y,
                                Z1 = Z[arr2[n], Y],
                                X2 = arr2[n + 1] + 1,
                                Y2 = Y + 1,
                                Z2 = Z[arr2[n + 1] + 1, Y + 1],
                                X3 = arr2[n],
                                Y3 = Y + 1,
                                Z3 = Z[arr2[n], Y + 1]
                            };

                            Triangle try2 = new Triangle()
                            {
                                X1 = arr2[n],
                                Y1 = Y,
                                Z1 = Z[arr2[n], Y],
                                X2 = arr2[n + 1] + 1,
                                Y2 = Y,
                                Z2 = Z[arr2[n + 1] + 1, Y],
                                X3 = arr2[n + 1] + 1,
                                Y3 = Y + 1,
                                Z3 = Z[arr2[n + 1] + 1, Y + 1]
                            };
                            BOTTOM.Enqueue(try1);
                            BOTTOM.Enqueue(try2);

                            n = n + 2;
                        }
                    }//END:LINE_FOR(X)
                }//END:FOR(Y) _ XYZ
            }//END:unsafe
        }

        //* 밑면 삼각화 시작 *//
        public static Boolean bottomPolygon()
        {
            // 밑면추출성공여부
            bool isSuccess = false;

            // 뒷배경처리
            Cv.Flip(srcImage);
            IplImage dstImage = srcImage.Clone();
            IplConvKernel element = new IplConvKernel(3, 3, 1, 1, ElementShape.Rect, null);
            IplImage srcImage2;
            //dstImage.Dilate(dstImage);


            #region <<FILL THE BOTTOM>>
            // FIND Contours //
            CvMemStorage storage = Cv.CreateMemStorage(0);
            CvSeq<CvPoint> first_contour;
            int header_size = CvContour.SizeOf;
            CvPoint offset = new CvPoint(0, 0);

            bool checkFillBottom = false;
            int checkTime = 0;

            do
            {
                srcImage2 = dstImage.Clone();
                srcImage2.Erode(srcImage2);
                srcImage2.Dilate(srcImage2);
                //srcImage2.Erode(srcImage2);
                
                int nContour = Cv.FindContours(srcImage2, storage, out first_contour, header_size, ContourRetrieval.List, ContourChain.ApproxSimple, offset);
                nContour--; //전체 이미지 테두리 숫자 제거

                int k;
                CvSeq<CvPoint> contour;

                if (nContour > 1 && checkTime < 20)
                {
                    dstImage.Dispose();
                    dstImage = srcImage.Clone();
                    
                    for (contour = first_contour, k = 0; contour != null; contour = contour.HNext, k++)
                    {
                        if (contour.HNext != null)
                            Cv.DrawContours(dstImage, contour, CvColor.Black, CvColor.Black, -1, Cv.FILLED, LineType.AntiAlias);
                    }
                    checkTime++;
                }
                else if (checkTime == 20) //실패 : 기본사각형으로 밑면 설정
                {
                    checkFillBottom = true;
                    bottomBackground();
                    isSuccess = false;
                }
                else //성공
                {
                    checkFillBottom = true;
                    isSuccess = true;
                    //dstImage.Erode(dstImage);
                    dstImage.Smooth(dstImage, SmoothType.Gaussian);
                    //dstImage.Erode(dstImage);
                    //dstImage.Smooth(dstImage, SmoothType.Gaussian);
                    dstImage.Threshold(dstImage, 100, 255, ThresholdType.Otsu);
                    Cv.Flip(dstImage);
                    triangulate(dstImage);
                }
            } while (!checkFillBottom);
            #endregion

            //Console.WriteLine(checkTime);
            return isSuccess;
        }

        public static void bottomBackground()
        {
            BOTTOM = new Queue<Triangle>();

            int space = 5;
            int lowX = minX - space;
            int lowY = minY - space;
            int highX = maxX + space;
            int highY = maxY + space;

            //윗면
            Triangle try1 = new Triangle()
            {
                X1 = lowX,
                Y1 = highY,
                Z1 = SET_BOTTOM_Z,
                X2 = lowX,
                Y2 = lowY,
                Z2 = SET_BOTTOM_Z,
                X3 = highX,
                Y3 = lowY,
                Z3 = SET_BOTTOM_Z
            };

            Triangle try2 = new Triangle()
            {
                X1 = highX,
                Y1 = highY,
                Z1 = SET_BOTTOM_Z,
                X2 = lowX,
                Y2 = highY,
                Z2 = SET_BOTTOM_Z,
                X3 = highX,
                Y3 = lowY,
                Z3 = SET_BOTTOM_Z
            };

            //아랫면
            Triangle try3 = new Triangle()
            {
                X1 = lowX,
                Y1 = lowY,
                Z1 = 0,
                X2 = lowX,
                Y2 = highY,
                Z2 = 0,
                X3 = highX,
                Y3 = lowY,
                Z3 = 0
            };

            Triangle try4 = new Triangle()
            {
                X1 = lowX,
                Y1 = highY,
                Z1 = 0,
                X2 = highX,
                Y2 = highY,
                Z2 = 0,
                X3 = highX,
                Y3 = lowY,
                Z3 = 0
            };

            //기둥1
            Triangle try5 = new Triangle()
            {
                X1 = lowX,
                Y1 = highY,
                Z1 = 0,
                X2 = lowX,
                Y2 = lowY,
                Z2 = 0,
                X3 = lowX,
                Y3 = lowY,
                Z3 = SET_BOTTOM_Z
            };

            Triangle try6 = new Triangle()
            {
                X1 = lowX,
                Y1 = lowY,
                Z1 = SET_BOTTOM_Z,
                X3 = lowX,
                Y3 = highY,
                Z3 = 0,
                X2 = lowX,
                Y2 = highY,
                Z2 = SET_BOTTOM_Z
            };

            //기둥2
            Triangle try7 = new Triangle()
            {
                X1 = lowX,
                Y1 = lowY,
                Z1 = SET_BOTTOM_Z,
                X2 = lowX,
                Y2 = lowY,
                Z2 = 0,
                X3 = highX,
                Y3 = lowY,
                Z3 = SET_BOTTOM_Z,

            };

            Triangle try8 = new Triangle()
            {               
                X1 = lowX,
                Y1 = lowY,
                Z1 = 0,
                X2 = highX,
                Y2 = lowY,
                Z2 = 0,
                X3 = highX,
                Y3 = lowY,
                Z3 = SET_BOTTOM_Z
            };

            //기둥3
            Triangle try9 = new Triangle()
            {
                X1 = highX,
                Y1 = lowY,
                Z1 = SET_BOTTOM_Z,
                X2 = highX,
                Y2 = lowY,
                Z2 = 0,
                X3 = highX,
                Y3 = highY,
                Z3 = SET_BOTTOM_Z,
                
            };

            Triangle try10 = new Triangle()
            {
                X1 = highX,
                Y1 = highY,
                Z1 = SET_BOTTOM_Z,
                X2 = highX,
                Y2 = lowY,
                Z2 = 0,
                X3 = highX,
                Y3 = highY,
                Z3 = 0,

            };

            //기둥4
            Triangle try11 = new Triangle()
            {
                X1 = highX,
                Y1 = highY,
                Z1 = SET_BOTTOM_Z,
                X2 = highX,
                Y2 = highY,
                Z2 = 0,
                X3 = lowX,
                Y3 = highY,
                Z3 = SET_BOTTOM_Z
            };

            Triangle try12 = new Triangle()
            {
                X1 = highX,
                Y1 = highY,
                Z1 = 0,
                X2 = lowX,
                Y2 = highY,
                Z2 = 0,
                X3 = lowX,
                Y3 = highY,
                Z3 = SET_BOTTOM_Z        
            };

            BOTTOM.Enqueue(try1);
            BOTTOM.Enqueue(try2);
            BOTTOM.Enqueue(try3);
            BOTTOM.Enqueue(try4);
            //1
            BOTTOM.Enqueue(try5);
            BOTTOM.Enqueue(try6);
            //2
            BOTTOM.Enqueue(try7);
            BOTTOM.Enqueue(try8);
            //3
            BOTTOM.Enqueue(try9);
            BOTTOM.Enqueue(try10);
            //4
            BOTTOM.Enqueue(try11);
            BOTTOM.Enqueue(try12);

            //To STL
            //binarySTL_BackBottom();
        }


        public static void stampModeling()
        {
            BOTTOM = new Queue<Triangle>();

            //lowX, highX, lowY, highY
            int s1_X1 = 0, s1_X2 = 200, s1_Y1 = 0, s1_Y2 = 200;
            int s2_X1 = 10, s2_X2 = 190, s2_Y1 = 10, s2_Y2 = 190;

            // 기둥 높이
            int s1_Z = 50, s2_Z = 65;

            #region 도장판_아래
            //윗면
            Triangle s1_1 = new Triangle()
            {
                X1 = s1_X1,
                Y1 = s1_Y2,
                Z1 = s1_Z,
                X2 = s1_X1,
                Y2 = s1_Y1,
                Z2 = s1_Z,
                X3 = s1_X2,
                Y3 = s1_Y1,
                Z3 = s1_Z
            };

            Triangle s1_2 = new Triangle()
            {
                X1 = s1_X2,
                Y1 = s1_Y2,
                Z1 = s1_Z,
                X2 = s1_X1,
                Y2 = s1_Y2,
                Z2 = s1_Z,
                X3 = s1_X2,
                Y3 = s1_Y1,
                Z3 = s1_Z
            };

            //아랫면
            Triangle s1_3 = new Triangle()
            {
                X1 = s1_X1,
                Y1 = s1_Y1,
                Z1 = 0,
                X2 = s1_X1,
                Y2 = s1_Y2,
                Z2 = 0,
                X3 = s1_X2,
                Y3 = s1_Y1,
                Z3 = 0
            };

            Triangle s1_4 = new Triangle()
            {
                X1 = s1_X1,
                Y1 = s1_Y2,
                Z1 = 0,
                X2 = s1_X2,
                Y2 = s1_Y2,
                Z2 = 0,
                X3 = s1_X2,
                Y3 = s1_Y1,
                Z3 = 0
            };

            //기둥1
            Triangle s1_5 = new Triangle()
            {
                X1 = s1_X1,
                Y1 = s1_Y2,
                Z1 = 0,
                X2 = s1_X1,
                Y2 = s1_Y1,
                Z2 = 0,
                X3 = s1_X1,
                Y3 = s1_Y1,
                Z3 = s1_Z
            };

            Triangle s1_6 = new Triangle()
            {
                X1 = s1_X1,
                Y1 = s1_Y1,
                Z1 = s1_Z,
                X3 = s1_X1,
                Y3 = s1_Y2,
                Z3 = 0,
                X2 = s1_X1,
                Y2 = s1_Y2,
                Z2 = s1_Z
            };

            //기둥2
            Triangle s1_7 = new Triangle()
            {
                X1 = s1_X1,
                Y1 = s1_Y1,
                Z1 = s1_Z,
                X2 = s1_X1,
                Y2 = s1_Y1,
                Z2 = 0,
                X3 = s1_X2,
                Y3 = s1_Y1,
                Z3 = s1_Z,

            };

            Triangle s1_8 = new Triangle()
            {
                X1 = s1_X1,
                Y1 = s1_Y1,
                Z1 = 0,
                X2 = s1_X2,
                Y2 = s1_Y1,
                Z2 = 0,
                X3 = s1_X2,
                Y3 = s1_Y1,
                Z3 = s1_Z
            };

            //기둥3
            Triangle s1_9 = new Triangle()
            {
                X1 = s1_X2,
                Y1 = s1_Y1,
                Z1 = s1_Z,
                X2 = s1_X2,
                Y2 = s1_Y1,
                Z2 = 0,
                X3 = s1_X2,
                Y3 = s1_Y2,
                Z3 = s1_Z,

            };

            Triangle s1_10 = new Triangle()
            {
                X1 = s1_X2,
                Y1 = s1_Y2,
                Z1 = s1_Z,
                X2 = s1_X2,
                Y2 = s1_Y1,
                Z2 = 0,
                X3 = s1_X2,
                Y3 = s1_Y2,
                Z3 = 0,

            };

            //기둥4
            Triangle s1_11 = new Triangle()
            {
                X1 = s1_X2,
                Y1 = s1_Y2,
                Z1 = s1_Z,
                X2 = s1_X2,
                Y2 = s1_Y2,
                Z2 = 0,
                X3 = s1_X1,
                Y3 = s1_Y2,
                Z3 = s1_Z
            };

            Triangle s1_12 = new Triangle()
            {
                X1 = s1_X2,
                Y1 = s1_Y2,
                Z1 = 0,
                X2 = s1_X1,
                Y2 = s1_Y2,
                Z2 = 0,
                X3 = s1_X1,
                Y3 = s1_Y2,
                Z3 = s1_Z
            };


            BOTTOM.Enqueue(s1_1);
            BOTTOM.Enqueue(s1_2);
            BOTTOM.Enqueue(s1_3);
            BOTTOM.Enqueue(s1_4);
            //1
            BOTTOM.Enqueue(s1_5);
            BOTTOM.Enqueue(s1_6);
            //2
            BOTTOM.Enqueue(s1_7);
            BOTTOM.Enqueue(s1_8);
            //3
            BOTTOM.Enqueue(s1_9);
            BOTTOM.Enqueue(s1_10);
            //4
            BOTTOM.Enqueue(s1_11);
            BOTTOM.Enqueue(s1_12);
            #endregion

            #region 도장판_위
            //윗면
            Triangle s2_1 = new Triangle()
            {
                X1 = s2_X1,
                Y1 = s2_Y2,
                Z1 = s2_Z,
                X2 = s2_X1,
                Y2 = s2_Y1,
                Z2 = s2_Z,
                X3 = s2_X2,
                Y3 = s2_Y1,
                Z3 = s2_Z
            };

            Triangle s2_2 = new Triangle()
            {
                X1 = s2_X2,
                Y1 = s2_Y2,
                Z1 = s2_Z,
                X2 = s2_X1,
                Y2 = s2_Y2,
                Z2 = s2_Z,
                X3 = s2_X2,
                Y3 = s2_Y1,
                Z3 = s2_Z
            };

            //아랫면
            Triangle s2_3 = new Triangle()
            {
                X1 = s2_X1,
                Y1 = s2_Y1,
                Z1 = 0,
                X2 = s2_X1,
                Y2 = s2_Y2,
                Z2 = 0,
                X3 = s2_X2,
                Y3 = s2_Y1,
                Z3 = 0
            };

            Triangle s2_4 = new Triangle()
            {
                X1 = s2_X1,
                Y1 = s2_Y2,
                Z1 = 0,
                X2 = s2_X2,
                Y2 = s2_Y2,
                Z2 = 0,
                X3 = s2_X2,
                Y3 = s2_Y1,
                Z3 = 0
            };

            //기둥1
            Triangle s2_5 = new Triangle()
            {
                X1 = s2_X1,
                Y1 = s2_Y2,
                Z1 = 0,
                X2 = s2_X1,
                Y2 = s2_Y1,
                Z2 = 0,
                X3 = s2_X1,
                Y3 = s2_Y1,
                Z3 = s2_Z
            };

            Triangle s2_6 = new Triangle()
            {
                X1 = s2_X1,
                Y1 = s2_Y1,
                Z1 = s2_Z,
                X3 = s2_X1,
                Y3 = s2_Y2,
                Z3 = 0,
                X2 = s2_X1,
                Y2 = s2_Y2,
                Z2 = s2_Z
            };

            //기둥2
            Triangle s2_7 = new Triangle()
            {
                X1 = s2_X1,
                Y1 = s2_Y1,
                Z1 = s2_Z,
                X2 = s2_X1,
                Y2 = s2_Y1,
                Z2 = 0,
                X3 = s2_X2,
                Y3 = s2_Y1,
                Z3 = s2_Z,

            };

            Triangle s2_8 = new Triangle()
            {
                X1 = s2_X1,
                Y1 = s2_Y1,
                Z1 = 0,
                X2 = s2_X2,
                Y2 = s2_Y1,
                Z2 = 0,
                X3 = s2_X2,
                Y3 = s2_Y1,
                Z3 = s2_Z
            };

            //기둥3
            Triangle s2_9 = new Triangle()
            {
                X1 = s2_X2,
                Y1 = s2_Y1,
                Z1 = s2_Z,
                X2 = s2_X2,
                Y2 = s2_Y1,
                Z2 = 0,
                X3 = s2_X2,
                Y3 = s2_Y2,
                Z3 = s2_Z,

            };

            Triangle s2_10 = new Triangle()
            {
                X1 = s2_X2,
                Y1 = s2_Y2,
                Z1 = s2_Z,
                X2 = s2_X2,
                Y2 = s2_Y1,
                Z2 = 0,
                X3 = s2_X2,
                Y3 = s2_Y2,
                Z3 = 0,

            };

            //기둥4
            Triangle s2_11 = new Triangle()
            {
                X1 = s2_X2,
                Y1 = s2_Y2,
                Z1 = s2_Z,
                X2 = s2_X2,
                Y2 = s2_Y2,
                Z2 = 0,
                X3 = s2_X1,
                Y3 = s2_Y2,
                Z3 = s2_Z
            };

            Triangle s2_12 = new Triangle()
            {
                X1 = s2_X2,
                Y1 = s2_Y2,
                Z1 = 0,
                X2 = s2_X1,
                Y2 = s2_Y2,
                Z2 = 0,
                X3 = s2_X1,
                Y3 = s2_Y2,
                Z3 = s2_Z
            };

            BOTTOM.Enqueue(s2_1);
            BOTTOM.Enqueue(s2_2);
            BOTTOM.Enqueue(s2_3);
            BOTTOM.Enqueue(s2_4);
            //1
            BOTTOM.Enqueue(s2_5);
            BOTTOM.Enqueue(s2_6);
            //2
            BOTTOM.Enqueue(s2_7);
            BOTTOM.Enqueue(s2_8);
            //3
            BOTTOM.Enqueue(s2_9);
            BOTTOM.Enqueue(s2_10);
            //4
            BOTTOM.Enqueue(s2_11);
            BOTTOM.Enqueue(s2_12);

            #endregion           

            //binarySTL_BackBottom();
        }



        //* 이미지 반전() *//
        public static void negativeImg(IplImage imageName)
        {
            double r, s;
            for (int Y = 0; Y < imageName.Height; Y++)
            {
                for (int X = 0; X < imageName.Width; X++)
                {
                    r = Cv.GetReal2D(imageName, Y, X);
                    s = 255.0 - r;
                    Cv.SetReal2D(imageName, Y, X, s);
                }
            }
        }

        //* 좌표정보를 바탕으로 STL파일 생성 *//
        public static void asciiSTL()
        {
            using (StreamWriter WT = new StreamWriter(@"c:\TookTemp\output.stl"))
            {
                // Header
                WT.WriteLine("solid 2D TOOK 3D");

                // 윗면
                foreach (Triangle tri in TRI)
                {
                    Triangle tempTRI = tri;
                    WT.WriteLine("facet normal 0 0 0");
                    WT.WriteLine("outer loop");
                    WT.WriteLine("vertex {0} {1} {2}", tempTRI.X1, tempTRI.Y1, tempTRI.Z1);
                    WT.WriteLine("vertex {0} {1} {2}", tempTRI.X2, tempTRI.Y2, tempTRI.Z2);
                    WT.WriteLine("vertex {0} {1} {2}", tempTRI.X3, tempTRI.Y3, tempTRI.Z3);
                    WT.WriteLine("endloop");
                    WT.WriteLine("endfacet");
                }
                
                // 아랫면
                foreach (Triangle tri in TRI)
                {
                    Triangle tempTRI = tri;
                    WT.WriteLine("facet normal 0 0 0");
                    WT.WriteLine("outer loop");
                    WT.WriteLine("vertex {0} {1} {2}", tempTRI.X1, tempTRI.Y1, 0);
                    WT.WriteLine("vertex {0} {1} {2}", tempTRI.X2, tempTRI.Y2, 0);
                    WT.WriteLine("vertex {0} {1} {2}", tempTRI.X3, tempTRI.Y3, 0);
                    WT.WriteLine("endloop");
                    WT.WriteLine("endfacet");
                }

                // End
                WT.WriteLine("endsolid");
            }
        }

        //* Binary STL 생성 *//
        public static void binarySTL()
        {
            using (FileStream stream = new FileStream(@"c:\TookTemp\output", FileMode.Create, FileAccess.Write))
            {
                /*
                UINT8[80] – Header
                UINT32 – Number of triangles

                foreach triangle
                REAL32[3] – Normal vector
                REAL32[3] – Vertex 1
                REAL32[3] – Vertex 2
                REAL32[3] – Vertex 3
                UINT16 – Attribute byte count
                end
                */

                //Header
                UTF8Encoding utf = new UTF8Encoding();
                byte[] bytes = utf.GetBytes("solid 2D TOOK 3D");
                foreach (var bytByte in bytes)
                {
                    stream.WriteByte(bytByte);
                }

                //Number of triangles
                bytes = BitConverter.GetBytes(TRI.Count*2);
                stream.Seek(80, System.IO.SeekOrigin.Begin);
                foreach (var bytByte in bytes)
                    stream.WriteByte(bytByte);


                //foreach triangle
                foreach (var tris in TRI)
                {
                    //Normal Vector
                    byByte(0F, stream);
                    byByte(0F, stream);
                    byByte(0F, stream);

                    //Vecter1,2,3
                    byByte((float)tris.X1, stream);
                    byByte((float)tris.Y1, stream);
                    byByte((float)tris.Z1, stream);

                    byByte((float)tris.X2, stream);
                    byByte((float)tris.Y2, stream);
                    byByte((float)tris.Z2, stream);

                    byByte((float)tris.X3, stream);
                    byByte((float)tris.Y3, stream);
                    byByte((float)tris.Z3, stream);

                    // UINT16 – Attribute byte count (this should be zero)
                    bytes = BitConverter.GetBytes((UInt16)0);
                    foreach (var bytByte in bytes)
                        stream.WriteByte(bytByte);
                }

                //Bottom
                foreach (var tris in TRI)
                {
                    //Normal Vector
                    byByte(0F, stream);
                    byByte(0F, stream);
                    byByte(0F, stream);

                    //Vecter1,2,3
                    byByte((float)tris.X1, stream);
                    byByte((float)tris.Y1, stream);
                    byByte(0F, stream);

                    byByte((float)tris.X2, stream);
                    byByte((float)tris.Y2, stream);
                    byByte(0F, stream);

                    byByte((float)tris.X3, stream);
                    byByte((float)tris.Y3, stream);
                    byByte(0F, stream);

                    // UINT16 – Attribute byte count (this should be zero)
                    bytes = BitConverter.GetBytes((UInt16)0);
                    foreach (var bytByte in bytes)
                        stream.WriteByte(bytByte);
                }
            }
        }

        //* Binary STL 생성 _ Polygon Bottom Version *//
        public static void binarySTL_Bottom()
        {
            using (FileStream stream = new FileStream(@"c:\TookTemp\output", FileMode.Create, FileAccess.Write))
            {
                /*
                UINT8[80] – Header
                UINT32 – Number of triangles

                foreach triangle
                REAL32[3] – Normal vector
                REAL32[3] – Vertex 1
                REAL32[3] – Vertex 2
                REAL32[3] – Vertex 3
                UINT16 – Attribute byte count
                end
                */

                //Header
                UTF8Encoding utf = new UTF8Encoding();
                byte[] bytes = utf.GetBytes("solid 2D TOOK 3D");
                foreach (var bytByte in bytes)
                {
                    stream.WriteByte(bytByte);
                }

                //Number of triangles
                bytes = BitConverter.GetBytes(TRI.Count+(BOTTOM.Count*2));
                stream.Seek(80, System.IO.SeekOrigin.Begin);
                foreach (var bytByte in bytes)
                    stream.WriteByte(bytByte);


                //foreach triangle
                foreach (var tris in TRI)
                {
                    //Normal Vector
                    byByte(0F, stream);
                    byByte(0F, stream);
                    byByte(0F, stream);

                    //Vecter1,2,3
                    byByte((float)tris.X1, stream);
                    byByte((float)tris.Y1, stream);
                    byByte((float)tris.Z1, stream);

                    byByte((float)tris.X2, stream);
                    byByte((float)tris.Y2, stream);
                    byByte((float)tris.Z2, stream);

                    byByte((float)tris.X3, stream);
                    byByte((float)tris.Y3, stream);
                    byByte((float)tris.Z3, stream);

                    // UINT16 – Attribute byte count (this should be zero)
                    bytes = BitConverter.GetBytes((UInt16)0);
                    foreach (var bytByte in bytes)
                        stream.WriteByte(bytByte);
                }

                //Bottom_1
                foreach (var tris in BOTTOM)
                {
                    //Normal Vector
                    byByte(0F, stream);
                    byByte(0F, stream);
                    byByte(0F, stream);

                    //Vecter1,2,3
                    byByte((float)tris.X1, stream);
                    byByte((float)tris.Y1, stream);
                    byByte((float)tris.Z1, stream);

                    byByte((float)tris.X2, stream);
                    byByte((float)tris.Y2, stream);
                    byByte((float)tris.Z2, stream);

                    byByte((float)tris.X3, stream);
                    byByte((float)tris.Y3, stream);
                    byByte((float)tris.Z3, stream);

                    // UINT16 – Attribute byte count (this should be zero)
                    bytes = BitConverter.GetBytes((UInt16)0);
                    foreach (var bytByte in bytes)
                        stream.WriteByte(bytByte);
                }
                //Bottom_2
                foreach (var tris in BOTTOM)
                {
                    //Normal Vector
                    byByte(0F, stream);
                    byByte(0F, stream);
                    byByte(0F, stream);

                    //Vecter1,2,3
                    byByte((float)tris.X1, stream);
                    byByte((float)tris.Y1, stream);
                    byByte(0F, stream);

                    byByte((float)tris.X2, stream);
                    byByte((float)tris.Y2, stream);
                    byByte(0F, stream);

                    byByte((float)tris.X3, stream);
                    byByte((float)tris.Y3, stream);
                    byByte(0F, stream);

                    // UINT16 – Attribute byte count (this should be zero)
                    bytes = BitConverter.GetBytes((UInt16)0);
                    foreach (var bytByte in bytes)
                        stream.WriteByte(bytByte);
                }
            }
        }

        //* Binary STL 생성 _ Background Bottom Version *//
        public static void binarySTL_BackBottom()
        {
            using (FileStream stream = new FileStream(@"c:\TookTemp\output", FileMode.Create, FileAccess.Write))
            {
                /*
                UINT8[80] – Header
                UINT32 – Number of triangles

                foreach triangle
                REAL32[3] – Normal vector
                REAL32[3] – Vertex 1
                REAL32[3] – Vertex 2
                REAL32[3] – Vertex 3
                UINT16 – Attribute byte count
                end
                */

                //Header
                UTF8Encoding utf = new UTF8Encoding();
                byte[] bytes = utf.GetBytes("solid 2D TOOK 3D");
                foreach (var bytByte in bytes)
                {
                    stream.WriteByte(bytByte);
                }

                //Number of triangles
                bytes = BitConverter.GetBytes(TRI.Count + BOTTOM.Count);
                stream.Seek(80, System.IO.SeekOrigin.Begin);
                foreach (var bytByte in bytes)
                    stream.WriteByte(bytByte);


                //foreach triangle
                foreach (var tris in TRI)
                {
                    //Normal Vector
                    byByte(0F, stream);
                    byByte(0F, stream);
                    byByte(0F, stream);

                    //Vecter1,2,3
                    byByte((float)tris.X1, stream);
                    byByte((float)tris.Y1, stream);
                    byByte((float)tris.Z1, stream);

                    byByte((float)tris.X2, stream);
                    byByte((float)tris.Y2, stream);
                    byByte((float)tris.Z2, stream);

                    byByte((float)tris.X3, stream);
                    byByte((float)tris.Y3, stream);
                    byByte((float)tris.Z3, stream);

                    // UINT16 – Attribute byte count (this should be zero)
                    bytes = BitConverter.GetBytes((UInt16)0);
                    foreach (var bytByte in bytes)
                        stream.WriteByte(bytByte);
                }

                //Bottom_1
                foreach (var tris in BOTTOM)
                {
                    //Normal Vector
                    byByte(0F, stream);
                    byByte(0F, stream);
                    byByte(0F, stream);

                    //Vecter1,2,3
                    byByte((float)tris.X1, stream);
                    byByte((float)tris.Y1, stream);
                    byByte((float)tris.Z1, stream);

                    byByte((float)tris.X2, stream);
                    byByte((float)tris.Y2, stream);
                    byByte((float)tris.Z2, stream);

                    byByte((float)tris.X3, stream);
                    byByte((float)tris.Y3, stream);
                    byByte((float)tris.Z3, stream);

                    // UINT16 – Attribute byte count (this should be zero)
                    bytes = BitConverter.GetBytes((UInt16)0);
                    foreach (var bytByte in bytes)
                        stream.WriteByte(bytByte);
                }
            }
        }

        //* Text to Binary *//
        private static void byByte(float f, FileStream stream)
        {
            byte[] bytes = BitConverter.GetBytes(f);
            foreach (var bytByte in bytes)
                stream.WriteByte(bytByte);
        }
    
        //* Size Check **/
        public static void checkSize(IplImage src)
        {
            IplImage srcImage = src.Clone();
            Cv.Threshold(srcImage, srcImage, 150, 255, ThresholdType.Otsu);

            minX = srcImage.Width - 1;
            minY = srcImage.Height - 1;
            maxX = 0;
            maxY = 0;

            unsafe
            {
                for (int Y = 1; Y < srcImage.Height; Y++)
                {
                    for (int X = 1; X < srcImage.Width; X++)
                    {
                        if (srcImage.ImageDataPtr[Y * srcImage.WidthStep + X] == 0)
                        {
                            if (X > maxX)
                                maxX = X;

                            if (Y > maxY)
                                maxY = Y;

                            if (X > 0 && X < minX)
                                minX = X;

                            if (Y > 0 && Y < minY)
                                minY = Y;
                        }
                    }//END:FOR(Z)
                }//END:FOR(Y)
            }
            srcImage.Dispose();
        }
    }
}