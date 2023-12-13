using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using YG;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private CharacterDatabase characterDB;
    [SerializeField] private GameObject iconsProgress;
    private Image skill1;
    private Image skill2;
    private Image skill3;
    public Text nameCharacter;
    public Image spriteCharacter;

    private int selectedOption = 0; 
    void Start()
    {
        skill1 = iconsProgress.transform.GetChild(0).GetComponent<Image>();
        skill2 = iconsProgress.transform.GetChild(1).GetComponent<Image>();
        skill3 = iconsProgress.transform.GetChild(2).GetComponent<Image>();

        UpdateCharacter(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= characterDB.CharacterCount)
            selectedOption = 0;

        UpdateCharacter(selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;

        if(selectedOption < 0)
            selectedOption = characterDB.CharacterCount - 1;

        UpdateCharacter(selectedOption);
    }

    public void UpdateCharacter(int selectedCharacter)
    {
        var character = characterDB.GetCharacter(selectedCharacter);
        spriteCharacter.sprite = character.sprite;
        nameCharacter.text = character.name;

        YandexGame.savesData.NowCharacter = character;

        skill1.sprite = characterDB.characterList[selectedOption].skill1;
        skill2.sprite = characterDB.characterList[selectedOption].skill2;
        skill3.sprite = characterDB.characterList[selectedOption].skill3;

        skill1.gameObject.GetComponentInChildren<Text>().text = character.SkillName[0];
        skill2.gameObject.GetComponentInChildren<Text>().text = character.SkillName[1];
        skill3.gameObject.GetComponentInChildren<Text>().text = character.SkillName[2];

        skill1.gameObject.GetComponentsInChildren<Text>()[1].text = character.SkillLevel[0].ToString();
        skill2.gameObject.GetComponentsInChildren<Text>()[1].text = character.SkillLevel[1].ToString();
        skill3.gameObject.GetComponentsInChildren<Text>()[1].text = character.SkillLevel[2].ToString();

        var shield = iconsProgress.transform.GetChild(3);
        var mana = iconsProgress.transform.GetChild(4);

        shield.GetComponentsInChildren<Text>()[1].text = character.SkillLevel[3].ToString();
        mana.GetComponentsInChildren<Text>()[1].text = character.SkillLevel[4].ToString();

        switch (selectedCharacter)
        {
            case 0:
                YandexGame.savesData.skin = SavesYG.PlayerSkin.Wanderer;
                break;
            case 1:
                YandexGame.savesData.skin = SavesYG.PlayerSkin.Cliric;
                break;
            case 2:
                YandexGame.savesData.skin = SavesYG.PlayerSkin.Piromant;
                break;
        }

    }

    public void SaveChange()
    {
        YandexGame.SaveProgress();
    }

    public void UpLevel(int numberItem)
    {
        var character = characterDB.GetCharacter(selectedOption);
        character.SkillLevel[numberItem]++;
        if (character.SkillLevel[numberItem] > 3)
            character.SkillLevel[numberItem] = 3;

        UpdateCharacter(selectedOption);
    }
}
