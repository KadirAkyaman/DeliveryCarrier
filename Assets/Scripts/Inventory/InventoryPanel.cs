using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public static InventoryPanel Instance;

    [SerializeField] private List<InventoryTab> _inventoryTabs;
    public Dictionary<PoolType, int> Products = new Dictionary<PoolType, int>();
    private void Awake()
    {
        ProductManager.Instance.Products.Clear();
        ProductManager.Instance.ItemCountChange.AddListener(LoadPanel);//removelistenerLoadPanel() diyene kadar dinlemeye devam eder, RemoveAllListeners
    }

    private void OnEnable()
    {
        Products = ProductManager.Instance.GetAllProducts();//
        LoadPanel();
    }

    private void Start()
    {
        HideAllTabs();
    }

    private void LoadPanel()
    {
        int index = 0;
        foreach (var item in Products)
        {
            var productData = ProductManager.Instance.GetProduct(item.Key);
            var productCount = productData.Name + " x" + item.Value;
            _inventoryTabs[index].gameObject.SetActive(true);
            _inventoryTabs[index].Load(productCount, productData.image);
            index++;
        }
    }


    public void HideAllTabs()
    {
        foreach (InventoryTab tab in _inventoryTabs)
        {
            tab.gameObject.SetActive(false);//
        }
    }
}
