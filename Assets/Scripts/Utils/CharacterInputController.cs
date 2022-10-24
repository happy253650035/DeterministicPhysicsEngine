using BEPUutilities;
using FixMath.NET;
using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    private BaseCharacterController _character;
    private Animator _animator;
    private void Start()
    {
        _character = GetComponent<BaseCharacterController>();
        _animator = transform.Find("Animator").GetComponent<Animator>();
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

        if (totalMovement.Length() < 1)
        {
            _animator.SetBool("idle", true);
            _animator.SetBool("walk", false);
            _animator.SetBool("jump1", false);
            _animator.SetBool("jump2", false);
        }
        else
        {
            _animator.SetBool("walk", true);
            _animator.SetBool("idle", false);
            _animator.SetBool("jump1", false);
            _animator.SetBool("jump2", false);
            _character.transform.forward = new UnityEngine.Vector3((float) totalMovement.X, 0, (float) -totalMovement.Y);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _animator.SetBool("jump1", true);
            _animator.SetBool("jump2", true);
            _animator.SetBool("walk", false);
            _animator.SetBool("idle", false);
            _character.Jump();
        }

        _character.mCharacterController.HorizontalMotionConstraint.MovementDirection = totalMovement;
    }
}
