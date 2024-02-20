using Assets.Scripts.Data;
using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Enemyes;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerHealth), typeof(PlayerSpeed))]
    public class Toy : MonoBehaviour, ISaveProgress
    {
        private static readonly int Attack = Animator.StringToHash("Attack");

        [SerializeField] protected Sprite _toyImage;
        [SerializeField] private List<PartData> _parts;
        [SerializeField] private AudioSource _attackSound;

        private float _animationDelay = 1.2f;
        private BaseEnemy _enemy;
        private SkillPanel _skillPanel;
        private RoutMap _routMap;
        private PlayerHud _playerHud;
        private List<Minion> _activeMinions;
        private Animator _animator;
        private SkillData _negativeEffect;

        public SkillPanel SkillPanel => _skillPanel;
        public PlayerHud PlayerHud => _playerHud;
        public SkillData NegativeEffect => _negativeEffect;
        public Sprite ToyImage => _toyImage;
        public List<PartData> Parts => _parts;
        public List<Minion> Minions => _activeMinions;

        public event UnityAction AnimationEnded;
        public event UnityAction<AreaType> AreaChanged;
        public event UnityAction<SkillData> EffectReceived;

        private void Start()
        {
            _skillPanel.LoadPanelOrInitNew();
            _animator = GetComponent<Animator>();
        }
        
        public void Construct(SkillPanel skillPanel, RoutMap routMap, PlayerHud playerHud)
        {
            _skillPanel = skillPanel;
            _routMap = routMap;
            _playerHud = playerHud;
        }            

        public void InitEnemy(BaseEnemy enemy)
            => _enemy = enemy;

        public void ApplyEffect(SkillData skillData)
        {
            _negativeEffect = skillData;
            EffectReceived?.Invoke(_negativeEffect);
        }

        public void ResetNegativeEffect()
            => _negativeEffect = null;

        public void ChangeArea(AreaType type)
            => AreaChanged?.Invoke(type);        

        public void PlayAttack()
        {
            if (this.gameObject != null)
            {
                _animator.SetTrigger(Attack);
                StartCoroutine(EndTurn());
            }
        }

        private IEnumerator EndTurn()
        {
            yield return new WaitForSeconds(_animationDelay);
            AnimationEnded?.Invoke();
        }            

        public void Save(PlayerProgress progress)
        {
        }

        public void Load(PlayerProgress progress)
        {
            _activeMinions = progress.PlayerParts.ActiveMinions;
        }
    }
}
