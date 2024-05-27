using Assets.Scripts.Data;
using Assets.Scripts.SaveLoad;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

namespace Assets.Scripts.UI
{
    public class SisterDialog : MonoBehaviour, ISaveProgress
    {
        private const string IsTalking = "IsTalking";
        [SerializeField] private GameObject _dialogWindow;
        [SerializeField] private GameObject _playerCurtain;
        [SerializeField] private GameObject _sisterCurtain;
        [SerializeField] private MenuTutorial _menuTutorial;
        [SerializeField] private List<TMP_Text> _texts;
        [SerializeField] private float _typingSpeed;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Animator _sisterAnimator;

        private int _textNumber;
        private PlayerProgress _playerProgress;

        private void Start()
        {            
            if (_playerProgress.WorldData.IsNewGame == true)
            {
                _dialogWindow.SetActive(true);
                StartCoroutine(WaitNextText());
                _sisterAnimator.SetBool(IsTalking, true);
            }
        }

        private IEnumerator WaitNextText()
        {
            while (_textNumber != _texts.Count)
            {
                yield return StartCoroutine(DisplayLine(_texts[_textNumber]));
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                ShowNext();
                yield return null;
            }

            _menuTutorial.gameObject.SetActive(true);
            _dialogWindow.SetActive(false);
        }

        private IEnumerator DisplayLine(TMP_Text text)
        {
            text.gameObject.SetActive(true);
            text.maxVisibleCharacters = 0;

            for (int i = 0; i < text.text.Length; i++)
            {
                text.maxVisibleCharacters++;                
                yield return new WaitForSeconds(_typingSpeed);                
            }

            _textNumber++;
        }

        private void ShowNext()
        {
            if (_textNumber > 0)
            {
                _texts[_textNumber-1].gameObject.SetActive(false);
                ChangeCurtain();
            }
        }

        private void ChangeCurtain()
        {
            if (_playerCurtain.activeSelf == true)
            {
                _playerCurtain.SetActive(false);
                _playerAnimator.SetBool(IsTalking, true);
                _sisterCurtain.SetActive(true);
                _sisterAnimator.SetBool(IsTalking, false);
            }
            else
            {
                _playerCurtain.SetActive(true);
                _playerAnimator.SetBool(IsTalking, false);
                _sisterCurtain.SetActive(false);
                _sisterAnimator.SetBool(IsTalking, true);
            }
        }

        public void Load(PlayerProgress progress)
        {
            _playerProgress = progress;
        }

        public void Save(PlayerProgress progress)
        {
        }
    }
}
