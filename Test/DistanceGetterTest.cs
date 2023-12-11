using System;
using System.Collections.Generic;
using System.Text;

namespace ISA_2.Test
{
    public class DistanceGetterTest : IDistanceSensor
    {
        float _distance;
        int i = 0;
        public DistanceGetterTest(float distance)
        {
            SetDistance(distance);
        }

        public void SetDistance(float distance)
        {
            _distance = distance;
        }
        public float GetDistance()
        {
            i++;
            return 10 * MathF.Sin(i / 10f) + _distance;
        }
    }
}
