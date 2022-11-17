using Base;
using Managers;
using UnityEngine;
using Utils;

namespace MapComponents
{
    public class ChangeColorCom : MonoBehaviour
    {
        public Color color;
        
        private void Start()
        {
            var command = new Command
            {
                commandID = (int) CommandID.ChangeColorCommand,
                objectId = GetComponent<PhysicsObject>().id,
                color = color
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}