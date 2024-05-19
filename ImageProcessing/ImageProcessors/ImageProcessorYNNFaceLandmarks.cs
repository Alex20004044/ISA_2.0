using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using ISA_2.ImageProcessing.DetectorCore;

namespace ISA_2.ImageProcessing.ImageProcessors
{
    public class ImageProcessorYNNFaceLandmarks : IImageProcessor
    {
        DetectorYNN detectorYNN;
        public ImageProcessorYNNFaceLandmarks(int videoCaptureWidth, int videoCaptureHeight)
        {
            detectorYNN = new DetectorYNN(videoCaptureWidth, videoCaptureHeight);
        }

        public float MeasureFaceAndDrawBorders(Mat imageMat)
        {
            var face = detectorYNN.GetMainFaceRectangle(imageMat, out var facePoints, out var faceWidth);
            if (face == null)
                return 0;

            FaceDetectorUtilities.DrawRectangles(imageMat, face.Value);
            FaceDetectorUtilities.DrawPoints(imageMat, facePoints);

            return faceWidth;
        }

    }
}
