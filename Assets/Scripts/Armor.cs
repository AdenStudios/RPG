using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Armor : Item
{
    public int pDefense;
    public int mDefense;

    public enum ArmorType { Head, Body, Pants, Feet, Gloves }
    public ArmorType armorType;

    public override bool Use(Player player)
    {
        if (player.EquipArmor(this))
        {
            return true;
        }
        return false;
    }

    public Armor (ArmorType type ,string Name, int ID, string Description,
                  Sprite Icon, int SellValue, int PurchaseValue, int pDef, int mDef)
    {
        armorType = type;
        itemName = Name;
        itemID = ID;
        itemDescription = Description;
        itemIcon = Icon;
        sellValue = SellValue;
        purchaseValue = PurchaseValue;
        pDefense = pDef;
        mDefense = mDef;
        canStack = false;
        maxStack = 1;
        stackSize = 1;
    }
}
