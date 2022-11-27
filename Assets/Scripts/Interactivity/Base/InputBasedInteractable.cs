
using UnityEngine;

namespace FunkyQuest
{
    internal class InputBasedInteractable : Interactable
    {
        public enum InteractionState
        {
            None        = 0,
            Activating  = 1,
            Activated   = 2,
            Canceling   = 3
        }

        [field: Header("Input Based Interactable - Read Only")]
        [field: SerializeField][field: ReadOnly]    public InteractionState         State { get; private set; }
        [SerializeField][ReadOnly]                  private int                     _effectorActivationsCount;

        [Header("Input Based Interactable - Properties")]
        [SerializeField]                            private int                     _maxEffectorActivationsCount;
        [SerializeField]                            private InteractableEffector[]  _activating;
        [SerializeField]                            private InteractableEffector[]  _canceling;
        [field: SerializeField]                     public KeyCode                  ActivationKey { get; private set; }
        [field: SerializeField]                     public KeyCode                  CancelationKey { get; private set; }

        public void Reset()
        {
            State = InteractionState.None;
            _effectorActivationsCount = 0;
        }

        public override void Interact(CharacterInteractivity triggerSource)
        {
            InteractionState current = State;
            UpdateState(
                InteractionState.None,
                InteractionState.Activating,
                InteractionState.Activated,
                ActivationKey,
                _activating
            );

            if (current == State)
            {
                UpdateState(
                    InteractionState.Activated,
                    InteractionState.Canceling,
                    InteractionState.None,
                    CancelationKey,
                    _canceling
                );
            }
        }

        private void UpdateState(InteractionState interact, InteractionState next, InteractionState finished, in KeyCode keyCode, InteractableEffector[] effectors)
        {
            if (State == interact)
            {
                bool areEffectorsActivated = false;
                for (int i = 0; i < effectors.Length && !areEffectorsActivated; i++)
                {
                    areEffectorsActivated = effectors[0].IsActivated;
                }

                bool canInteract =
                    !areEffectorsActivated &&
                    (_effectorActivationsCount < _maxEffectorActivationsCount || _maxEffectorActivationsCount == -1) &&
                    keyCode != KeyCode.None &&
                    Input.GetKeyDown(keyCode);

                if (canInteract)
                {
                    State = next;
                    _effectorActivationsCount++;

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
            State = state;
            effector.Finished -= (InteractableEffector effector) => OnEffectorFinished(state, effector);
        }
    }
}