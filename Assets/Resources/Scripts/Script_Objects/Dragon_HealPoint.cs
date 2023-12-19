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
    private AudioSource playerAudioSource;

    [SerializeField] 
    private AudioSource dragonAudioSource;
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
        healtPoint = YandexGame.savesData.NowDragon.health;
        percentHealth = healtPoint / 100;

        GameEndUI = GameObject.Find("Win Menu");
        GameEndUICanvas = GameEndUI.GetComponent<CanvasGroup>();
        mainClip = Resources.Load<AudioClip>("Music/Reveler's_Dance_60_second_loop");

        healthHover = healthBar.transform.GetChild(0).GetComponent<Image>();
        healthMain = healthBar.transform.GetChild(1).GetComponent<Image>();

        dragonAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        healthMain.fillAmount = healtPoint / (percentHealth * 100);
        if (healthHover.fillAmount != healthMain.fillAmount)
        {
            healthTimer -= Time.deltaTime;
            if (healthTimer <= 0)
            {
                Debug.Log($"healthHover = {healthHover.fillAmount}");
                Debug.Log($"healthMain = {healthMain.fillAmount}");
                healthTimer = 1.3f;
                healthHover.fillAmount = healthMain.fillAmount;

            }
        }
        if (healtPoint == 0)
            isDead = true;
        if (!isDead) return;

        if(goldReward != 0) return;
        GetTreasure();
    }

    public void GetDamage(float damage)
    {
        healtPoint -= damage;
        if (healtPoint - damage <= 0)
        {
            var death = Resources.Load<AudioClip>("Sound/dragon_death");
            dragonAudioSource.PlayOneShot(death);
            GetComponents<AudioSource>()[1].Stop();
        }
        else
        {
            var clipHit = Resources.Load<AudioClip>("Sound/dragon_hit");
            dragonAudioSource.PlayOneShot(clipHit);
        }

        if (healtPoint <= 0)
            healtPoint = 0;
        Debug.Log($"Now healt_point dragon = {healtPoint}");
    }

    public bool Dead()
    {
        return isDead;
    }

    private void GetTreasure()
    {
        var aplloudice = Resources.Load<AudioClip>("Music/Win_applause");
        playerAudioSource.PlayOneShot(aplloudice);
        mainAudioSource.clip = mainClip;
        mainAudioSource.Play();

        var random = new Random();
        var modifire = YandexGame.savesData.NowDragon.priceModifire;
        goldReward = random.Next(10 * modifire, 30 * modifire);
        YandexGame.savesData.gold += goldReward;

        GameEndUICanvas.alpha = 1;
        GameEndUICanvas.blocksRaycasts = true;
        GameEndUICanvas.interactable = true;

        var countGold = GameEndUI.transform.GetChild(4).GetComponent<Text>();
        countGold.text = goldReward.ToString();

        YandexGame.SaveProgress();
    }
}
