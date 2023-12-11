using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV;

namespace ISA_2.ImageProcessing
{
    public interface IImageSource
    {
        Mat GetImage();
    }
}
