using Utils;

namespace Base
{
    public abstract class BaseBuff
    {
        public enum BuffType
        {
            Active,
            DeActive,
        }

        public BuffType buffType;
        public BuffName name;

        public abstract void Update();
    }
}