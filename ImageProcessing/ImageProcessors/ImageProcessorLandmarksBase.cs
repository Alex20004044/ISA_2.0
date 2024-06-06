using Emgu.CV;
using ISA_2.ImageProcessing.DetectorCore;
using ISA_2.ImageProcessing.FaceLandmarksDetectors;

namespace ISA_2.ImageProcessing.ImageProcessors
{
    public class ImageProcessorLandmarksBase : IImageProcessor
    {
        IFaceDetector faceDetector;
        ILandmarksDetector landmarksDetector;

        public ImageProcessorLandmarksBase(IFaceDetector faceDetector, ILandmarksDetector landmarksDetector)
        {
            this.faceDetector = faceDetector;
            this.landmarksDetector = landmarksDetector;
        }

        public float MeasureFaceAndDrawBorders(Mat imageMat)
        {
            var faceRect = faceDetector.GetMainFaceRectangle(imageMat);
            if (faceRect == null)
                return 0;

            var faceSize = landmarksDetector.GetFaceWidthAndLandmarks(imageMat, faceRect.Value, out var faceLandmarks);
            FaceDetectorUtilities.DrawRectangles(imageMat, faceRect.Value);
            FaceDetectorUtilities.DrawPoints(imageMat, faceLandmarks);

            return faceSize;
        }
    }
}
