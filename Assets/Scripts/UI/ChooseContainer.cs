using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ChooseContainer : MonoBehaviour
    {        
        [SerializeField] private GameObject[] _containers;
        [SerializeField] private GameObject _attachedContainer;
        
        public void PushButton()
        {
            _attachedContainer.SetActive(true);

            foreach (var container in _containers)
            {
                container.SetActive(false);
            }
        }
    }
}
