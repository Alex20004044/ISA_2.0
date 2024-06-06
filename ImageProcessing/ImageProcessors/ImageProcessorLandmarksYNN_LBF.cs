using Emgu.CV;
using ISA_2.ImageProcessing.DetectorCore;
using ISA_2.ImageProcessing.FaceLandmarksDetectors;

namespace ISA_2.ImageProcessing.ImageProcessors
{
    public class ImageProcessorLandmarksYNN_LBF : IImageProcessor
    {
        ImageProcessorLandmarksBase imageProcessor;
        public ImageProcessorLandmarksYNN_LBF(int videoCaptureWidth, int videoCaptureHeight)
        {
            imageProcessor = new ImageProcessorLandmarksBase(new DetectorYNN(videoCaptureWidth, videoCaptureHeight),
                new LandmarkDetectorLBF());
        }

        public float MeasureFaceAndDrawBorders(Mat image)
        {
            return imageProcessor.MeasureFaceAndDrawBorders(image);
        }
    }
}
