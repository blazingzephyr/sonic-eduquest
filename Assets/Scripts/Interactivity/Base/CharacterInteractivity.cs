
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static FunkyQuest.InputBasedInteractable;

namespace FunkyQuest
{
    [RequireComponent(typeof(Collider2D))]
    internal class CharacterInteractivity : MonoBehaviour
    {
        [Header("Character Interactivity - Read Only")]
        [SerializeField][ReadOnly]  private List<Interactable>      _interactables;
                                    private Dictionary<int, int>    _typeIds;

        [Header("Character Interactivity - Properties")]
        [SerializeField]            private LayerMask           _interactableLayer;
        [SerializeField]            private GameObject[]        _signs;
        [SerializeField]            private TMP_Text[]          _keys;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Utility.OnTriggerEvent(collision, _interactableLayer, _interactables);
            UpdateSigns();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Utility.OnTriggerEvent(collision, _interactableLayer, _interactables);
            UpdateSigns();
        }

        private void Update()
        {
            _typeIds = new Dictionary<int, int>(_typeIds?.Count ?? 0);
            for (int i = 0; i < _interactables.Count; i++)
            {
                Interactable interactable = _interactables[i];
                _typeIds.TryGetValue(interactable.InteractableTypeId, out int count);

                if (interactable.CanInteract && (count < interactable.MaxActivations || interactable.MaxActivations == -1))
                {
                    interactable.Interact(this);
                    _typeIds[interactable.InteractableTypeId] = ++count;

                    UpdateSigns();
                }
            }
        }

        private void UpdateSigns()
        {
            List<InputBasedInteractable> inputBasedInteractables = new List<InputBasedInteractable>();
            for (int i = 0; i < _interactables.Count; i++)
            {
                if (_interactables[i] is InputBasedInteractable ibi)
                {
                    inputBasedInteractables.Add(ibi);
                }
            }

            for (int i = 0; i < _signs.Length; i++)
            {
                if (_keys.Length > i && inputBasedInteractables.Count > i)
                {
                    InputBasedInteractable interactable = inputBasedInteractables[i];
                    var info = interactable.State switch
                    {
                        InteractionState.None       when interactable.ActivationKey  != KeyCode.None    => new { Key = interactable.ActivationKey.ToString(),   IsSignActive = true },
                        InteractionState.Activated  when interactable.CancelationKey != KeyCode.None    => new { Key = interactable.CancelationKey.ToString(),  IsSignActive = true },
                        _                                                                               => new { Key = string.Empty,                            IsSignActive = false }
                    };

                    _keys[i].text = info.Key;

                    GameObject sign = _signs[i];
                    sign.SetActive(info.IsSignActive);
                }
                else
                {
                    _signs[i].SetActive(false);
                }
            }
        }
    }
}