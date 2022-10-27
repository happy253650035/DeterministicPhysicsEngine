using System.Collections.Generic;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
public class ColliderListener : MonoBehaviour
{
    public struct CollisionInfo
    {
        public EntityCollidable Sender;
        public Collidable Other;
        public CollidablePairHandler Pair;
    }

    private readonly List<CollisionInfo> _collisionEnterInfos = new();
    private readonly List<CollisionInfo> _collisionExitInfos = new();
    private PhysicsObject _physicsObject;

    public delegate void CollisionEnterDetected(EntityCollidable sender, Collidable other, CollidablePairHandler pair);

    public delegate void CollisionEnterDetectedMainThread(EntityCollidable sender, Collidable other,
        CollidablePairHandler pair);

    public delegate void CollisionExitDetected(EntityCollidable sender, Collidable other, CollidablePairHandler pair);

    public delegate void CollisionExitDetectedMainThread(EntityCollidable sender, Collidable other,
        CollidablePairHandler pair);

    public event CollisionEnterDetected OnEnterCollision;
    public event CollisionEnterDetectedMainThread OnEnterCollisionMainThread;
    public event CollisionEnterDetected OnExitCollision;
    public event CollisionEnterDetectedMainThread OnExitCollisionMainThread;

    // Start is called before the first frame update
    void Start()
    {
        _physicsObject = GetComponent<PhysicsObject>();
        _physicsObject.mEntity.CollisionInformation.Events.InitialCollisionDetected +=
            InitialCollisionDetected;
        _physicsObject.mEntity.CollisionInformation.Events.CollisionEnded +=
            CollisionEnded;
    }

    private void InitialCollisionDetected(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
    {
        OnEnterCollision?.Invoke(sender, other, pair);
        var info = new CollisionInfo {Sender = sender, Other = other, Pair = pair};
        _collisionEnterInfos.Add(info);
    }

    private void CollisionEnded(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
    {
        OnExitCollision?.Invoke(sender, other, pair);
        var info = new CollisionInfo {Sender = sender, Other = other, Pair = pair};
        _collisionExitInfos.Add(info);
    }

    private void Update()
    {
        foreach (var info in _collisionEnterInfos)
        {
            OnEnterCollisionMainThread?.Invoke(info.Sender, info.Other, info.Pair);
        }
        foreach (var info in _collisionExitInfos)
        {
            OnExitCollisionMainThread?.Invoke(info.Sender, info.Other, info.Pair);
        }
        _collisionEnterInfos.Clear();
        _collisionExitInfos.Clear();
    }
}