﻿using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;

public class TumbleComHandler : BaseMapComHandler
{
    public override void HandleEnterCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
    {
        var com = other.GameObject.GetComponent<TumbleCom>();
        if (com)
        {
            
        }
    }

    public override void HandleExitCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
    {
        var com = other.GameObject.GetComponent<TumbleCom>();
        if (com)
        {
            
        }
    }
}