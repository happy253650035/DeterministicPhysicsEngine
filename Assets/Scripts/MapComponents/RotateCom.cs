using System;
using Managers;
using UnityEngine;
using Utils;

public class RotateCom : MonoBehaviour
{
    public Vector3 rotateVelocity;

    private void Start()
    {
        var command = new Command
        {
            commandID = (int) CommandID.RotateCommand,
            objectId = GetComponent<PhysicsObject>().id,
            vector = rotateVelocity
        };
        CommandManager.Instance.SendCommand(command);
    }
}
