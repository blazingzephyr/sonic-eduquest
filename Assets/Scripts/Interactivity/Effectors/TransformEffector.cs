
using System;
using UnityEngine;

namespace FunkyQuest
{
    internal class TransformEffector : InteractableEffector
    {
        public override event Action<InteractableEffector> Finished;

        [Header("Transform Effector - Properties")]
        [SerializeField]    private Transform   _transform;
        [SerializeField]    private Transform   _parentTransform;
        [SerializeField]    private bool        _setX;
        [SerializeField]    private bool        _setY;
        [SerializeField]    private float       _x;
        [SerializeField]    private float       _y;

        public override void PerformEffect()
        {
            IsActivated = true;
            _transform.parent = _parentTransform;

            Vector3 position = _transform.localPosition;
            _transform.localPosition = new Vector2(
                _setX ? _x : position.x,
                _setY ? _y : position.y
            );

            IsActivated = false;
            Finished?.Invoke(this);
        }
    }
}