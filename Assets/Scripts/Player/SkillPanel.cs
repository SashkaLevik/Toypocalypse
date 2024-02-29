using Assets.Scripts.Data;
using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.SaveLoad;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class SkillPanel : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<RectTransform> _skillSlots;
        [SerializeField] private MinionSlot _minionSlot;
        [SerializeField] private SkillView _skillView;
        [SerializeField] private SkillView _moveSkill;
        [SerializeField] private Button _wait;
        [SerializeField] private Button _foward;
        [SerializeField] private Button _backward;
        [SerializeField] private List<SkillView> _skills;

        bool _isInBattle;
        private Toy _player;
        private Minion _currentMinion;
        private SkillView _currentSkill;
        private PlayerSpeed _playerSpeed;
        private AttackPanel _attackPanel;
        private PlayerMovement _movement;
        private PlayerHud _playerHud;
        private BattleSystem _battleSystem;
        private List<SkillView> _playerSkills = new List<SkillView>();
        private List<SkillData> _playerSkillDatas = new List<SkillData>();
        private PlayerProgress _playerProgress;
        private Transform _skillPosition;

        public Toy Player => _player;
        public PlayerHud PlayerHud => _playerHud;
        public BattleSystem BattleSystem => _battleSystem;
        public List<SkillView> PlayerSkills => _playerSkills;
        public AttackPanel AttackPanel => _attackPanel;

        public event UnityAction<SkillView> SkillChoosed;
        public event UnityAction RoundEnded;

        private void Start()
        {
            _canvas.worldCamera = Camera.main;
            _attackPanel = _playerHud.GetComponent<AttackPanel>();
            _wait.onClick.AddListener(OnWaitButton);
            _foward.onClick.AddListener(OnRightButton);
            _backward.onClick.AddListener(OnLeftButton);
            _movement.PlayerMoved += OnPlayerMove;
            _battleSystem.BattleEntered += CheckBattleStatus;
            SetMovmentAP();
        }
        
        private void OnDestroy()
        {
            _wait.onClick.RemoveListener(OnWaitButton);
            _foward.onClick.RemoveListener(OnRightButton);
            _backward.onClick.RemoveListener(OnLeftButton);

            foreach (var skill in _playerSkills)
                _player.AreaChanged -= skill.ChangeOnArea;
        }

        public void Construct(Toy player, PlayerHud playerHud, BattleSystem battleSystem)
        {
            _player = player;
            _playerSpeed = _player.GetComponent<PlayerSpeed>();
            _movement = _player.GetComponent<PlayerMovement>();
            _playerHud = playerHud;
            _battleSystem = battleSystem;
        }     

        private void SetMovmentAP()
        {
            _foward.GetComponentInChildren<TMP_Text>().text = _movement.MovementAP.ToString();
            _backward.GetComponentInChildren<TMP_Text>().text = _movement.MovementAP.ToString();
        }

        public void LoadPanelOrInitNew()
        {
            if (_playerProgress.IsPlayerCreated == true)
                LoadSkillPanel();
            else
            {
                CreateNewPanel();
            }
            _playerProgress.IsPlayerCreated = true;
        }

        public void TakeBack(SkillView skillView)
        {
            _currentSkill = skillView;
            _currentSkill.transform.position = GetSkillPosition().position;
            _currentSkill.transform.SetParent(GetSkillPosition());
            //_currentSkill.SkillButtonPressed += ChooseSkill;
        }

        public void AddMinion(Minion minion)
        {
            _currentMinion = minion;
            _minionSlot.OnMinionAdded(_currentMinion);           
        }
        
        public void ChooseSkill(SkillView skillView)
        {
            _currentSkill = skillView;

            if (_isInBattle == true)
                OnAttack(skillView);
            else
                OnEvent(skillView);
        }

        public void ResetCooldown()
        {
            foreach (var skill in _playerSkills)
                skill.ResetCooldown();
        }

        public void ResetMoveButtons()
        {
            _foward.interactable = true;
            _backward.interactable = true;
        }

        public void ResetWaitButton()
            => _wait.interactable = true;

        private void CheckBattleStatus(bool isInBattle)
            => _isInBattle = isInBattle;        

        private void CreatePanel()
        {            
            for (int i = 0; i < _playerSkillDatas.Count; i++)
            {
                _skills[i].Init(_playerSkillDatas[i]);
                _skills[i].SkillButtonPressed += ChooseSkill;
                _player.AreaChanged += _skills[i].ChangeOnArea;
                _playerSkills.Add(_skills[i]);
            }
        }

        private Transform GetSkillPosition()
        {
            foreach (var container in _skillSlots)
            {
                if (container.GetComponentInChildren<SkillView>() == null)
                {
                    _skillPosition = container.transform;
                }
            }
            return _skillPosition;
        }

        private void LoadSkillPanel()
        {
            _playerSkillDatas = _playerProgress.PlayerSkills.ToList();
            CreatePanel();
            if (_currentMinion != null) AddMinion(_currentMinion);
            Debug.Log("LoadPanel");
        }

        private void CreateNewPanel()
        {
            GetSkillDatas();
            CreatePanel();
            Debug.Log("NewPanel");
        }

        private void GetSkillDatas()
        {
            foreach (var data in _player.Parts)
                _playerSkillDatas.Add(data.SkillData);
        }

        private void OnWaitButton()
        {            
            _attackPanel.Attack();
            _wait.interactable = false;
            RoundEnded?.Invoke();
        }        

        private void OnRightButton()
        {
            if (_movement.IsMoving == true) return;

            if (_movement.MovementAP <= _playerSpeed.CurrentSpeed)
            {
                _movement.MoveRight();
                _playerSpeed.SpentAP(_movement.MovementAP);
            }
            else
                _playerHud.Warning.Enable(_playerHud.Warning.APWarning);
        }

        private void OnLeftButton()
        {
            if (_movement.IsMoving == true) return;

            if (_movement.MovementAP <= _playerSpeed.CurrentSpeed)
            {
                _movement.MoveLeft();
                _playerSpeed.SpentAP(_movement.MovementAP);
            }
            else
                _playerHud.Warning.Enable(_playerHud.Warning.APWarning);
        }

        private void OnPlayerMove()
            => _movement.CheckButtons(_foward, _backward);

        private void OnAttack(SkillView skillView)
        {           
            if (skillView.RequiredAP <= _playerSpeed.CurrentSpeed)
            {
                _playerSpeed.SpentAP(skillView.RequiredAP);
                _attackPanel.PrepareSkills(skillView);
                skillView.DecreaseCooldown();
                //skillView.SkillButtonPressed -= ChooseSkill;
            }
            else
            {
                _playerHud.Warning.Enable(_playerHud.Warning.APWarning);
                return;
            }
        }

        private void OnEvent(SkillView skillView)
        {
            SkillChoosed?.Invoke(skillView);
            //skillView.SkillButtonPressed -= ChooseSkill; ansigne in events
        }      

        public void Save(PlayerProgress progress)
        {
            progress.PlayerSkills = _playerSkillDatas.ToList();

            if(_currentMinion != null)
                progress.PlayerParts.CurrentMinion = _currentMinion.MinionData;
        }

        public void Load(PlayerProgress progress)
        {
            _playerProgress = progress;

            if (progress.PlayerParts.CurrentMinion != null)
                _currentMinion = _playerProgress.PlayerParts.CurrentMinion.MinionPrefab;
        }
    }
}
