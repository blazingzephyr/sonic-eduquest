
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FunkyQuest
{
    internal class BlitzObserver : ConditionObserver
    {
        [Header("Blitz Observer - Read Only")]
        [SerializeField][ReadOnly]  private int                     _questionIndex;
                                    private TimeSpan                _fixedDeltaTime;
                                    private TimeSpan                _currentTime;

        [field: Header("Blitz Observer - Properties")]
        [field: SerializeField]     public bool                     Active { get; set; }
        [SerializeField]            private Button[]                _options;
        [SerializeField]            private TMP_Text[]              _labels;
        [SerializeField]            private TMP_Text                _timer;
        [SerializeField]            private string                  _timerFormatting;
        [SerializeField]            private int                     _count;
        [SerializeField]            private string[]                _text;
        [SerializeField]            private int[]                   _answers;
        [SerializeField]            private float                   _time;
        [SerializeField]            private InteractableEffector[]  _failed;

        public void Reset()
        {
            _questionIndex = 0;
            _currentTime = TimeSpan.FromSeconds(_time);
            _timer.text = _currentTime.ToString(_timerFormatting);
            UpdateText();
        }

        private void Start()
        {
            for (int i = 0; i < _options.Length; i++)
            {
                Button option = _options[i];
                option.onClick.AddListener(
                    () =>
                    {
                        if (Active)
                        {
                            if (option == _options[_answers[_questionIndex]])
                            {
                                if (++_questionIndex == _count)
                                {
                                    IsFulfilled = true;
                                }
                                else
                                {
                                    UpdateText();
                                }
                            }
                            else
                            {
                                OnFailed();
                            }
                        }
                    }
                );
            }

            _fixedDeltaTime = TimeSpan.FromSeconds(Time.fixedDeltaTime);
            UpdateText();
        }

        private void FixedUpdate()
        {
            if (!IsFulfilled && Active)
            {
                _currentTime -= _fixedDeltaTime;

                if (_timer != null)
                {
                    _timer.text = _currentTime.ToString(_timerFormatting);
                }

                if (_currentTime.TotalMilliseconds <= 0)
                {
                    OnFailed();
                }
            }
        }

        private void UpdateText()
        {
            for (int i = 0; i < _labels.Length; i++)
            {
                _labels[i].text = _text[_questionIndex * _labels.Length + i];
            }

            _currentTime = TimeSpan.FromSeconds(_time);
        }

        private void OnFailed()
        {
            for (int i = 0; i < _failed.Length; i++)
            {
                _failed[i].PerformEffect();
            }
        }
    }
}