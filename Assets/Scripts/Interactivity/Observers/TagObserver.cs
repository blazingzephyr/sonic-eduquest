
using System.Collections.Generic;
using UnityEngine;

namespace FunkyQuest
{
    internal class TagObserver : ConditionObserver
    {
        [Header("Tag Observer - ReadOnly")]
        [SerializeField][ReadOnly]  private List<TaggedObject>  _objects;

        [Header("Tag Observer - Properties")]
        [SerializeField]            private string[]            _requiredTags;
        [SerializeField]            private string[]            _allowedTags;
        [SerializeField]            private LayerMask           _taggedObjectsLayer;

        public void UpdateObserver()
        {
            bool hasInvalidTags = false;
            bool[] hasRequiredTags = new bool[_requiredTags.Length];

            for (int i = 0; i < _objects.Count && !hasInvalidTags; i++)
            {
                TaggedObject taggedObject = _objects[i];
                string[] tags = taggedObject.Tags;

                for (int j = 0; j < tags.Length; j++)
                {
                    string tag = tags[j];
                    int requiredTag = _requiredTags.Contains(tag);

                    if (requiredTag != -1)
                    {
                        hasRequiredTags[requiredTag] = true;
                    }
                    else
                    {
                        hasInvalidTags = !_allowedTags.Contains(tag);
                    }
                }
            }

            IsFulfilled = !hasInvalidTags && hasRequiredTags.All(p => p);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Utility.OnTriggerEvent(collision, _taggedObjectsLayer, _objects);
            UpdateObserver();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Utility.OnTriggerEvent(collision, _taggedObjectsLayer, _objects);
            UpdateObserver();
        }
    }
}