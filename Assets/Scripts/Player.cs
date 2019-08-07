using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string playerName = "";
    private string playerGender = "";

    private Stat Damage = new Stat();
    private Stat Defense = new Stat();

    private int maxHealth = 100;
    private int maxMana = 100;
    private int curHealth;
    private int curMana;

    private float maxStamina = 100;
    private float curStamina;
    private float staminaRegenRate = 6f;
    private float staminaNotUsedTimer = 0;

    public int playerMaxHealth { get { return maxHealth; } }
    public int playerHealth { get { return curHealth; } }
    public float playerMaxStamina { get { return maxStamina; } }
    public float playerStamina { get { return curStamina; } }

    private ICharacterClass myClass;

    private Weapon mainHand;
    private Weapon offHand;
    private Armor[] EquippedArmor = new Armor[5];
    public Armor[] getEquippedArmor { get { return EquippedArmor; } }
    
    private Inventory inventory;

    private List<GameObject> targets = new List<GameObject>();
    public List<GameObject> playerTargets { get { return targets; } }
    
    public GameObject playerModelPrefeb;
    public GameObject femaleModel;
    public GameObject maleModel;

    public delegate void OnHealthChange(float health);
    public static event OnHealthChange onHealthChange;

    public delegate void OnStaminaChange(float stamina);
    public static event OnStaminaChange onStaminaChange;

    public delegate void OnSaveGame();
    public static event OnSaveGame onSaveGame;

    private void Awake() 
    {
        inventory = GetComponent<Inventory>();
        BuildLoadedPlayer();
        curHealth = maxHealth;
        curMana = maxMana;
        curStamina = maxStamina;      
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        StaminaPassiveRegen();
    }

    private void BuildLoadedPlayer()
    {
        PlayerData player = LoadedPlayerData.data;
        if (player.playerGender == "Male")
        {
            femaleModel.SetActive(false);
            maleModel.SetActive(true);
        }
        else
        {
            femaleModel.SetActive(true);
            maleModel.SetActive(false);
        }
        if (player.playerClass == "Warrior")
        {
            myClass = new Warrior();
            maxHealth = myClass.baseMaxHealth;
            maxStamina = myClass.baseMaxStamina;
        }
    }
    public void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interaction();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            onSaveGame();
            SaveSystem.SavePlayer(LoadedPlayerData.data);
            print("SAVED");
        }
    }

    public void Interaction()
    {
        foreach (var target in targets)
        {
            IInteractable interactable = target.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void RestoreHealth(int amount)
    {
        print("healed player for " + amount);
        curHealth += amount;
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;         
        }
        onHealthChange(curHealth);
    }
    public void RestoreMana(int amount)
    {
        curMana += amount;
        if (curMana > maxMana)
        {
            curMana = maxMana;
        }
    }

    public void TakeDamage(int amount)
    {
        curHealth -= amount;
        if (curHealth <= 0)
        {
            // die
        }
        onHealthChange(curHealth);
    }

    public void UseStamina(float rate)
    {
        staminaNotUsedTimer = 0;
        curStamina -= Time.deltaTime * rate;
        onStaminaChange(curStamina);
        print(curStamina);
    }

    private void StaminaPassiveRegen()
    {
        if (staminaNotUsedTimer >= 1 && curStamina < maxStamina)
        {
            curStamina += Time.deltaTime * staminaRegenRate;
            onStaminaChange(curStamina);
        }
        else
        {
            staminaNotUsedTimer += Time.deltaTime;
        }
    }

    public bool EquipArmor(Armor armor)
    {
        int index = (int)armor.armorType;
        var armorSlot = EquippedArmor[index];

        if (armorSlot == null)
        {
            armorSlot = armor;
            Defense.AddModifier(armor.pDefense);

            print("armor equipped");
            return true;
        }
        else
        {
            if (inventory.AddItem(armorSlot, 1))
            {
                armorSlot = armor;
                return true;
            }
            return false;
        }   
    }

    public bool EquipWeapon(Weapon weapon)
    {
        switch (weapon.weaponType)
        {
            case Weapon.WeaponType.OneHanded:
                if (mainHand ==  null)
                {
                    inventory.RemoveItem(weapon);
                    mainHand = weapon;
                }
                else if (mainHand.weaponType == Weapon.WeaponType.TwoHanded)
                {
                    if (UnEquipWeapoon(mainHand))
                    {
                        inventory.RemoveItem(weapon);
                        mainHand = weapon;
                        offHand = null;
                        return true;
                    }                    
                }
                else
                {
                    if (UnEquipWeapoon(mainHand))
                    {
                        inventory.RemoveItem(weapon);
                        mainHand = weapon;
                        return true;
                    }
                }
            break;

            case Weapon.WeaponType.TwoHanded:
            break;

            case Weapon.WeaponType.OffHand:
            break;         
        }
        return false;
    }

    public bool UnEquipWeapoon(Weapon weapon)
    {
        if (inventory.AddItem(weapon, 1))
        {
            if (weapon.weaponType == Weapon.WeaponType.TwoHanded)
            {
                offHand = null;
            }
            return true;
        }
        return false;
    }

    public bool UnEquipArmor()
    {
        return false;
    }

    public void AddTaraget(GameObject target)
    {
        targets.Add(target);
    }
    public void RemoveTarget(GameObject target)
    {
        targets.Remove(target);
    }
}
