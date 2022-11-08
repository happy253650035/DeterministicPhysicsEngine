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
    public class AccelerateComHandler : BaseMapComHandler
    {
        public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<AccelerateCom>();
            if (!com) return;
            if (characterController is not PlayerController) return;
            var direction = com.direction;
            direction = direction.normalized * com.force;
            var command = new Command
            {
                commandID = (int) CommandID.AccelerateCommand,
                vector3 = direction
            };
            CommandManager.Instance.SendCommand(command);
        }

        public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<AccelerateCom>();
            if (com)
            {
            
            }
        }
    }
}
