using System;
using Base;
using BEPUphysics.Entities.Prefabs;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(SphereCollider))]
    public class PhysicsSphere : PhysicsObject
    {
        private Sphere _sphere;

        private void Awake()
        {
            var sphere = GetComponent<SphereCollider>();
            center = sphere.center;
            var radius = sphere.radius;
            if (useScale)
            {
                var localScale = sphere.transform.localScale;
                radius *= Mathf.Max(Mathf.Max(localScale.x, localScale.y),
                    localScale.z);
            }

            _sphere = isStatic
                ? new Sphere(new BEPUutilities.Vector3(0, 0, 0), radius)
                : new Sphere(new BEPUutilities.Vector3(0, 0, 0), radius, mass);

            var pos = this.transform.position + center;
            _sphere.position = new BEPUutilities.Vector3(pos.x,
                pos.y,
                pos.z);
            var orientation = transform.rotation;
            _sphere.orientation = new BEPUutilities.Quaternion(orientation.x,
                orientation.y, orientation.z, orientation.w);
            mEntity = _sphere;
            mEntity.CollisionInformation.GameObject = gameObject;
        }
    
        private void Start()
        {
            var material = GetComponent<PhysicsMaterial>();
            if (material)
            {
                _sphere.material = new BEPUphysics.Materials.Material(material.staticFriction,
                    material.kineticFriction, material.bounciness);
            }
            else
            {
                _sphere.material = new BEPUphysics.Materials.Material(PhysicsWorld.Instance.staticFriction,
                    PhysicsWorld.Instance.kineticFriction, PhysicsWorld.Instance.bounciness);
            }
            Activate();
        }
    }
}