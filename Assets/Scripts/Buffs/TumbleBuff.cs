using Base;
using BEPUphysics.Character;
using Managers;

namespace Buffs
{
    public class TumbleBuff : BaseBuff
    {
        public float duration;
        private float _startTime;
        public override void Start()
        {
            _startTime = PhysicsWorld.Instance.TimeSinceStart;
            PlayerManager.Instance.myPlayer.mCharacterController.StanceManager.DesiredStance =
                Stance.Prone;
        }

        public override void End()
        {
            PlayerManager.Instance.myPlayer.mCharacterController.StanceManager.DesiredStance =
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