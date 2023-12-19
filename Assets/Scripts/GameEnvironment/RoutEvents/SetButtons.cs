using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.RoutEvents
{
    public class SetButtons : MonoBehaviour
    {
        [SerializeField] private List<Button> _eventButtons;

        public List<Button> Buttons => _eventButtons;
    }
}
