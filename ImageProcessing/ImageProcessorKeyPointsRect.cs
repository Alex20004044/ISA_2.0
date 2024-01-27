using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;

namespace ISA_2.ImageProcessing
{
    public class ImageProcessorKeyPointsRect : IImageProcessor
    {
        static string ynnModelPath = @"D:\Data\Projects\ISA 2.0\ISA 2\Resources\face_detection_yunet_2023mar.onnx";
        FaceDetectorYN model;

        public ImageProcessorKeyPointsRect(int videoCaptureWidth, int videoCaptureHeight)
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

        ~ImageProcessorKeyPointsRect()
        {
            model.Dispose();
        }

        public float MeasureFaceAndDrawBorders(Mat frame)
        {
            var faces = new Mat();
            model.Detect(frame, faces);
            DrawDetectedFaces(frame, faces, false, out float faceWidth);
            return faceWidth;
        }

        void DrawDetectedFaces(Mat frame, Mat faces, bool renderConfidence, out float faceWidth)
        {
            faceWidth = 0;
            if (faces.Rows <= 0)
                return;

            
            // facesData is multidimensional array.
            // The first dimension is the index of the face, the second dimension is the data for that face.
            // The data for each face is 15 elements long:
            //  - the first 4 elements are the bounding box of the face (x, y, width, height)
            //  - the next 10 elements are the x and y coordinates of 5 facial landmarks:
            //      right eye, left eye, nose tip, right mouth corner, left mouth corner
            //  - the last element is the confidence score
            var facesData = (float[,])faces.GetData(jagged: true);


            for (var i = 0; i < facesData.GetLength(0); i++)
            {
                DrawFaceRectangle(frame, (int)facesData[i, 0], (int)facesData[i, 1], (int)facesData[i, 2], (int)facesData[i, 3]);
                DrawFaceLandMarks(frame, i, facesData);
            }

            var face = new float[15];
            for (var i = 0; i < 15; i++)
            {
                face[i] = facesData[0, i];
            }
            faceWidth = DetectFaceWidth(face);
        }

        float DetectFaceWidth(float[] faceElements)
        {
            return MathF.Abs(faceElements[2]);
            //return MathF.Abs(faceElements[10] - faceElements[12]);
        }

        static void DrawFaceRectangle(Mat frame, int x, int y, int width, int height)
        {
            var faceRectangle = new Rectangle(x, y, width, height);
            CvInvoke.Rectangle(frame, faceRectangle, new MCvScalar(0, 255, 0), 1);
        }

        static void DrawFaceLandMarks(Mat frame, int faceIndex, float[,] facesData)
        {
            var landMarkColors = new MCvScalar[]
            {
                new MCvScalar(255, 0, 0),   // 4,5 right eye
                new MCvScalar(0, 0, 255),   // 6,7 left eye
                new MCvScalar(0, 255, 0),   // 8,9 nose tip
                new MCvScalar(255, 0, 255), // 10,11 right mouth corner
                new MCvScalar(0, 255, 255)  // 12,13 left mouth corner
            };

            for (var landMark = 0; landMark < 5; landMark++)
            {
                var x = (int)facesData[faceIndex, 4 + landMark * 2];
                var y = (int)facesData[faceIndex, 4 + landMark * 2 + 1];
                CvInvoke.Circle(frame, new Point(x, y), 2, landMarkColors[landMark], -1);
            }
        }
    }
}
