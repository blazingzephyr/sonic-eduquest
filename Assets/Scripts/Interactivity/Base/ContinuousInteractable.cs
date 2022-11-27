
using UnityEngine;

namespace FunkyQuest
{
    internal class ContinuousInteractable : Interactable
    {
        [Header("Continuous Interactable - Read Only")]
        [SerializeField][ReadOnly]  private bool                    _isInteractionFinished;

        [Header("Continuous Interactable - Properties")]
        [SerializeField]            private ConditionObserver       _conditionObserver;
        [SerializeField]            private InteractableEffector[]  _finished;

        public override void Interact(CharacterInteractivity triggerSource)
        {
            if (_conditionObserver != null && !_isInteractionFinished && _conditionObserver.IsConditionFulfilled(this, triggerSource))
            {
                _isInteractionFinished = true;

                for (int i = 0; i < _finished.Length; i++)
                {
                    _finished[i].PerformEffect();
                }
            }
        }
    }
}