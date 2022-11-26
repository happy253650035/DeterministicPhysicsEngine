using UnityEngine;
using UnityEngine.Serialization;

namespace Utils
{
    public class CameraFollow : MonoBehaviour
    {
        public float followSpeed = 10;
        public Transform follower;
        public Vector3 offset = new(0, 3, -3);

        private void Start()
        {
            LateUpdate();
        }

        private void LateUpdate()
        {
            if (follower == null) return;
            transform.localPosition = Vector3.Lerp(transform.localPosition, follower.position + offset, followSpeed * Time.deltaTime);
        }
    }
}