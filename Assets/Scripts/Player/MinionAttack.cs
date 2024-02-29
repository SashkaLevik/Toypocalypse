using UnityEngine;

namespace Assets.Scripts.Player
{
    public class MinionAttack :MonoBehaviour
    {
        [SerializeField] private AudioSource _attackSound;
        [SerializeField] private GameObject _attackObj;

        public void OnAttack()
        {
            _attackSound.Play();
            _attackObj.SetActive(true);
        }

        public void EndAttack()
        {
            _attackObj.SetActive(false);
            Destroy(gameObject);
        }            
    }
}
