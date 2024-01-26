using UnityEngine;
using TMPro;
namespace Assets.Scripts.UI
{
    public class PartSkillDescription : MonoBehaviour
    {
        [SerializeField] private PartDescription _partDescription;
        [SerializeField] private TMP_Text _cooldownDescription;
        [SerializeField] private TMP_Text _skillDescription;        

        private void OnMouseEnter()
        {
            _cooldownDescription.gameObject.SetActive(true);
            _skillDescription.gameObject.SetActive(true);
            _skillDescription.text = _partDescription.ChoosedPart.SkillData.SkillDescription;
        }

        private void OnMouseExit()
        {
            _cooldownDescription.gameObject.SetActive(false);
            _skillDescription.gameObject.SetActive(false);
        }
    }
}
