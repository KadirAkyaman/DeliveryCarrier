using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardController : MonoBehaviour
{

    public static KeyboardController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ProductManager.Instance.AddProduct(PoolType.Cheesecake);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ProductManager.Instance.AddProduct(PoolType.Hamburger);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ProductManager.Instance.AddProduct(PoolType.Pizza);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }
}