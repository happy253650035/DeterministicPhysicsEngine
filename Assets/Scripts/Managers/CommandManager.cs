using System;
using System.Collections.Generic;
using BEPUphysics.Character;
using BEPUphysics.Constraints.SolverGroups;
using BEPUutilities;
using Buffs;
using Utils;

namespace Managers
{
    public class CommandManager
    {
        private static CommandManager _instance;
        private readonly Queue<Command> _commandsQueue = new();
        private readonly Queue<int> _conveyorQueue = new();

        public static CommandManager Instance
        {
            get { return _instance ??= new CommandManager(); }
        }

        public void SendCommand(Command command)
        {
            _commandsQueue.Enqueue(command);
        }

        public void Execute()
        {
            while (_commandsQueue.Count > 0)
            {
                var command = _commandsQueue.Dequeue();
                var commandID = (CommandID) command.commandID;
                switch (commandID)
                {
                    case CommandID.DeadCommand:
                        PlayerManager.Instance.myPlayer.transform.position =
                            PlayerManager.Instance.myPlayer.survivePoint;
                        break;
                    case CommandID.SaveCommand:
                        PlayerManager.Instance.myPlayer.survivePoint = command.vector3;
                        break;
                    case CommandID.RotateCommand:
                        PhysicsObjectManager.Instance.GetPhysicsObjectById(command.objectId).mEntity.angularVelocity =
                            new BEPUutilities.Vector3(Convert.ToDecimal(command.vector3.x),
                                Convert.ToDecimal(command.vector3.y), Convert.ToDecimal(command.vector3.z));
                        break;
                    case CommandID.BounceCommand:
                    case CommandID.AccelerateCommand:
                        PlayerManager.Instance.myPlayer.mCharacterController.Body.ApplyImpulse(
                            PlayerManager.Instance.myPlayer.mCharacterController.Body.position,
                            new BEPUutilities.Vector3(Convert.ToDecimal(command.vector3.x),
                                Convert.ToDecimal(command.vector3.y), Convert.ToDecimal(command.vector3.z)));
                        break;
                    case CommandID.TumbleCommand:
                        PlayerManager.Instance.myPlayer.mCharacterController.Body.ApplyImpulse(
                            PlayerManager.Instance.myPlayer.mCharacterController.Body.position,
                            new BEPUutilities.Vector3(Convert.ToDecimal(command.vector3.x),
                                Convert.ToDecimal(command.vector3.y), Convert.ToDecimal(command.vector3.z)));
                        var tumbleBuff = new TumbleBuff();
                        tumbleBuff.duration = 2;
                        PlayerManager.Instance.myPlayer.AddBuff(tumbleBuff);
                        break;
                    case CommandID.ConveyorCommand:
                        if (command.enterOrExit == 0)
                        {
                            var speedBuff = new SpeedBuff();
                            speedBuff.direction = command.vector3;
                            var id = PlayerManager.Instance.myPlayer.AddBuff(speedBuff);
                            _conveyorQueue.Enqueue(id);
                        }
                        else
                        {
                            var id = _conveyorQueue.Dequeue();
                            PlayerManager.Instance.myPlayer.RemoveBuff(id);
                        }

                        break;
                    case CommandID.MarshCommand:
                        PlayerManager.Instance.myPlayer.speed = command.enterOrExit == 0 ? 0.3f : 0.5f;
                        break;
                    case CommandID.IceCommand:
                        PlayerManager.Instance.myPlayer.speed = command.enterOrExit == 0 ? 0.7f : 0.5f;
                        break;
                    case CommandID.MoveCommand:
                        break;
                    case CommandID.SeeSawCommand:
                        var srcGo = PhysicsObjectManager.Instance.GetPhysicsObjectById(command.objectId);
                        var desGo = PhysicsObjectManager.Instance.GetPhysicsObjectById(command.intValue1);
                        var joint = new RevoluteJoint(srcGo.mEntity, desGo.mEntity, new BEPUutilities.Vector3(
                            Convert.ToDecimal(command.vector3.x),
                            Convert.ToDecimal(command.vector3.y), Convert.ToDecimal(command.vector3.z)), Vector3.Down);
                        PhysicsWorld.Instance.AddJoint(joint);
                        break;
                    case CommandID.AutoDisappearCommand:
                        break;
                    case CommandID.TriggerCommand:
                        break;
                    case CommandID.ChangeColorCommand:
                        break;
                    case CommandID.FanCommand:
                        break;
                    case CommandID.JumpCommand:
                        PlayerManager.Instance.myPlayer.mCharacterController.Jump();
                        break;
                    case CommandID.SprintCommand:
                        PlayerManager.Instance.myPlayer.mCharacterController.Body.ApplyImpulse(
                            PlayerManager.Instance.myPlayer.mCharacterController.Body.position,
                            new Vector3(Convert.ToDecimal(command.vector3.x), Convert.ToDecimal(command.vector3.y),
                                Convert.ToDecimal(command.vector3.z)) * 30);
                        break;
                    case CommandID.TumbleBuffCommand:
                        if (command.boolValue1)
                        {
                        }
                        else
                        {
                        }

                        break;
                    case CommandID.PlayerMoveCommand:
                        PlayerManager.Instance.myPlayer.mCharacterController.HorizontalMotionConstraint
                            .MovementDirection = new Vector2(Convert.ToDecimal(command.vector2.x),
                            Convert.ToDecimal(command.vector2.y));
                        break;
                }
            }
        }
    }
}