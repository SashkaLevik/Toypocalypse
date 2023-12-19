using UnityEngine;
using TMPro;
namespace Assets.Scripts.UI
{
    public class PartSkillDescription : MonoBehaviour
    {
        [SerializeField] private PartDescription _partDescription;
        [SerializeField] private TMP_Text _damageDescription;
        [SerializeField] private TMP_Text _cooldownDescription;
        [SerializeField] private TMP_Text _skillDescription;

        private void OnMouseEnter()
        {
            _damageDescription.gameObject.SetActive(true);
            _cooldownDescription.gameObject.SetActive(true);
            _skillDescription.gameObject.SetActive(true);
            _skillDescription.text = _partDescription.ChoosedPart.SkillData.SkillDescription;
        }

        private void OnMouseExit()
        {
            _damageDescription.gameObject.SetActive(false);
            _cooldownDescription.gameObject.SetActive(false);
            _skillDescription.gameObject.SetActive(false);
        }
    }
}
