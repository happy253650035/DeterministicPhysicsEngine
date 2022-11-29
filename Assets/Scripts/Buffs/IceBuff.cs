using System;
using Base;
using FixMath.NET;
using UnityEngine;

namespace Buffs
{
    public class IceBuff : BaseBuff
    {
        public float damp;
        private bool _isMoving;
        private Fix64 _damp;
        private BEPUutilities.Vector2 _initDirection;
        private BEPUutilities.Vector2 _currentDirection;

        public override void Start()
        {
            _damp = damp;
            _initDirection = BEPUutilities.Vector2.Zero;
            _currentDirection = BEPUutilities.Vector2.Zero;
        }

        public void CheckMovement(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                _initDirection = characterController.mCharacterController.HorizontalMotionConstraint.MovementDirection;
                _currentDirection = BEPUutilities.Vector2.Zero;
                _isMoving = false;
                _damp = damp;
            }
            else
            {
                if (!_isMoving)
                {
                    _damp = damp;
                    _isMoving = true;
                }
                _currentDirection = new BEPUutilities.Vector2(direction.x, direction.y);
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
            _damp -= 0.01m;
            _initDirection *= _damp;
            characterController.mCharacterController.HorizontalMotionConstraint.MovementDirection =
                _initDirection + _currentDirection;
        }
    }
}