using System;
using Base;
using Managers;
using UnityEngine;
using Utils;

namespace MapComponents
{
    public class SeeSawCom : MonoBehaviour
    {
        public Transform fulcrumTransform;
        public Vector3 fulcrumPoint;

        private void Start()
        {
            var command = new Command
            {
                commandID = (int) CommandID.RotateCommand,
                objectId = GetComponent<PhysicsObject>().id,
                intValue1 = fulcrumTransform.GetComponent<PhysicsObject>().id,
                vector3_1 = fulcrumPoint,
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}