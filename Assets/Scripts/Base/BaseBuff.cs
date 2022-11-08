using Utils;

namespace Base
{
    public abstract class BaseBuff
    {
        public enum BuffState
        {
            Active,
            DeActive,
        }

        public int id;
        public BuffState state;
        public BaseCharacterController characterController;

        public abstract void Start();
        public abstract void End();
        public abstract void Tick();
    }
}