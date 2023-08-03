using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectsOnGrid : MonoBehaviour
{
    public static PlaceObjectsOnGrid Instance;

    public CharacterState characterState = CharacterState.Available;

    private int _gridNumber;
    public bool isCellOccupied;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _gridNumber = 0;
        isCellOccupied = false;

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grid") && ObjectStackController.Instance.objectList.Count > 0)//Eðer stack'te objeler varsa
        {
            PlaceObjects();
        }
    }

    void PlaceObjects()
    {
        if (characterState == CharacterState.Available)
        {


            if (_gridNumber < ObjectStackController.Instance.stackSize)
            {
                ObjectStackController.Instance._lastObject.transform.position = GridController.Instance.cells[_gridNumber].transform.position;
                ObjectStackController.Instance._lastObject.transform.rotation = Quaternion.Euler(-90f, 0, 0);
                _gridNumber++;
                characterState = CharacterState.Busy;

                ObjectStackController.Instance.ChangeLastObject();
                ObjectStackController.Instance._stackCount--;
                Invoke(nameof(SetCharacterAvailable), GameManager.Instance.itemDropCooldown);
            }
            else
            {
                isCellOccupied = true;
            }

        }
    }

    public void SetCharacterAvailable()
    {
        characterState = CharacterState.Available;
    }

}

public enum CharacterState
{
    Busy,
    Available
}