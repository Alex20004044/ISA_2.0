using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISA_2
{
    public class ImageProcessorHaarCascade : IImageProcessor
    {
        CascadeClassifier defaultClassifier = new CascadeClassifier(@"D:\Data\Projects\ISA 2.0\ISA 2\Resources\haarcascade_frontalface_default.xml");
        public float MeasureFaceAndDrawBorders(Mat imageMat)
        {
            Image<Gray, byte> grayImage = imageMat.ToImage<Gray, byte>();

            Rectangle[] facesDef = defaultClassifier.DetectMultiScale(grayImage, 1.1, 3);
            int biggestFace = 0;
            foreach (Rectangle x in facesDef)
            {
                CvInvoke.Rectangle(imageMat, x, new MCvScalar(0, 255, 0));

                if (x.Width > biggestFace)
                {
                    biggestFace = x.Width;
                }
            }

            return biggestFace;
        }
    }
}
