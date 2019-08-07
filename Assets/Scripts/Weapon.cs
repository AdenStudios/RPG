using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int pAtk;
    public int mAtk;

    public enum WeaponType { OneHanded, TwoHanded, OffHand }
    public WeaponType weaponType;

    public override bool Use(Player player)
    {
        if (player.EquipWeapon(this))
        {
            return true;
        }
        return false;
    }
    
    public Weapon(WeaponType type ,string Name, int ID, string Description, Sprite Icon, int SellValue, int PurchaseValue, int Patk, int Matk)
    {
        weaponType = type;
        itemName = Name;
        itemID = ID;
        itemDescription = Description;
        itemIcon = Icon;
        sellValue = SellValue;
        purchaseValue = PurchaseValue;
        pAtk = Patk;
        mAtk = Matk;
    }
}
