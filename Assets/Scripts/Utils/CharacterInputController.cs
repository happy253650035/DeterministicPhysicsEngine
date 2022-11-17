using Base;
using Managers;
using UnityEngine;

namespace Utils
{
    public class CharacterInputController : MonoBehaviour
    {
        private BaseCharacterController _character;
        private Animator _animator;
        private Vector2 _preTotalMovement = Vector2.zero;
        private void Start()
        {
            _character = GetComponent<BaseCharacterController>();
            if (transform.Find("Animator"))
            {
                _animator = transform.Find("Animator").GetComponent<Animator>();
            }
        }

        private void Update()
        {
            if (_character == null) return;
            if (!_character.IsActive) return;
            var totalMovement = Vector2.zero;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                totalMovement = Vector2.down;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                totalMovement = Vector2.up;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                totalMovement = Vector2.left;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                totalMovement = Vector2.right;
            }

            if (totalMovement.magnitude < 1)
            {
                if (_animator)
                {
                    _animator.SetBool("idle", true);
                    _animator.SetBool("walk", false);
                    _animator.SetBool("jump1", false);
                    _animator.SetBool("jump2", false);
                }
            }
            else
            {
                if (_animator)
                {
                    _animator.SetBool("walk", true);
                    _animator.SetBool("idle", false);
                    _animator.SetBool("jump1", false);
                    _animator.SetBool("jump2", false);
                }
                _character.transform.forward = new Vector3(totalMovement.x, 0, -totalMovement.y);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (_animator)
                {
                    _animator.SetBool("jump1", true);
                    _animator.SetBool("jump2", true);
                    _animator.SetBool("walk", false);
                    _animator.SetBool("idle", false);
                }
                _character.ExecuteSkill(SkillName.Jump);
            }

            if (Input.GetKey(KeyCode.J))
            {
                _character.ExecuteSkill(SkillName.Sprint);
            }

            if (_preTotalMovement != totalMovement)
            {
                var command = new Command
                {
                    commandID = (int) CommandID.PlayerMoveCommand,
                    vector2_1 = totalMovement
                };
                CommandManager.Instance.SendCommand(command);
            }

            _preTotalMovement = totalMovement;
        }
    }
}
