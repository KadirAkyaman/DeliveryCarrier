using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStackController : MonoBehaviour
{
    public static ObjectStackController Instance;

    public List<GameObject> objectList = new List<GameObject>();
    private GameObject _lastObject;
    [SerializeField] private Transform _stackStartPos;

    private void Awake()
    {
        Instance = this;
        _lastObject = null;
    }


    public void IncreaseStackSize(GameObject _gameObject)
    {
        if (_lastObject == null)
        {
            _gameObject.transform.position = new Vector3(_stackStartPos.transform.position.x, _stackStartPos.transform.position.y);
        }
        else
        {
            _gameObject.transform.position = new Vector3(_lastObject.transform.position.x, _lastObject.transform.position.y + 1);
        }

        _gameObject.transform.SetParent(transform);
        objectList.Add(_gameObject);

        UpdateLastObject();
    }

    private void UpdateLastObject()
    {
        if (_lastObject==null)
        {
            _lastObject = objectList[objectList.Count];
            Debug.Log(_lastObject);
        }
        else
        {
            _lastObject = objectList[objectList.Count - 1];
        }

        _lastObject.gameObject.transform.localScale *= 2;
    }
}
