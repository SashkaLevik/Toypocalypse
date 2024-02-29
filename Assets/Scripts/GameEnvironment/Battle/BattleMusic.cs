using Assets.Scripts.GameEnvironment.RoutEvents;
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class BattleMusic : MonoBehaviour
    {
        [SerializeField] private RoutMap _routMap;
        [SerializeField] private List<AudioSource> _battleAudios;
        [SerializeField] private List<AudioSource> _eventAudios;

        private int _randomBattleAudio;
        private int _randomEventAudio;
        private RoutEvent _currentEvent;

        private void Start()
        {
            _randomBattleAudio = Random.Range(0, _battleAudios.Count);
            _battleAudios[_randomBattleAudio].Play();
            _routMap.EventEntered += ChangeOnEvent;
        }

        private void OnDestroy()
            => _routMap.EventEntered += ChangeOnEvent;

        private void ChangeOnEvent(RoutEvent routEvent)
        {
            _currentEvent = routEvent;
            _currentEvent.EventCompleted += ChangeOnBattle;
            _randomEventAudio = Random.Range(0, _eventAudios.Count);
            _battleAudios[_randomBattleAudio].Stop();
            _eventAudios[_randomEventAudio].Play();
        }

        private void ChangeOnBattle()
        {
            _randomBattleAudio = Random.Range(0, _battleAudios.Count);
            _eventAudios[_randomEventAudio].Stop();
            _battleAudios[_randomBattleAudio].Play();
            _currentEvent.EventCompleted -= ChangeOnBattle;
        }
    }
}
