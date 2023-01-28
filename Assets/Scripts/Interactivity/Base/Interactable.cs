
using UnityEngine;

namespace SonicEduquest
{
    public abstract class Interactable : MonoBehaviour
    {
        public bool CanInteract { get => this._canInteract; set => this._canInteract = value; }
        public int TypeId { get => this._typeId; set => this._typeId = value; }
        public int MaxActivations { get => this._maxActivations; set => this._maxActivations = value; }

        [Header("Interactable Properties", "Editable properties of this Interactable.", PropertyVisibilityMode.EditorAndPlaymode)]
        [Tooltip("Determines whether the character can interact with this entity.")]
        [SerializeField]
        protected bool _canInteract;

        [Tooltip("Type of interactable.")]
        [SerializeField]
        protected int _typeId;

        [Tooltip(
            "Max number of times the character can interact " +
            "with this type of entities in one frame.")
        ]
        [SerializeField]
        protected int _maxActivations;

        public abstract void Interact(Character triggerSource);
    }
}
