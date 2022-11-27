
using UnityEngine;

namespace FunkyQuest
{
    internal abstract class ConditionObserver : MonoBehaviour
    {
        [field: Header("Text Input Observer - Read Only")]
        [field: SerializeField][field: ReadOnly]  public bool IsFulfilled { get; protected set; }

        public virtual bool IsConditionFulfilled(ContinuousInteractable source, CharacterInteractivity triggerSource) => IsFulfilled;
    }
}