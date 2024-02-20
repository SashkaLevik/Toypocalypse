using UnityEngine;
using TMPro;
using System.Collections;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class FirstTurnCalculator : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerCount;
        [SerializeField] private TMP_Text _enemyCount;
        [SerializeField] private TMP_Text _playerTurn;
        [SerializeField] private TMP_Text _enemyTurn;

        private int _playerValue;
        private int _enemyValue;

        public int PlayerD6 => _playerValue;

        public int EnemyD6 => _enemyValue;        

        public bool IsPlayerTurn()
        {
            if (_playerValue < _enemyValue)
                return false;

            return true;
        }

        public void CalculateTurn()
        {
            _playerValue = Random.Range(1, 7);
            _enemyValue = Random.Range(1, 7);
            _playerCount.text = _playerValue.ToString();
            _enemyCount.text = _enemyValue.ToString();
            StartCoroutine(ShowTurnText());
        }

        public IEnumerator ShowTurnText()
        {
            yield return new WaitForSeconds(1);

            _playerCount.gameObject.SetActive(false);
            _enemyCount.gameObject.SetActive(false);

            if (_playerValue < _enemyValue)
                _enemyTurn.gameObject.SetActive(true);
            else
                _playerTurn.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            _enemyTurn.gameObject.SetActive(false);
            _playerTurn.gameObject.SetActive(false);

        }

        private void Hide()
        {
            _playerCount.gameObject.SetActive(false);
            _enemyCount.gameObject.SetActive(false);
        }
    }
}
