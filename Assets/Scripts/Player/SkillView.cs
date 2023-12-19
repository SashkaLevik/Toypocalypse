using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.Data.StaticData;
using UnityEngine.Events;
using System;

namespace Assets.Scripts.Player
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private Image _skillIcon;       
        [SerializeField] private Image _full;
        [SerializeField] private Image _empty;
        [SerializeField] private Image[] _fullImages;
        [SerializeField] private Button _playSkill;

        public float _cooldown;
        public float _damage;

        public event UnityAction SkillPlayed;

        private void OnEnable()
        {
            _playSkill.onClick.AddListener(OnSkillPlay);
        }        

        public void Init(SkillData skillData)
        {
            _damage = skillData.Damage;
            _cooldown = skillData.Cooldown;
            _skillIcon.sprite = skillData.Icon;
            _damageText.text = _damage.ToString();

            UpdateCooldown();
        } 

        private void UpdateCooldown()
        {
            for (int i = 0; i < _cooldown; i++)
                _fullImages[i].gameObject.SetActive(true);
        }

        private void OnSkillPlay()
        {
            SkillPlayed?.Invoke();
            //Debug.Log("Skill");
        }
    }
}
