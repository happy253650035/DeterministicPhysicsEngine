using System;
using System.Collections.Generic;
using Base;
using BEPUutilities;

namespace PhysicsTweens
{
    public class PhysicsTweenRotation : PhysicsTween
    {
        public Vector3 from;
        public Vector3 to;
        public UnityEngine.Vector3 rotateVelocity;
        public bool useVelocity;
        private List<PhysicsObject> _physicsObjects = new();

        public override void Start()
        {
            if (loopType != LoopType.Loop)
            {
                totalTime = duration - startTime;
            }

            for (var i = 0; i < target.transform.childCount; i++)
            {
                var po = target.transform.GetChild(i).GetComponent<PhysicsObject>();
                if (po) _physicsObjects.Add(po);
            }
        }

        public override void End()
        {
        }

        public override void Update()
        {
            if (!isActive) return;
            var elapse = PhysicsWorld.Instance.TimeSinceStart - startTime;

            if (elapse > totalTime)
            {
                if (loopType != LoopType.Loop)
                {
                    elapse -= totalTime;
                    if (loopType == LoopType.PingPong)
                    {
                        var temp = from;
                        from = to;
                        to = temp;
                    }
                    else
                    {
                        isActive = false;
                    }
                }
            }
            if (useVelocity)
            {
                target.transform.Rotate(rotateVelocity);
            }
            else
            {
                var rotate = (from - to) * elapse / totalTime + from;
                target.transform.localEulerAngles =
                    new UnityEngine.Vector3((float) rotate.X, (float) rotate.Y, (float) rotate.Z);
            }
            foreach (var po in _physicsObjects)
            {
                var position = po.transform.position;
                var orientation = po.transform.rotation;
                po.mEntity.Position = new Vector3(Convert.ToDecimal(position.x),
                    Convert.ToDecimal(position.y), Convert.ToDecimal(position.z));
                po.mEntity.Orientation = new Quaternion(Convert.ToDecimal(orientation.x),
                    Convert.ToDecimal(orientation.y), Convert.ToDecimal(orientation.z),
                    Convert.ToDecimal(orientation.w));
            }
        }
    }
}