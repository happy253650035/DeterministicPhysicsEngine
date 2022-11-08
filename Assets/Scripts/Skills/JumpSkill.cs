using Base;
using Managers;
using Utils;

namespace Skills
{
    public class JumpSkill : BaseSkill
    {
        public JumpSkill()
        {
            name = SkillName.Jump;
        }
        
        public override void Execute()
        {
            var command = new Command
            {
                commandID = (int) CommandID.JumpCommand,
            };
            CommandManager.Instance.SendCommand(command);
        }
    }
}