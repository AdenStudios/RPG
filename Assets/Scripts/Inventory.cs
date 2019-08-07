using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int inventorySize = 20;
    public List<Item> inventory = new List<Item>();

    public static Inventory instance;

    public delegate void AquiredItem();
    public static event AquiredItem onAquiredItem;

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start() 
    {
        Player.onSaveGame += SaveInventory;
        LoadInventory();
    }

    public bool AddItem(Item newItem, int stackSize)
    {
        if (newItem.canStack)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                var item = inventory[i];

                if (item.itemID == newItem.itemID && item.stackSize != item.maxStack)
                {
                    if (stackSize + item.stackSize <= item.maxStack)
                    {
                        item.stackSize += stackSize;
                        return true;
                    }
                    else 
                    {
                        int totalStack =stackSize + item.stackSize;
                        int remaindingStack = totalStack -= newItem.maxStack;
                        item.stackSize = item.maxStack;
                        AddItem(newItem, remaindingStack);
                        UpdateUI();
                        return true;
                    }
                }
            }
            if (inventory.Count >= inventorySize)
            {
                Debug.Log("Not Enough Room");
                return false;
            }
            else 
            {
                newItem.stackSize = stackSize;
                inventory.Add(newItem);
                UpdateUI();
                Debug.Log("added item");
                return true;
            }
        }
        else 
        {
            if (inventory.Count >= inventorySize)
            {
                Debug.Log("Not Enough Room");
                return false;
            }
            else 
            {
                inventory.Add(newItem);
                UpdateUI();
                return true;
            }
        }
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
    }
    private void UpdateUI()
    {
        if (onAquiredItem != null)
        {
            onAquiredItem();
        }
    }

    public void SaveInventory()
    {
        LoadedPlayerData.data.SaveInventory(inventory);
    }

    public void LoadInventory()
    {
      var loadedInventory = LoadedPlayerData.data.LoadInventory();
      if (loadedInventory != null)
      {
        foreach (var item in loadedInventory)
        {
            AddItem(item, item.stackSize);
        }
      }
    }
}
