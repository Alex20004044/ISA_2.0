using Emgu.CV;
using Emgu.CV.Structure;

namespace ISA_2.ImageProcessing.ImageProcessors
{
    public interface IImageProcessor
    {
        float MeasureFaceAndDrawBorders(Mat image);
    }
}
