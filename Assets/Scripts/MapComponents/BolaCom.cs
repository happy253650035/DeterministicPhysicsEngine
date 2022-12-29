using Base;
using Managers;
using UnityEngine;
using Utils;

namespace MapComponents
{
    public class BolaCom : MonoBehaviour
    {
        public Transform staticTransform;
        public Transform bolaTransform;
        public Transform pointTransform;

        private void Start()
        {
            var command = new Command
            {
                commandID = (int) CommandID.BolaCommand,
                intValue1 = staticTransform.GetComponent<PhysicsObject>().id,
                intValue2 = bolaTransform.GetComponent<PhysicsObject>().id,
                vector3_1 = pointTransform.position
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}