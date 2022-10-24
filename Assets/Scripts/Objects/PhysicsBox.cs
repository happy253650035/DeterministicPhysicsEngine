using System;
using BEPUphysics.Entities.Prefabs;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PhysicsBox : PhysicsObject
{
    private Box _mBox;

    private void Awake()
    {
        var box = GetComponent<BoxCollider>();
        var size = box.size;
        if (useScale)
        {
            size = Vector3.Scale(box.size, transform.localScale);
        }
        var center = box.center;
        mCenterX = center.x;
        mCenterY = center.y;
        mCenterZ = center.z;

        _mBox = isStatic
            ? new Box(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(size.x),
                Convert.ToDecimal(size.y), Convert.ToDecimal(size.z))
            : new Box(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(size.x),
                Convert.ToDecimal(size.y), Convert.ToDecimal(size.z), Convert.ToDecimal(mass));

        _mBox.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(staticFriction),
            Convert.ToDecimal(kineticFriction), Convert.ToDecimal(bounciness));

        var pos = transform.position + center;
        _mBox.position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x),
            Convert.ToDecimal(pos.y), Convert.ToDecimal(pos.z));
        var orientation = transform.rotation;
        _mBox.orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(orientation.x),
            Convert.ToDecimal(orientation.y), Convert.ToDecimal(orientation.z), Convert.ToDecimal(orientation.w));
        mEntity = _mBox;
        mEntity.angularVelocity = new BEPUutilities.Vector3(Convert.ToDecimal(angularVelocity.x),
            Convert.ToDecimal(angularVelocity.y), Convert.ToDecimal(angularVelocity.z));
        Activate();
    }
}