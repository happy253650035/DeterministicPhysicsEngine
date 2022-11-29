using System;
using System.Collections.Generic;
using System.Linq;
using Base;
using UnityEngine;

namespace PhysicsTweens
{
    public class PhysicsTweenRotation : PhysicsTween
    {
        public Vector3 from;
        public Vector3 to;
        public Vector3 rotateVelocity;
        public bool useVelocity;
        private List<PhysicsObject> _physicsObjects = new();

        public override void Start()
        {
            totalTime = duration + delay - startTime;
            _physicsObjects = target.transform.GetComponentsInChildren<PhysicsObject>().ToList();
        }

        public override void End()
        {
        }

        public override void Update()
        {
            if (!isActive) return;
            var elapse = PhysicsWorld.Instance.TimeSinceStart - startTime;
            if (elapse < delay) return;

            if (elapse > totalTime)
            {
                switch (loopType)
                {
                    case LoopType.None:
                        isActive = false;
                        break;
                    case LoopType.Loop:
                        startTime = PhysicsWorld.Instance.TimeSinceStart - delay;
                        elapse = 0;
                        break;
                    case LoopType.PingPong:
                        startTime = PhysicsWorld.Instance.TimeSinceStart - delay;
                        elapse = 0;
                        (from, to) = (to, from);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            if (useVelocity)
            {
                target.transform.Rotate(rotateVelocity);
            }
            else
            {
                var factor = Mathf.Clamp01( (elapse - delay) / totalTime);
                target.transform.localRotation = Quaternion.Euler(new Vector3(
                    Mathf.Lerp(@from.x, to.x, factor),
                    Mathf.Lerp(@from.y, to.y, factor),
                    Mathf.Lerp(@from.z, to.z, factor)));;
            }
            foreach (var po in _physicsObjects)
            {
                var position = po.transform.position;
                var orientation = po.transform.rotation;
                po.mEntity.Position = new BEPUutilities.Vector3(position.x, position.y, position.z);
                po.mEntity.Orientation = new BEPUutilities.Quaternion(orientation.x, orientation.y, orientation.z,
                    orientation.w);
            }
        }
    }
}