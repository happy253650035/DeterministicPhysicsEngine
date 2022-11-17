using System;
using Base;
using Managers;
using UnityEngine;
using Utils;

namespace MapComponents
{
    public class MoveCom : MonoBehaviour
    {
        public Vector3 from;
        public Vector3 to;
        public bool loop;
        public float duration;
        public AnimationCurve curve;

        private void Start()
        {
            var command = new Command
            {
                commandID = (int) CommandID.MoveCommand,
                objectId = GetComponent<PhysicsObject>().id,
                boolValue1 = loop,
                floatValue1 = duration,
                vector3_1 = from,
                vector3_2 = to
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}