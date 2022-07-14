using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Graphic _background = null;
        [SerializeField] private Image _foreground = null;
        [SerializeField] private float _speedMultiplier = 5;

        private float _targetValue = 0;

        private void Update()
        {
            _foreground.fillAmount = Mathf.Lerp(_foreground.fillAmount, _targetValue, Time.deltaTime * _speedMultiplier);
        }

        public void Show()
        {
            _background.gameObject.SetActive(true);
            _foreground.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _background.gameObject.SetActive(false);
            _foreground.gameObject.SetActive(false);
        }

        public void SetValue(float value)
        {
            _targetValue = value;
        }

        public void SetValueInstantly(float value)
        {
            SetValue(value);
            
            _foreground.fillAmount = value;
        }
    }
