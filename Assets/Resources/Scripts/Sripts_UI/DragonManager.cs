using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class DragonManager : MonoBehaviour
{
    [SerializeField] private DragonDatabase dragonDB;
    [SerializeField] private GameObject openDragon;
    public Text nameDragon;
    public Image spriteDragon;
    [CanBeNull] public Text description;
    [CanBeNull] public Text parameter;

    private int selectedOption = 0;
    void Start()
    {
        if (YandexGame.SDKEnabled)
            UpdateDragon();

    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= dragonDB.DragonCount)
            selectedOption = 0;

        UpdateDragon();
    }

    public void BackOption()
    {
        selectedOption--;

        if (selectedOption < 0)
            selectedOption = dragonDB.DragonCount - 1;

        UpdateDragon();
    }

    public void UpdateDragon()
    {
        var dragon = dragonDB.GetDragon(selectedOption);
        CheckUnlocked(dragon);
        spriteDragon.sprite = dragon.sprite;
        nameDragon.text = dragon.name;
        if(description != null)
            description.text = dragon.description;
        if (parameter != null)
        {
            var speedWord = dragon.speedMove % 10 == 1 ? "узел" : "узлов";
            parameter.text = string.Format("Здоровье: {0} \n Скорость: {1} {2} \n Увеличение награды: {3}", dragon.health,
                dragon.speedMove, speedWord, dragon.priceModifire);
        }
        YandexGame.savesData.NowDragon = dragon;

        switch (selectedOption)
        {
            case 0:
                YandexGame.savesData.enemy = SavesYG.DragonType.Vivern;
                break;
            case 1:
                YandexGame.savesData.enemy = SavesYG.DragonType.SwampDragon;
                break;
            case 2:
                YandexGame.savesData.enemy = SavesYG.DragonType.MountainDragon;
                break;
        }

        SaveChange();

    }

    public void SaveChange()
    {
        YandexGame.SaveProgress();
    }

    public void CheckUnlocked(Dragon dragon)
    {
        if (!dragon.isUnloked)
        {
            openDragon.SetActive(true);
            openDragon.transform.GetChild(1).GetComponent<Text>().text = $"Цена: {dragon.price}";

        }
        else
        {
            openDragon.SetActive(false);
        }
    }

    public void UnlockDragon()
    {
        var dragon = dragonDB.GetDragon(selectedOption);
        if (YandexGame.savesData.gold - dragon.price >= 0)
        {
            YandexGame.savesData.gold -= dragon.price;
            dragon.isUnloked = true;
        }
        UpdateDragon();
    }
}
