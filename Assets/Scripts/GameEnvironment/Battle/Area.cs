using Assets.Scripts.Enemyes;
using Assets.Scripts.Player;
using UnityEngine;
using TMPro;
namespace Assets.Scripts.GameEnvironment.Battle
{
    public class Area : MonoBehaviour
    {
        [SerializeField] private AreaType _areaType;
        [SerializeField] private GameObject _areaEnter;
        [SerializeField] private float _increasedValue;
        [SerializeField] private float _decreasedValue;
        [SerializeField] private TMP_Text _battleValue;

        public float IncreasedValue => _increasedValue;
        public float DecreasedValue => _decreasedValue;
        public AreaType AreaType => _areaType;

        public void ChangeIncreased(float value)
        {
            _increasedValue += value;
        }

        public void ChangeDecreased(float value)
        {
            _decreasedValue += value;
        }                
    }
}
