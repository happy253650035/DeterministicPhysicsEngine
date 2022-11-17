
using BEPUutilities;
using FixMath.NET;

namespace Base
{
    public abstract class PhysicsTween
    {
        public PhysicsObject target;
        public bool loop;
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