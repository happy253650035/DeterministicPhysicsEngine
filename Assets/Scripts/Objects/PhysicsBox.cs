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
        center = box.center;

        _mBox = isStatic
            ? new Box(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(size.x),
                Convert.ToDecimal(size.y), Convert.ToDecimal(size.z))
            : new Box(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(size.x),
                Convert.ToDecimal(size.y), Convert.ToDecimal(size.z), Convert.ToDecimal(mass));

        var pos = transform.position + center;
        _mBox.position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x),
            Convert.ToDecimal(pos.y), Convert.ToDecimal(pos.z));
        var orientation = transform.rotation;
        _mBox.orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(orientation.x),
            Convert.ToDecimal(orientation.y), Convert.ToDecimal(orientation.z), Convert.ToDecimal(orientation.w));
        mEntity = _mBox;
        mEntity.CollisionInformation.GameObject = gameObject;
    }
    
    private void Start()
    {
        var material = GetComponent<PhysicsMaterial>();
        if (material)
        {
            _mBox.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(material.staticFriction),
                Convert.ToDecimal(material.kineticFriction), Convert.ToDecimal(material.bounciness));
        }
        else
        {
            _mBox.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(PhysicsWorld.Instance.staticFriction),
                Convert.ToDecimal(PhysicsWorld.Instance.kineticFriction), Convert.ToDecimal(PhysicsWorld.Instance.bounciness));
        }
        Activate();
    }
}