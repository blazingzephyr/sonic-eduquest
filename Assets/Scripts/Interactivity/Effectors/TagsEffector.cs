
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SonicEduquest
{
    public class TagsEffector : InteractableEffector
    {
        public override event Action<InteractableEffector> Finished;

        [Header("Properties", "Editable properties of this EventEffector.", PropertyVisibilityMode.EditorAndPlaymode)]
        [Tooltip("Tagged entity to edit.")]
        [SerializeField]
        private TaggedEntity _object;

        [Tooltip("List of tags to add.")]
        [SerializeField]
        private string[] _tagsToAdd;

        [Tooltip("Range of tags to remove.")]
        [SerializeField]
        private Pair<int, int> _tagsToRemove;

        public override void PerformEffect()
        {
            this.IsActivated = true;

            List<string> tags = new (this._object.Tags);
            tags.AddRange(this._tagsToAdd);
            tags.RemoveRange(this._tagsToRemove.Value1, this._tagsToRemove.Value2);
            this._object.Tags = tags.ToArray();

            this.IsActivated = false;
            this.Finished?.Invoke(this);
        }
    }
}