using System;
using Base;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace MapComponents
{
    public class SeeSawCom : MonoBehaviour
    {
        public Transform staticTransform;
        public Transform boardTransform;
        public Transform pointTransform;

        private void Start()
        {
            var command = new Command
            {
                commandID = (int) CommandID.SeeSawCommand,
                intValue1 = staticTransform.GetComponent<PhysicsObject>().id,
                intValue2 = boardTransform.GetComponent<PhysicsObject>().id,
                vector3_1 = pointTransform.position
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}