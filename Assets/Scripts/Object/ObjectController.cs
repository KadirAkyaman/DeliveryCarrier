using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public static ObjectController Instance;

    [SerializeField] private float followSpeed;

    public bool moveToCell;

    public bool onCell;//Cell'e deðiyor mu?

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
    public void UpdateObjectPosition(Transform followedObject, bool isFollowStart, int count)
    {

        StartCoroutine(StartFollowingToLastObjectPosition(followedObject, isFollowStart, count));
    }

    IEnumerator StartFollowingToLastObjectPosition(Transform followedObject, bool isFollowStart, int count)
    {

        if (count == 1)
        {
            while (isFollowStart)
            {

                yield return new WaitForEndOfFrame();
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, followedObject.position.x, 5 * followSpeed * Time.deltaTime), transform.position.y, Mathf.Lerp(transform.position.z, followedObject.position.z, 5 * followSpeed * Time.deltaTime));
                if (moveToCell)
                    break;
            }
        }
        else
        {
            while (isFollowStart)
            {

                yield return new WaitForEndOfFrame();
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, followedObject.position.x, 5 * followSpeed * Time.deltaTime), Mathf.Lerp(transform.position.y, GameManager.Instance.distanceBetweenObjects * count, followSpeed * Time.deltaTime), Mathf.Lerp(transform.position.z, followedObject.position.z, 5 * followSpeed * Time.deltaTime));
                if (moveToCell)
                    break;
            }
        }
    }



    public void PlaceObjectOnCell(Vector3 _lastObject)
    {
        StartCoroutine(FollowingToCell(_lastObject));
    }

    IEnumerator FollowingToCell(Vector3 followedObject)
    {

        while (!onCell)
        {
            yield return new WaitForEndOfFrame();
            transform.parent = _gridObject.transform;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followedObject.x, followSpeed * Time.deltaTime), Mathf.Lerp(transform.position.y, followedObject.y, followSpeed * Time.deltaTime), Mathf.Lerp(transform.position.z, followedObject.z, followSpeed * Time.deltaTime));
        }
    }
}

