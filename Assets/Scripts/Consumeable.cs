using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumeable : Item
{
    public enum PotionType { Health, Mana };
    public PotionType potionType;

    public int restoreAmount;
    public override bool Use(Player player)
    {      
         switch (potionType)
        {
            case PotionType.Health:
                player.RestoreHealth(restoreAmount);
                return true;

            case PotionType.Mana:
                player.RestoreMana(restoreAmount);
                return true;
        }
        return false;
    }

    public Consumeable (PotionType type, string Name, int ID, string Description, Sprite Icon, int SellValue, int PurchaseValue,
                        bool CanStack, int MaxStack, int RestoreAmount)
    {
        potionType = type;
        itemName = Name;
        itemID = ID;
        itemDescription = Description;
        itemIcon = Icon;
        sellValue = SellValue;
        purchaseValue = PurchaseValue;
        canStack = CanStack;
        maxStack = MaxStack;
        stackSize = 1;
        restoreAmount = RestoreAmount;
    }
}
