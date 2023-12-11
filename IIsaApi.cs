namespace ISA_2
{
    public interface IIsaApi
    {
        void Start();
        void Pause();
        void SetTargetDistance(float targetDistance);
        float GetDistance();
        //InitVideoStream();
    }
}
