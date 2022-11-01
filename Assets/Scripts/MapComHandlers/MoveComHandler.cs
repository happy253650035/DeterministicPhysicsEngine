using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using MapComponents;

namespace MapComHandlers
{
    public class MoveComHandler : BaseMapComHandler
    {
        public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair,
            BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<MoveCom>();
            if (!com) return;
            if (characterController is not PlayerController) return;
        }

        public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair,
            BaseCharacterController characterController)
        {
            var com = other.GameObject.GetComponent<MoveCom>();
            if (!com) return;
            if (characterController is not PlayerController) return;
        }
    }
}