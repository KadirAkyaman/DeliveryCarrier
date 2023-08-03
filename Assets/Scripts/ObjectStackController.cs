using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStackController : MonoBehaviour
{
    public static ObjectStackController Instance;

    public List<GameObject> objectList = new List<GameObject>();
    public GameObject _lastObject;
    [SerializeField] private Transform _stackStartPos;
    public int stackSize;

    private float _colliderSize;
    [SerializeField] private BoxCollider _objectCollider;
    public float _stackCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        _lastObject = null;

        stackSize = GridController.Instance._width * GridController.Instance._height;//Stack size belirlemek

        _colliderSize = _objectCollider.bounds.size.y;
        Debug.Log(_colliderSize);
        _stackCount = 0;
    }

    public void IncreaseStackSize(GameObject _gameObject)
    {
        _lastObject = _gameObject;//deðdiðimiz obje son obje olsun;

        _lastObject.transform.SetParent(transform);//child obje olsun


        objectList.Add(_lastObject);


        _gameObject.transform.position = new Vector3(_stackStartPos.position.x, _stackStartPos.position.y + DistanceBetweenObjects(), _stackStartPos.position.z);
        _stackCount++;        
    }

    public void ChangeLastObject()
    {
        _lastObject.transform.SetParent(GameObject.Find("Grid").transform);
        objectList.Remove(_lastObject);
        if (objectList.Count > 0)
        {
            _lastObject = objectList[objectList.Count - 1];
        }
    }

    private float DistanceBetweenObjects()
    {
        return 0.15f * _stackCount;
    }
}
