
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Product", menuName = "Product")]
public class ProductCollection : ScriptableObject
{
    public List<Product> Products = new List<Product>();

    public Dictionary<PoolType, Product> ProductDictionary = new Dictionary<PoolType, Product>();



    public void LoadDictionary()
    {
        foreach (Product product in Products)
        {
            if (!ProductDictionary.ContainsKey(product.PoolType))
            {
                ProductDictionary[product.PoolType] = product;
            }

        }
    }

    public Product GetProduct(PoolType poolType)
    {
        if (ProductDictionary.TryGetValue(poolType, out Product product))
        {
            return product;
        }
        else
        {
            return null;
        }
    }
}
[Serializable]
public class Product
{
    public PoolType PoolType;

    public string Name;

    public Sprite image;
}

