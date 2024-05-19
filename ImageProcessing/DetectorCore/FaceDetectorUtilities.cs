using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace ISA_2.ImageProcessing.DetectorCore
{
    internal static class FaceDetectorUtilities
    {
        public static readonly MCvScalar Green = new MCvScalar(0, 255, 0);
        public static readonly MCvScalar Blue = new MCvScalar(255, 0, 0);

        public static void DrawRectangles(Mat imageMat, params Rectangle[] rectangles)
        {
            foreach (Rectangle p in rectangles)
            {
                CvInvoke.Rectangle(imageMat, p, Green);
            }
        }

        public static void DrawPoints(Mat imageMat, params PointF[] points)
        {
            foreach (var p in points)
            {
                CvInvoke.Circle(imageMat, new Point((int)MathF.Round(p.X), (int)MathF.Round(p.Y)), 2, Blue);
            }
        }        
        public static void DrawFacePoints(Mat imageMat, params Point[] points)
        {
            foreach (var x in points)
            {
                CvInvoke.Circle(imageMat, x, 2, Blue);
            }
        }

        public static Rectangle? GetMaxRectangle(this Rectangle[] rectangles)
        {
            if (rectangles == null || rectangles.Length == 0)
                return null;

            Rectangle rectangle = rectangles[0];
            for (int i = 1; i < rectangles.Length; i++)
            {
                if (rectangles[i].Width > rectangle.Width)
                    rectangle = rectangles[i];
            }
            return rectangle;
        }
    }
}
