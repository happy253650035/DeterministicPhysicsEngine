using UnityEngine;

namespace Utils
{
    public class CameraFollow : MonoBehaviour
    {
        public float followSpeed = 10;
        public Transform follower;
        private readonly Vector3 _offset = new Vector3(0, 3, -3);

        private void Start()
        {
            LateUpdate();
        }

        private void LateUpdate()
        {
            if (follower == null) return;
            transform.localPosition = Vector3.Lerp(transform.localPosition, follower.position + _offset, followSpeed * Time.deltaTime);
        }
    }
}