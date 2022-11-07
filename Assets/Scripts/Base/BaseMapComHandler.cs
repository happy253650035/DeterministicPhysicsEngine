using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;

namespace Base
{
    public abstract class BaseMapComHandler
    {
        public abstract void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController);
        public abstract void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController);
    }
}