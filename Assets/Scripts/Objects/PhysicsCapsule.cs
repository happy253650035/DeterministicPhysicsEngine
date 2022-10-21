using System;
using BEPUphysics.Entities.Prefabs;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PhysicsCapsule : PhysicsObject
{
    private Capsule _capsure;

    private void Awake()
    {
        var capsule = GetComponent<CapsuleCollider>();
        var center = capsule.center;
        mCenterX = center.x;
        mCenterY = center.y;
        mCenterZ = center.z;

        _capsure = isStatic
            ? new Capsule(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(capsule.height), Convert.ToDecimal(capsule.radius))
            : new Capsule(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(capsule.height), Convert.ToDecimal(capsule.radius),
                Convert.ToDecimal(mass));

        _capsure.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(staticFriction),
            Convert.ToDecimal(kineticFriction), Convert.ToDecimal(bounciness));

        var pos = transform.position + center;
        _capsure.position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x),
            Convert.ToDecimal(pos.y), Convert.ToDecimal(pos.z));
        var orientation = transform.rotation;
        _capsure.orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(orientation.x),
            Convert.ToDecimal(orientation.y), Convert.ToDecimal(orientation.z), Convert.ToDecimal(orientation.w));
        mEntity = _capsure;
        mEntity.angularVelocity = new BEPUutilities.Vector3(Convert.ToDecimal(angularVelocity.x),
            Convert.ToDecimal(angularVelocity.y), Convert.ToDecimal(angularVelocity.z));
        Activate();
    }
}