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
        
        public AreaType AreaType => _areaType;
                      
    }
}
