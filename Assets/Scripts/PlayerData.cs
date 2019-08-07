using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public string playerGender;
    public string playerClass;
    public int saveSlot;
    public int level;
    public int experiance;
    public List<InventoryItem> savedInventory = new List<InventoryItem>();

    public PlayerData(Player player)
    {

    }

    public PlayerData(string Name, string Gender, string Class, int SaveSlot, int Level)
    {
        playerName = Name;
        playerGender = Gender;
        playerClass = Class;
        saveSlot = SaveSlot;
        level = Level;
    }

    [System.Serializable]
    public struct InventoryItem
    {
        public int id;
        public int quantity;
    }

    public void SaveInventory(List<Item> inventory)
    {
        List<InventoryItem> updatedInventory = new List<InventoryItem>();
        foreach (var item in inventory)
        {
            InventoryItem savedItem = new InventoryItem();
            savedItem.id = item.itemID;
            savedItem.quantity = item.stackSize;

            updatedInventory.Add(savedItem);
        }
        savedInventory = updatedInventory;
    }

    public List<Item> LoadInventory()
    {
        if (savedInventory != null)
        {
             List<Item> LoadedItems = new List<Item>();
            foreach (var savedItem in savedInventory)
            {
                Item loadedItem = ItemDatabase.instance.RetrieveItem(savedItem.id);
                loadedItem.stackSize = savedItem.quantity;

                LoadedItems.Add(loadedItem);
            }
            return LoadedItems;
        }
    return null;
    }
}
