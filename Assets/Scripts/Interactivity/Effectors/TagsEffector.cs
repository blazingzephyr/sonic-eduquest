
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FunkyQuest
{
    internal class TagsEffector : InteractableEffector
    {
        public override event Action<InteractableEffector> Finished;

        [Header("Tags Effector - Properties")]
        [SerializeField]    private TaggedObject    _object;
        [SerializeField]    private string[]        _tagsToAdd;
        [SerializeField]    private Pair<int, int>  _tagsToRemove;

        public override void PerformEffect()
        {
            IsActivated = true;

            List<string> tags = new (_object.Tags);
            tags.AddRange(_tagsToAdd);
            tags.RemoveRange(_tagsToRemove.Value1, _tagsToRemove.Value2);
            _object.Tags = tags.ToArray();

            IsActivated = false;
            Finished?.Invoke(this);
        }
    }
}