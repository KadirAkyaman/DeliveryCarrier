using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerController : MonoBehaviour
{
    [SerializeField] private float _objectSpawnCountdown;
    [SerializeField] private GameObject _objectsParent;

    private void Start()
    {
        InvokeRepeating("SpawnObjectsOnTable", _objectSpawnCountdown, _objectSpawnCountdown);
    }
    private void Update()
    {
        if (CharacterController.Instance._objectsListOnCell.Count > 0)//listede obje varsa
        {
            if (Input.GetKeyDown(KeyCode.T) && GridController.Instance.isCellOccupied)
            {

                GridController.Instance.isCellOccupied = false;
                GridController.Instance.emptyGridNumber = 0;
                foreach (GameObject _obj in CharacterController.Instance._objectsListOnCell)
                {
                    if (_obj.transform.parent.tag == "Grid")
                    {
                        _obj.transform.parent = _objectsParent.transform;//Parent objelerini degistiriyoruz
                        ObjectPool.Instance.AddObjectToPool(_obj);
                    }

                }

            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            foreach (Transform childTransform in _objectsParent.GetComponentsInChildren<Transform>(true))
            {
                if (childTransform != _objectsParent.transform)
                {
                    GameObject childGameObject = childTransform.gameObject;
                    ObjectPool.Instance._pooledObjects.Enqueue(childGameObject);
                }
            }
        }
    }

    private void SpawnObjectsOnTable()
    {
        ObjectPool.Instance.GetPooledObject();
    }


}
