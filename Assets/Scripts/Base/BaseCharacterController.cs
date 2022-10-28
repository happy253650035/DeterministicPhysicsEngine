using System;
using System.Collections.Generic;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using UnityEngine;
using CharacterController = BEPUphysics.Character.CharacterController;

[RequireComponent(typeof(CapsuleCollider))]
public abstract class BaseCharacterController : MonoBehaviour
{
    public float mass = 2;
    public float speed = 0.5f;
    public float gravity = 9.81f;
    public CharacterController mCharacterController;
    public event ColliderListener.CollisionEnterDetected OnEnterCharacter;
    public event ColliderListener.CollisionEnterDetectedMainThread OnEnterCharacterMainThread;
    public event ColliderListener.CollisionEnterDetected OnExitCharacter;
    public event ColliderListener.CollisionExitDetectedMainThread OnExitCharacterMainThread;
    private readonly List<ColliderListener.CollisionInfo> _collisionEnterInfos = new();
    private readonly List<ColliderListener.CollisionInfo> _collisionExitInfos = new();
    protected abstract void OnAwake();
    protected abstract void OnStart();
    protected abstract void Enable();
    protected abstract void OnUpdate();

    public bool IsActive { get; private set; }

    private void OnEnable()
    {
        Enable();
    }

    private void Awake()
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
        mCharacterController.Body.Gravity = new BEPUutilities.Vector3(0, -Convert.ToDecimal(gravity), 0);
        mCharacterController.Body.CollisionInformation.GameObject = gameObject;
        OnAwake();
    }
    
    private void Start()
    {
        var material = GetComponent<PhysicsMaterial>();
        if (material)
        {
            mCharacterController.Body.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(material.staticFriction),
                Convert.ToDecimal(material.kineticFriction), Convert.ToDecimal(material.bounciness));
        }
        else
        {
            mCharacterController.Body.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(PhysicsWorld.Instance.staticFriction),
                Convert.ToDecimal(PhysicsWorld.Instance.kineticFriction), Convert.ToDecimal(PhysicsWorld.Instance.bounciness));
        }
        Activate();
        mCharacterController.Body.CollisionInformation.Events.InitialCollisionDetected +=
            InitialCollisionDetected;
        mCharacterController.Body.CollisionInformation.Events.CollisionEnded +=
            CollisionEnded;
        OnStart();
    }
    
    private void InitialCollisionDetected(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
    {
        OnEnterCharacter?.Invoke(sender, other, pair);
        var info = new ColliderListener.CollisionInfo {Sender = sender, Other = other, Pair = pair};
        _collisionEnterInfos.Add(info);
    }

    private void CollisionEnded(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
    {
        OnExitCharacter?.Invoke(sender, other, pair);
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

    public void Sprint()
    {
        var forward = transform.forward;
        mCharacterController.Body.ApplyImpulse(mCharacterController.Body.position,
            new BEPUutilities.Vector3(Convert.ToDecimal(forward.x), Convert.ToDecimal(forward.y), Convert.ToDecimal(forward.z)) * 30);
    }
    
    private void Update()
    {
        foreach (var info in _collisionEnterInfos)
        {
            OnEnterCharacterMainThread?.Invoke(info.Sender, info.Other, info.Pair);
        }
        foreach (var info in _collisionExitInfos)
        {
            OnExitCharacterMainThread?.Invoke(info.Sender, info.Other, info.Pair);
        }
        _collisionEnterInfos.Clear();
        _collisionExitInfos.Clear();
        OnUpdate();
    }
}