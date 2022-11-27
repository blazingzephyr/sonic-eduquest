
using TMPro;
using UnityEngine;

namespace FunkyQuest
{
    internal class TextInputObserver : ConditionObserver
    {
        [Header("Text Input Observer - Properties")]
        [SerializeField]            private string          _acceptedString;
        [SerializeField]            private TMP_InputField  _inputField;

        private void Start() => _inputField.onValueChanged.AddListener(OnValueChanged);

        private void OnValueChanged(string value)
        {
            IsFulfilled = value.ToLower() == _acceptedString.ToLower();
            if (IsFulfilled)
            {
                _inputField.onValueChanged.RemoveListener(OnValueChanged);
            }
        }
    }
}