
using UnityEngine;

namespace SonicEduquest
{
    public abstract class ConditionObserver : MonoBehaviour
    {
        public virtual bool IsFulfilled => this._isFulfilled;

        [Header("Properties", "Properties for reference.", PropertyVisibilityMode.PlaymodeOnly)]
        [Tooltip("Is condition fulfilled.")]
        [SerializeField]
        [ReadOnly]
        [PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        protected bool _isFulfilled;

        protected virtual void Start()
        {
            this._isFulfilled = false;
        }
    }
}