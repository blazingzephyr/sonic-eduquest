
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace SonicEduquest
{
    internal class DirectorEffector : InteractableEffector
    {
        public override event Action<InteractableEffector> Finished;

        [Header("Properties", "Editable properties of this DirectorEffector.", PropertyVisibilityMode.EditorAndPlaymode)]
        [Tooltip("Director played when this effector is activated.")]
        [SerializeField]
        private PlayableDirector _director;

        public override void PerformEffect()
        {
            this.IsActivated = true;
            this._director.stopped += OnDirectorStopped;
            this._director.Play();
        }

        private void OnDirectorStopped(PlayableDirector director)
        {
            this.IsActivated = false;
            this.Finished?.Invoke(this);
            director.stopped -= OnDirectorStopped;
        }
    }
}