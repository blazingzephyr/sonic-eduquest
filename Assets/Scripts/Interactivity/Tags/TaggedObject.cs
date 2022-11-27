
using UnityEngine;

namespace FunkyQuest
{
    internal class TaggedObject : MonoBehaviour
    {
        [field: Header("Tag - Properties")]
        [field: SerializeField] public  string[] Tags { get; set; }
    }
}