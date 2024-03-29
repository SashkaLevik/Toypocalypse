using Assets.Scripts.GameEnvironment.Battle;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class SkillAreaWatcher : MonoBehaviour
    {
        [SerializeField] private SkillPanel _skillPanel;
        [SerializeField] private TMP_Text _damageText;

        private float _damage;
        private float _defence;
        private float _defaultDamage;
        private float _defaultDefence;
        private Area _showedArea;
        private SkillView _skillView;
        private PlayerSpawnPoint _playerSpawner;

        private void Awake()
        {
            _skillView = GetComponent<SkillView>();
            _skillView.DamageChanged += ChangeDamage;
            _skillView.DefenceChanged += ChangeDefence;
        }        

        private void Start()
        {
            _playerSpawner = _skillPanel.PlayerSpawner;            
        }

        private void OnDestroy()
        {
            _skillView.DamageChanged -= ChangeDamage;
            _skillView.DefenceChanged -= ChangeDefence;
        }

        private void ChangeDamage(float value)
        {
            _damage = value;
            _defaultDamage = value;
        }

        private void ChangeDefence(float value)
        {
            _defence = value;
            _defaultDefence = value;
        }

        public void ShowOnLeftButton(Area currentArea)
        {                     
            if (currentArea.AreaType == AreaType.Common)
            {
                _showedArea = _playerSpawner.DefenceArea;                
                float tempDamage = _damage - _showedArea.DecreasedValue;
                float tempDefence = _defence + _showedArea.IncreasedValue;

                if (_skillView.SkillData.SkillType == SkillType.Attack)
                {
                    _damageText.text = tempDamage.ToString();

                    if(tempDamage < _damage)
                        _damageText.color = Color.red;
                    else
                        _damageText.color = Color.white;
                }
                else if (_skillView.SkillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.green;
                    _damageText.text = tempDefence.ToString();
                }
            }
            else if (currentArea.AreaType == AreaType.Attack)
            {
                if (_skillView.SkillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defaultDamage.ToString();
                }
                else if (_skillView.SkillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defaultDefence.ToString();
                }
            }
        }

        public void ShowOnRightButton(Area currentArea)
        {                      
            if (currentArea.AreaType == AreaType.Common)
            {
                _showedArea = _playerSpawner.AttackArea;                
                float tempDamage = _damage + _showedArea.IncreasedValue;
                float tempDefence = _defence - _showedArea.DecreasedValue;

                if (_skillView.SkillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.green;
                    _damageText.text = tempDamage.ToString();
                }
                else if (_skillView.SkillData.SkillType == SkillType.Defence)
                {
                    _damageText.text = tempDefence.ToString();

                    if(tempDefence < _defence)
                        _damageText.color = Color.red;
                    else
                        _damageText.color = Color.white;
                }
            }
            else if (currentArea.AreaType == AreaType.Defence)
            {
                if (_skillView.SkillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defaultDamage.ToString();
                }
                else if (_skillView.SkillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defaultDefence.ToString();
                }
            }
        }

        public void SetDefaultLeft(Area area)
        {            
            float tempDamage = _damage + area.IncreasedValue;
            float tempDefence = _defence - area.DecreasedValue;

            if (area.AreaType == AreaType.Common)
            {
                if (_skillView.SkillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _damage.ToString();
                }
                else if (_skillView.SkillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defence.ToString();
                }
            }
            else if (area.AreaType == AreaType.Attack)
            {
                if (_skillView.SkillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.green;
                    _damageText.text = tempDamage.ToString();
                }
                else if (_skillView.SkillData.SkillType == SkillType.Defence)
                {
                    _damageText.text = tempDefence.ToString();

                    if (tempDefence < _defaultDefence)
                        _damageText.color = Color.red;
                    else
                        _damageText.color = Color.white;
                }
            }
        }

        public void SetDefaultRight(Area area)
        {
            float tempDamage = _damage - area.DecreasedValue;
            float tempDefence = _defence + area.IncreasedValue;

            if (area.AreaType == AreaType.Common)
            {
                if (_skillView.SkillData.SkillType == SkillType.Attack)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _damage.ToString();
                }
                else if (_skillView.SkillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.white;
                    _damageText.text = _defence.ToString();
                }
            }
            else if (area.AreaType == AreaType.Defence)
            {
                if (_skillView.SkillData.SkillType == SkillType.Attack)
                {
                    _damageText.text = tempDamage.ToString();

                    if (tempDamage < _defaultDamage)
                        _damageText.color = Color.red;
                    else
                        _damageText.color = Color.white;

                }
                else if (_skillView.SkillData.SkillType == SkillType.Defence)
                {
                    _damageText.color = Color.green;
                    _damageText.text = tempDefence.ToString();
                }
            }
        }
    }
}
