using System;
using Base;
using Managers;
using UnityEngine;
using Utils;

namespace MapComponents
{
    public class MoveCom : BaseObject
    {
        public enum LoopType
        {
            None = 1,
            Loop = 2,
            PingPong = 3,
        }
        public LoopType loopType = LoopType.None;
        public Vector3 from;
        public Vector3 to;
        public float duration;
        public AnimationCurve curve;

        private void Start()
        {
            var command = new Command
            {
                commandID = (int) CommandID.MoveCommand,
                objectId = id,
                intValue1 = (int)loopType,
                floatValue1 = duration,
                vector3_1 = from,
                vector3_2 = to
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}