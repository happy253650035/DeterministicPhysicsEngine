using Base;
using Managers;
using UnityEngine;
using Utils;

namespace MapComponents
{
    public class RotateCom : BaseObject
    {
        public enum LoopType
        {
            None = 1,
            Loop = 2,
            PingPong = 3,
        }
        public enum RotateType
        {
            Veclocity = 1,
            StartEnd = 2,
        }
        public LoopType loopType = LoopType.None;
        public float duration;
        public RotateType rotateType = RotateType.Veclocity;
        public Vector3 rotateVelocity;
        public Vector3 from;
        public Vector3 to;
        public AnimationCurve curve = new();

        private void Start()
        {
            ObjectManager.Instance.Add(this);
            var command = new Command
            {
                objectId = id,
                commandID = (int) CommandID.RotateCommand,
                intValue1 = (int)loopType,
                intValue2 = (int)rotateType,
                floatValue1 = duration,
                vector3_1 = rotateVelocity,
                vector3_2 = from,
                vector3_3 = to
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}