using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using MapComponents;

namespace MapComHandlers
{
    public class RotateComHandler : BaseMapComHandler
    {
        public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<RotateCom>();
            if (com)
            {
            
            }
        }

        public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<RotateCom>();
            if (com)
            {
            
            }
        }
    }
}