using System;
using FixMath.NET;
using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    private BaseCharacterController _character;
    private void Start()
    {
        _character = GetComponent<BaseCharacterController>();
    }

    private void Update()
    {
        if (_character == null) return;
        if (!_character.IsActive) return;
        var totalMovement = BEPUutilities.Vector2.Zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            totalMovement += new BEPUutilities.Vector2(0, -1);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            totalMovement += new BEPUutilities.Vector2(0, 1);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            totalMovement += new BEPUutilities.Vector2(-1, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            totalMovement += new BEPUutilities.Vector2(1, 0);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _character.Jump();
        }

        _character.mCharacterController.HorizontalMotionConstraint.MovementDirection = totalMovement;
    }
}
