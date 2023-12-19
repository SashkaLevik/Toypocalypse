using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.TreeHouse;
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
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _skillDamage;
        [SerializeField] private Image _skillImage;

        private PartData _choosedPartData;
        public PartData ChoosedPart => _choosedPartData;

        public void SetValues(PartData partData)
        {
            _partName.text = partData.Name;
            _health.text = partData.Health.ToString();
            _speed.text = partData.Speed.ToString();
            _damage.text = partData.Damage.ToString();
            _skillDamage.text = partData.SkillData.Damage.ToString();
            _skillImage.sprite = partData.SkillData.Icon;
            _choosedPartData = partData;
        }
    }
}
