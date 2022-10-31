using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;

public class BounceComHandler : BaseMapComHandler
{
    public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
    {
        var com = other.GameObject.GetComponent<BounceCom>();
        if (com)
        {
            
        }
    }

    public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
    {
        var com = other.GameObject.GetComponent<BounceCom>();
        if (com)
        {
            
        }
    }
}