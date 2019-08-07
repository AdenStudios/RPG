using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public int itemID;
    public string itemDescription;
    public Sprite itemIcon;
    public int sellValue;
    public int purchaseValue;
    public bool canStack;
    public int maxStack;
    public int stackSize;

    public Item NewCopy()
    {
        Item newCopy = new Item();

        newCopy.itemName = itemName;
        newCopy.itemID = itemID;
        newCopy.itemDescription = itemDescription;
        newCopy.itemIcon = itemIcon;
        newCopy.sellValue = sellValue;
        newCopy.purchaseValue = purchaseValue;
        newCopy.canStack = canStack;
        newCopy.maxStack = maxStack;
        newCopy.stackSize = stackSize;

        return newCopy;
    }

    public virtual bool Use(Player player)
    {
       return false;
    }
}

