using Base;
using BEPUutilities;
using FixMath.NET;

namespace PhysicsTweens
{
    public class PhysicsTweenPosition : PhysicsTween
    {
        public Vector3 from;
        public Vector3 to;
        
        public override void Start()
        {
            if (loopType != LoopType.Loop)
            {
                totalTime = duration - startTime;
            }
        }

        public override void End()
        {
            
        }

        public override void Update()
        {
            if (!isActive) return;
            var elapse = PhysicsWorld.Instance.TimeSinceStart - startTime;
            var pos = Vector3.Lerp(from, to, elapse / totalTime);
            if (elapse > totalTime) isActive = false;
            // target.mEntity.Position = pos;
        }
    }
}