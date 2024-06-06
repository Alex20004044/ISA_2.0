using Emgu.CV;
using ISA_2.ImageProcessing.DetectorCore;

namespace ISA_2.ImageProcessing.ImageProcessors
{
    public class ImageProcessorYNNFaceRect : IImageProcessor
    {
        DetectorYNN detectorYNN;
        public ImageProcessorYNNFaceRect(int videoCaptureWidth, int videoCaptureHeight)
        {
            detectorYNN = new DetectorYNN(videoCaptureWidth, videoCaptureHeight);
        }

        public float MeasureFaceAndDrawBorders(Mat imageMat)
        {
            var face = detectorYNN.GetMainFaceRectangle(imageMat);
            if (face == null)
                return 0;

            FaceDetectorUtilities.DrawRectangles(imageMat, face.Value);
            return face.Value.Width;
        }

    }
}
