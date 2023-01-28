
using UnityEngine;

namespace SonicEduquest
{
    public class TaggedEntity : MonoBehaviour
    {
        [field: Header("Properties", "Editable properties of this TaggedEntity.", PropertyVisibilityMode.EditorAndPlaymode)]
        [field: Tooltip("Tags associated with this entity.")]
        [field: SerializeField]
        public string[] Tags { get; set; }
    }
}