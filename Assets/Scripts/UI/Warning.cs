using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Assets.Scripts.UI
{
    public class Warning : MonoBehaviour
    {
        [SerializeField] protected Image _warning;        
        [SerializeField] protected AudioSource _warningSound;
        [SerializeField] private TMP_Text _noMaterialChoosed;
        [SerializeField] private TMP_Text _allPartsChoosed;
        [SerializeField] private TMP_Text _identicalType;
        [SerializeField] private TMP_Text _noMoney;
        [SerializeField] private TMP_Text _fullSlots;

        public TMP_Text NoMaterialChoosed => _noMaterialChoosed;
        public TMP_Text AllPartsChoosed => _allPartsChoosed;
        public TMP_Text SamePartChoosed => _identicalType;
        public TMP_Text NoMoney => _noMoney;
        public TMP_Text FullSlots => _fullSlots;

        public void Enable(TMP_Text text)
        {
            _warning.gameObject.SetActive(true);
            _warningSound.Play();
            text.gameObject.SetActive(true);
            StartCoroutine(Disable(text));
        }

        private IEnumerator Disable(TMP_Text text)
        {
            yield return new WaitForSeconds(2f);
            text.gameObject.SetActive(false);
            _warningSound.Stop();
            _warning.gameObject.SetActive(false);
        }
    }
}
