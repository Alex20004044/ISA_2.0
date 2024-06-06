using System.Drawing;
using Emgu.CV;

namespace ISA_2.ImageProcessing.FaceLandmarksDetectors
{
    public interface ILandmarksDetector
    {
        float GetFaceWidthAndLandmarks(in Mat imageMat, Rectangle faceRectangle, out PointF[] faceLandmarks);
    }
}
