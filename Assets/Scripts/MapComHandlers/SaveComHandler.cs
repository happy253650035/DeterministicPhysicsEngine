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
    public class SaveComHandler : BaseMapComHandler
    {
        public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            if (!other.GameObject) return;
            var com = other.GameObject.GetComponent<SaveCom>();
            if (!com) return;
            if (characterController is not PlayerController) return;
            var command = new Command
            {
                objectId = other.GameObject.GetComponent<PhysicsObject>().id,
                commandID = (int) CommandID.SaveCommand,
                vector3_1 = com.survivePoint.position
            };
            CommandManager.Instance.SendCommand(command);
        }

        public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            if (!other.GameObject) return;
            var com = other.GameObject.GetComponent<SaveCom>();
            if (com)
            {
            
            }
        }
    }
}