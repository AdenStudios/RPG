using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Item itemInSlot;
    public Image icon;
    public Text quantity;


    public void AddItem(Item item)
    {
        itemInSlot = item;
        icon.sprite = item.itemIcon;
        if (item.stackSize > 1)
        {
            quantity.text = item.stackSize.ToString();
            quantity.gameObject.SetActive(true);
        }
        icon.color = Color.white;      
    }

    public void RemoveItem()
    {
        itemInSlot = null;
        icon.sprite = null;
        quantity.gameObject.SetActive(false);
        icon.color = Color.black;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (itemInSlot.Use(FindObjectOfType<Player>()))
            {
                RemoveItem();
            }
        }
    }
}
