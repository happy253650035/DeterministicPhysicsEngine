using Base;
using BEPUphysics.Character;
using FixMath.NET;
using Managers;

namespace Buffs
{
    public class TumbleBuff : BaseBuff
    {
        public Fix64 duration;
        private Fix64 _startTime;
        public override void Start()
        {
            _startTime = PhysicsWorld.Instance.TimeSinceStart;
            characterController.mCharacterController.StanceManager.DesiredStance =
                Stance.Prone;
        }

        public override void End()
        {
            characterController.mCharacterController.StanceManager.DesiredStance =
                Stance.Standing;
        }

        public override void Tick()
        {
            if (state == BuffState.DeActive) return;
            var elapse = PhysicsWorld.Instance.TimeSinceStart - _startTime;
            if (elapse >= duration) state = BuffState.DeActive;
        }
    }
}