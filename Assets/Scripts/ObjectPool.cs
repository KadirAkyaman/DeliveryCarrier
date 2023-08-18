using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Queue<GameObject> _pooledObjects;
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private int _poolSize;

    [SerializeField] private GameObject _startPos;
    [SerializeField] private GameObject _objectsParent;
    #region Singleton

    public static ObjectPool Instance;

    public float distanceBetweenObjects;


    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        distanceBetweenObjects = _objectPrefab.GetComponent<BoxCollider>().size.y + 0.2f;
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
            if (!obj.GetComponent<ObjectFollowController>().onPlayer)//BU KISIM DUZELTILECEK
            {
                obj.transform.position = new Vector3(Random.Range(ObjectPoolManager.Instance.objectPoolMaxXCount / 2, -ObjectPoolManager.Instance.objectPoolMaxXCount / 2), _startPos.transform.position.y, Random.Range(_startPos.transform.position.z, _startPos.transform.position.z - ObjectPoolManager.Instance.objectPoolMaxZCount));
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
        ObjectFollowController _objController = obj.GetComponent<ObjectFollowController>();
        _objController.onCell = false;
        _objController.onPlayer = false;
        _objController.moveToCell = false;
        obj.SetActive(false);

    }
}
