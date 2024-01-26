using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.TreeHouse;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PartDescription : MonoBehaviour
    {
        [SerializeField] private TMP_Text _partName;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _material;
        [SerializeField] private SkillView _skillView;
        [SerializeField] private TMP_Text _skillDescription;

        private PartData _choosedPartData;
        public PartData ChoosedPart => _choosedPartData;

        public void SetValues(PartData partData)
        {
            _choosedPartData = partData;
            _partName.text = partData.Name;
            _health.text = partData.Health.ToString();
            _speed.text = partData.Speed.ToString();
            _material.text = partData.MaterialAmount.ToString();
            _skillDescription.text = partData.SkillData.SkillDescription;
            _skillView.Init(partData.SkillData);            
        }
    }
}
