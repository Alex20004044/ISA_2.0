using System;
using System.Collections.Generic;
using System.Text;

namespace ISA_2.Test
{
    public class DistanceGetterTest : IDistanceGetter
    {
        float _distance = 0;

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
            return MathF.Sin(DateTime.Now.Second % 360) * _distance;
        }
    }
}
