using System;
using System.Collections.Generic;
using Base;
using BEPUphysics.CollisionShapes;
using BEPUphysics.CollisionShapes.ConvexShapes;
using BEPUphysics.Entities.Prefabs;
using Colliders;
using UnityEngine;

namespace Objects
{
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
                if (!sphere.gameObject.activeSelf)
                    continue;
                var radius = sphere.radius;
                if (useScale)
                {
                    var localScale = sphere.transform.localScale;
                    radius *= Mathf.Max(Mathf.Max(localScale.x, localScale.y),
                        localScale.z);
                }
                var pos = sphere.transform.localPosition + sphere.center;
                var position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x), Convert.ToDecimal(pos.y),
                    Convert.ToDecimal(pos.z));
                var shape = new CompoundShapeEntry(new SphereShape(Convert.ToDecimal(radius)), position);
                var shapeRotation = sphere.transform.localRotation;
                shape.LocalTransform.Orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(shapeRotation.x),
                    Convert.ToDecimal(shapeRotation.y), Convert.ToDecimal(shapeRotation.z), Convert.ToDecimal(shapeRotation.w));
                compoundShapes.Add(shape);
            }
            foreach (var box in boxes)
            {
                if (!box.gameObject.activeSelf)
                    continue;
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
                var shapeRotation = box.transform.localRotation;
                shape.LocalTransform.Orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(shapeRotation.x),
                    Convert.ToDecimal(shapeRotation.y), Convert.ToDecimal(shapeRotation.z), Convert.ToDecimal(shapeRotation.w));
                compoundShapes.Add(shape);
            }
            foreach (var capsule in capsules)
            {
                if (!capsule.gameObject.activeSelf)
                    continue;
                var radius = capsule.radius;
                var height = capsule.height;
                if (useScale)
                {
                    var localScale = capsule.transform.localScale;
                    radius *= Mathf.Max(localScale.x, localScale.z);
                    height *= localScale.y;
                }
                var pos = capsule.transform.localPosition + capsule.center;
                var position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x), Convert.ToDecimal(pos.y),
                    Convert.ToDecimal(pos.z));
                var shape = new CompoundShapeEntry(new CapsuleShape(Convert.ToDecimal(height), Convert.ToDecimal(radius)), position);
                var shapeRotation = capsule.transform.localRotation;
                shape.LocalTransform.Orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(shapeRotation.x),
                    Convert.ToDecimal(shapeRotation.y), Convert.ToDecimal(shapeRotation.z), Convert.ToDecimal(shapeRotation.w));
                compoundShapes.Add(shape);
            }
            foreach (var cylinder in cylinders)
            {
                if (!cylinder.gameObject.activeSelf)
                    continue;
                var radius = cylinder.radius;
                var height = cylinder.height;
                if (useScale)
                {
                    var localScale = cylinder.transform.localScale;
                    radius *= Mathf.Max(localScale.x, localScale.z);
                    height *= localScale.y;
                }
                var pos = cylinder.transform.localPosition + cylinder.center;
                var position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x), Convert.ToDecimal(pos.y),
                    Convert.ToDecimal(pos.z));
                var shape = new CompoundShapeEntry(new CapsuleShape(Convert.ToDecimal(height), Convert.ToDecimal(radius)), position);
                var shapeRotation = cylinder.transform.localRotation;
                shape.LocalTransform.Orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(shapeRotation.x),
                    Convert.ToDecimal(shapeRotation.y), Convert.ToDecimal(shapeRotation.z), Convert.ToDecimal(shapeRotation.w));
                compoundShapes.Add(shape);
            }

            _body = isStatic ? new CompoundBody(compoundShapes) : new CompoundBody(compoundShapes, Convert.ToDecimal(mass));
            var length = (_body.position - BEPUutilities.Vector3.Zero).Length();
            if (length > 0.01m)
            {
                foreach (var sphere in spheres)
                {
                    if (!sphere.gameObject.activeSelf)
                        continue;
                    sphere.transform.localPosition -= new Vector3((float)_body.position.X, (float)_body.position.Y,
                        (float)_body.position.Z);
                }
                foreach (var box in boxes)
                {
                    if (!box.gameObject.activeSelf)
                        continue;
                    box.transform.localPosition -= new Vector3((float)_body.position.X, (float)_body.position.Y,
                        (float)_body.position.Z);
                }
                foreach (var capsule in capsules)
                {
                    if (!capsule.gameObject.activeSelf)
                        continue;
                    capsule.transform.localPosition -= new Vector3((float)_body.position.X, (float)_body.position.Y,
                        (float)_body.position.Z);
                }
                foreach (var cylinder in cylinders)
                {
                    if (!cylinder.gameObject.activeSelf)
                        continue;
                    cylinder.transform.localPosition -= new Vector3((float)_body.position.X, (float)_body.position.Y,
                        (float)_body.position.Z);
                }
            }

            var p = transform.position;
            _body.position = new BEPUutilities.Vector3(Convert.ToDecimal(p.x),
                Convert.ToDecimal(p.y), Convert.ToDecimal(p.z));
            var orientation = transform.rotation;
            _body.orientation = new BEPUutilities.Quaternion(Convert.ToDecimal(orientation.x),
                Convert.ToDecimal(orientation.y), Convert.ToDecimal(orientation.z), Convert.ToDecimal(orientation.w));
            mEntity = _body;
            mEntity.CollisionInformation.GameObject = gameObject;
        }

        private void Start()
        {
            var material = GetComponent<PhysicsMaterial>();
            if (material)
            {
                _body.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(material.staticFriction),
                    Convert.ToDecimal(material.kineticFriction), Convert.ToDecimal(material.bounciness));
            }
            else
            {
                _body.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(PhysicsWorld.Instance.staticFriction),
                    Convert.ToDecimal(PhysicsWorld.Instance.kineticFriction), Convert.ToDecimal(PhysicsWorld.Instance.bounciness));
            }
            Activate();
        }
    }
}