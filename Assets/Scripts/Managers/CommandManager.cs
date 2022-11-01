using System;
using System.Collections.Generic;
using BEPUphysics.Character;
using Utils;

namespace Managers
{
    public class CommandManager
    {
        private static CommandManager _instance;
        private readonly Queue<Command> _commandsQueue = new();

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
            foreach (var command in _commandsQueue)
            {
                var commandID = (CommandID) command.commandID;
                switch (commandID)
                {
                    case CommandID.DeadCommand:
                        PlayerManager.Instance.myPlayer.transform.position =
                            PlayerManager.Instance.myPlayer.survivePoint;
                        break;
                    case CommandID.SaveCommand:
                        PlayerManager.Instance.myPlayer.survivePoint = command.vector;
                        break;
                    case CommandID.RotateCommand:
                        PhysicsObjectManager.Instance.GetPhysicsObjectById(command.objectId).mEntity.angularVelocity =
                            new BEPUutilities.Vector3(Convert.ToDecimal(command.vector.x),
                                Convert.ToDecimal(command.vector.y), Convert.ToDecimal(command.vector.z));
                        break;
                    case CommandID.BounceCommand:
                    case CommandID.AccelerateCommand:
                        PlayerManager.Instance.myPlayer.mCharacterController.Body.ApplyImpulse(
                            PlayerManager.Instance.myPlayer.mCharacterController.Body.position,
                            new BEPUutilities.Vector3(Convert.ToDecimal(command.vector.x),
                                Convert.ToDecimal(command.vector.y), Convert.ToDecimal(command.vector.z)));
                        break;
                    case CommandID.TumbleCommand:
                        PlayerManager.Instance.myPlayer.mCharacterController.Body.ApplyImpulse(
                            PlayerManager.Instance.myPlayer.mCharacterController.Body.position,
                            new BEPUutilities.Vector3(Convert.ToDecimal(command.vector.x),
                                Convert.ToDecimal(command.vector.y), Convert.ToDecimal(command.vector.z)));
                        PlayerManager.Instance.myPlayer.mCharacterController.StanceManager.DesiredStance = Stance.Crouching;
                        break;
                    case CommandID.ConveyorCommand:
                        if (command.enterOrExit == 0)
                        {
                            
                        }
                        break;
                    case CommandID.MarshCommand:
                        if (command.enterOrExit == 0)
                        {
                            
                        }
                        break;
                    case CommandID.IceCommand:
                        if (command.enterOrExit == 0)
                        {
                            
                        }
                        break;
                    case CommandID.MoveCommand:
                        break;
                    case CommandID.SeeSawCommand:
                        break;
                    case CommandID.AutoDisappearCommand:
                        break;
                    case CommandID.TriggerCommand:
                        break;
                    case CommandID.ChangeColorCommand:
                        break;
                    case CommandID.FanCommand:
                        break;
                }
            }
        }
    }
}