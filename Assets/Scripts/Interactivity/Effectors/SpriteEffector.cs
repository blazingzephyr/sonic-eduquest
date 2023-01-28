
using System;
using UnityEngine;

namespace SonicEduquest
{
    public class SpriteEffector : InteractableEffector
    {
        public override event Action<InteractableEffector> Finished;

        [Header("Properties", "Editable properties of this EventEffector.", PropertyVisibilityMode.EditorAndPlaymode)]
        [Tooltip("Sprite renderer.")]
        [Required]
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [Tooltip("Sorting order to set to the sprite renderer.")]
        [SerializeField]
        private int _sortingOrder;

        public override void PerformEffect()
        {
            this.IsActivated = true;
            this._spriteRenderer.sortingOrder = this._sortingOrder;
            this.IsActivated = false;
            this.Finished?.Invoke(this);
        }
    }
}