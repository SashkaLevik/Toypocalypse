using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class SkillPanel : MonoBehaviour
    {
        [SerializeField] private SkillView _skillView;

        public Toy _toy;
        public SkillView _currentSkill;

        private void Awake()
        {
            _toy = GetComponentInParent<Toy>();
        }

        private void Start()
        {
            FillSkillPanel();
        }

        private void FillSkillPanel()
        {            
            for (int i = 0; i < _toy.Parts.Count; i++)
            {
                _skillView.Init(_toy.Parts[i].SkillData);
                _currentSkill = Instantiate(_skillView, transform);
                _currentSkill.SkillPlayed += PlaySkill;
            }
        }                

        private void PlaySkill()
        {
            Debug.Log("Skill");
        }
    }
}
