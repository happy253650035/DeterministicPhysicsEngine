using System;
using BEPUphysics.Entities.Prefabs;
using UnityEngine;

[RequireComponent(typeof(CylinderCollider))]
public class PhysicsCylinder : PhysicsObject
{
    private Cylinder _cylinder;
    private void Awake()
    {
        var cylinder = GetComponent<CylinderCollider>();
        var center = cylinder.center;
        mCenterX = center.x;
        mCenterY = center.y;
        mCenterZ = center.z;

        _cylinder = isStatic
            ? new Cylinder(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(cylinder.height), Convert.ToDecimal(cylinder.radius))
            : new Cylinder(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(cylinder.height), Convert.ToDecimal(cylinder.radius),
                Convert.ToDecimal(mass));

        _cylinder.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(staticFriction),
            Convert.ToDecimal(kineticFriction), Convert.ToDecimal(bounciness));

        var pos = transform.position + center;
        _cylinder.position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x),
            Convert.ToDecimal(pos.y), Convert.ToDecimal(pos.z));
        var orientation = transform.rotation;
        _cylinder.orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(orientation.x),
            Convert.ToDecimal(orientation.y), Convert.ToDecimal(orientation.z), Convert.ToDecimal(orientation.w));
        mEntity = _cylinder;
        Activate();
    }
}
