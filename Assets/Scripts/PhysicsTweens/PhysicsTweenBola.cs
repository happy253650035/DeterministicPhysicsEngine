using System;
using Base;
using UnityEngine;

namespace PhysicsTweens
{
    public class PhysicsTweenBola : PhysicsTween
    {
        public Vector3 from;
        public Vector3 to;
        public override void Start()
        {
            totalTime = duration + delay - startTime;
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
            target.transform.localRotation = Quaternion.Euler(new Vector3(
                Mathf.Lerp(@from.x, to.x, factor),
                Mathf.Lerp(@from.y, to.y, factor),
                Mathf.Lerp(@from.z, to.z, factor)));;
        }
    }
}