
using UnityEngine;

namespace FunkyQuest
{
    internal class TransformFollower : MonoBehaviour
    {
        [Header("Transform Follow - Read Only")]
        [SerializeField][ReadOnly]  private Vector3     _lastPosition;

        [Header("Transform Follow - Properties")]
        [SerializeField]            private Transform   _follow;
        [SerializeField]            private Transform   _transform;
        [SerializeField]            private Vector3     _offset;

        private void Update()
        {
            if (_follow.localPosition != _lastPosition)
            {
                _transform.position = _follow.localPosition + _offset;
            }
        }
    }
}