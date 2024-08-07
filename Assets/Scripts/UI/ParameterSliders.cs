﻿using Assets.Scripts.GameEnvironment.TreeHouse;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ParameterSliders : MonoBehaviour
    {
        [SerializeField] private Table _table;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _speedSlider;
        //[SerializeField] private Slider _damageSlider;
        [SerializeField] private TMP_Text _materialCount;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _speedText;
        //erializeField] private TMP_Text _damageText;        

        private void Start()
        {
            _healthSlider.onValueChanged.AddListener((value) => {
                _healthText.text = value.ToString();
            });
            _speedSlider.onValueChanged.AddListener((value) => {
                _speedText.text = value.ToString();
            });            
        }

        private void OnEnable()
        {
            _table.PartAdded += IncreaseSliders;
            _table.PartRemoved += DicreaseSliders;
            _table.MaterialAdded += IncreaseOnMaterial;
        }

        private void OnDisable()
        {
            _table.PartAdded -= IncreaseSliders;
            _table.PartRemoved -= DicreaseSliders;
            _table.MaterialAdded -= IncreaseOnMaterial;
        }

        private void IncreaseOnMaterial(float health)
        {
            _healthSlider.maxValue = health;
            _healthSlider.value = health;
        }

        private void IncreaseSliders(float health, float speed, float material)
        {            
            _healthSlider.maxValue = health;
            _healthSlider.value = health;
            _speedSlider.maxValue = speed;
            _speedSlider.value = speed;
            _materialCount.text = material.ToString();
        }

        private void DicreaseSliders(float health, float speed, float material)
        {
            _healthSlider.maxValue = health;
            _speedSlider.maxValue = speed;
            _healthSlider.value = health;
            _speedSlider.value = speed;
            _materialCount.text = material.ToString();
        }
    }
}
