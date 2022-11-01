﻿using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using Managers;
using MapComponents;
using UnityEngine;
using Utils;

namespace MapComHandlers
{
    public class BounceComHandler : BaseMapComHandler
    {
        public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<BounceCom>();
            if (!com) return;
            if (characterController is not PlayerController playerController) return;
            var direction = new Vector3(0, com.force, 0);
            if (com.bounceType != BounceCom.BounceType.Ground)
            {
                direction = playerController.transform.position - com.transform.position;
                direction.y = 0;
                direction = direction.normalized * com.force;
            }
            var command = new Command
            {
                commandID = (int) CommandID.BounceCommand,
                vector = direction
            };
            CommandManager.Instance.SendCommand(command);
        }

        public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<BounceCom>();
            if (com)
            {
            
            }
        }
    }
}