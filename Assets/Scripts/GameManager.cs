using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterState characterState = CharacterState.Available;

    public static GameManager Instance;

    public float characterStateCooldown = 1f;

    public float objectMoveSpeed = 20f;

    public int maxStackSize;

    public float distanceBetweenObjects = 0.5f;

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
