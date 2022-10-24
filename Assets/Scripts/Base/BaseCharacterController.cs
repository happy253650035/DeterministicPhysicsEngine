using System;
using System.Collections.Generic;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using UnityEngine;
using CharacterController = BEPUphysics.Character.CharacterController;

[RequireComponent(typeof(CapsuleCollider))]
public class BaseCharacterController : MonoBehaviour
{
    public float mass = 2;
    public float speed = 0.5f;
    public CharacterController mCharacterController;
    public event ColliderListener.CollisionEnterDetected OnEnterCollision;
    public event ColliderListener.CollisionEnterDetectedMainThread OnEnterCollisionMainThread;
    public event ColliderListener.CollisionEnterDetected OnExitCollision;
    public event ColliderListener.CollisionEnterDetectedMainThread OnExitCollisionMainThread;
    private readonly List<ColliderListener.CollisionInfo> _collisionEnterInfos = new();
    private readonly List<ColliderListener.CollisionInfo> _collisionExitInfos = new();

    public bool IsActive { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        var capsule = GetComponent<CapsuleCollider>();
        mCharacterController = new CharacterController();
        var pos = transform.position;
        var center = capsule.center;
        mCharacterController.Body.position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x + center.x),
            Convert.ToDecimal(pos.y + center.y), Convert.ToDecimal(pos.z + center.z));
        mCharacterController.Body.Height = Convert.ToDecimal(capsule.height);
        mCharacterController.Body.Radius = Convert.ToDecimal(capsule.radius);
        mCharacterController.Body.Mass = Convert.ToDecimal(mass);
        mCharacterController.SpeedScale *= Convert.ToDecimal(speed);
        Activate();
    }

    private void Start()
    {
        mCharacterController.Body.CollisionInformation.Events.InitialCollisionDetected +=
            InitialCollisionDetected;
        mCharacterController.Body.CollisionInformation.Events.CollisionEnded +=
            CollisionEnded;
    }
    
    private void InitialCollisionDetected(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
    {
        OnEnterCollision?.Invoke(sender, other, pair);
        var info = new ColliderListener.CollisionInfo {Sender = sender, Other = other, Pair = pair};
        _collisionEnterInfos.Add(info);
    }

    private void CollisionEnded(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
    {
        OnExitCollision?.Invoke(sender, other, pair);
        var info = new ColliderListener.CollisionInfo {Sender = sender, Other = other, Pair = pair};
        _collisionExitInfos.Add(info);
    }

    public void Activate()
    {
        if (IsActive) return;
        IsActive = true;
        PhysicsWorld.Instance.AddPhysicsCharacterController(this);
    }

    public void Deactivate()
    {
        if (!IsActive) return;
        IsActive = false;
        PhysicsWorld.Instance.RemovePhysicsCharacterController(this);
    }

    public void Jump()
    {
        mCharacterController.Jump();
    }
}