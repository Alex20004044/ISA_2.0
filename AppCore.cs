using System;
using System.Threading.Tasks;
using Emgu.CV;
using ISA_2.ImageProcessing;

namespace ISA_2
{
    public class AppCore
    {
        IDistanceSensor _distanceSensor;
        IReaction _reaction;
        IImageSource _imageSource;

        DistanceSensorCamera sensorCamera;

        const int UpdateDelay = 500;
        const string WindowName = "ISA 2.0";
        public AppCore(IDistanceSensor distanceGetter, IReaction reaction, IImageSource imageSource)
        {
            _distanceSensor = distanceGetter;
            sensorCamera = _distanceSensor as DistanceSensorCamera;
            _reaction = reaction;
            _imageSource = imageSource;
        }

        public async Task StartAsync()
        {
            Console.WriteLine(WindowName);

            if (sensorCamera == null)
            {
                Console.WriteLine("Датчик не поддерживатеся в начтоящий момент.");
                return;
            }

            int calibrateDist = ShowTextAndGetInt("Введите расстояние в см, на котором будет выполняться калибровка");

            Console.WriteLine($"Расположитесь на расстоянии {calibrateDist} см и нажмите Enter для калибровки");
            Mat frame;
            while (true)
            {
                frame = _imageSource.GetImage();

                sensorCamera.GetDistanceFromImageAndDrawBorders(frame);
                CvInvoke.Imshow("ISA 2.0", frame);

                int key = CvInvoke.WaitKey(1);
                if (key == (int)ConsoleKey.Enter)
                    break;
                else
                    frame.Dispose();
            }
            sensorCamera.Calibrate(frame, calibrateDist, out var calibrateCoefficient);
            frame.Dispose();

            Console.WriteLine($"Калибровка выполнена успешно. Коэффициент: {calibrateCoefficient}");

            int alarmDistance = ShowTextAndGetInt("Введите минимально допустимое расстояние");

            _reaction.SetAlarmDistance(alarmDistance);

            bool isShowCameraOutput = ShowTextAndGetInt("Выводить изображение с камеры? (0 - нет, 1 - да)") > 0;

            while (true)
            {
                float distance;

                if (isShowCameraOutput)
                {
                    using var frame2 = _imageSource.GetImage();
                    distance = sensorCamera.GetDistanceFromImageAndDrawBorders(frame2);
                    CvInvoke.Imshow(WindowName, frame2);
                }
                else
                    distance = _distanceSensor.GetDistance();

                _reaction.ProcessDistance(distance);

                int key = CvInvoke.WaitKey(UpdateDelay);
                if (key == (int)ConsoleKey.Escape)
                    break;
            }
        }

        async Task Update()
        {
            //float distance = _distanceSensor.GetDistance();
            //_reaction.SetDistance(distance);
        }

        int ShowTextAndGetInt(string text)
        {
            while (true)
            {
                Console.Write($"{text}: ");
                string rawValue = Console.ReadLine();
                if (int.TryParse(rawValue, out int result))
                    return result;

                Console.WriteLine("Некорректный ввод");
            }
        }
    }
}
