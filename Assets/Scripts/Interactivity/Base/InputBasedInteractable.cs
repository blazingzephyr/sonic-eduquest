
using UnityEngine;

namespace SonicEduquest
{
    public class InputBasedInteractable : Interactable
    {
        public enum InteractionState
        {
            None       = 0,
            Activating = 1,
            Activated  = 2,
            Canceling  = 3
        }

        [field: Header("Properties", "Properties for reference.", PropertyVisibilityMode.PlaymodeOnly)]
        [field: Tooltip("Current interaction state")]
        [field: SerializeField]
        [field: ReadOnly]
        [field: PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        public InteractionState State { get; private set; }

        [Tooltip("Number of times this effector has been actived")]
        [SerializeField]
        [ReadOnly]
        [PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        private int _effectorActivationsCount;

        [Header("Properties", "Editable properties of this InputBasedInteractable.", PropertyVisibilityMode.EditorAndPlaymode)]
        [SerializeField]
        [Tooltip("Effects applied when the interaction is activated")]
        private InteractableEffector[] _activating;

        [SerializeField]
        [Tooltip("Effects applied when the interaction is canceled")]
        private InteractableEffector[] _canceling;

        [field: Tooltip("Key used to activate the interaction")]
        [field: SerializeField]
        public KeyCode ActivationKey { get; private set; }

        [field: Tooltip("Key used to cancel the interaction")]
        [field: SerializeField]
        public KeyCode CancelationKey { get; private set; }

        [Tooltip(
            "Max number of times the character can interact" +
            "with this input based entity in one frame.")
        ]
        [SerializeField]
        private int _maxEffectorActivationsCount;

        public void Reset()
        {
            this.State = InteractionState.None;
            this._effectorActivationsCount = 0;
        }

        public override void Interact(Character triggerSource)
        {
            InteractionState current = this.State;
            UpdateState(
                InteractionState.None,
                InteractionState.Activating,
                InteractionState.Activated,
                this.ActivationKey,
                this._activating
            );

            if (current == this.State)
            {
                UpdateState(
                    InteractionState.Activated,
                    InteractionState.Canceling,
                    InteractionState.None,
                    this.CancelationKey,
                    this._canceling
                );
            }
        }

        private void UpdateState(InteractionState interact, InteractionState next, InteractionState finished, in KeyCode keyCode, InteractableEffector[] effectors)
        {
            if (this.State == interact)
            {
                bool areEffectorsActivated = false;
                for (int i = 0; i < effectors.Length && !areEffectorsActivated; i++)
                {
                    areEffectorsActivated = effectors[0].IsActivated;
                }

                bool canInteract =
                    !areEffectorsActivated &&
                    (this._effectorActivationsCount < this._maxEffectorActivationsCount || this._maxEffectorActivationsCount == -1) &&
                    keyCode != KeyCode.None &&
                    Input.GetKey(keyCode);

                if (canInteract)
                {
                    this.State = next;
                    this._effectorActivationsCount++;

                    for (int i = 0; i < effectors.Length; i++)
                    {
                        InteractableEffector effector = effectors[i];
                        effector.Finished += (InteractableEffector e) => OnEffectorFinished(finished, e);
                        effector.PerformEffect();
                    }
                }
            }
        }

        private void OnEffectorFinished(InteractionState state, InteractableEffector effector)
        {
            this.State = state;
            effector.Finished -= (InteractableEffector effector) => OnEffectorFinished(state, effector);
        }

        private void Start()
        {
            Reset();
        }
    }
}