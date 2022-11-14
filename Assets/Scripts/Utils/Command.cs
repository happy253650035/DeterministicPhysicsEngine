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
        ConveyorCommand = 7,
        MarshCommand = 8,
        IceCommand = 9,
        MoveCommand = 10,
        SeeSawCommand = 11,
        AutoDisappearCommand = 12,
        TriggerCommand = 13,
        ChangeColorCommand = 14,
        FanCommand = 15,
        JumpCommand = 16,
        SprintCommand = 17,
        PlayerMoveCommand = 18,
    }
    public struct Command
    {
        public int commandID;
        public int objectId;
        public int enterOrExit; //0:enter, 1:exit
        public bool boolValue1;
        public int intValue1;
        public float floatValue1;
        public Vector3 vector3;
        public Vector2 vector2;
    }
}