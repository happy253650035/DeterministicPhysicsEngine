using System;
using BEPUphysics.Entities.Prefabs;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PhysicsSphere : PhysicsObject
{
    private Sphere _sphere;

    private void Awake()
    {
        var sphere = GetComponent<SphereCollider>();
        var center = sphere.center;
        mCenterX = center.x;
        mCenterY = center.y;
        mCenterZ = center.z;

        _sphere = isStatic
            ? new Sphere(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(sphere.radius))
            : new Sphere(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(sphere.radius), Convert.ToDecimal(mass));
        _sphere.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(staticFriction),
            Convert.ToDecimal(kineticFriction), Convert.ToDecimal(bounciness));
        var pos = this.transform.position + center;
        _sphere.position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x),
            Convert.ToDecimal(pos.y),
            Convert.ToDecimal(pos.z));
        var orientation = transform.rotation;
        _sphere.orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(orientation.x),
            Convert.ToDecimal(orientation.y), Convert.ToDecimal(orientation.z), Convert.ToDecimal(orientation.w));
        mEntity = _sphere;
        mEntity.angularVelocity = new BEPUutilities.Vector3(Convert.ToDecimal(angularVelocity.x),
            Convert.ToDecimal(angularVelocity.y), Convert.ToDecimal(angularVelocity.z));
        Activate();
    }
}