using Assets.Scripts.Data.StaticData;
using Assets.Scripts.GameEnvironment.Dice;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class EnemySkillView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _effectDescription;
        [SerializeField] private TMP_Text _skillValueText;
        [SerializeField] private Image _skillIcon;
        [SerializeField] private Image _descImage;

        private float _damage;
        private float _defence;
        private SkillData _skillData;
        private AreaType _currentArea;

        public float Damage => _damage;
        public float Defence => _defence;
        public SkillData SkillData => _skillData;

        private void Start()
        {
            _currentArea = AreaType.Common;
        }

        public void InitData(SkillData skillData)
        {
            _skillData = skillData;
            _skillIcon.gameObject.SetActive(true);
            _skillIcon.sprite = skillData.Icon;

            if (_skillData.SkillType == SkillType.Defence)
            {
                _defence = _skillData.Defence;
                
            }
            else if (_skillData.SkillType == SkillType.Attack)
            {
                _damage = _skillData.Damage;
                
            }

            UpdateParameters(_skillData);
        }            

        public void UpdateParameters(SkillData skillData)
        {
            if (skillData.SkillType == SkillType.Attack)
                _skillValueText.text = skillData.Damage.ToString();
            else if (skillData.SkillType == SkillType.Defence)
                _skillValueText.text = skillData.Defence.ToString();
        }        

        public void ResetArea()
        {
            _skillValueText.color = Color.white;
            _currentArea = AreaType.Common;
        }

        public void Reset()
        {
            _skillValueText.color = Color.white;
            _skillIcon.gameObject.SetActive(false);
            _skillData = null;
        }

        public void ChangeOnArea(DiceFace diceFace)
        {
            if (_currentArea == diceFace.FaceType) return;
            else
                ChangeOnCommon();

            _currentArea = diceFace.FaceType;

            if (_currentArea == AreaType.StrongAttack)
                ChangeOnStrongAttack(diceFace);
            else if (_currentArea == AreaType.StrongDefence)
                ChangeOnStrongDefence(diceFace);
            else if (_currentArea == AreaType.Attack)
                ChangeOnAttack(diceFace);
            else if (_currentArea == AreaType.Defence)
                ChangeOnDefence(diceFace);
            else if (_currentArea == AreaType.Common)
                ChangeOnCommon();
        }

        private void ChangeOnAttack(DiceFace diceFace)
        {
            if (_skillData.SkillType == SkillType.Attack)
            {
                _damage += diceFace.IncreaseValue;
                _skillValueText.color = Color.green;
                UpdateBattleValue(_damage);
                Debug.Log("Attack");
            }
        }

        private void ChangeOnDefence(DiceFace diceFace)
        {
            if (_skillData.SkillType == SkillType.Defence)
            {
                _defence += diceFace.IncreaseValue;
                _skillValueText.color = Color.green;
                UpdateBattleValue(_defence);
                Debug.Log("Defence");

            }
        }

        private void ChangeOnStrongAttack(DiceFace diceFace)
        {
            if (_skillData.SkillType == SkillType.Attack)
            {
                _damage += diceFace.IncreaseValue;
                _skillValueText.color = Color.green;
                UpdateBattleValue(_damage);
            }
            else if (_skillData.SkillType == SkillType.Defence)
            {
                _defence -= diceFace.DecreaseValue;
                _skillValueText.color = Color.red;

                UpdateBattleValue(_defence);
            }
            Debug.Log("StrongAttack");

        }

        private void ChangeOnStrongDefence(DiceFace diceFace)
        {
            if (_skillData.SkillType == SkillType.Defence)
            {
                _defence += diceFace.IncreaseValue;
                _skillValueText.color = Color.green;
                UpdateBattleValue(_defence);
            }
            else if (_skillData.SkillType == SkillType.Attack)
            {
                _damage -= diceFace.DecreaseValue;
                _skillValueText.color = Color.red;
                UpdateBattleValue(_damage);
            }
            Debug.Log("StrongDefence");

        }

        private void ChangeOnCommon()
        {
            if (_skillData.SkillType == SkillType.Defence)
            {
                _defence = _skillData.Defence;
                UpdateBattleValue(_defence);
            }
            else if (_skillData.SkillType == SkillType.Attack)
            {
                _damage = _skillData.Damage;
                UpdateBattleValue(_damage);
            }
            Debug.Log("Common");

        }

        private void UpdateBattleValue(float value)
            => _skillValueText.text = value.ToString();        

        public void OnEnter()
        {
            _descImage.gameObject.SetActive(true);
            _effectDescription.text = GetLocalizedDescription(_skillData);
        }

        public void OnExit()
            => _descImage.gameObject.SetActive(false);

        private string GetLocalizedDescription(SkillData skillData)
        {            
            if (Application.systemLanguage == SystemLanguage.Russian)
                return skillData.RuDescription;
            else
                return skillData.EnDescription;
        }
    }
}
