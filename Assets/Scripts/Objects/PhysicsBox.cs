using System;
using Base;
using BEPUphysics.Entities.Prefabs;
using UnityEngine;

namespace Objects
{
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
                ? new Box(new BEPUutilities.Vector3(0, 0, 0), size.x,
                    size.y, size.z)
                : new Box(new BEPUutilities.Vector3(0, 0, 0), size.x,
                    size.y, size.z, mass);

            var pos = transform.position + center;
            _mBox.position = new BEPUutilities.Vector3(pos.x,
                pos.y, pos.z);
            var orientation = transform.rotation;
            _mBox.orientation = new BEPUutilities.Quaternion(orientation.x,
                orientation.y, orientation.z, orientation.w);
            mEntity = _mBox;
            mEntity.CollisionInformation.GameObject = gameObject;
        }
    
        private void Start()
        {
            var material = GetComponent<PhysicsMaterial>();
            if (material)
            {
                _mBox.material = new BEPUphysics.Materials.Material(material.staticFriction,
                    material.kineticFriction, material.bounciness);
            }
            else
            {
                _mBox.material = new BEPUphysics.Materials.Material(PhysicsWorld.Instance.staticFriction,
                    PhysicsWorld.Instance.kineticFriction, PhysicsWorld.Instance.bounciness);
            }
            Activate();
        }
    }
}