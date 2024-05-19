using Emgu.CV;
using ISA_2.ImageProcessing.DetectorCore;

namespace ISA_2.ImageProcessing.ImageProcessors
{
    public class ImageProcessorHaarCascade : IImageProcessor
    {
        DetectorHaarCascade detectorHaarCascade = new DetectorHaarCascade();
        public float MeasureFaceAndDrawBorders(Mat imageMat)
        {
            var face = detectorHaarCascade.GetMainFaceRectangle(imageMat);
            if (!face.HasValue)
                return 0;

            FaceDetectorUtilities.DrawRectangles(imageMat, face.Value);
            return face.Value.Width;
        }
    }
}
