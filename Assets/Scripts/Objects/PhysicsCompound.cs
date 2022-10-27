using System;
using System.Collections.Generic;
using BEPUphysics.CollisionShapes;
using BEPUphysics.CollisionShapes.ConvexShapes;
using BEPUphysics.Entities.Prefabs;
using UnityEngine;

public class PhysicsCompound : PhysicsObject
{
    private CompoundBody _body;

    private void Awake()
    {
        var spheres = GetComponentsInChildren<SphereCollider>();
        var boxes = GetComponentsInChildren<BoxCollider>();
        var capsules = GetComponentsInChildren<CapsuleCollider>();
        var cylinders = GetComponentsInChildren<CylinderCollider>();
        var compoundShapes = new List<CompoundShapeEntry>();
        foreach (var sphere in spheres)
        {
            var pos = sphere.transform.localPosition + sphere.center;
            var position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x), Convert.ToDecimal(pos.y),
                Convert.ToDecimal(pos.z));
            var shape = new CompoundShapeEntry(new SphereShape(Convert.ToDecimal(sphere.radius)), position);
            compoundShapes.Add(shape);
        }
        foreach (var box in boxes)
        {
            var size = box.size;
            if (useScale)
            {
                size = Vector3.Scale(box.size, box.transform.localScale);
            }
            var pos = box.transform.localPosition + box.center;
            var position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x), Convert.ToDecimal(pos.y),
                Convert.ToDecimal(pos.z));
            var shape = new CompoundShapeEntry(new BoxShape( Convert.ToDecimal(size.x),
                Convert.ToDecimal(size.y), Convert.ToDecimal(size.z)), position);
            compoundShapes.Add(shape);
        }
        foreach (var capsule in capsules)
        {
            var pos = capsule.transform.localPosition + capsule.center;
            var position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x), Convert.ToDecimal(pos.y),
                Convert.ToDecimal(pos.z));
            var shape = new CompoundShapeEntry(new CapsuleShape(Convert.ToDecimal(capsule.height), Convert.ToDecimal(capsule.radius)), position);
            compoundShapes.Add(shape);
        }
        foreach (var cylinder in cylinders)
        {
            var pos = cylinder.transform.localPosition + cylinder.center;
            var position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x), Convert.ToDecimal(pos.y),
                Convert.ToDecimal(pos.z));
            var shape = new CompoundShapeEntry(new CapsuleShape(Convert.ToDecimal(cylinder.height), Convert.ToDecimal(cylinder.radius)), position);
            compoundShapes.Add(shape);
        }

        _body = isStatic ? new CompoundBody(compoundShapes) : new CompoundBody(compoundShapes, Convert.ToDecimal(mass));
        _body.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(staticFriction),
            Convert.ToDecimal(kineticFriction), Convert.ToDecimal(bounciness));

        var p = transform.position;
        _body.position = new BEPUutilities.Vector3(Convert.ToDecimal(p.x),
            Convert.ToDecimal(p.y), Convert.ToDecimal(p.z));
        var orientation = transform.rotation;
        _body.orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(orientation.x),
            Convert.ToDecimal(orientation.y), Convert.ToDecimal(orientation.z), Convert.ToDecimal(orientation.w));
        mEntity = _body;
    }

    private void Start()
    {
        Activate();
    }
}