using System;
using Base;
using Managers;
using Utils;

namespace Skills
{
    public class SprintSkill : BaseSkill
    {
        public SprintSkill()
        {
            name = SkillName.Sprint;
        }
        
        public override void Execute()
        {
            var command = new Command
            {
                commandID = (int) CommandID.SprintCommand,
                vector3_1 = characterController.transform.forward
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}