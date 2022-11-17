using Base;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using CharacterControllers;
using Managers;
using MapComponents;
using Utils;

namespace MapComHandlers
{
    public class ConveyorComHandler : BaseMapComHandler
    {
        public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair,
            BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<ConveyorCom>();
            if (!com) return;
            if (characterController is not PlayerController) return;
            var direction = com.direction;
            direction = direction.normalized * com.force;
            var command = new Command
            {
                commandID = (int) CommandID.ConveyorCommand,
                enterOrExit = 0,
                vector3_1 = direction
            };
            CommandManager.Instance.SendCommand(command);
        }

        public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair,
            BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<ConveyorCom>();
            if (!com) return;
            if (characterController is not PlayerController) return;
            var command = new Command
            {
                commandID = (int) CommandID.ConveyorCommand,
                enterOrExit = 1
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}