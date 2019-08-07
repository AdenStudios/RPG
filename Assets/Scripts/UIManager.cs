using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryUI;
    public static bool isUIActive = false; 
    private List<GameObject> activePanels = new List<GameObject>();

    // Player Target Variables
    public GameObject targetWindow;
    public Slider targetHealthBar;
    public Text targetName;
    public GameObject playerTarget;
    private Player player;

    // Player Health / Stamina Sliders
    public Slider healthBar;
    public Slider staminaBar;

    public Button sprintButton;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        InitializeInventoryUI();

        healthBar.maxValue = player.playerMaxHealth;
        healthBar.value = player.playerHealth;
        staminaBar.maxValue = player.playerMaxStamina;
        staminaBar.value = player.playerStamina;

        Player.onHealthChange += UpdateHealthBar;
        Player.onStaminaChange += UpdateStaminaBar;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        isUIActive = activePanels.Count == 0 ? false : true;
        if (targetWindow.activeSelf) 
        {

        }
    }
    private void GetInputs()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            if (inventoryUI.activeSelf == true)
            {
                activePanels.Add(inventoryUI);
            }
            else
            {
                activePanels.Remove(inventoryUI);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activePanels[0])
            {
                activePanels[0].SetActive(false);
                activePanels.Remove(activePanels[0]);
            }
        }
    }
    private void InitializeInventoryUI()
    {
        inventoryUI.GetComponent<InventoryUI>().InitializeUI();
        Inventory.onAquiredItem += inventoryUI.GetComponent<InventoryUI>().UpdateSlots;
    }

    private void UpdateHealthBar(float health)
    {
        healthBar.value = health;
    }

    private void UpdateStaminaBar(float stamina)
    {
        staminaBar.value = stamina;
    }
}
