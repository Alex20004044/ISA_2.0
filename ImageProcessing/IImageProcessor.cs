using Emgu.CV;
using Emgu.CV.Structure;

namespace ISA_2
{
    public interface IImageProcessor
    {
        float MeasureFaceAndDrawBorders(Mat imageMat);
    }
}
