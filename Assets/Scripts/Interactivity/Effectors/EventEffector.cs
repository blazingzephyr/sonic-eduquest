
using System;
using UnityEngine;
using UnityEngine.Events;

namespace SonicEduquest
{
    public class EventEffector : InteractableEffector
    {
        public override event Action<InteractableEffector> Finished;

        [Header("Properties", "Editable properties of this EventEffector.", PropertyVisibilityMode.EditorAndPlaymode)]
        [Tooltip("Event activated when this effector is activated.")]
        [SerializeField]
        private UnityEvent _event;

        public override void PerformEffect()
        {
            this.IsActivated = true;
            this._event?.Invoke();
            this.IsActivated = false;
            this.Finished?.Invoke(this);
        }
    }
}