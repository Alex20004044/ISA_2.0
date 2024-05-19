using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Dnn;

namespace ISA_2.ImageProcessing.DetectorCore
{
    internal class DetectorYNN : IFaceDetector
    {
        static string ynnModelPath = @"D:\Data\Projects\ISA 2.0\ISA 2\Resources\face_detection_yunet_2023mar.onnx";
        FaceDetectorYN model;

        public DetectorYNN(int videoCaptureWidth, int videoCaptureHeight)
        {
            model = new FaceDetectorYN(
                model: ynnModelPath,
                config: string.Empty,
                inputSize: new Size(videoCaptureWidth, videoCaptureHeight),
                scoreThreshold: 0.9f,
                nmsThreshold: 0.3f,
                topK: 5000,
                backendId: Emgu.CV.Dnn.Backend.Default,
                targetId: Target.Cpu);
        }

        ~DetectorYNN()
        {
            model.Dispose();
        }

        public Rectangle? GetMainFaceRectangle(in Mat imageMat)
        {
            return GetMainFaceRectangle(imageMat, out var _, out var _);
        }
        public float GetFaceWidth(in Mat imageMat)
        {
            float[,] facesData = GetFacesData(imageMat);

            return GetFaceWidthFromFaceData(0, facesData);
        }
        public Rectangle? GetMainFaceRectangle(in Mat imageMat, out PointF[] facePoints, out float faceWidth)
        {
            var facesData = GetFacesData(in imageMat);

            if (facesData == null || facesData.Length == 0)
            {
                facePoints = null;
                faceWidth = 0;
                return null;
            }

            facePoints = GetFaceLandmarksPoints(0, facesData);
            faceWidth = GetFaceWidthFromFaceData(0, facesData);

            return new Rectangle(
                (int)MathF.Round(facesData[0, 0]),
                (int)MathF.Round(facesData[0, 1]),
                (int)MathF.Round(facesData[0, 2]),
                (int)MathF.Round(facesData[0, 3])
                );
        }

        float GetFaceWidthFromFaceData(int faceIndex, float[,] facesData)
        {
            var faceLandmarks = new float[15];
            for (var i = 0; i < 15; i++)
            {
                faceLandmarks[i] = facesData[faceIndex, i];
            }

            return MathF.Abs(faceLandmarks[4] - faceLandmarks[6]);
            //return MathF.Abs(faceElements[10] - faceElements[12]);
        }

        static PointF[] GetFaceLandmarksPoints(int faceIndex, float[,] facesData)
        {
            List<PointF> faceLandmarks = new List<PointF>();
            for (var landMark = 0; landMark < 5; landMark++)
            {
                var x = (int)facesData[faceIndex, 4 + landMark * 2];
                var y = (int)facesData[faceIndex, 4 + landMark * 2 + 1];
                faceLandmarks.Add(new PointF(x, y));
            }
            return faceLandmarks.ToArray();
        }


        float[,]? GetFacesData(in Mat imageMat)
        {
            var faces = new Mat();
            model.Detect(imageMat, faces);

            // facesData is multidimensional array.
            // The first dimension is the index of the face, the second dimension is the data for that face.
            // The data for each face is 15 elements long:
            //  - the first 4 elements are the bounding box of the face (x, y, width, height)
            //  - the next 10 elements are the x and y coordinates of 5 facial landmarks:
            //      right eye, left eye, nose tip, right mouth corner, left mouth corner
            //  - the last element is the confidence score
            var facesData = (float[,])faces.GetData(jagged: true);

            return facesData;
        }
    }
}
