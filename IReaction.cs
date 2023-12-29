using System;
using System.Collections.Generic;
using System.Text;

namespace ISA_2
{
    public interface IReaction
    {
        void SetAlarmDistance(float distance);
        void ProcessDistance(float distance);
    }
}
