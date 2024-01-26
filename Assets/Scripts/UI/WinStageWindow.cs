using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class WinStageWindow : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _completeStage;
        [SerializeField] private TMP_Text _gumAmount;
        [SerializeField] private TMP_Text _plasticineAmount;
        [SerializeField] private TMP_Text _glueAmount;
        [SerializeField] private TMP_Text _screwAmount;

        private int _gum;
        private int _plasticine;
        private int _glue;
        private int _screw;

        public int Gum => _gum;
        public int Plasticine => _plasticine;
        public int Glue => _glue;
        public int Screw => _screw;

        public event UnityAction StageCompleted;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }

        private void Start()
        {
            CalculatePrize();
        }

        private void OnEnable()
        {
            _completeStage.onClick.AddListener(OnStageComplete);
        }

        public void CalculatePrize()
        {
            _gum = Random.Range(6, 10);
            _plasticine = Random.Range(4, 6);
            _glue = Random.Range(2, 4);
            _screw = Random.Range(0, 2);
            _gumAmount.text = _gum.ToString();
            _plasticineAmount.text = _plasticine.ToString();
            _glueAmount.text = _glue.ToString();
            _screwAmount.text = _screw.ToString();
        }

        private void OnStageComplete()
        {
            StageCompleted?.Invoke();
            this.gameObject.SetActive(false);
        }


    }
}
