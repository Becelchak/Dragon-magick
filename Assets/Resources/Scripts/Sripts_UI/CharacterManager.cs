using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private CharacterDatabase characterDB;
    [SerializeField] private GameObject iconsProgress;
    [SerializeField] private GameObject openCharacter;
    [SerializeField] private Text descriptionText;
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
    }

    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void GetData()
    {
        YandexGame.savesData.Load();
    }

    private void Update()
    {
            UpdateCharacter(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= YandexGame.savesData.characters.Length)
            selectedOption = 0;

        UpdateCharacter(selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;

        if(selectedOption < 0)
            selectedOption = YandexGame.savesData.characters.Length - 1;

        UpdateCharacter(selectedOption);
    }

    public void UpdateCharacter(int selectedCharacter)
    {
        var character = YandexGame.savesData.characters[selectedCharacter];
        spriteCharacter.sprite = character.sprite;
        nameCharacter.text = character.name;

        YandexGame.savesData.NowCharacter = character;

        skill1.sprite = YandexGame.savesData.characters[selectedCharacter].skill1;
        skill2.sprite = YandexGame.savesData.characters[selectedCharacter].skill2;
        skill3.sprite = YandexGame.savesData.characters[selectedCharacter].skill3;

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

        CheckUnlocked(character);
        PrepareTextDescription();

        if(character.isUnlock)
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

        var character = YandexGame.savesData.characters[selectedOption];
        if (character.SkillLevel[numberItem]+ 1 <= 3 && (YandexGame.savesData.gold - character.PriceList[numberItem] >= 0))
        {
            character.SkillLevel[numberItem]++;
            YandexGame.savesData.gold -= character.PriceList[numberItem];
        }

        UpdateCharacter(selectedOption);
        YandexGame.SaveProgress();
    }

    public void UnlockCharacter()
    {
        var character = YandexGame.savesData.characters[selectedOption];
        if (YandexGame.savesData.gold - character.PriceList[5] >= 0)
        {
            character.isUnlock = true;
            YandexGame.savesData.gold -= character.PriceList[5];
        }
        UpdateCharacter(selectedOption);
        YandexGame.SaveProgress();
    }

    public void CheckUnlocked(Character character)
    {
        if (!character.isUnlock)
        {
            var skills = GameObject.Find("Skills").GetComponentsInChildren<Button>();
            foreach (var button in skills)
            {
                button.interactable = false;
            }

            GameObject.Find("Button save").GetComponent<Button>().interactable = false;

            openCharacter.SetActive(true);
            openCharacter.transform.GetChild(1).GetComponent<Text>().text = $"Цена: {character.PriceList[5]}";

        }
        else
        {
            var skills = GameObject.Find("Skills").GetComponentsInChildren<Button>();
            foreach (var button in skills)
            {
                button.interactable = true;
            }

            GameObject.Find("Button save").GetComponent<Button>().interactable = true;
            openCharacter.SetActive(false);
        }
    }

    public void PrepareTextDescription()
    {
        var character = YandexGame.savesData.characters[selectedOption];
        var listText = character.description.Split(';');
        descriptionText.text = String.Format("{0} \n {1} - {2} \n {3} - {4} \n {5} - {6}", listText[0], 
            character.SkillName[0], listText[1],
            character.SkillName[1], listText[2],
            character.SkillName[2], listText[3]);
    }
}
