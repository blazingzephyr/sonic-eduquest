
using UnityEngine;

namespace FunkyQuest
{
    internal class ForceApplier : MonoBehaviour
    {
        [Header("Force Applier - Properties")]
        [SerializeField]    private Rigidbody2D _rigidbody;
        [SerializeField]    private float       _x;
        [SerializeField]    private float       _y;

        public void ApplyForce()
        {
            _rigidbody.AddForce(new Vector2(_x, _y), ForceMode2D.Force);
        }
    }
}