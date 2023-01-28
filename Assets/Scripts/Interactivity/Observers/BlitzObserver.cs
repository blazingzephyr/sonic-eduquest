
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SonicEduquest
{
    public class BlitzObserver : ConditionObserver
    {
        [Header("Properties", "Properties for reference.", PropertyVisibilityMode.PlaymodeOnly)]
        [Tooltip("Index of the current question.")]
        [SerializeField]
        [ReadOnly]
        [PropertyVisibility(PropertyVisibilityMode.PlaymodeOnly)]
        private int _questionIndex;

        [field: Header("Properties", "Editable properties of this TagObserver.", PropertyVisibilityMode.EditorAndPlaymode)]
        [field: Tooltip("Index of the current question.")]
        [field: SerializeField]
        public bool Active { get; set; }

        [SerializeField]
        [Tooltip("Blitz option buttons.")]
        private Button[] _options;

        [SerializeField]
        [Tooltip("Text labels.")]
        private TMP_Text[] _labels;

        [SerializeField]
        [Tooltip("Blitz timer.")]
        private TMP_Text _timer;

        [SerializeField]
        [Tooltip("Timer formatting options.")]
        private string _timerFormatting;

        [SerializeField]
        [Tooltip("Total count of the questions.")]
        private int _count;

        [SerializeField]
        [Tooltip("Blitz questions.")]
        private string[] _text;

        [SerializeField]
        [Tooltip("Indices of the correct answers.")]
        private int[] _answers;

        [SerializeField]
        [Tooltip("Time to answer the questions.")]
        private float _time;

        [SerializeField]
        [Tooltip("Effectors, activated if blitz fails.")]
        private InteractableEffector[] _failed;

        private TimeSpan _fixedDeltaTime;
        private TimeSpan _currentTime;

        public void Reset()
        {
            this._questionIndex = 0;
            this._currentTime = TimeSpan.FromSeconds(this._time);
            this._timer.text = this._currentTime.ToString(this._timerFormatting);
            UpdateText();
        }

        protected override void Start()
        {
            base.Start();
            for (int i = 0; i < this._options.Length; i++)
            {
                Button option = this._options[i];
                option.onClick.AddListener(
                    () =>
                    {
                        if (Active)
                        {
                            if (option == this._options[this._answers[this._questionIndex]])
                            {
                                if (++this._questionIndex == this._count)
                                {
                                    this._isFulfilled = true;
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

            this._fixedDeltaTime = TimeSpan.FromSeconds(Time.fixedDeltaTime);
            UpdateText();
        }

        private void FixedUpdate()
        {
            if (!this.IsFulfilled && this.Active)
            {
                this._currentTime -= this._fixedDeltaTime;

                if (this._timer != null)
                {
                    this._timer.text = this._currentTime.ToString(this._timerFormatting);
                }

                if (this._currentTime.TotalMilliseconds <= 0)
                {
                    OnFailed();
                }
            }
        }

        private void UpdateText()
        {
            for (int i = 0; i < this._labels.Length; i++)
            {
                this._labels[i].text = this._text[this._questionIndex * this._labels.Length + i];
            }

            this._currentTime = TimeSpan.FromSeconds(this._time);
        }

        private void OnFailed()
        {
            for (int i = 0; i < this._failed.Length; i++)
            {
                this._failed[i].PerformEffect();
            }
        }
    }
}