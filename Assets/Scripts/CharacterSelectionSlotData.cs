using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionSlotData : MonoBehaviour
{
    public Text playerName;
    public Text classAndLevel;
    public PlayerData data;

    public void SetSlot(PlayerData Data)
    {
        data = Data;
        playerName.text = data.playerName;
        classAndLevel.text = "Level " + data.level + " " + data.playerClass;
    }

    public void Select()
    {
        FindObjectOfType<MainMenuLobby>().ChangeSelectedCharacter(data);
        print("Select");
    }
}
