using UnityEngine;

namespace Assets.Scripts.Enemyes
{
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
    }
}
