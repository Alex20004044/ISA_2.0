using ISA_2.Test;

namespace ISA_2
{
    class Program
    {
        static void Main(string[] args)
        {

            IDistanceGetter distanceGetter = new DistanceGetterTest(40);
            IReaction reaction = new ReactionStandard();

            AppCore core = new AppCore(distanceGetter, reaction);
            core.Start().GetAwaiter().GetResult();
        }

        public static float Map(float value, float low, float high, float low2 = 0, float high2 = 1)
        {
            //Положение числа в исходном отрезке, от 0 до 1
            float percentage = (value - low) / (high - low);
            //Накладываем его на конечный отрезок
            float transformed = low2 + (high2 - low2) * percentage;
            return transformed;
        }
    }
}
