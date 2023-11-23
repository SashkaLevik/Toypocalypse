using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.TreeHouse
{
    public class DragAndDrop : MonoBehaviour
    {
        //private Container _container;
        //private Vector3 _startPosition;
        //private Vector3 _offset;
        //private Camera _camera;
        //private Vector3 _mousePosition;
        //private Canvas _canvas;
        //public bool _isOnTable;
        //private Image _image;
        //public Table _table;
        //public Inventory _inventory;

        //public Vector3 StartPosition => _startPosition;

        //private void Awake()
        //    => _camera = Camera.main;

        //private void Start()
        //{
        //    _inventory = GetComponentInParent<Inventory>();
        //    _table = _inventory.Table;
        //    _startPosition = transform.position;
        //    _container = GetComponentInParent<Container>();
        //    _canvas = GetComponentInParent<Canvas>();
        //    _image = GetComponent<Image>();
        //}

        //public void SetCollider(bool value)
        //    => transform.GetComponent<Collider2D>().enabled = value;

        //public void SetStartPosition()
        //{
        //    transform.position = _container.transform.position;
        //    transform.SetParent(_container.transform);
        //}

        //private void OnMouseUpAsButton()
        //{
            
        //}

        //private void OnMouseDown()
        //{
            //if (_isOnTable == false)
            //{
            //    transform.position = _table.transform.position;
            //    transform.SetParent(_table.transform);
            //    _isOnTable = true;
            //}            
            //else if (_isOnTable == true)
            //{
            //    transform.position = _inventory.Container.transform.position;
            //    transform.SetParent(_inventory.Container.transform);
            //    _isOnTable = false;
            //}
        //    _offset = transform.position - GetMousePosition();
        //    transform.SetParent(_canvas.transform);

        //    var rayOrigin = _camera.transform.position;
        //    var rayDirection = GetMousePosition() - rayOrigin;

        //    RaycastHit2D hitInfo = Physics2D.Raycast(rayOrigin, rayDirection);
        //    Debug.Log(hitInfo.collider.name);
        //    SetCollider(false);
        //}

        //private void OnMouseDrag()
        //    => transform.position = GetMousePosition() + _offset;

        //private void OnMouseUp()
        //{
        //    Vector2 clickPosition = GetMousePosition();
        //    RaycastHit2D[] hitInfo = Physics2D.RaycastAll(clickPosition, Vector2.zero);

        //    foreach (RaycastHit2D hit in hitInfo)
        //    {
        //        if (hit.collider.TryGetComponent(out Table table))
        //        {
        //            _isOnTable = true;
        //        }
        //    }
        //    SetCollider(true);
        //    //if (_isOnTable == false) SetStartPosition();
        //}

        //private Vector3 GetMousePosition()
        //{
        //    if (Input.GetMouseButton(0))
        //    {
        //        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        //        _mousePosition.z = 0;
        //    }
        //    return _mousePosition;
        //}
    }
}
