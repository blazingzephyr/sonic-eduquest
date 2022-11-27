
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace FunkyQuest
{
    internal class DirectorEffector : InteractableEffector
    {
        public override event Action<InteractableEffector> Finished;

        [Header("Director Effector - Properties")]
        [SerializeField]    private PlayableDirector _director;

        public override void PerformEffect()
        {
            IsActivated = true;
            _director.stopped += OnDirectorStopped;
            _director.Play();
        }

        private void OnDirectorStopped(PlayableDirector director)
        {
            IsActivated = false;
            Finished?.Invoke(this);
            director.stopped -= OnDirectorStopped;
        }
    }
}