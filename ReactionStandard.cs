using System;
using System.Threading;
using System.Threading.Tasks;

namespace ISA_2
{
    public class ReactionStandard : IReaction
    {
        float _alarmDistance = 40;

        public void ProcessDistance(float distance)
        {
            Console.WriteLine($"Дистанция: {distance}");
            if (distance <= _alarmDistance)
                React();
        }

        public void SetAlarmDistance(float distance)
        {
            _alarmDistance = distance;
        }

        void React()
        {
            Console.Write('\a');     
        }
    }
}
