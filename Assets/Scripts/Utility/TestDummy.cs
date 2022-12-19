
using UnityEngine;

namespace SonicEduquest
{
    public interface ICovariant
    {
        public  string  Member  { get; set; }
    }

    public class A : ICovariant
    {
        [field: SerializeField]
        public  string  Member  { get; set; }

        [field: SerializeField]
        public  bool    Member2 { get; set; }
    }

    public class B : ICovariant
    {
        [field: SerializeField]
        public  string  Member  { get; set; }

        [field: SerializeField]
        public  int     Member3 { get; set; }
    }

    public class C
    {
        public  int Foo { get; set; }
        public  int Bar { get; set; }
    }

    public class TestDummy : MonoBehaviour
    {
        [Header("Header 1", "Is this supposed to be something funny?")]
        [ReadOnly]
        [SerializeField]
        [Tooltip("String - Read-only.")]
        private string          _readOnlyString;

        [ReadOnly]
        [SerializeReference]
        [PropertyDrawer(0)]
        [Tooltip("C sample.")]
        private C _sample;

        [SerializeField]
        [Tooltip("Sprite Renderer.")]
        private SpriteRenderer  _renderer;

        [SerializeField]
        [Required]
        [Tooltip("Animator - Required.")]
        private Animator        _requiredAnimatorVariable;

        [SerializeField]
        [Required]
        [Tooltip("Animator 2 - Required.")]
        private Animator        _requiredAnimatorVariable2;

        [SerializeReference]
        [Tooltip("Covariant - covariant.")]
        [InspectorCovariant(typeof(A), typeof(B))]
        private ICovariant      _covariantEditor = new A();

        [Header("Header 2", "I have no idea what to write here anymore.")]
        [SerializeField]
        [Tooltip("Just a string.")]
        private string          _fooBar;

        [CanBeNull]
        [SerializeField]
        [Tooltip("String - Can Be Null.")]
        private string          _canBeNull;
    }
}