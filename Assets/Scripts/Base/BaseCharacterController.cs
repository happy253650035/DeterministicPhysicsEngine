using System;
using UnityEngine;
using CharacterController = BEPUphysics.Character.CharacterController;

[RequireComponent(typeof(CapsuleCollider))]
public class BaseCharacterController : MonoBehaviour
{
    public float mass = 2;
    public float speed = 0.5f;
    public CharacterController mCharacterController;

    public bool IsActive { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        var capsule = GetComponent<CapsuleCollider>();
        mCharacterController = new CharacterController();
        var pos = transform.position;
        var center = capsule.center;
        mCharacterController.Body.position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x + center.x),
            Convert.ToDecimal(pos.y + center.y), Convert.ToDecimal(pos.z + center.z));
        mCharacterController.Body.Height = Convert.ToDecimal(capsule.height);
        mCharacterController.Body.Radius = Convert.ToDecimal(capsule.radius);
        mCharacterController.Body.Mass = Convert.ToDecimal(mass);
        mCharacterController.SpeedScale *= Convert.ToDecimal(speed);
        Activate();
    }

    public void Activate()
    {
        if (IsActive) return;
        IsActive = true;
        PhysicsWorld.Instance.AddPhysicsCharacterController(this);
    }

    public void Deactivate()
    {
        if (!IsActive) return;
        IsActive = false;
        PhysicsWorld.Instance.RemovePhysicsCharacterController(this);
    }

    public void Jump()
    {
        mCharacterController.Jump();
    }
}