using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameEnvironment.Dice
{
    public class AreaDice : MonoBehaviour
    {
        [SerializeField] private Transform[] _checkSides;
        [SerializeField] private DiceFace[] _faces;
        [SerializeField] private float _throwForce;
        [SerializeField] private float _rollForce;
        [SerializeField] private float _minRollValue;
        [SerializeField] private float _maxRollValue;

        private int _rerolls = 2;
        private int _side;
        private AreaType _topFace;
        private Rigidbody _rigidbody;
        private Vector3 _startPosition;

        public int Rerolls => _rerolls;

        public event UnityAction<AreaType> OnDiceResult;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
            _startPosition = transform.position;
        }        

        public void Roll()
        {
            StartCoroutine(RollDice());
        }

        private IEnumerator RollDice()
        {
            float randX = Random.Range(_minRollValue, _maxRollValue);
            float randY = Random.Range(_minRollValue, _maxRollValue);
            float randZ = Random.Range(_minRollValue, _maxRollValue);

            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(new Vector3(0, 0, -1) * (_throwForce), ForceMode.Impulse);
            _rigidbody.AddTorque(randX, randY, randZ);
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(()=> _rigidbody.velocity.magnitude == 0);
            GetDiceFace();
            yield return new WaitForSeconds(1);
            _rigidbody.isKinematic = true;
            transform.position = _startPosition;
        }        

        private void GetDiceFace()
        {
            _topFace = _faces[0].FaceType;
            float lastPosition = _checkSides[0].transform.position.z;

            for (int i = 0; i < _checkSides.Length; i++)
            {
                if (_checkSides[i].transform.position.z < lastPosition)
                {
                    lastPosition = _checkSides[i].transform.position.z;
                    _side = i;
                    _topFace = _faces[i].FaceType;
                }
            }

            OnDiceResult?.Invoke(_topFace);
        }
    }
}
