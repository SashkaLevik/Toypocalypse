using Assets.Scripts.Enemyes;
using Assets.Scripts.GameEnvironment.Battle;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class SkillPanel : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private SkillView _skillView;
        //[SerializeField] private AttackPanel _attackPanel;
        [SerializeField] private Button _wait;
        [SerializeField] private Button _foward;
        [SerializeField] private Button _backward;
        [SerializeField] private Button _applyAction;

        public Toy _player;
        public BaseEnemy _enemy;
        public SkillView _currentSkill;
        public PlayerSpeed _playerSpeed;
        public AttackPanel _attackPanel;
        private PlayerMovement _movement;

        //public event UnityAction<SkillView> ActionChoosed;
        public event UnityAction SkillPlayed;

        private void Awake()
        {
        }

        private void Start()
        {
            _canvas.worldCamera = Camera.main;
            FillSkillPanel();
            _attackPanel = _player.GetComponentInChildren<AttackPanel>();
            _applyAction.onClick.AddListener(_attackPanel.ApplySkill);
        }

        private void OnEnable()
        {
            _wait.onClick.AddListener(OnWaitButton);
        }

        public void Construct(Toy player, PlayerSpeed playerSpeed)
        {
            _player = player;
            _playerSpeed = playerSpeed;
            _movement = _player.GetComponent<PlayerMovement>();
            _movement.MovePlayer(_foward, _backward);            
        }            

        public void ReturnPlayed(SkillView skillView)
        {
            _currentSkill = skillView;
            _currentSkill.transform.position = transform.position;
            _currentSkill.transform.SetParent(transform);
            _currentSkill.SkillButtonPressed += ChooseSkill;
            SkillPlayed += _currentSkill.RecoverCooldown;            
        }

        public void TakeBack(SkillView skillView)
        {
            _currentSkill = skillView;
            _currentSkill.transform.position = transform.position;
            _currentSkill.transform.SetParent(transform);
            _currentSkill.SkillButtonPressed += ChooseSkill;
        }

        private void OnWaitButton()
        {
            _playerSpeed.RecoverAP();
            SkillPlayed?.Invoke();
        }

        private void FillSkillPanel()
        {            
            for (int i = 0; i < _player.Parts.Count; i++)
            {                
                if (_player.Parts[i].SkillData.Type == SkillType.Move)
                {
                    var moveSkill = _player.Parts[i].SkillData;
                    _foward.GetComponent<SkillView>().Init(moveSkill);
                    _backward.GetComponent<SkillView>().Init(moveSkill);
                }
                else
                {
                    _skillView.Init(_player.Parts[i].SkillData);
                    _currentSkill = Instantiate(_skillView, transform);
                    _currentSkill.SkillButtonPressed += ChooseSkill;
                }                
            }
        }                

        private void ChooseSkill(SkillView skillView)
        {
            _currentSkill = skillView;

            if (_attackPanel.AttackSkill != null)
            {
                Debug.Log("SkillAllreadyPrepared");
                return;
            }

            if (_currentSkill.RequiredAP <= _playerSpeed.CurrentSpeed)
            {
                SkillPlayed -= _currentSkill.RecoverCooldown;
                SkillPlayed?.Invoke();
                _playerSpeed.SpentAP(_currentSkill.RequiredAP);
                _attackPanel.OnSkillChoosed(_currentSkill);
                _currentSkill.SkillButtonPressed -= ChooseSkill;
            }
            else
            {
                Debug.Log("NoAP");
                return;
            }
        }        
    }
}
