using System;
using Base;
using BEPUphysics.Entities.Prefabs;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class PhysicsCapsule : PhysicsObject
    {
        private Capsule _capsure;

        private void Awake()
        {
            var capsule = GetComponent<CapsuleCollider>();
            center = capsule.center;
            var radius = capsule.radius;
            var height = capsule.height;
            if (useScale)
            {
                var localScale = capsule.transform.localScale;
                radius *= Mathf.Max(localScale.x, localScale.z);
                height *= localScale.y;
            }
            
            _capsure = isStatic
                ? new Capsule(new BEPUutilities.Vector3(0, 0, 0), height, radius)
                : new Capsule(new BEPUutilities.Vector3(0, 0, 0), height, radius,
                    mass);

            var pos = transform.position + center;
            _capsure.position = new BEPUutilities.Vector3(pos.x,
                pos.y, pos.z);
            var orientation = transform.rotation;
            _capsure.orientation = new BEPUutilities.Quaternion(orientation.x,
                orientation.y, orientation.z, orientation.w);
            mEntity = _capsure;
            mEntity.CollisionInformation.GameObject = gameObject;
        }
    
        private void Start()
        {
            var material = GetComponent<PhysicsMaterial>();
            if (material)
            {
                _capsure.material = new BEPUphysics.Materials.Material(material.staticFriction,
                    material.kineticFriction, material.bounciness);
            }
            else
            {
                _capsure.material = new BEPUphysics.Materials.Material(PhysicsWorld.Instance.staticFriction,
                    PhysicsWorld.Instance.kineticFriction, PhysicsWorld.Instance.bounciness);
            }
            Activate();
        }
    }
}