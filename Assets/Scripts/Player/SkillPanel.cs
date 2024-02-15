using Assets.Scripts.Data;
using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Infrastructure.Services;
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
        [SerializeField] private SkillView _skillView;
        [SerializeField] private SkillView _moveSkill;
        [SerializeField] private Button _wait;
        [SerializeField] private Button _foward;
        [SerializeField] private Button _backward;
        [SerializeField] private Button _applyAction;

        private Toy _player;
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

        public event UnityAction SkillPlayed;
        public event UnityAction<SkillView> SkillChoosed;        

        private void Start()
        {
            _canvas.worldCamera = Camera.main;
            _attackPanel = _player.GetComponentInChildren<AttackPanel>();
            _applyAction.onClick.AddListener(_attackPanel.ApplySkill);
            _wait.onClick.AddListener(OnWaitButton);
            _foward.onClick.AddListener(() => OnRightButton(_moveSkill));
            _backward.onClick.AddListener(() => OnLeftButton(_moveSkill));
            _routMap = _player.GetComponentInParent<PlayerSpawnPoint>().RoutMap;
            _movement.PlayerMoved += OnPlayerMove;
        }        

        private void OnDestroy()
        {
            _applyAction.onClick.RemoveListener(_attackPanel.ApplySkill);
            _wait.onClick.RemoveListener(OnWaitButton);
            _foward.onClick.RemoveListener(() => OnRightButton(_moveSkill));
            _backward.onClick.RemoveListener(() => OnLeftButton(_moveSkill));
        }

        public void Construct(Toy player, PlayerHud playerHud)
        {
            _player = player;
            _playerSpeed = _player.GetComponent<PlayerSpeed>();
            _movement = _player.GetComponent<PlayerMovement>();
            _playerHud = playerHud;
        }     

        public void LoadPanelOrInitNew()
        {
            if (_playerProgress.IsPlayerCreated == true)
                LoadSkillPanel();
            else
                CreateNewPanel();

            _playerProgress.IsPlayerCreated = true;
        }               

        public void ReturnPlayed(SkillView skillView)
        {
            TakeBack(skillView);            
            SkillPlayed += _currentSkill.RecoverCooldown;            
        }

        public void TakeBack(SkillView skillView)
        {
            _currentSkill = skillView;
            _currentSkill.transform.position = GetSkillPosition().position;
            _currentSkill.transform.SetParent(GetSkillPosition());
            _currentSkill.SkillButtonPressed += ChooseSkill;
        }

        public void AddMinion(SkillData skillData)
        {
            _skillView.Init(skillData);
            _currentSkill = Instantiate(_skillView, GetSkillPosition());
            _currentSkill.SkillButtonPressed += ChooseSkill;
            _playerSkills.Add(_currentSkill);
            _playerSkillDatas.Add(_currentSkill.SkillData);
        }
        
        public void ChooseSkill(SkillView skillView)
        {
            _currentSkill = skillView;

            if (_routMap.IsInBattle == true)
                OnAttack(_currentSkill);
            else
                OnEvent(_currentSkill);
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

        private void CreatePanel()
        {
            for (int i = 0; i < _playerSkillDatas.Count; i++)
            {
                if (_playerSkillDatas[i].SkillType == SkillType.Move)
                {
                    _moveSkill.Init(_playerSkillDatas[i]);
                    _foward.GetComponent<SkillView>().Init(_playerSkillDatas[i]);
                    _backward.GetComponent<SkillView>().Init(_playerSkillDatas[i]);
                }
                else
                {
                    _skillView.Init(_playerSkillDatas[i]);
                    _currentSkill = Instantiate(_skillView, GetSkillPosition());
                    _currentSkill.SkillButtonPressed += ChooseSkill;
                    _playerSkills.Add(_currentSkill);
                }
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
            Debug.Log("PanelLoad");
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
            _playerSpeed.ResetAP();
            PlaySkillOnBattle();
        }        

        private void OnRightButton(SkillView skillView)
        {
            if (_movement.IsMoving == true) return;

            if (skillView.RequiredAP <= _playerSpeed.CurrentSpeed)
            {
                _movement.MoveRight();
                _playerSpeed.SpentAP(skillView.RequiredAP);
                PlaySkillOnBattle();
            }
            else
                _playerHud.Warning.Enable(_playerHud.Warning.APWarning);
        }

        private void OnLeftButton(SkillView skillView)
        {
            if (_movement.IsMoving == true) return;

            if (skillView.RequiredAP <= _playerSpeed.CurrentSpeed)
            {
                _movement.MoveLeft();
                _playerSpeed.SpentAP(skillView.RequiredAP);
                PlaySkillOnBattle();
            }
            else
                _playerHud.Warning.Enable(_playerHud.Warning.APWarning);
        }

        private void OnPlayerMove()
            => _movement.CheckButtons(_foward, _backward);

        private void OnAttack(SkillView skillView)
        {
            if (_attackPanel.AttackSkill != null)
            {
                _playerHud.Warning.Enable(_playerHud.Warning.SkillWarning);
                return;
            }

            if (skillView.RequiredAP <= _playerSpeed.CurrentSpeed)
            {
                SkillPlayed?.Invoke();
                _playerSpeed.SpentAP(skillView.RequiredAP);
                _attackPanel.OnSkillChoosed(skillView);
                skillView.SkillButtonPressed -= ChooseSkill;
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

        private void PlaySkillOnBattle()
        {
            if (_routMap.IsInBattle == true) SkillPlayed?.Invoke();
        }

        public void Save(PlayerProgress progress)
        {
            progress.PlayerSkills = _playerSkillDatas.ToList();
            Debug.Log("SkillsSaved");
        }

        public void Load(PlayerProgress progress)
        {
            _playerProgress = progress;
        }
    }
}
