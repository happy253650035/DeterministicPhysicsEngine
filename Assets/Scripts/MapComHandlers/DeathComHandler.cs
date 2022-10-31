using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;

public class DeathComHandler : BaseMapComHandler
{
    public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
    {
        var deathCom = other.GameObject.GetComponent<DeathCom>();
        if (deathCom)
        {
            
        }
    }

    public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
    {
        var deathCom = other.GameObject.GetComponent<DeathCom>();
        if (deathCom)
        {
            
        }
    }
}