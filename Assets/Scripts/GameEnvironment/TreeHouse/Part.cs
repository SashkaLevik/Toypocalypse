using Assets.Scripts.Data.StaticData;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class Part : MonoBehaviour
    {
        [SerializeField] private PartData _partData;
        [SerializeField] private bool _isActivated;

        private Button _chooseButton;

        public UnityAction<Part> PartChoosed;

        public PartData PartData => _partData;

        private void Awake()
        {
            _chooseButton = GetComponent<Button>();
        }
        private void Start()
        {
        }

        private void OnEnable()
        {
            _chooseButton.onClick.AddListener(ChoosePart);
        }

        private void OnDisable()
        {
            _chooseButton.onClick.RemoveListener(ChoosePart);
        }

        public bool IsAvalable()
        {
            return _isActivated;
        }

        public void SetEnableColor()
            => _chooseButton.image.color = Color.green;

        public void SetDisableColor()
            => _chooseButton.image.color = Color.white;

        private void ChoosePart()
        {            
            PartChoosed?.Invoke(this);
        }
    }
}
