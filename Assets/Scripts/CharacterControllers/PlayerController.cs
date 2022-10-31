using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using Managers;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
    public Vector3 survivePoint;
    protected override void OnAwake()
    {
        PlayerManager.Instance.myPlayer = this;
    }

    protected override void OnStart()
    {
        OnEnterCharacterMainThread += EnterCharacterMainThread;
        OnExitCharacterMainThread += ExitCharacterMainThread;
    }

    private void EnterCharacterMainThread(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
    {
        MapComManager.Instance.HandleEnterMapCom(sender, other, pair, this);
    }

    private void ExitCharacterMainThread(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
    {
        MapComManager.Instance.HandleExitMapCom(sender, other, pair, this);
    }

    protected override void Enable()
    {
        
    }

    protected override void OnUpdate()
    {
        
    }
}
