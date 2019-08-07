using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuLobby : MonoBehaviour
{
    public Button [] characterSlots;
    public GameObject mainLobby;
    public GameObject characterSelection;
    public GameObject createCharacter;

    public GameObject femaleModel;
    public GameObject maleModel;
    public GameObject characterInfoPanel;
    public Text characterPreviewName;
    public Text characterPreviewClassLevel;

    private List<PlayerData> saves;
    private PlayerData selectedCharacter;

    private void Awake() 
    {
        UpdatePlayerSaves();
        ChangeSelectedCharacter(saves[0]);
    }

    public void CreateCharacterMenu()
    {
        mainLobby.SetActive(false);
        createCharacter.SetActive(true);
        characterInfoPanel.SetActive(false);
    }
    
    public void SelectCharacterMenu()
    {
        UpdatePlayerSaves();
        mainLobby.SetActive(false);
        characterSelection.SetActive(true);
    }

    public void ExitSelectCharacterMenu()
    {
        characterSelection.SetActive(false);
        mainLobby.SetActive(true);
    }

    public void ExitCharacterCreation()
    {
        createCharacter.SetActive(false);
        mainLobby.SetActive(true);
        characterInfoPanel.SetActive(true);
        SetPlayerModel(selectedCharacter.playerGender);
    }

    public void EnterSoloGame()
    {
        LoadedPlayerData.data = selectedCharacter;
        SceneManager.LoadScene("StartingZone");
        
    }

    private void UpdatePlayerSaves()
    {
        saves = SaveSystem.LoadAllSaves();
        for (int i = 0; i < characterSlots.Length; i++)
        {
            if (i < saves.Count)
            {
                characterSlots[i].gameObject.SetActive(true);
                characterSlots[i].GetComponent<CharacterSelectionSlotData>().SetSlot(saves[i]);
            }
            else
            {
                characterSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void ChangeSelectedCharacter(PlayerData data)
    {
        selectedCharacter = data;
        characterPreviewName.text = selectedCharacter.playerName;
        characterPreviewClassLevel.text = "Level " + selectedCharacter.level + " " + selectedCharacter.playerClass;
        SetPlayerModel(selectedCharacter.playerGender);
    }

    public void SetPlayerModel(string gender)
    {
        if (gender == "Male")
        {
            femaleModel.SetActive(false);
            maleModel.SetActive(true);
        }
        else
        {
            maleModel.SetActive(false);
            femaleModel.SetActive(true);
        }
    }
}

public static class LoadedPlayerData
{
    public static PlayerData data;
}
