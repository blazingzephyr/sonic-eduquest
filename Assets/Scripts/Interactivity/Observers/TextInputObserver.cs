
using TMPro;
using UnityEngine;

namespace SonicEduquest
{
    public class TextInputObserver : ConditionObserver
    {
        [Header("Properties", "Editable properties of this TextInputObserver.", PropertyVisibilityMode.EditorAndPlaymode)]
        [Tooltip("Correct string input.")]
        [SerializeField]
        private string _acceptedString;

        [SerializeField]
        [Tooltip("Input field to observe.")]
        private TMP_InputField _inputField;

        protected override void Start()
        {
            base.Start();
            this._inputField.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(string value)
        {
            this._isFulfilled = value.ToLower() == this._acceptedString.ToLower();
            if (this.IsFulfilled)
            {
                this._inputField.onValueChanged.RemoveListener(OnValueChanged);
            }
        }
    }
}