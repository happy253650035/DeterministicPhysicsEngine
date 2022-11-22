
using BEPUutilities;
using FixMath.NET;

namespace Base
{
    public abstract class PhysicsTween
    {
        public enum LoopType
        {
            None = 1,
            Loop = 2,
            PingPong = 3,
        }
        public BaseObject target;
        public LoopType loopType;
        public Fix64 duration;
        public bool isActive;
        public bool isPause;
        protected Fix64 startTime;
        protected Fix64 totalTime;

        public abstract void Start();
        public abstract void End();
        public abstract void Update();

        public void Stop()
        {
            isActive = false;
        }

        public void Pause()
        {
            isPause = true;
        }

        public void Resume()
        {
            isPause = false;
        }
    }
}