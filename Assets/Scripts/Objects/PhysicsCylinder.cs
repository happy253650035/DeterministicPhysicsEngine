using System;
using Base;
using BEPUphysics.Entities.Prefabs;
using Colliders;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(CylinderCollider))]
    public class PhysicsCylinder : PhysicsObject
    {
        private Cylinder _cylinder;
        private void Awake()
        {
            var cylinder = GetComponent<CylinderCollider>();
            center = cylinder.center;

            _cylinder = isStatic
                ? new Cylinder(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(cylinder.height), Convert.ToDecimal(cylinder.radius))
                : new Cylinder(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(cylinder.height), Convert.ToDecimal(cylinder.radius),
                    Convert.ToDecimal(mass));

            var pos = transform.position + center;
            _cylinder.position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x),
                Convert.ToDecimal(pos.y), Convert.ToDecimal(pos.z));
            var orientation = transform.rotation;
            _cylinder.orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(orientation.x),
                Convert.ToDecimal(orientation.y), Convert.ToDecimal(orientation.z), Convert.ToDecimal(orientation.w));
            mEntity = _cylinder;
            mEntity.CollisionInformation.GameObject = gameObject;
        }
    
        private void Start()
        {
            var material = GetComponent<PhysicsMaterial>();
            if (material)
            {
                _cylinder.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(material.staticFriction),
                    Convert.ToDecimal(material.kineticFriction), Convert.ToDecimal(material.bounciness));
            }
            else
            {
                _cylinder.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(PhysicsWorld.Instance.staticFriction),
                    Convert.ToDecimal(PhysicsWorld.Instance.kineticFriction), Convert.ToDecimal(PhysicsWorld.Instance.bounciness));
            }
            Activate();
        }
    }
}
