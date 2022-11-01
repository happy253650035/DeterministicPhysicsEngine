using UnityEngine;

namespace MapComponents
{
    public class BounceCom : MonoBehaviour
    {
        public enum BounceType
        {
            Ground = 1,
            Wall = 2,
            Cylinder = 3,
        }

        public BounceType bounceType;
        public float force;
    }
}
