
using System;
using UnityEngine;

namespace SonicEduquest
{
    public class TransformEffector : InteractableEffector
    {
        public override event Action<InteractableEffector> Finished;

        [Header("Properties", "Editable properties of this TransformEffector.", PropertyVisibilityMode.EditorAndPlaymode)]
        [Tooltip("Transform to edit.")]
        [SerializeField]
        private Transform _transform;

        [Tooltip("Parent to assign to the transform.")]
        [SerializeField]
        private Transform _parentTransform;

        [Tooltip("Indicates whether to set X transform.")]
        [SerializeField]
        private bool _setX;

        [Tooltip("Indicates whether to set Y transform.")]
        [SerializeField]
        private bool _setY;

        [Tooltip("X coordinate to assign.")]
        [SerializeField]
        private float _x;

        [Tooltip("Y coordinate to assign.")]
        [SerializeField]
        private float _y;

        public override void PerformEffect()
        {
            this.IsActivated = true;
            this._transform.parent = this._parentTransform;

            Vector3 position = this._transform.localPosition;
            this._transform.localPosition = new Vector2(
                this._setX ? this._x : position.x,
                this._setY ? this._y : position.y
            );

            this.IsActivated = false;
            this.Finished?.Invoke(this);
        }
    }
}