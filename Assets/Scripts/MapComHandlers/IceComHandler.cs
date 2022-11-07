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
    public class IceComHandler : BaseMapComHandler
    {
        public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair,
            BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<IceCom>();
            if (!com) return;
            if (characterController is not PlayerController) return;
            var command = new Command
            {
                commandID = (int) CommandID.IceCommand,
                enterOrExit = 0,
                floatValue1 = com.damp
            };
            CommandManager.Instance.SendCommand(command);
        }

        public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair,
            BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<IceCom>();
            if (!com) return;
            if (characterController is not PlayerController) return;
            var command = new Command
            {
                commandID = (int) CommandID.IceCommand,
                enterOrExit = 1
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}