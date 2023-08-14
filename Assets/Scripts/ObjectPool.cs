using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Queue<GameObject> _pooledObjects;
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private int _poolSize;

    [SerializeField] private GameObject _table;
    [SerializeField] private GameObject _objectsParent;
    #region Singleton

    public static ObjectPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        _pooledObjects = new Queue<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_objectPrefab);
            obj.SetActive(false);

            _pooledObjects.Enqueue(obj);//siraya ekle
            obj.transform.parent = _objectsParent.transform;
        }
    }

    public GameObject GetPooledObject()
    {
        if (_pooledObjects.Count > 0)
        {
            GameObject obj = _pooledObjects.Dequeue();
            if (!obj.GetComponent<ObjectController>().onPlayer)//BU KISIM DUZELTILECEK
            {
                obj.transform.position = new Vector3(Random.Range(-3.72f, 3.72f), _table.transform.position.y + 0.1f, _table.transform.position.z);
                obj.transform.rotation = Quaternion.Euler(-90, Random.Range(0, 360), Random.Range(0, 20));

                obj.SetActive(true);
            }
            return obj;
        }
        return null;
    }

    public void AddObjectToPool(GameObject obj)
    {
        _pooledObjects.Enqueue(obj);
        ObjectController _objController = obj.GetComponent<ObjectController>();
        _objController.onCell = false;
        _objController.onPlayer = false;
        _objController.moveToCell = false;
        obj.SetActive(false);

    }


}
