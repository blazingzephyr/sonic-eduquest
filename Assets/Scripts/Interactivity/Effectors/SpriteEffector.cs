
using System;
using UnityEngine;

namespace FunkyQuest
{
    internal class SpriteEffector : InteractableEffector
    {
        public override event Action<InteractableEffector> Finished;

        [Header("Sprite Effector - Properties")]
        [SerializeField]    private SpriteRenderer  _spriteRenderer;
        [SerializeField]    private int             _sortingOrder;

        public override void PerformEffect()
        {
            IsActivated = true;
            _spriteRenderer.sortingOrder = _sortingOrder;
            IsActivated = false;
            Finished?.Invoke(this);
        }
    }
}