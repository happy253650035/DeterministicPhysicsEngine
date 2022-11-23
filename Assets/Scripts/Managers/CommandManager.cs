using System;
using System.Collections.Generic;
using Base;
using BEPUphysics.Character;
using BEPUphysics.Constraints.SolverGroups;
using BEPUutilities;
using Buffs;
using PhysicsTweens;
using Utils;

namespace Managers
{
    public class CommandManager
    {
        private static CommandManager _instance;
        private readonly Queue<Command> _commandsQueue = new();
        private readonly Queue<int> _conveyorQueue = new();
        private IceBuff _iceBuff;

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
                        PlayerManager.Instance.myPlayer.survivePoint = command.vector3_1;
                        break;
                    case CommandID.RotateCommand:
                        var tweenRotate = new PhysicsTweenRotation();
                        tweenRotate.loopType = (PhysicsTween.LoopType) command.intValue1;
                        tweenRotate.target = ObjectManager.Instance.GetBaseObjectById(command.objectId);
                        tweenRotate.useVelocity = command.intValue2 == 1;
                        if (tweenRotate.useVelocity)
                        {
                            tweenRotate.rotateVelocity = command.vector3_1;
                        }
                        else
                        {
                            tweenRotate.duration = command.floatValue1;
                            tweenRotate.from = command.vector3_2;
                            tweenRotate.to = command.vector3_3;
                        }
                        PhysicsTweenManager.Instance.PlayTween(tweenRotate);
                        break;
                    case CommandID.BounceCommand:
                    case CommandID.AccelerateCommand:
                        PlayerManager.Instance.myPlayer.mCharacterController.Body.ApplyImpulse(
                            PlayerManager.Instance.myPlayer.mCharacterController.Body.position,
                            new Vector3(command.vector3_1.x,
                                command.vector3_1.y, command.vector3_1.z));
                        break;
                    case CommandID.TumbleCommand:
                        PlayerManager.Instance.myPlayer.mCharacterController.Body.ApplyImpulse(
                            PlayerManager.Instance.myPlayer.mCharacterController.Body.position,
                            new Vector3(command.vector3_1.x,
                                command.vector3_1.y, command.vector3_1.z));
                        var tumbleBuff = new TumbleBuff();
                        tumbleBuff.duration = 2;
                        PlayerManager.Instance.myPlayer.AddBuff(tumbleBuff);
                        break;
                    case CommandID.ConveyorCommand:
                        if (command.enterOrExit == 0)
                        {
                            var speedBuff = new SpeedBuff();
                            speedBuff.direction = command.vector3_1;
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
                        if (command.enterOrExit == 0)
                        {
                            if (_iceBuff != null) PlayerManager.Instance.myPlayer.RemoveBuff(_iceBuff);
                            _iceBuff = new IceBuff();
                            _iceBuff.damp = command.floatValue1;
                            PlayerManager.Instance.myPlayer.AddBuff(_iceBuff);
                        }
                        else
                        {
                            PlayerManager.Instance.myPlayer.RemoveBuff(_iceBuff);
                        }

                        break;
                    case CommandID.MoveCommand:
                        var tween = new PhysicsTweenPosition();
                        tween.from = new Vector3(command.vector3_1.x,
                            command.vector3_1.y, command.vector3_1.z);
                        tween.to = new Vector3(command.vector3_2.x,
                            command.vector3_2.y, command.vector3_2.z);
                        tween.loopType = (PhysicsTween.LoopType) command.intValue1;
                        tween.duration = command.floatValue1;
                        tween.target = ObjectManager.Instance.GetBaseObjectById(command.objectId);
                        PhysicsTweenManager.Instance.PlayTween(tween);
                        break;
                    case CommandID.SeeSawCommand:
                        var srcGo = PhysicsObjectManager.Instance.GetPhysicsObjectById(command.intValue1);
                        var desGo = PhysicsObjectManager.Instance.GetPhysicsObjectById(command.intValue2);
                        var joint = new RevoluteJoint(srcGo.mEntity, desGo.mEntity, new Vector3(
                                command.vector3_1.x, command.vector3_1.y, command.vector3_1.z),
                            Vector3.Backward);
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
                            new Vector3(command.vector3_1.x, command.vector3_1.y,
                                command.vector3_1.z) * 30);
                        break;
                    case CommandID.PlayerMoveCommand:
                        if (_iceBuff != null)
                            _iceBuff.CheckMovement(command.vector2_1);
                        else
                            PlayerManager.Instance.myPlayer.mCharacterController.HorizontalMotionConstraint
                                .MovementDirection = new Vector2(command.vector2_1.x,
                                command.vector2_1.y);
                        break;
                }
            }
        }
    }
}