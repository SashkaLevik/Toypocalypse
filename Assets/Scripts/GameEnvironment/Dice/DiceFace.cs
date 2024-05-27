using UnityEngine;
using TMPro;
using Assets.Scripts.Data.StaticData;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Dice
{
    public class DiceFace : MonoBehaviour
    {
        [SerializeField] private DiceFaceData _diceFaceData;
        [SerializeField] private TMP_Text _increaseText;
        [SerializeField] private TMP_Text _decreaseText;

        private int _increaseValue;
        private int _decreaseValue;
        private Sprite _faceImage;
        private AreaType _faceType;
        private MeshRenderer _renderer;

        public AreaType FaceType => _faceType;
        public int IncreaseValue => _increaseValue;
        public int DecreaseValue => _decreaseValue;
        public DiceFaceData FaceData => _diceFaceData;
        public Sprite FaceImage => _faceImage;

        private void Awake()
        {
            InitDiceFace(_diceFaceData);
        }

        public void InitDiceFace(DiceFaceData faceData)
        {
            _renderer = GetComponent<MeshRenderer>();
            _renderer.material = faceData.Material;
            _increaseValue = faceData.IncreaseValue;
            _decreaseValue = faceData.DecreaseValue;
            _faceType = faceData.AreaType;
            _faceImage = faceData.FaceImage;

            if (_faceType == AreaType.StrongAttack || _faceType == AreaType.StrongDefence)
            {
                _increaseText.gameObject.SetActive(true);
                _decreaseText.gameObject.SetActive(true);
            }
            else if (_faceType == AreaType.Attack || _faceType == AreaType.Defence)
                _increaseText.gameObject.SetActive(true);

            UpdateFaceValues();
        }               
        
        private void UpdateFaceValues()
        {
            if (_faceType == AreaType.StrongDefence || _faceType == AreaType.StrongAttack)
            {
                _increaseText.text = _increaseValue.ToString();
                _decreaseText.text = _decreaseValue.ToString();
            }
            else if (_faceType == AreaType.Attack || _faceType == AreaType.Defence)
                _increaseText.text = _increaseValue.ToString();
        }

    }
}
