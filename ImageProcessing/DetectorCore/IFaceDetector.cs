using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISA_2.ImageProcessing.DetectorCore
{
    internal interface IFaceDetector
    {
        Rectangle? GetMainFaceRectangle(in Mat imageMat);
    }
}
