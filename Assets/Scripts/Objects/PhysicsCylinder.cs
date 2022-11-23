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
            var radius = cylinder.radius;
            var height = cylinder.height;
            if (useScale)
            {
                var localScale = cylinder.transform.localScale;
                radius *= Mathf.Max(localScale.x, localScale.z);
                height *= localScale.y;
            }

            _cylinder = isStatic
                ? new Cylinder(new BEPUutilities.Vector3(0, 0, 0), height, radius)
                : new Cylinder(new BEPUutilities.Vector3(0, 0, 0), height, radius,
                    mass);

            var pos = transform.position + center;
            _cylinder.position = new BEPUutilities.Vector3(pos.x,
                pos.y, pos.z);
            var orientation = transform.rotation;
            _cylinder.orientation = new BEPUutilities.Quaternion(orientation.x,
                orientation.y, orientation.z, orientation.w);
            mEntity = _cylinder;
            mEntity.CollisionInformation.GameObject = gameObject;
        }
    
        private void Start()
        {
            var material = GetComponent<PhysicsMaterial>();
            if (material)
            {
                _cylinder.material = new BEPUphysics.Materials.Material(material.staticFriction,
                    material.kineticFriction, material.bounciness);
            }
            else
            {
                _cylinder.material = new BEPUphysics.Materials.Material(PhysicsWorld.Instance.staticFriction,
                    PhysicsWorld.Instance.kineticFriction, PhysicsWorld.Instance.bounciness);
            }
            Activate();
        }
    }
}
