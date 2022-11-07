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

            _capsure = isStatic
                ? new Capsule(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(capsule.height), Convert.ToDecimal(capsule.radius))
                : new Capsule(new BEPUutilities.Vector3(0, 0, 0), Convert.ToDecimal(capsule.height), Convert.ToDecimal(capsule.radius),
                    Convert.ToDecimal(mass));

            var pos = transform.position + center;
            _capsure.position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x),
                Convert.ToDecimal(pos.y), Convert.ToDecimal(pos.z));
            var orientation = transform.rotation;
            _capsure.orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(orientation.x),
                Convert.ToDecimal(orientation.y), Convert.ToDecimal(orientation.z), Convert.ToDecimal(orientation.w));
            mEntity = _capsure;
            mEntity.CollisionInformation.GameObject = gameObject;
        }
    
        private void Start()
        {
            var material = GetComponent<PhysicsMaterial>();
            if (material)
            {
                _capsure.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(material.staticFriction),
                    Convert.ToDecimal(material.kineticFriction), Convert.ToDecimal(material.bounciness));
            }
            else
            {
                _capsure.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(PhysicsWorld.Instance.staticFriction),
                    Convert.ToDecimal(PhysicsWorld.Instance.kineticFriction), Convert.ToDecimal(PhysicsWorld.Instance.bounciness));
            }
            Activate();
        }
    }
}