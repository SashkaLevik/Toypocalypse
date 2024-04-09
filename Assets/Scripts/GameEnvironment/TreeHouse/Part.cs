using Assets.Scripts.Data.StaticData;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class Part : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private PartData _partData;

        private Button _chooseButton;

        public UnityAction<Part> PartChoosed;
        public UnityAction DoubleClicked;

        public PartData PartData => _partData;

        private void Awake()
        {
            _chooseButton = GetComponent<Button>();
        }                      

        public void SetEnableColor()
            => _chooseButton.image.color = Color.green;

        public void SetDisableColor()
            => _chooseButton.image.color = Color.white;        

        public void DisableOnTutorial()
            => GetComponent<Image>().raycastTarget = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            int clickCount = eventData.clickCount;

            if (clickCount == 1)
                OnSingleClick();
            else if (clickCount == 2)
                OnDoubleClick();
        }

        private void OnSingleClick()
        {
            PartChoosed?.Invoke(this);
        }

        private void OnDoubleClick()
        {
            DoubleClicked?.Invoke();
        }       
    }
}
