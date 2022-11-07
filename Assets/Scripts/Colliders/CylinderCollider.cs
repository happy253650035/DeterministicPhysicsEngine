using UnityEngine;

namespace Colliders
{
    public class CylinderCollider : MonoBehaviour
    {
        public Vector3 center = Vector3.zero;
        public float radius = 1;
        public float height = 1;

        private void OnDrawGizmosSelected()
        {
            var offset = height / 2;
            DrawCircle(offset, radius);
            DrawCircle(-offset, radius);
        }

        private void DrawCircle(float offset, float r)
        {
            const int sideCount = 30;
            const int intervalAngle = 360 / sideCount;
            for (var i = 0; i < sideCount; i++)
            {
                var angleFrom = intervalAngle * i * Mathf.Deg2Rad;
                var angleTo = intervalAngle * (i + 1) * Mathf.Deg2Rad;
                var from = transform.localRotation * new Vector3(r * Mathf.Cos(angleFrom), offset,
                    r * Mathf.Sin(angleFrom)) + transform.position;
                var to = transform.localRotation * new Vector3(r * Mathf.Cos(angleTo), offset,
                    r * Mathf.Sin(angleTo)) + transform.position;
                Gizmos.DrawLine(from, to);
            }
        }
    }
}