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
    public class TumbleComHandler : BaseMapComHandler
    {
        public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<TumbleCom>();
            if (!com) return;
            if (characterController is not PlayerController playerController) return;
            var direction = playerController.transform.position - com.transform.position;
            direction.y = 0;
            direction = direction.normalized * com.force;
            var command = new Command
            {
                commandID = (int) CommandID.TumbleCommand,
                vector = direction
            };
            CommandManager.Instance.SendCommand(command);
        }

        public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<TumbleCom>();
            if (com)
            {
            
            }
        }
    }
}