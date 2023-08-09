using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public static ObjectController Instance;

    [SerializeField] private float followSpeed;

    public bool moveToCell;

    public bool onCell;//Cell'e de�iyor mu?

    public static GameObject lastObject;

    public bool onPlayer;

    [SerializeField] private GameObject _gridObject;

    private void Start()
    {
        moveToCell = false;
        onCell = false;
        onPlayer = false;
        _gridObject = GridManager.Instance.grid;

    }

    private void Awake()
    {
        Instance = this;
    }
    public void UpdateObjectPosition(Transform followedObject, bool isFollowStart)
    {

        StartCoroutine(StartFollowingToLastObjectPosition(followedObject, isFollowStart));
    }

    IEnumerator StartFollowingToLastObjectPosition(Transform followedObject, bool isFollowStart)
    {

        while (isFollowStart)
        {

            yield return new WaitForEndOfFrame();
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followedObject.position.x, followSpeed * Time.deltaTime), transform.position.y, Mathf.Lerp(transform.position.z, followedObject.position.z, followSpeed * Time.deltaTime));
            if (moveToCell)
                break;
        }
    }



    public void PlaceObjectOnCell(Transform _lastObject)
    {
        StartCoroutine(FollowingToCell(_lastObject.transform));
    }

    IEnumerator FollowingToCell(Transform followedObject)
    {

        while (!onCell)
        {
            yield return new WaitForEndOfFrame();
            transform.parent = _gridObject.transform;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followedObject.position.x, followSpeed * Time.deltaTime), Mathf.Lerp(transform.position.y, followedObject.position.y, followSpeed * Time.deltaTime), Mathf.Lerp(transform.position.z, followedObject.position.z, followSpeed * Time.deltaTime));
        }
    }
}

