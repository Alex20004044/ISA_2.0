using System.Drawing;
using Emgu.CV;

namespace ISA_2.ImageProcessing.DetectorCore
{
    public interface IFaceDetector
    {
        Rectangle? GetMainFaceRectangle(in Mat imageMat);
    }
}
