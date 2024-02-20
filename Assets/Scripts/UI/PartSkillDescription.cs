using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PartSkillDescription : MonoBehaviour
    {
        [SerializeField] private PartDescription _partDescription;
        [SerializeField] private Image _cooldownDescription;
        [SerializeField] private Image _apDescription;
        [SerializeField] private Image _skillDescription;

        public void OnEnter()
        {
            _cooldownDescription.gameObject.SetActive(true);
            _apDescription.gameObject.SetActive(true);
            _skillDescription.gameObject.SetActive(true);
        }

        public void OnExit()
        {
            _cooldownDescription.gameObject.SetActive(false);
            _apDescription.gameObject.SetActive(false);
            _skillDescription.gameObject.SetActive(false);
        }                
    }
}
