using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ProgressCheck : MonoBehaviour
{
    [SerializeField] 
    [CanBeNull] private Text goldCount;

    [SerializeField] 
    private Text price;

    [SerializeField] 
    private int number;
    void Start()
    {
    }

    void Update()
    {
        if(goldCount != null)
            goldCount.text = YandexGame.savesData.gold.ToString();
        else
        {
            price.text = $"Цена: {YandexGame.savesData.NowCharacter.PriceList[number].ToString()}";
        }
    }
}
