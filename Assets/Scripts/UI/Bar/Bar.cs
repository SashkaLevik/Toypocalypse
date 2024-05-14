using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Assets.Scripts.UI.Bar
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] protected Image _fillImage;
        [SerializeField] protected TMP_Text _amount;
        [SerializeField] protected Slider _slider;
        [SerializeField] protected float _hgealthChangeSpeed;

        protected Coroutine _currentCoroutine;

        public void SetValue(float current, float max)
        {
            _slider.maxValue = max;
            _slider.value = current;
            _amount.text = current.ToString();
        }

        private IEnumerator UpdateBar(float target)
        {
            while (_slider.value != target)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, target, _hgealthChangeSpeed * Time.deltaTime);
                yield return null;
            }
        }

        public void OnValueChange(float value)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _amount.text = value.ToString();
            _currentCoroutine = StartCoroutine(UpdateBar(value));
        }
    }
}
