
using System;
using UnityEngine;
using UnityEngine.Events;

namespace FunkyQuest
{
    internal class EventEffector : InteractableEffector
    {
        public override event Action<InteractableEffector> Finished;

        [Header("Event Effector - Properties")]
        [SerializeField]    private UnityEvent  _event;
        public override void PerformEffect()
        {
            IsActivated = true;
            _event?.Invoke();
            IsActivated = false;
            Finished?.Invoke(this);
        }
    }
}