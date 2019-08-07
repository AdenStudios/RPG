using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public Dictionary<int, Item> items = new Dictionary<int, Item>();
    
    public static ItemDatabase instance;

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
        LoadWeapons();
        LoadArmors();
        LoadPotions();
        LoadResources();
    }

    private void Start() {
        //FindObjectOfType<Inventory>().AddItem(items[30], 1);
        //FindObjectOfType<Inventory>().AddItem(items[31], 1);
        //FindObjectOfType<Inventory>().inventory[0].Use(GetComponent<Player>());
    }

    public Item RetrieveItem(int id)
    {
        return items[id];
    }
    private void LoadWeapons()
    {
        NewWeapon(Weapon.WeaponType.OneHanded, "Short Sword", 30, "", 200, 500, 8, 6);
        NewWeapon(Weapon.WeaponType.OneHanded, "Broad Sword", 31, "", 500, 1200, 11, 9);
        NewWeapon(Weapon.WeaponType.OneHanded, "Long Sword", 32, "", 3000, 11500, 24, 17);
    }

    private void LoadArmors()
    {
        NewArmor(Armor.ArmorType.Head, "Wooden Helmet", 20, "", 200, 1200, 20, 20);
        NewArmor(Armor.ArmorType.Body, "Wooden BreastPlate", 21, "", 600, 2500, 40, 20);
        NewArmor(Armor.ArmorType.Pants, "Wooden Gaiters", 22, "", 400, 1800, 30, 20);
    }

    private void LoadPotions()
    {
        NewHealthPotion("Minor Health Potion", 1, "A Minor Health Potion", 20, 5, 20);
        NewHealthPotion("Health Potion", 2, "A Health Potion", 40, 10, 40);
        NewHealthPotion("Greater Health Potion", 3, "A Greater Health Potion", 80, 20, 60);

        NewHealthPotion("Minor Mana Potion", 11, "A Minor Mana Potion",20, 5, 20);
        NewHealthPotion("Mana Potion", 12, "A Health Potion", 40, 10, 40);
        NewHealthPotion("Greater Mana Potion", 13, "A Greater Mana Potion", 80, 20, 60);
    }
    
    private void LoadResources()
    {
        NewResource("Wood", 100, "Wood", 50, 300, 3, 8);
        NewResource("Stone", 101, "Stone", 50, 300, 3, 8);
    }

    private void NewHealthPotion(string Name, int ID, string Des, int RestoreAmount, int SellPrice, int BuyPrice)
    {
        Sprite icon = Resources.Load<Sprite>("Consumeables/" + Name);
        Consumeable newPotion = new Consumeable(Consumeable.PotionType.Health, Name, ID, Des, icon, SellPrice, BuyPrice, true, 99, RestoreAmount); 
        items.Add(ID, newPotion);
    }

    private void NewManaPotion(string Name, int ID, string Des, int RestoreAmount, int SellPrice, int BuyPrice)
    {
        Sprite icon = Resources.Load<Sprite>("Consumeables/" + Name);
        Consumeable newPotion = new Consumeable(Consumeable.PotionType.Mana, Name, ID, Des, icon, SellPrice, BuyPrice, true, 99, RestoreAmount); 
        items.Add(ID, newPotion);
    }

    private void NewArmor(Armor.ArmorType type, string Name, int ID, string des, int SellValue, int PurchaseValue, int Pdef, int Mdef)
    {
        Sprite icon = Resources.Load<Sprite>("Armors/" + Name);
        Armor newArmor = new Armor(type, Name, ID, des, icon, SellValue, PurchaseValue, Pdef, Mdef);
        items.Add(ID, newArmor);
    }

    private void NewWeapon(Weapon.WeaponType type, string Name, int ID, string des, int SellValue, int PurchaseValue, int Patk, int Matk)
    {
        Sprite icon = Resources.Load<Sprite>("Weapons/" + Name);
        Weapon newWeapon = new Weapon(type, Name, ID, des, icon, SellValue, PurchaseValue, Patk, Matk);
        items.Add(ID, newWeapon);
    }

    private void NewResource(string Name, int ID, string des, int SellValue, int PurchaseValue, int MinGatherAmount, int MaxGatherAmount)
    {
        Sprite icon = Resources.Load<Sprite>("Resources/" + Name);
        Resource newResource = new Resource(Name, ID, des, icon, SellValue, PurchaseValue, MinGatherAmount, MaxGatherAmount);
        items.Add(ID, newResource);
    }
}
