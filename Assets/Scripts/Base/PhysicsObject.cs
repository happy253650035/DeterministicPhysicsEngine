using System;
using BEPUphysics.Entities;
using BEPUphysics.PositionUpdating;
using UnityEngine;

public abstract class PhysicsObject : MonoBehaviour
{
    public bool isStatic;
    public bool isBullet;
    public bool useScale;
    public float mass = 1;
    public Entity mEntity;
    [HideInInspector] public Vector3 center;

    public bool IsActive { get; private set; }

    public void Activate()
    {
        if (IsActive) return;
        IsActive = true;
        if (isBullet) mEntity.PositionUpdateMode = PositionUpdateMode.Continuous;
        PhysicsWorld.Instance.AddPhysicsObject(this);
    }

    public void Deactivate()
    {
        if (!IsActive) return;
        IsActive = false;
        PhysicsWorld.Instance.RemovePhysicsObject(this);
    }

    public void ApplyImpulse(BEPUutilities.Vector3 location, BEPUutilities.Vector3 impulse)
    {
        mEntity.ApplyImpulse(location, impulse);
    }
}