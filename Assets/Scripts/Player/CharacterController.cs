using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour//CharacterController
{
    [SerializeField] private Transform _player;
    public static CharacterController Instance;

    private Vector3 _currentObjectPos;
    [SerializeField] private Transform _firstObjectPos;

    public List<GameObject> _objectList = new List<GameObject>();

    [SerializeField] private GameObject _lastObject;

    private PlayerAnimatorController PlayerAnimatorController => playerAnimatorController ??= GetComponent<PlayerAnimatorController>();
    private PlayerAnimatorController playerAnimatorController;

    private void Awake()
    {
        Instance = this;
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Pizza") && GameManager.Instance.characterState == CharacterState.Available)//Deðdiðimiz son obje cell'e gitmiyorsa
        {
            ObjectController _objController = other.GetComponent<ObjectController>();//DÜZENLENDÝ
            if (!_objController.moveToCell && !_objController.onPlayer)
            {
                if (_objectList.Count < GameManager.Instance.maxStackSize)//Karakterimiz üst üste kaç obje toplayabilir?
                {

                    _objController.onPlayer = true;
                    _objectList.Add(other.gameObject);


                    if (_objectList.Count == 1)
                    {
                        _objController.UpdateObjectPosition(_firstObjectPos, true);//, _objectList.Count
                        StartCoroutine(nameof(ChangeCharacterState));
                        PlayerAnimatorController.ChangeAnimationLayer(true);
                    }
                    else if (_objectList.Count > 1)
                    {
                        Vector3 _newPosition;
                        _currentObjectPos.y = _lastObject.transform.position.y;//current object pos y'yi bir önceki objenin y'sine getiriyoruz
                        //other.gameObject.transform.position = _currentObjectPos + new Vector3(0, GameManager.Instance.distanceBetweenObjects, 0);//çarptýðýmýz objenin pozisyhonunu bir önceki objenin pozisyonunn üzerine eklenmiþ halini yapýyoruz
                        _newPosition = _currentObjectPos + new Vector3(0, GameManager.Instance.distanceBetweenObjects, 0);
                        other.gameObject.transform.position += _newPosition;
                        _objController.UpdateObjectPosition(_objectList[_objectList.Count - 2].transform, true);//, _objectList.Count
                        StartCoroutine(nameof(ChangeCharacterState));
                    }
                    _lastObject = other.gameObject;//çarptýðýmýz obje son objemiz olacak
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grid") && GameManager.Instance.characterState == CharacterState.Available)
        {

            if (_objectList.Count > 0)
            {
                if (!GridController.Instance.isCellOccupied)//Gridde bos yer var mi?
                {

                    ObjectController _objController = _lastObject.GetComponent<ObjectController>();
                    StartCoroutine(nameof(ChangeCharacterState));
                    _objController.moveToCell = true;//Cell'e gidecegini belirtiyoruz
                    _lastObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                    if (_lastObject != null)
                    {
                        _objController.PlaceObjectOnCell(GridController.Instance.cells[GridController.Instance.emptyGridNumber].transform);
                        GridController.Instance.emptyGridNumber++;
                        ChangeLastObject();

                        if (_objectList.Count <= 0)
                        {
                            PlayerAnimatorController.ChangeAnimationLayer(false);
                        }
                    }
                    if (GridController.Instance.emptyGridNumber >= GridController.Instance.cells.Count)//Eger cell doluysa
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
