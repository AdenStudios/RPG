using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    public InputField playerName;
    public string [] genders = new string [2] { "Male", "Female" };
    public int selectedGenderID = 0;
    public string [] classes = new string [3] { "Warrior", "Mage", "Archer" };
    public int selectedClassID = 0;
 
    public GameObject femaleModel;
    public GameObject maleModel;

    public Text genderSelectionText;
    public Text classSelectionText;

    public Button classLeftArrow;
    public Button classRightArrow;

    public Button genderLeftArrow;
    public Button genderRightArrow;

    public Button createCharacter;

    private void OnEnable() 
    {
        femaleModel.SetActive(false);
        maleModel.SetActive(true);
    }
    private void Start() 
    {
        classLeftArrow.GetComponent<Image>().color = Color.grey;
        genderLeftArrow.GetComponent<Image>().color = Color.grey;
        foreach (var savedata in SaveSystem.LoadAllSaves())
        {
           print(savedata.playerName  + " The " + savedata.playerClass); 
        }
    }

    public void ClassSelect(int buttonNum)
    {
        switch(buttonNum)
        {
            case 0:
                if (selectedClassID > 0)
                {
                    selectedClassID--;
                    classSelectionText.text = classes[selectedClassID];
                    classRightArrow.GetComponent<Image>().color = Color.white;
                }
                if (selectedClassID == 0)
                {
                    classLeftArrow.GetComponent<Image>().color = Color.grey;
                }
            break;

            case 1: 
            if (selectedClassID < classes.Length -1)
            {
                selectedClassID++;
                classSelectionText.text = classes[selectedClassID];
                classLeftArrow.GetComponent<Image>().color = Color.white;
            }
            if (selectedClassID == classes.Length -1)
            {
                classRightArrow.GetComponent<Image>().color = Color.grey;
            }
            break;
        }
    }

    public void GenderSelect(int buttonNum)
    {
        switch (buttonNum)
        {
            case 0:
                if (selectedGenderID == 1)
                {
                    selectedGenderID = 0;
                    genderSelectionText.text = genders[selectedGenderID];
                    maleModel.SetActive(true);
                    femaleModel.SetActive(false);
                    genderLeftArrow.GetComponent<Image>().color = Color.grey;
                    genderRightArrow.GetComponent<Image>().color = Color.white;
                }
            break;

            case 1:
                if (selectedGenderID == 0)
                {
                    selectedGenderID = 1;
                    genderSelectionText.text = genders[selectedGenderID];
                    maleModel.SetActive(false);
                    femaleModel.SetActive(true);
                    genderLeftArrow.GetComponent<Image>().color = Color.white;
                    genderRightArrow.GetComponent<Image>().color = Color.grey;
                }
            break;
        }
    }

    public void CreateCharacter()
    {
        int saveSlot = SaveSystem.GetFreeSaveSlotNumber();
        if (playerName.text.Length >= 4 && saveSlot != -1) 
        {
            PlayerData newCharacter = new PlayerData (
            playerName.text,
            genders[selectedGenderID],
            classes[selectedClassID],
            saveSlot,
            1
        );
        SaveSystem.SavePlayer(newCharacter);
        print("Saved");
        FindObjectOfType<MainMenuLobby>().ExitCharacterCreation();
        PlayerData temp = SaveSystem.LoadPlayer(saveSlot);
        FindObjectOfType<MainMenuLobby>().ChangeSelectedCharacter(temp);
      }  

    }
}
