using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour//CharacterController
{
    [SerializeField] private Transform _player;
    public static CharacterController Instance;

    //OBJECT STACK POSITIONS
    [SerializeField] private int _objectZCount;
    [SerializeField] private Transform _parentObjectPosition;
    [SerializeField] private List<Transform> _stackStartObjects;
    private int _objectsYCount;

    public List<GameObject> _objectList = new List<GameObject>();
    public List<GameObject> _objectsListOnCell = new List<GameObject>();

    [SerializeField] private GameObject _lastObject;
    [SerializeField] private GameObject _heldObjectParent;

    private int _count;
    private int _stackNum;


    private PlayerAnimatorController PlayerAnimatorController => playerAnimatorController ??= GetComponent<PlayerAnimatorController>();
    private PlayerAnimatorController playerAnimatorController;

    private void Start()
    {
        PlayerAnimatorController.ChangeAnimationLayer(false);
        _count = 0;
        _stackNum = 0;
        _objectsYCount = 1;
        for (int i = 0; i < _objectZCount; i++)
        {
            GameObject _obj = new GameObject(i + ".StartPos");

            _obj.transform.parent = _parentObjectPosition;
            _obj.transform.position = new Vector3(0, 0, i + 0.566f);
            _stackStartObjects.Add(_obj.transform);
        }
    }

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
                if (_objectList.Count < GameManager.Instance.maxStackSize * _objectZCount)
                {

                    _objController.onPlayer = true;
                    _objectList.Add(other.gameObject);
                    other.gameObject.transform.parent = _heldObjectParent.transform;

                    if (_stackNum == 0)
                    {
                        if (_objectList.Count == 1)
                        {
                            _objController.UpdateObjectPosition(_stackStartObjects[_stackNum], true, 0);
                            StartCoroutine(nameof(ChangeCharacterState));
                            PlayerAnimatorController.ChangeAnimationLayer(true);
                        }
                        else if (_objectList.Count > 1)
                        {
                            _objController.UpdateObjectPosition(_objectList[_objectList.Count - 2].transform, true, _objectsYCount);
                            _objectsYCount++;
                            StartCoroutine(nameof(ChangeCharacterState));
                        }

                        if (_objectList.Count >= GameManager.Instance.maxStackSize)
                        {
                            _stackNum++;
                            _objectsYCount = 1;
                        }
                    }
                    else if (_stackNum == 1)
                    {
                        if (_objectList.Count == GameManager.Instance.maxStackSize + 1)
                        {
                            _objController.UpdateObjectPosition(_stackStartObjects[_stackNum], true, 0);
                            StartCoroutine(nameof(ChangeCharacterState));
                        }
                        else if (_objectList.Count > GameManager.Instance.maxStackSize + 1)
                        {
                            _objController.UpdateObjectPosition(_objectList[_objectList.Count - 2].transform, true, _objectsYCount);
                            _objectsYCount++;
                            StartCoroutine(nameof(ChangeCharacterState));
                        }

                        if (_objectList.Count >= GameManager.Instance.maxStackSize * 2)
                        {
                            _stackNum++;
                            _objectsYCount = 1;
                        }
                    }
                    else if (_stackNum > 1)
                    {
                        if (_objectList.Count == GameManager.Instance.maxStackSize * _stackNum + 1)
                        {
                            _objController.UpdateObjectPosition(_stackStartObjects[_stackNum], true, 0);
                            StartCoroutine(nameof(ChangeCharacterState));
                        }
                        else if (_objectList.Count > GameManager.Instance.maxStackSize * _stackNum + 1)
                        {
                            _objController.UpdateObjectPosition(_objectList[_objectList.Count - 2].transform, true, _objectsYCount);
                            _objectsYCount++;
                            StartCoroutine(nameof(ChangeCharacterState));
                        }

                        if (_objectList.Count >= GameManager.Instance.maxStackSize * (_stackNum + 1))
                        {
                            _stackNum++;
                            _objectsYCount = 1;
                        }
                    }
                    _lastObject = other.gameObject;
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
                        //OBJENIN YUKSEKLIGINI AYARLAMA BOLUMU
                        _objectsYCount = _objectList.Count % GameManager.Instance.maxStackSize - 1;



                        _objController.PlaceObjectOnCell(GridController.Instance.cells[GridController.Instance.emptyGridNumber].transform.position + new Vector3(0, _count * GameManager.Instance.distanceBetweenObjects, 0)); ;
                        _count++;
                        if (_count >= GridController.Instance._maxObjectOnCell)
                        {
                            _count = 0;
                            GridController.Instance.emptyGridNumber++;
                        }

                        if (_objController.moveToCell)
                        {
                            AddObjectsOnCellList();//Son objeyi Cell'deki objelerin listesine ekle
                        }

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
                _stackNum = Mathf.RoundToInt(_objectList.Count / GameManager.Instance.maxStackSize);
            }
            else
            {
                _stackNum = 0;
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

    void AddObjectsOnCellList()
    {
        _objectsListOnCell.Add(_lastObject);
    }


    IEnumerator ChangeCharacterState()
    {
        GameManager.Instance.characterState = CharacterState.Busy;
        yield return new WaitForSeconds(GameManager.Instance.characterStateCooldown);
        GameManager.Instance.characterState = CharacterState.Available;
    }
}
