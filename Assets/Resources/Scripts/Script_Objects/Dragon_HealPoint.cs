using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using YG;

public class Dragon_HealPoint : MonoBehaviour
{
    [SerializeField]
    private float healtPoint = 100;

    private float percentHealth;

    [SerializeField] 
    private AudioSource mainAudioSource;
    [SerializeField] 
    private AudioSource hoverAudioSource;
    private AudioClip mainClip;
    private bool isDead = false;

    [SerializeField] 
    private GameObject healthBar;
    private Image healthMain;
    private Image healthHover;
    private float healthTimer = 1.3f;

    private GameObject GameEndUI;
    private CanvasGroup GameEndUICanvas;
    private int goldReward;
    void Start()
    {
        percentHealth = healtPoint / 100;

        GameEndUI = GameObject.Find("Win Menu");
        GameEndUICanvas = GameEndUI.GetComponent<CanvasGroup>();
        mainClip = Resources.Load<AudioClip>("Music/Reveler's_Dance_60_second_loop");

        healthHover = healthBar.transform.GetChild(0).GetComponent<Image>();
        healthMain = healthBar.transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
        healthMain.fillAmount = healtPoint / (percentHealth * 100);
        if (Math.Abs(healthHover.fillAmount - healthMain.fillAmount) > 0.0001f)
        {
            healthTimer -= Time.deltaTime;
            if (healthTimer <= 0)
            {
                healthTimer = 1.3f;
                healthHover.fillAmount = healthMain.fillAmount;
            }
        }
        if (healtPoint == 0)
            isDead = true;
        if (!isDead) return;
        if(goldReward != 0) return;
        YandexGame.ResetSaveProgress();
        var aplloudice = Resources.Load<AudioClip>("Music/Win_applause");
        hoverAudioSource.PlayOneShot(aplloudice);
        mainAudioSource.clip = mainClip;
        mainAudioSource.Play();

        var random = new Random();
        goldReward = random.Next(10, 30);
        YandexGame.savesData.gold += goldReward;

        GameEndUICanvas.alpha = 1;
        GameEndUICanvas.blocksRaycasts = true;
        GameEndUICanvas.interactable = true;

        var countGold = GameEndUI.transform.GetChild(4).GetComponent<Text>();
        countGold.text = goldReward.ToString();

        YandexGame.SaveProgress();
    }

    public void GetDamage(float damage)
    {
        healtPoint -= damage;
        if(healtPoint < 0)
            healtPoint = 0;
        Debug.Log($"Now healt_point dragon = {healtPoint}");
    }

    public bool Dead()
    {
        return isDead;
    }
}
