using UnityEngine;

namespace MapComponents
{
    public class MoveCom : MonoBehaviour
    {
        public Vector3 from;
        public Vector3 to;
        public bool loop;
        public float duration;
        public AnimationCurve curve;
    }
}