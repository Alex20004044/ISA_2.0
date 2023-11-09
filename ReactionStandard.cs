using System;
using System.Collections.Generic;
using System.Text;

namespace ISA_2
{
    public class ReactionStandard : IReaction
    {
        float _minDistance = 30;
        float _alarmDistance = 60;

        public void SetDistance(float distance)
        {
            float alarmLevel = CalculateAlarmLevel(distance);

            

            SetBrigtness(alarmLevel);
        }

        float CalculateAlarmLevel(float distance)
        {
            return Program.Map(distance, _minDistance, _alarmDistance /*0, start brightness level*/);
        }

        void SetBrigtness(float level)
        {
            Console.WriteLine($"Brigtness set to {level}");
        }
    }
}
