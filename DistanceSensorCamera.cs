using Emgu.CV;
using ISA_2.ImageProcessing;
using ISA_2.ImageProcessing.ImageProcessors;

namespace ISA_2
{
    public class DistanceSensorCamera : IDistanceSensor
    {
        IImageProcessor imageProcessor;
        IImageSource imageSource;

        float faceWidthToDistanceCoefficient = 1;

        public DistanceSensorCamera(IImageProcessor imageProcessor, IImageSource imageSource)
        {
            this.imageProcessor = imageProcessor;
            this.imageSource = imageSource;
        }

        public float GetDistance()
        {
            return GetDistanceFromImageAndDrawBorders(imageSource.GetImage());
        }

        public void Calibrate(Mat imageMat, float distance, out float calibrateCoefficient)
        {
            float faceMeasure = imageProcessor.MeasureFaceAndDrawBorders(imageMat);

            faceWidthToDistanceCoefficient = distance * (faceMeasure / imageMat.Size.Width);

            calibrateCoefficient = faceWidthToDistanceCoefficient;
        }

        public float GetDistanceFromImageAndDrawBorders(Mat imageMat)
        {
            return faceWidthToDistanceCoefficient / (imageProcessor.MeasureFaceAndDrawBorders(imageMat) / imageMat.Size.Width);
            //return faceWidthToDistanceCoefficient * (imageProcessor.MeasureFaceAndDrawBorders(imageMat) / imageMat.Size.Width);
        }

    }
}
