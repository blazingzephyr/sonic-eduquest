
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SonicEduquest
{
    public class TagObserver : ConditionObserver
    {
        public override bool IsFulfilled
        {
            get
            {
                UpdateObservable();
                return base.IsFulfilled;
            }
        }

        [Header("Properties", "Properties for reference.", PropertyVisibilityMode.PlaymodeOnly)]
        [Tooltip("Tagged entities to observe for tags.")]
        [SerializeField]
        [ReadOnly]
        [PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        private List<TaggedEntity> _entities;

        [Header("Properties", "Editable properties of this TagObserver.", PropertyVisibilityMode.EditorAndPlaymode)]
        [Tooltip("Required tags.")]
        [SerializeField]
        private string[] _requiredTags;

        [Tooltip("Tags which are not checked.")]
        [SerializeField]
        private string[] _allowedTags;

        [Tooltip("Layers to collect entities from.")]
        [SerializeField]
        private LayerMask _taggedObjectsLayer;

        private void UpdateObservable()
        {
            bool hasInvalidTags = false;
            bool[] hasRequiredTags = new bool[this._requiredTags.Length];

            for (int i = 0; i < this._entities.Count && !hasInvalidTags; i++)
            {
                TaggedEntity taggedEntity = this._entities[i];
                string[] tags = taggedEntity.Tags;

                for (int j = 0; j < tags.Length; j++)
                {
                    string tag = tags[j];
                    int requiredTag = this._requiredTags.Contains(tag);

                    if (requiredTag != -1)
                    {
                        hasRequiredTags[requiredTag] = true;
                    }
                    else
                    {
                        hasInvalidTags = !this._allowedTags.Contains(tag);
                    }
                }
            }

            this._isFulfilled = !hasInvalidTags && hasRequiredTags.All(p => p);
        }

        private void OnTriggerEnter2D(Collider2D collision) => OnTrigger(collision);
        private void OnTriggerExit2D(Collider2D collision) => OnTrigger(collision);

        private void OnTrigger(Collider2D collision)
        {
            bool touching = collision.IsTouchingLayers(this._taggedObjectsLayer);
            if (touching && collision.TryGetComponent(out TaggedEntity tagged))
            {
                if (!this._entities.Contains(tagged))
                {
                    this._entities.Add(tagged);
                }
                else
                {
                    this._entities.Remove(tagged);
                }

                UpdateObservable();
            }
        }

        protected override void Start()
        {
            base.Start();
            this._entities = new List<TaggedEntity>();
        }
    }
}