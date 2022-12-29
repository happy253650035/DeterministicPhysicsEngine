using Base;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace MapComponents
{
    public class HammerCom : MonoBehaviour
    {
        public Transform staticTransform;
        public Transform hammerTransform;
        public Transform pointTransform;

        private void Start()
        {
            var command = new Command
            {
                commandID = (int) CommandID.HammerCommand,
                intValue1 = staticTransform.GetComponent<PhysicsObject>().id,
                intValue2 = hammerTransform.GetComponent<PhysicsObject>().id,
                vector3_1 = pointTransform.position
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}