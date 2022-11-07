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
    public class DeathComHandler : BaseMapComHandler
    {
        public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<DeathCom>();
            if (!com) return;
            if (characterController is not PlayerController playerController) return;
            var command = new Command
            {
                commandID = (int) CommandID.DeadCommand
            };
            CommandManager.Instance.SendCommand(command);
        }

        public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<DeathCom>();
            if (com)
            {
            
            }
        }
    }
}