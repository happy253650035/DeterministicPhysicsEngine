using Utils;

namespace Base
{
    public abstract class BaseSkill
    {
        public BaseCharacterController characterController;
        public SkillName name;
        public abstract void Execute();
    }
}