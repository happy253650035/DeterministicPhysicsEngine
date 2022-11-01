using Managers;
using UnityEngine;
using Utils;

namespace MapComponents
{
    public class RotateCom : MonoBehaviour
    {
        public bool loop;
        public float duration;
        public Vector3 rotateVelocity;
        public AnimationCurve curve;

        private void Start()
        {
            var command = new Command
            {
                commandID = (int) CommandID.RotateCommand,
                objectId = GetComponent<PhysicsObject>().id,
                boolValue1 = loop,
                floatValue1 = duration,
                vector = rotateVelocity
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}
