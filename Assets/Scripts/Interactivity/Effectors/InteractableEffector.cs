
using System;
using UnityEngine;

namespace FunkyQuest
{
    internal abstract class InteractableEffector : MonoBehaviour
    {
        [field: Header("Interactable - Read Only")]
        [field: SerializeField][field: ReadOnly]    public bool IsActivated { get; protected set; }

        public abstract event Action<InteractableEffector> Finished;

        public abstract void PerformEffect();
    }
}