using System.Threading.Tasks;

namespace ISA_2
{
    public class AppCore
    {
        IDistanceSensor _distanceGetter;
        IReaction _reaction;

        int updateDelay = 1000;
        public AppCore(IDistanceSensor  distanceGetter, IReaction reaction)
        {
            _distanceGetter = distanceGetter;
            _reaction = reaction;
        }

        public async Task StartAsync()
        {
            while(true)
            {
                await Update();
                await Task.Delay(updateDelay);
            }
        }

        async Task Update()
        {
            float distance = _distanceGetter.GetDistance();
            _reaction.SetDistance(distance);

            
        }
    }
}
