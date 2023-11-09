using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISA_2
{

    public class LegacyCode
    {
        VideoCapture videoCapture = new VideoCapture();
        CascadeClassifier defaultClassifier = new CascadeClassifier(@"D:\Data\Projects\ISA 2.0\ISA 2\haarcascade_frontalface_default.xml");
        
        float faceWidthToDistanceCoefficient = 12.317f;

        int frameWidth = 320;
        int frameHeight = 240;

        float minDistance = 40;

        int updateDelay = 200;
        public LegacyCode()
        {
            videoCapture.Set(Emgu.CV.CvEnum.CapProp.FrameWidth, frameWidth);
            videoCapture.Set(Emgu.CV.CvEnum.CapProp.FrameHeight, frameHeight);
        }

        ~LegacyCode()
        {
            videoCapture?.Dispose();
        }

        public void Start()
        {
            videoCapture.Start();
            CvInvoke.NamedWindow("ISA");
            while (true)
            {
                Mat mat = videoCapture.QueryFrame();

                var outputMat = FindFaces(mat, out int faceWidth);
                float distance = faceWidthToDistanceCoefficient / (float)(faceWidth) * frameWidth;

                CvInvoke.Imshow("ISA", outputMat);
                int key = CvInvoke.WaitKey(updateDelay);
                if (key == (int)ConsoleKey.Escape)
                    break;


                Console.Write($"Distance: {distance.ToString("#")} ");

                if (distance < minDistance)
                    Console.WriteLine("Too close!");
                else
                    Console.WriteLine("Ok");
            }
        }

        Mat FindFaces(Mat mat, out int faceWidth)
        {
            Image<Gray, byte> grayImage = mat.ToImage<Gray, byte>();
            Rectangle[] facesDef = defaultClassifier.DetectMultiScale(grayImage, 1.1, 3);
            int biggestFace = 0;
            foreach (Rectangle x in facesDef)
            {
                CvInvoke.Rectangle(mat, x, new MCvScalar(0, 255, 0));

                if (x.Width > biggestFace)
                {
                    biggestFace = x.Width;
                }
            }

            faceWidth = biggestFace;
            return mat;
        }
    }
}
