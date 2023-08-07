using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStackController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    public static ObjectStackController Instance;

    private Vector3 _currentObjectPos;
    [SerializeField] private Transform _firstObjectPos;

    public List<GameObject> _objectList = new List<GameObject>();

    [SerializeField] private GameObject _lastObject;


    private void Awake()
    {
        Instance = this;
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Pizza") && GameManager.Instance.characterState == CharacterState.Available && !other.GetComponent<ObjectController>().moveToCell && !other.GetComponent<ObjectController>().onPlayer)//Deðdiðimiz son obje cell'e gitmiyorsa
        {
            if (_objectList.Count < GameManager.Instance.maxStackSize)//Karakterimiz üst üste kaç obje toplayabilir?
            {
                other.GetComponent<ObjectController>().onPlayer = true;
                _objectList.Add(other.gameObject);


                if (_objectList.Count == 1)
                {
                    // _currentObjectPos = new Vector3(other.transform.position.x, _firstObjectPos.position.y, other.transform.position.z); //Y ekseninde ayný hizaya getiriyor

                    //other.gameObject.transform.position = _currentObjectPos;
                    other.gameObject.GetComponent<ObjectController>().UpdateObjectPosition(_firstObjectPos, true);
                    //other.transform.rotation = Quaternion.Euler(-90, transform.rotation.y, 0);
                    StartCoroutine(nameof(ChangeCharacterState));
                }
                else if (_objectList.Count > 1)
                {

                    _currentObjectPos.y = _lastObject.transform.position.y;
                    other.gameObject.transform.position = _currentObjectPos + new Vector3(0, GameManager.Instance.distanceBetweenObjects, 0);
                    other.gameObject.GetComponent<ObjectController>().UpdateObjectPosition(_objectList[_objectList.Count - 2].transform, true);
                    //other.transform.rotation = Quaternion.Euler(-90, transform.rotation.y, 0);  
                    StartCoroutine(nameof(ChangeCharacterState));
                }
                _lastObject = other.gameObject;//çarptýðýmýz obje son objemiz olacak
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grid") && GameManager.Instance.characterState == CharacterState.Available)
        {

            if (_objectList.Count > 0)
            {
                if (!GridController.Instance.isCellOccupied)//Gridde boþ yer var mý?
                {
                    StartCoroutine(nameof(ChangeCharacterState));
                    _lastObject.GetComponent<ObjectController>().moveToCell = true;//Cell'e gideceðini belirtiyoruz
                    _lastObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                    if (_lastObject != null)
                    {
                        _lastObject.GetComponent<ObjectController>().PlaceObjectOnCell(GridController.Instance.cells[GridController.Instance.emptyGridNumber].transform);
                        GridController.Instance.emptyGridNumber++;
                        ChangeLastObject();
                    }
                    if (GridController.Instance.emptyGridNumber >= GridController.Instance.cells.Count)//Eðer cell doluysa
                    {
                        GridController.Instance.isCellOccupied = true;
                    }
                }
            }
        }
    }

    void ChangeLastObject()
    {
        _objectList.Remove(_lastObject);

        if (_objectList.Count > 0)
        {
            _lastObject = _objectList[_objectList.Count - 1];
        }
    }


    IEnumerator ChangeCharacterState()
    {
        GameManager.Instance.characterState = CharacterState.Busy;
        yield return new WaitForSeconds(GameManager.Instance.characterStateCooldown);
        GameManager.Instance.characterState = CharacterState.Available;
    }
}
