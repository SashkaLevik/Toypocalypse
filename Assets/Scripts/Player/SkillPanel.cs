using Assets.Scripts.Data;
using Assets.Scripts.Data.StaticData;
using TMPro;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class SkillPanel : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<RectTransform> _skillSlots;
        [SerializeField] private RectTransform _minionSlot;
        [SerializeField] private SkillView _skillView;
        [SerializeField] private SkillView _moveSkill;
        [SerializeField] private Button _wait;
        [SerializeField] private Button _foward;
        [SerializeField] private Button _backward;

        private Toy _player;
        private Minion _currentMinion;
        private SkillView _currentSkill;
        private PlayerSpeed _playerSpeed;
        private AttackPanel _attackPanel;
        private PlayerMovement _movement;
        private PlayerHud _playerHud;
        private RoutMap _routMap;
        private List<SkillView> _playerSkills = new List<SkillView>();
        private List<SkillData> _playerSkillDatas = new List<SkillData>();
        private PlayerProgress _playerProgress;
        private Transform _skillPosition;

        public PlayerHud PlayerHud => _playerHud;
        public Toy Player => _player;

        public event UnityAction<SkillView> SkillChoosed;
        public event UnityAction RoundEnded;

        private void Start()
        {
            _canvas.worldCamera = Camera.main;
            _attackPanel = _playerHud.GetComponent<AttackPanel>();
            _wait.onClick.AddListener(OnWaitButton);
            _foward.onClick.AddListener(OnRightButton);
            _backward.onClick.AddListener(OnLeftButton);
            _routMap = _player.GetComponentInParent<PlayerSpawnPoint>().RoutMap;
            _movement.PlayerMoved += OnPlayerMove;
            SetMovmentAP();
        }        

        private void OnDestroy()
        {
            _wait.onClick.RemoveListener(OnWaitButton);
            _foward.onClick.RemoveListener(OnRightButton);
            _backward.onClick.RemoveListener(OnLeftButton);
        }

        public void Construct(Toy player, PlayerHud playerHud)
        {
            _player = player;
            _playerSpeed = _player.GetComponent<PlayerSpeed>();
            _movement = _player.GetComponent<PlayerMovement>();
            _playerHud = playerHud;
        }     

        private void SetMovmentAP()
        {
            _foward.GetComponentInChildren<TMP_Text>().text = _movement.RequiredAP.ToString();
            _backward.GetComponentInChildren<TMP_Text>().text = _movement.RequiredAP.ToString();
        }

        public void LoadPanelOrInitNew()
        {
            if (_playerProgress.IsPlayerCreated == true)
                LoadSkillPanel();
            else
                CreateNewPanel();

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
            _currentMinion.transform.position = _minionSlot.position;
            _currentMinion.transform.SetParent(_minionSlot.transform);
            //_currentSkill = Instantiate(_skillView, GetSkillPosition());
            //_currentSkill.SkillButtonPressed += ChooseSkill;
            //_playerSkills.Add(_currentSkill);
            //_playerSkillDatas.Add(_currentSkill.SkillData);
        }
        
        public void ChooseSkill(SkillView skillView)
        {
            _currentSkill = skillView;

            if (_routMap.IsInBattle == true)
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

        private void CreatePanel()
        {
            for (int i = 0; i < _playerSkillDatas.Count; i++)
            {
                _skillView.Init(_playerSkillDatas[i]);
                _currentSkill = Instantiate(_skillView, GetSkillPosition());
                _currentSkill.SkillButtonPressed += ChooseSkill;
                _playerSkills.Add(_currentSkill);                
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
        }

        private void CreateNewPanel()
        {
            GetSkillDatas();
            CreatePanel();
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

            if (_movement.RequiredAP <= _playerSpeed.CurrentSpeed)
            {
                _movement.MoveRight();
                _playerSpeed.SpentAP(_movement.RequiredAP);
            }
            else
                _playerHud.Warning.Enable(_playerHud.Warning.APWarning);
        }

        private void OnLeftButton()
        {
            if (_movement.IsMoving == true) return;

            if (_movement.RequiredAP <= _playerSpeed.CurrentSpeed)
            {
                _movement.MoveLeft();
                _playerSpeed.SpentAP(_movement.RequiredAP);
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
        }

        public void Load(PlayerProgress progress)
        {
            _playerProgress = progress;
        }
    }
}
