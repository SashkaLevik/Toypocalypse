using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ButtonFlyes : MonoBehaviour
    {
        [SerializeField] private MenuSignboard _signboard;

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _signboard.IsButtonEntered += Run;
        }
        
        private void Run(bool value)
        {
            _animator.SetBool("Run", value);
        }
    }
}
