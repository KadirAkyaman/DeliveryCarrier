using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProductManager : MonoBehaviour
{
    public Dictionary<PoolType, int> Products = new Dictionary<PoolType, int>();

    [SerializeField] private ProductCollection _productCollection;
    public static ProductManager Instance;

    public UnityEvent ItemCountChange = new UnityEvent();//<> örneklerine bak

    private void Awake()
    {
        Instance = this;
        Products.Clear();
        GetAllProducts();

        _productCollection.LoadDictionary();//
    }

    public Product GetProduct(PoolType productType)
    {
        return _productCollection.GetProduct(productType);
    }

    public Dictionary<PoolType, int> GetAllProducts()
    {
        return Products;
    }

    public void AddProduct(PoolType poolType)
    {
        if (!Products.ContainsKey(poolType))
        {
            Products.Add(poolType, 1);
        }
        else
        {
            Products[poolType]++;
        }
        ItemCountChange.Invoke();
    }

    public void RemoveProduct(PoolType poolType)
    {
        if (Products.ContainsKey(poolType))
        {
            Products.Remove(poolType);
        }
        ItemCountChange.Invoke();
    }
}