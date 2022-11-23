﻿using System;
using Base;
using UnityEngine;

namespace Buffs
{
    public class SpeedBuff : BaseBuff
    {
        public Vector3 direction;
        public BEPUutilities.Vector3 _impulse;

        public override void Start()
        {
            _impulse = new BEPUutilities.Vector3(direction.x, direction.y, direction.z);
        }

        public override void End()
        {
            state = BuffState.DeActive;
        }

        public override void Tick()
        {
            characterController.mCharacterController.Body.ApplyImpulse(
                characterController.mCharacterController.Body.position, _impulse);
        }
    }
}