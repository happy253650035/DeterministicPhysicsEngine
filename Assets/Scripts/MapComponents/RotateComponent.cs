using System;
using UnityEngine;

public class RotateComponent : MonoBehaviour
{
    public Vector3 rotateVelocity;

    private void Start()
    {
        GetComponent<PhysicsObject>().mEntity.angularVelocity = new BEPUutilities.Vector3(Convert.ToDecimal(rotateVelocity.x),
            Convert.ToDecimal(rotateVelocity.y), Convert.ToDecimal(rotateVelocity.z));
    }
}
