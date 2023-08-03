using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float itemCollectCooldown = 1f;
    public float itemDropCooldown = 1f;

    public float objectCollectDistance = 2f;

    public float objectMoveSpeed=20f;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
