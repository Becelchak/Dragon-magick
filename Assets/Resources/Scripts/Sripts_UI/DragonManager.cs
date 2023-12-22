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

    [SerializeField] private bool isBestiary;

    private int selectedOption = 0;


    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void GetData()
    {
        UpdateDragon();
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= YandexGame.savesData.dragons.Length)
            selectedOption = 0;

        UpdateDragon();
    }

    public void BackOption()
    {
        selectedOption--;

        if (selectedOption < 0)
            selectedOption = YandexGame.savesData.dragons.Length - 1;

        UpdateDragon();
    }

    public void UpdateDragon()
    {
        var dragon = YandexGame.savesData.dragons[selectedOption];
        Debug.Log($"{dragon.name}");
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
    }

    public void SaveChange()
    {
        YandexGame.savesData.enemy = selectedOption switch
        {
            0 => SavesYG.DragonType.Vivern,
            1 => SavesYG.DragonType.SwampDragon,
            2 => SavesYG.DragonType.MountainDragon,
            _ => YandexGame.savesData.enemy
        };
        YandexGame.savesData.NowDragon = YandexGame.savesData.dragons[selectedOption];
        YandexGame.SaveProgress();
    }

    public void CheckUnlocked(Dragon dragon)
    {
        Debug.Log($"{dragon.isUnloked}");
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
        var dragon = YandexGame.savesData.dragons[selectedOption];
        if (YandexGame.savesData.gold - dragon.price >= 0)
        {
            YandexGame.savesData.gold -= dragon.price;
            dragon.isUnloked = true;
        }
        UpdateDragon();
    }
}
