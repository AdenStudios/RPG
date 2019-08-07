using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Item
{
    public int minGatherAmount;
    public int maxGatherAmount;

    public Resource(string Name, int ID, string Description, Sprite Icon, int SellValue, int PurchaseValue, int MinAmount, int MaxAmount)
    {
        itemName = Name;
        itemID = ID;
        itemDescription = Description;
        itemIcon = Icon;
        sellValue = SellValue;
        canStack = true;
        maxStack = 99;
        stackSize = 1;
        purchaseValue = PurchaseValue;
        minGatherAmount = MinAmount;
        maxGatherAmount = MaxAmount;
    }
    private Resource()
    {

    }
    public Resource CopyResource()
    {
        Resource newCopy = new Resource();

        newCopy.itemName = itemName;
        newCopy.itemID = itemID;
        newCopy.itemDescription = itemDescription;
        newCopy.itemIcon = itemIcon;
        newCopy.sellValue = sellValue;
        newCopy.purchaseValue = purchaseValue;
        newCopy.canStack = canStack;
        newCopy.maxStack = maxStack;
        newCopy.stackSize = stackSize;
        newCopy.minGatherAmount = minGatherAmount;
        newCopy.maxGatherAmount = maxGatherAmount;

        return newCopy;
    }

}
