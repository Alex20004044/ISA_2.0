using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Util;

namespace ISA_2.ImageProcessing.FaceLandmarksDetectors
{
    internal class LandmarkDetectorLBF : ILandmarksDetector
    {
        string lbfModelPath = @"D:\Data\Projects\ISA 2.0\ISA 2\Resources\lbfmodel.yaml";
        FacemarkLBF facemarkLBF;
        public LandmarkDetectorLBF()
        {
            this.facemarkLBF = new FacemarkLBF(new FacemarkLBFParams());
            facemarkLBF.LoadModel(lbfModelPath);
        }

        public float GetFaceWidthAndLandmarks(in Mat imageMat, Rectangle faceRectangle, out PointF[] faceLandmarks)
        {
            VectorOfRect vectorOfRect = new VectorOfRect(new Rectangle[] { faceRectangle });
            VectorOfVectorOfPointF landmarks = new VectorOfVectorOfPointF();
            if (!facemarkLBF.Fit(imageMat, vectorOfRect, landmarks))
            {
                faceLandmarks = null;
                return 0;
            }
            var top = landmarks[0][27];
            var down = landmarks[0][8];
            faceLandmarks = new PointF[] { top, down };

            return MathF.Abs(top.Y - down.Y);
        }
    }
}
