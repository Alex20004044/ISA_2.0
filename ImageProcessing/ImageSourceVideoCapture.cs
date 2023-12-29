using Emgu.CV;

namespace ISA_2.ImageProcessing
{
    public class ImageSourceVideoCapture : IImageSource
    {
        VideoCapture videoCapture;

        public ImageSourceVideoCapture(VideoCapture videoCapture)
        {
            this.videoCapture = videoCapture;
        }

        public Mat GetImage()
        {
            return videoCapture.QueryFrame();
        }
    }
}
