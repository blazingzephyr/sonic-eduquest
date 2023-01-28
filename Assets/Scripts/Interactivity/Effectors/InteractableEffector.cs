
using System;
using UnityEngine;

namespace SonicEduquest
{
    public abstract class InteractableEffector : MonoBehaviour
    {
        [field: Header("Properties", "Properties for reference.", PropertyVisibilityMode.PlaymodeOnly)]
        [field: Tooltip("Indicates whether effector is activated.")]
        [field: SerializeField]
        [field: ReadOnly]
        public bool IsActivated { get; protected set; }

        public abstract event Action<InteractableEffector> Finished;

        public abstract void PerformEffect();
    }
}