using System;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace ISA_2
{
    public class ImageProcessorLBF : IImageProcessor
    {
        CascadeClassifier defaultClassifier = new CascadeClassifier(@"D:\Data\Projects\ISA 2.0\ISA 2\Resources\haarcascade_frontalface_default.xml");

        string lbfModelPath = @"D:\Data\Projects\ISA 2.0\ISA 2\Resources\lbfmodel.yaml";
        FacemarkLBF facemarkLBF;
        public ImageProcessorLBF()
        {
            this.facemarkLBF = new FacemarkLBF(new FacemarkLBFParams());
            facemarkLBF.LoadModel(lbfModelPath);
        }

        public float MeasureFaceAndDrawBorders(Mat imageMat)
        {
            Image<Gray, byte> grayImage = imageMat.ToImage<Gray, byte>();

            Rectangle[] facesDef = defaultClassifier.DetectMultiScale(grayImage, 1.1, 3);
            if (!facesDef.Any())
                return 0;

            int biggestFace = 0;
            Rectangle biggestRect = default;
            foreach (Rectangle x in facesDef)
            {
                CvInvoke.Rectangle(imageMat, x, new MCvScalar(0, 255, 0));

                if (x.Width > biggestFace)
                {
                    biggestFace = x.Width;
                    biggestRect = x;
                }
            }
            VectorOfRect vectorOfRect = new VectorOfRect(new Rectangle[] { biggestRect });
            VectorOfVectorOfPointF landmarks = new VectorOfVectorOfPointF();
            if (facemarkLBF.Fit(imageMat, vectorOfRect, landmarks))
            {
                FaceInvoke.DrawFacemarks(imageMat, landmarks[0], new MCvScalar(0, 255, 0));

                var top = landmarks[0][27];
                var down = landmarks[0][8];

                var targetMarks = new VectorOfPointF(new PointF[] { top, down });
                FaceInvoke.DrawFacemarks(imageMat, targetMarks, new MCvScalar(255, 0, 0));

                return MathF.Abs(top.Y - down.Y);
            }
            return 0;
        }
    }
}
