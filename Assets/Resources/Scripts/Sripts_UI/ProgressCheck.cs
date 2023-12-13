using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ProgressCheck : MonoBehaviour
{
    [SerializeField] private Text goldCount;
    void Start()
    {
    }

    void Update()
    {
        goldCount.text = YandexGame.savesData.gold.ToString();
    }
}
