
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SonicEduquest
{
    public class SceneLoader : MonoBehaviour
    {
        [Header("Properties", "Editable properties of this SceneLoader.", PropertyVisibilityMode.EditorAndPlaymode)]
        [Tooltip("Scene to load")]
        [SerializeField]
        private string _sceneName;

        [Tooltip("Allows to specify whether to load the scene additevely")]
        [SerializeField]
        private LoadSceneMode _loadMode;

        public void Load()
        {
            SceneManager.LoadScene(this._sceneName, this._loadMode);
        }
    }
}