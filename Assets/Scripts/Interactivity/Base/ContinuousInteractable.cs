
using UnityEngine;

namespace SonicEduquest
{
    public class ContinuousInteractable : Interactable
    {
        [Header("Properties", "Properties for reference.", PropertyVisibilityMode.PlaymodeOnly)]
        [Tooltip("Is interaction finished")]
        [SerializeField]
        [ReadOnly]
        [PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        private bool _isInteractionFinished;

        [Header("Properties", "Editable properties of this ContinuousInteractable.", PropertyVisibilityMode.EditorAndPlaymode)]
        [Tooltip("Provides continuous checks before activating the interaction")]
        [SerializeField]
        [Required]
        private ConditionObserver _conditionObserver;

        [Tooltip("Effects applied when the interaction is activated")]
        [SerializeField]
        private InteractableEffector[] _finished;

        public override void Interact(Character triggerSource)
        {
            if (!this._isInteractionFinished &&
                this._conditionObserver.IsFulfilled
            )
            {
                this._isInteractionFinished = true;

                for (int i = 0; i < this._finished.Length; i++)
                {
                    this._finished[i].PerformEffect();
                }
            }
        }

        private void Start()
        {
            this._isInteractionFinished = false;
        }
    }
}