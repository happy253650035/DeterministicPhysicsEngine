using System;
using System.Collections.Generic;
using System.Linq;
using Base;
using UnityEngine;
using Vector3 = BEPUutilities.Vector3;

namespace PhysicsTweens
{
    public class PhysicsTweenPosition : PhysicsTween
    {
        public Vector3 from;
        public Vector3 to;
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
            
            var factor = Mathf.Clamp01( (elapse - delay) / totalTime);
            var point = Vector3.Lerp(from, to, factor);
            target.transform.localPosition = new UnityEngine.Vector3(point.X, point.Y, point.Z);
            foreach (var po in _physicsObjects)
            {
                var position = po.transform.position;
                po.mEntity.Position = new Vector3(position.x, position.y, position.z);
            }
        }
    }
}