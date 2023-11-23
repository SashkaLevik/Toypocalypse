using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.TreeHouse;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PartDescription : MonoBehaviour
    {
        [SerializeField] private TMP_Text _partName;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _damage;

        public void SetValues(PartData partData)
        {
            _partName.text = partData.Name;
            _health.text = partData.Health.ToString();
            _speed.text = partData.Speed.ToString();
            _damage.text = partData.Damage.ToString();
        }
    }
}
