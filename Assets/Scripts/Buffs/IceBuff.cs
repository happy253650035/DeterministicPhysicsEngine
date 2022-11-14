using System;
using Base;
using FixMath.NET;
using UnityEngine;

namespace Buffs
{
    public class IceBuff : BaseBuff
    {
        public float damp;
        private Fix64 _damp;
        private Fix64 _totalDamp;
        private BEPUutilities.Vector2 _initDirection;
        private BEPUutilities.Vector2 _currentDirection;

        public override void Start()
        {
        }

        public void CheckMovement(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                _damp = 100 - Convert.ToDecimal(damp)*100;
                _totalDamp = _damp;
                _initDirection = characterController.mCharacterController.HorizontalMotionConstraint.MovementDirection;
                if (_initDirection == BEPUutilities.Vector2.Zero) _damp = 0;
            }
            else
            {
                _currentDirection = new BEPUutilities.Vector2(Convert.ToDecimal(direction.x),
                    Convert.ToDecimal(direction.y));
                if (_damp <= 0)
                {
                    characterController.mCharacterController.HorizontalMotionConstraint.MovementDirection =
                        _currentDirection;
                }
            }
        }

        public override void End()
        {
            state = BuffState.DeActive;
        }

        public override void Tick()
        {
            if (_damp <= 0) return;
            _damp -= 1;
            _initDirection *= _damp / _totalDamp;
            characterController.mCharacterController.HorizontalMotionConstraint.MovementDirection =
                _initDirection + _currentDirection;
        }
    }
}