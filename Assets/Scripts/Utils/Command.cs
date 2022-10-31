using UnityEngine;

namespace Utils
{
    public enum CommandID
    {
        DeadCommand = 1,
        SaveCommand = 2,
        RotateCommand = 3,
        BounceCommand = 4,
        TumbleCommand = 5,
        AccelerateCommand = 6,
    }
    public struct Command
    {
        public int commandID;
        public int objectId;
        public Vector3 vector;
    }
}