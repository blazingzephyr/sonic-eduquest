
using UnityEngine;

namespace FunkyQuest
{
    internal abstract class Interactable : MonoBehaviour
    {
        [field: Header("Interactable - Properties")]
        [field: SerializeField] public bool CanInteract { get; set; }
        [field: SerializeField] public int  InteractableTypeId { get; protected set; }
        [field: SerializeField] public int  MaxActivations { get; protected set; }

        public abstract void Interact(CharacterInteractivity triggerSource);
    }
}