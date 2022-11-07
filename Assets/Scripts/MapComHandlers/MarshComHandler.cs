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
    public class MarshComHandler : BaseMapComHandler
    {
        public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair,
            BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<MarshCom>();
            if (!com) return;
            if (characterController is not PlayerController) return;
            var command = new Command
            {
                commandID = (int) CommandID.MarshCommand,
                enterOrExit = 0,
                floatValue1 = com.speed
            };
            CommandManager.Instance.SendCommand(command);
        }

        public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair,
            BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<MarshCom>();
            if (!com) return;
            if (characterController is not PlayerController) return;
            var command = new Command
            {
                commandID = (int) CommandID.MarshCommand,
                enterOrExit = 1
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}