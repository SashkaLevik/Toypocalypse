﻿using Assets.Scripts.Data.StaticData;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class Material : MonoBehaviour
    {
        [SerializeField] private MaterialData _materialData;

        private Button _materialButton;

        public MaterialData Data => _materialData;

        public event UnityAction<Material> MaterialChoosed;

        private void Awake()
        {
            _materialButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _materialButton.onClick.AddListener(ChooseMaterial);
        }

        private void ChooseMaterial()
        {
            MaterialChoosed?.Invoke(this);
        }
    }
}
