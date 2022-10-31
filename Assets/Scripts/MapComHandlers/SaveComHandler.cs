using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using Managers;
using Utils;

public class SaveComHandler : BaseMapComHandler
{
    public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
    {
        var com = other.GameObject.GetComponent<SaveCom>();
        if (!com) return;
        if (characterController is not PlayerController playerController) return;
        var command = new Command
        {
            commandID = (int) CommandID.SaveCommand,
            vector = com.survivePoint.position
        };
        CommandManager.Instance.SendCommand(command);
    }

    public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
    {
        var com = other.GameObject.GetComponent<SaveCom>();
        if (com)
        {
            
        }
    }
}