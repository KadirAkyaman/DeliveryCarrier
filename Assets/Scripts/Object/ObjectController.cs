using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public static ObjectController Instance;

    private Rigidbody _objectRb;
    public BoxCollider _objectCollider;
    public GameObject _player;

    public bool onCell;//Obje Cell'de mi
    public bool isHeld;//Obje karakterimizin üzerinde mi

    [SerializeField] private float _movementSpeed;



    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _objectRb = GetComponent<Rigidbody>();
        _objectCollider = GetComponent<BoxCollider>();

        _player = GameObject.Find("Player");
        onCell = false;
        isHeld = false;
    }

    private void Update()
    {
        if (ObjectStackController.Instance.objectList.Count < ObjectStackController.Instance.stackSize && onCell == false && isHeld == false)//eðer stack boyutumuzdan küçükse objeleri toplasýn
        {

            if (PlaceObjectsOnGrid.Instance.characterState == CharacterState.Available)//Karakter müsait mi?
            {
                float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
                if (distanceToPlayer < GameManager.Instance.objectCollectDistance)
                {
                    StartCoroutine(nameof(CollectObject));
                }
            }
            else
            {
                Debug.Log("Chatacter is not avaliable");
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cell"))
        {
            onCell = true;
        }
    }


    IEnumerator CollectObject()
    {
       
        FollowPlayer();
        PlaceObjectsOnGrid.Instance.characterState = CharacterState.Busy;
        yield return new WaitForSeconds(GameManager.Instance.itemCollectCooldown);
        PlaceObjectsOnGrid.Instance.characterState = CharacterState.Available;
    }
    public void FollowPlayer()
    {
        isHeld = true;
        Vector3 targetPosition = _player.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.deltaTime);

        ObjectStackController.Instance.IncreaseStackSize(gameObject);
    }

    //IEnumerator FollowPlayer()
    //{
    //    _objectCollider.enabled = false;
    //    Vector3 targetPosition = _player.transform.position;
    //    transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.deltaTime);
    //
    //    yield return new WaitForSeconds(2f);
    //    ObjectStackController.Instance.IncreaseStackSize(gameObject);
    //}
}


