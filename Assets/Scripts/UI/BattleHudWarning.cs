using UnityEngine;
using TMPro;

namespace Assets.Scripts.UI
{
    public class BattleHudWarning : Warning
    {
        [SerializeField] private TMP_Text _apWarning;
        [SerializeField] private TMP_Text _skillWarning;
        [SerializeField] private TMP_Text _noSkillWarning;
        [SerializeField] private TMP_Text _areaWarning;

        public TMP_Text APWarning => _apWarning;
        public TMP_Text SkillWarning => _skillWarning;
        public TMP_Text NoSkillWarning => _noSkillWarning;
        public TMP_Text AreaWarning => _areaWarning;
    }
}
