
using UnityEngine;

namespace FunkyQuest
{
    public class CharacterPhysics : MonoBehaviour
    {
        [Header("Physics Controller - Read Only")]
        [SerializeField][ReadOnly]  private CharacterAnimationState _state;

        [field: Header("Physics Controller - Properties")]
        [field: SerializeField]     public bool CanInput { get; set; }
        [SerializeField]            private Animator _linkedAnimator;
        [SerializeField]            private SpriteRenderer _linkedRenderer;
        [SerializeField]            private Rigidbody2D _linkedRigidbody;
        [SerializeField]            private string _idlingState;
        [SerializeField]            private string _lookingUpState;
        [SerializeField]            private string _crouchingState;
        [SerializeField]            private string _walkingState;
        [SerializeField]            private float _speed;

        private void Update()
        {
            if (CanInput)
            {
                float vertical = Input.GetAxis("Vertical");
                if (vertical != 0)
                {
                    bool isLookingUp = vertical > 0;
                    SetState(isLookingUp ? CharacterAnimationState.LookingUp : CharacterAnimationState.Crouching, isLookingUp ? _lookingUpState : _crouchingState);

                    _linkedRigidbody.velocity = new Vector2(0, _linkedRigidbody.velocity.y);
                }
                else
                {
                    float horizontal = Input.GetAxis("Horizontal");
                    if (horizontal != 0)
                    {
                        SetState(CharacterAnimationState.Walking, _walkingState);
                        _linkedRigidbody.velocity = new Vector2(horizontal * _speed, _linkedRigidbody.velocity.y);
                        _linkedRenderer.flipX = horizontal < 0;
                    }
                    else
                    {
                        SetState(CharacterAnimationState.Idle, _idlingState);
                        _linkedRigidbody.velocity = new Vector2(0, _linkedRigidbody.velocity.y);
                    }
                }
            }
            else
            {
                _linkedRigidbody.velocity = new Vector2(0, _linkedRigidbody.velocity.y);
            }
        }

        private void SetState(CharacterAnimationState value, string state)
        {
            if (_state != value)
            {
                _state = value;
                _linkedAnimator.SetBool(_idlingState, false);
                _linkedAnimator.SetBool(_lookingUpState, false);
                _linkedAnimator.SetBool(_crouchingState, false);
                _linkedAnimator.SetBool(_walkingState, false);
                _linkedAnimator.SetBool(state, true);
            }
        }
    }
}