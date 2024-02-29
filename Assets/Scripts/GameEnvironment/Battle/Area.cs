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
        [SerializeField] private float _areaBattleValue;
        [SerializeField] private TMP_Text _battleValue;

        public float AreaBattleValue => _areaBattleValue;
        public AreaType AreaType => _areaType;

        private void Start()
        {
            //_battleValue.text = _areaBattleValue.ToString();                
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Toy player))
            {
                player.ChangeArea(_areaType);
                _areaEnter.SetActive(true);
            }
            if (collision.TryGetComponent(out BaseEnemy enemy))
            {
                enemy.ChangeArea(this, _areaType);
                _areaEnter.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Toy player))
            {
                _areaEnter.SetActive(false);
            }
            if (collision.TryGetComponent(out BaseEnemy enemy))
            {
                _areaEnter.SetActive(false);
            }
        }
    }
}
