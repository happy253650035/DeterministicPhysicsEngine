using Base;

namespace Skills
{
    public class JumpSkill : BaseSkill
    {
        public override void Execute()
        {
            characterController.Jump();
        }
    }
}