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
        center = sphere.center;

        _sphere = isStatic
            ? new Sphere(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(sphere.radius))
            : new Sphere(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(sphere.radius), Convert.ToDecimal(mass));

        var pos = this.transform.position + center;
        _sphere.position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x),
            Convert.ToDecimal(pos.y),
            Convert.ToDecimal(pos.z));
        var orientation = transform.rotation;
        _sphere.orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(orientation.x),
            Convert.ToDecimal(orientation.y), Convert.ToDecimal(orientation.z), Convert.ToDecimal(orientation.w));
        mEntity = _sphere;
        mEntity.CollisionInformation.GameObject = gameObject;
    }
    
    private void Start()
    {
        var material = GetComponent<PhysicsMaterial>();
        if (material)
        {
            _sphere.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(material.staticFriction),
                Convert.ToDecimal(material.kineticFriction), Convert.ToDecimal(material.bounciness));
        }
        else
        {
            _sphere.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(PhysicsWorld.Instance.staticFriction),
                Convert.ToDecimal(PhysicsWorld.Instance.kineticFriction), Convert.ToDecimal(PhysicsWorld.Instance.bounciness));
        }
        Activate();
    }
}