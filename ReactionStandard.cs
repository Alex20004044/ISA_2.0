using System;
using System.Threading;
using System.Threading.Tasks;

namespace ISA_2
{
    public class ReactionStandard : IReaction
    {
        float _minDistance = 30;
        float _alarmDistance = 60;

        int _beepDuration = 100;
        int _beepMaxFrequency = 20000;
        int _beepMinFrequency = 404;

        float _distance;

        public void SetDistance(float distance)
        {
            Console.WriteLine($"Distance: {distance}");
            if (distance <= _alarmDistance)
                React();
        }

        void React()
        {
            Console.Write('\a');     
        }
    }
}
