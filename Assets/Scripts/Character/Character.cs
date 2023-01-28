
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using static SonicEduquest.InputBasedInteractable;

namespace SonicEduquest
{
    public class Character : MonoBehaviour
    {
        [Flags]
        public enum InputFlags : byte
        {
            Physics = 1,
            Interactivity = 2
        }

        public InputFlags ReceiveInput
        {
            get => this._receiveInput;
            set
            {
                this._receiveInput = value;
                ToggleCoroutines();
            }
        }

        [Header("Properties", "Properties for reference.", PropertyVisibilityMode.PlaymodeOnly)]
        [Tooltip("Determines whenever this character should receive input.")]
        [SerializeField]
        [ReadOnly]
        [PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        private InputFlags _receiveInput;

        [Tooltip("Yield instruction, signalizing the coroutine to wait for the next fixed update.")]
        [SerializeReference]
        [ReadOnly]
        [PropertyDrawer(0)]
        [PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        private WaitForFixedUpdate _waitForFixedUpdate;

        [Tooltip("Collider attached to this character's rigidbody.")]
        [SerializeField]
        [ReadOnly]
        [Required]
        [PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        private Collider2D _attachedCollider;

        [Header("Properties", "Editable properties of this Character instance.", PropertyVisibilityMode.EditorOnly)]
        [Tooltip("Value to set to _receiveInput on Start.")]
        [SerializeField]
        [PropertyVisibility(PropertyVisibilityMode.EditorOnly)]
        private InputFlags _receiveInputOnStart;

        [Tooltip("Sprite component of this character.")]
        [SerializeField]
        [ReadOnly(InspectorAttributeUsage.PlaymodeOnly)]
        [Required]
        private SpriteRenderer _sprite;

        [Tooltip("Rigidbody physics component for this character.")]
        [SerializeField]
        [ReadOnly(InspectorAttributeUsage.PlaymodeOnly)]
        [Required]
        private Rigidbody2D _body;

        [Tooltip("Current state of the character")]
        [SerializeField]
        [ReadOnly]
        [PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        private CharacterAnimationState _state;

        [Tooltip("List of all currently available interactable scene objects.")]
        [SerializeField]
        [ReadOnly]
        [PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        private List<Interactable> _availableInteractables;

        [Tooltip("Key codes activating interactions.")]
        [SerializeField]
        [ReadOnly]
        [PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        private List<KeyCode> _keyCodes;

        [Tooltip("Indicates whether to update interaction tips.")]
        [SerializeField]
        [ReadOnly]
        [PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        private bool _updateSigns;

        [Tooltip("Animator of the character")]
        [SerializeField]
        [Required]
        [ReadOnly(InspectorAttributeUsage.PlaymodeOnly)]
        private Animator _animator;

        [Tooltip("Idle animation of the character")]
        [SerializeField]
        [ReadOnly(InspectorAttributeUsage.PlaymodeOnly)]
        private string _idlingState;

        [Tooltip("Looking up animation")]
        [SerializeField]
        [ReadOnly(InspectorAttributeUsage.PlaymodeOnly)]
        private string _lookingUpState;

        [Tooltip("Crouching animation of the character")]
        [SerializeField]
        [ReadOnly(InspectorAttributeUsage.PlaymodeOnly)]
        private string _crouchingState;

        [Tooltip("Walking animation of the character")]
        [SerializeField]
        [ReadOnly(InspectorAttributeUsage.PlaymodeOnly)]
        private string _walkingState;

        [Tooltip("Force used to launch this character on jump")]
        [SerializeField]
        private float _movementVelocity;

        [Tooltip("Coefficient applied to the body's velocity when no input is provided")]
        [SerializeField]
        private float _coeff;

        [Tooltip("List of all currently available interactable scene objects.")]
        [SerializeField]
        [PropertyVisibility(PropertyVisibilityMode.EditorOnly)]
        private List<Pair<Collider2D, Interactable[]>> _interactables;

        [SerializeField]
        private ContactFilter2D _contactFilters;

        [Tooltip("Key signs label.")]
        [SerializeField]
        [Required]
        private TMP_Text _keys;

        private Dictionary<int, int> _typeIds;
        private Dictionary<Collider2D, Interactable[]> _interactablesDictionary;

        public void SetInput(int inputFlags)
        {
            this.ReceiveInput = (InputFlags)(inputFlags);
        }

        private void Awake()
        {
            if (_body.IsUnityNotNull())
            {
                Collider2D[] results = new Collider2D[1];
                this._body.GetAttachedColliders(results);
                this._attachedCollider = results[0];
            }
        }

        private void Start()
        {
            this._availableInteractables = new List<Interactable>();
            this._keyCodes = new List<KeyCode>();
            this._interactablesDictionary = new (this._interactables.ConvertAll(t => KeyValuePair.Create(t.Value1, t.Value2)));

            this._waitForFixedUpdate = new WaitForFixedUpdate();
            this.ReceiveInput = this._receiveInputOnStart;
        }

        private IEnumerator UpdatePhysics()
        {
            while (true)
            {
                yield return this._waitForFixedUpdate;

                if (this.ReceiveInput.HasFlag(InputFlags.Physics))
                {
                    float vertical = Input.GetAxis("Vertical");
                    float horizontal = Input.GetAxis("Horizontal");
                    Vector2 velocity = this._body.velocity;

                    if (vertical != 0)
                    {
                        bool isLookingUp = vertical > 0;
                        SetState(
                            isLookingUp ? CharacterAnimationState.LookingUp : CharacterAnimationState.Crouching,
                            isLookingUp ? _lookingUpState : _crouchingState
                        );

                        this._body.velocity = new Vector2(0, velocity.y);
                    }
                    else if (horizontal != 0)
                    {
                        SetState(CharacterAnimationState.Walking, _walkingState);
                        this._body.velocity = new Vector2(horizontal * this._movementVelocity, velocity.y);
                        this._sprite.flipX = horizontal < 0;
                    }
                    else
                    {
                        SetState(CharacterAnimationState.Idle, _idlingState);
                        this._body.velocity = new Vector2(velocity.x * this._coeff, velocity.y);
                    }
                }
            }
        }

        private void ToggleCoroutines()
        {
            if (this._receiveInput.HasFlag(InputFlags.Physics))
            {
                StartCoroutine(UpdatePhysics());
            }
            else
            {
                StopCoroutine(UpdatePhysics());
            }

            if (this._receiveInput.HasFlag(InputFlags.Interactivity))
            {
                StartCoroutine(UpdateInteractivity());
            }
            else
            {
                StopCoroutine(UpdateInteractivity());
            }
        }

        private void SetState(CharacterAnimationState value, string state)
        {
            if (_state != value)
            {
                _state = value;
                this._animator.SetBool(_idlingState, false);
                this._animator.SetBool(_lookingUpState, false);
                this._animator.SetBool(_crouchingState, false);
                this._animator.SetBool(_walkingState, false);
                this._animator.SetBool(state, true);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) => OnTrigger(collision);
        private void OnTriggerExit2D(Collider2D collision) => OnTrigger(collision);
        private void OnTrigger(Collider2D collision)
        {
            if (this._interactablesDictionary.TryGetValue(collision, out Interactable[] interactables))
            {
                bool isInRange = this._attachedCollider.IsTouching(collision);
                if (isInRange)
                {
                    this._availableInteractables.AddRange(interactables);
                }
                else
                {
                    foreach (Interactable interactable in interactables)
                    {
                        this._availableInteractables.Remove(interactable);
                    }
                }

                _updateSigns = true;
            }
        }

        private IEnumerator UpdateInteractivity()
        {
            while (true)
            {
                yield return this._waitForFixedUpdate;

                if (this._receiveInput.HasFlag(InputFlags.Interactivity))
                {
                    this._typeIds = new Dictionary<int, int>(this._typeIds?.Count ?? 0);
                    for (int i = 0; i < this._availableInteractables.Count; i++)
                    {
                        Interactable interactable = this._availableInteractables[i];
                        this._typeIds.TryGetValue(interactable.TypeId, out int count);

                        if (interactable.CanInteract && (count < interactable.MaxActivations || interactable.MaxActivations == -1))
                        {
                            interactable.Interact(this);
                            this._typeIds[interactable.TypeId] = ++count;
                            this._updateSigns = true;
                        }
                    }
                }

                if (this._updateSigns)
                {
                    this._updateSigns = false;
                    StringBuilder text = new StringBuilder();
                    List<KeyCode> includedKeys = new List<KeyCode>();

                    int j = 0;
                    for (int i = 0; i < this._availableInteractables.Count; i++)
                    {
                        KeyCode key = this._availableInteractables[i] switch
                        {
                            InputBasedInteractable ibi => ibi.State switch
                            {
                                InteractionState.None => ibi.ActivationKey,
                                InteractionState.Activated => ibi.CancelationKey,
                                _ => KeyCode.None
                            },

                            _ => KeyCode.None
                        };

                        if (key != KeyCode.None && !includedKeys.Contains(key))
                        {
                            if (j > 0)
                            {
                                text.Append(", ");
                            }

                            includedKeys.Add(key);
                            text.Append(key);
                            j++;
                        }
                    }

                    this._keys.text = text.ToString();
                }
            }
        }
    }
}