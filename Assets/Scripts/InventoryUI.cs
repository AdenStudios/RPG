using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private InventorySlot[] inventorySlots;
    [SerializeField] private GameObject inventorySlotsParent;

    private void Awake() 
    {
        UpdateSlots();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeUI()
    {
        inventorySlots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        print(inventorySlots.Length);
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < Inventory.instance.inventory.Count; i++)
        {
            var inventoryItem = Inventory.instance.inventory[i];
            inventorySlots[i].AddItem(inventoryItem);
        }
        print("Updated Inventory Slots");
    }
}
