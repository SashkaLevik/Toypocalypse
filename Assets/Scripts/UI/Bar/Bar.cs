using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI.Bar
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] protected Image _fillImage;
        [SerializeField] protected TMP_Text _amount;
        [SerializeField] protected Slider _slider;
        

        public void SetValue(float current, float max)
        {
            _fillImage.fillAmount = current / max;
            _amount.text = current.ToString();
        }            
    }
}
