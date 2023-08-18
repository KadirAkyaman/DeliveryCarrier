using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    private void Awake()
    {
        Instance = this;
    }



    [Header("Object Instantiate X Count")]
    public float objectPoolMaxXCount;

    [Header("Object Instantiate Y Count")]
    public float objectPoolMaxYCount;

    [Header("Object Instantiate Z Count")]
    public float objectPoolMaxZCount;

}
