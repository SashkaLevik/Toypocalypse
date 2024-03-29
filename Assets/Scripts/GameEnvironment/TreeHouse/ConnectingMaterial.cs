﻿using Assets.Scripts.Data.StaticData;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class ConnectingMaterial : MonoBehaviour
    {
        [SerializeField] private MaterialData _materialData;
        [SerializeField] private AudioSource _chooseSound;

        private Button _materialButton;

        public Button MaterialButton => _materialButton;

        public MaterialData Data => _materialData;

        public event UnityAction<ConnectingMaterial> MaterialChoosed;

        private void Awake()
        {
            _materialButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _materialButton.onClick.AddListener(ChooseMaterial);
        }
        private void OnDestroy()
        {
            _materialButton.onClick.RemoveListener(ChooseMaterial);
        }

        private void ChooseMaterial()
        {
            _chooseSound.Play();
            MaterialChoosed?.Invoke(this);
        }
    }
}
