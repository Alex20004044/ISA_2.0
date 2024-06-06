using Emgu.CV;
using ISA_2.ImageProcessing.DetectorCore;
using ISA_2.ImageProcessing.FaceLandmarksDetectors;

namespace ISA_2.ImageProcessing.ImageProcessors
{
    public class ImageProcessorLandmarksHaarCascadeLBF : IImageProcessor
    {
        ImageProcessorLandmarksBase imageProcessor;
        public ImageProcessorLandmarksHaarCascadeLBF()
        {
            imageProcessor = new ImageProcessorLandmarksBase(new DetectorHaarCascade(), new LandmarkDetectorLBF());
        }

        public float MeasureFaceAndDrawBorders(Mat image)
        {
            return imageProcessor.MeasureFaceAndDrawBorders(image);
        }
    }
}
