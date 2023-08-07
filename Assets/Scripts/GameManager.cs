using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterState characterState = CharacterState.Available;

    public static GameManager Instance;

    public float characterStateCooldown = 1f;
    public float itemDropCooldown = 1f;

    public float objectCollectDistance = 2f;
    public float distanceBetweenObjects = 0.5f;

    public float objectMoveSpeed=20f;

    public int maxStackSize;
    private void Awake()
    {
        Instance = this;
    }
}

public enum CharacterState
{
    Busy,
    Available
}
