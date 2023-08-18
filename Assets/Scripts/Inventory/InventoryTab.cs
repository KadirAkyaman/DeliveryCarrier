using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InventoryTab : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI productNameText;

    [SerializeField]
    private Image productImage;


    public void Load(string productName, Sprite productIcon)
    {
        productNameText.text = productName;
        productImage.sprite = productIcon;
    }

}
