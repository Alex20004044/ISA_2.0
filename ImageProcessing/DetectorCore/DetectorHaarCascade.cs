using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISA_2.ImageProcessing.DetectorCore
{
    internal class DetectorHaarCascade : IFaceDetector
    {
        CascadeClassifier defaultClassifier = new CascadeClassifier(@"D:\Data\Projects\ISA 2.0\ISA 2\Resources\haarcascade_frontalface_default.xml");

        public Rectangle? GetMainFaceRectangle(in Mat imageMat)
        {
            using Image<Gray, byte> grayImage = imageMat.ToImage<Gray, byte>();
            Rectangle[] faces = defaultClassifier.DetectMultiScale(grayImage, 1.1, 3);

            return faces.GetMaxRectangle();
        }
    }
}
