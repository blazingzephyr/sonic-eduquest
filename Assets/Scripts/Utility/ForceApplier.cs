
using UnityEngine;

namespace SonicEduquest
{
    internal class ForceApplier : MonoBehaviour
    {
        [Header("Properties", "Editable properties of this ForceApplier.", PropertyVisibilityMode.EditorAndPlaymode)]
        [Tooltip("Rigidbody to add force to.")]
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        [Tooltip("Force X.")]
        private float _x;

        [SerializeField]
        [Tooltip("Force Y.")]
        private float _y;

        public void ApplyForce()
        {
            this._rigidbody.AddForce(new Vector2(this._x, _y), ForceMode2D.Force);
        }
    }
}