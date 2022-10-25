using System;
using UnityEngine;

public class CylinderCollider : MonoBehaviour
{
    public Vector3 center = Vector3.zero;
    public float radius = 1;
    public float height = 1;

    private void OnDrawGizmosSelected()
    {
        var offsetY = height / 2;
        var position = transform.position;
        DrawCircle(position + new Vector3(0, offsetY, 0), radius);
        DrawCircle(position - new Vector3(0, offsetY, 0), radius);
    }

    private void DrawCircle(Vector3 center, float radius)
    {
        const int sideCount = 30;
        const int intervalAngle = 360 / sideCount;
        for (var i = 0; i < sideCount; i++)
        {
            var angleFrom = intervalAngle * i * Mathf.Deg2Rad;
            var angleTo = intervalAngle * (i + 1) * Mathf.Deg2Rad;
            var position = transform.position;
            Gizmos.DrawLine(
                new Vector3(radius * Mathf.Cos(angleFrom) + center.x, center.y,
                    radius * Mathf.Sin(angleFrom) + center.z),
                new Vector3(radius * Mathf.Cos(angleTo) + center.x, center.y,
                    radius * Mathf.Sin(angleTo) + center.z));
        }
    }
}