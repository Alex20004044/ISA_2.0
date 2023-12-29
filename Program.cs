using System;
using Emgu.CV;
using ISA_2.ImageProcessing;

namespace ISA_2
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoCapture videoCapture = new VideoCapture(0);
            videoCapture.Start();

            IImageSource imageSource = new ImageSourceVideoCapture(videoCapture);

            //IImageProcessor imageProcessor = new ImageProcessorHaarCascade();
            IImageProcessor imageProcessor = new ImageProcessorKeyPoints(videoCapture.Width, videoCapture.Height);
            IDistanceSensor distanceSensor = new DistanceSensorCamera(imageProcessor, imageSource);
            IReaction reaction = new ReactionStandard();

            AppCore core = new AppCore(distanceSensor, reaction, imageSource);
            core.StartAsync().GetAwaiter().GetResult();
        }

        public static float Map(float value, float low, float high, float low2 = 0, float high2 = 1)
        {
            //Положение числа в исходном отрезке, от 0 до 1
            float percentage = (value - low) / (high - low);
            //Накладываем его на конечный отрезок
            float transformed = low2 + (high2 - low2) * percentage;
            return transformed;
        }
    }
}
