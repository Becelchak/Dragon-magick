using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Player_magic : MonoBehaviour
{
    [SerializeField] private Image Mana_Bar;
    [SerializeField] private GameObject Skills;

    [SerializeField] private GameObject starRain;
    [SerializeField] private GameObject thunder;
    [SerializeField] private GameObject pillarOfFire;

    [SerializeField] private CharacterDatabase CharacterDB;
    private float starRainLifeTime = 2f;
    private float thunderLifeTime = 1.5f;
    private float timer = 0;
    private bool isThirdSkillUse = false;

    private Animator animator;
    private float mana = 0;
    private int skillNow = 0;

    private GameObject dragon;
    private int manaLevel;
    private int firstSkilltLevel;
    private int secondSkilltLevel;
    private int thirdSkilltLevel;
    void Start()
    {
        animator = GetComponent<Animator>();
        dragon = GameObject.Find("Dragon");

        switch (YandexGame.savesData.skin)
        {
            case SavesYG.PlayerSkin.Wanderer:
                firstSkilltLevel = CharacterDB.characterList[0].SkillLevel[0];
                secondSkilltLevel = CharacterDB.characterList[0].SkillLevel[1];
                thirdSkilltLevel = CharacterDB.characterList[0].SkillLevel[2];
                manaLevel = CharacterDB.characterList[0].SkillLevel[4];
                break;
            case SavesYG.PlayerSkin.Cliric:
                firstSkilltLevel = CharacterDB.characterList[1].SkillLevel[0];
                secondSkilltLevel = CharacterDB.characterList[1].SkillLevel[1];
                thirdSkilltLevel = CharacterDB.characterList[1].SkillLevel[2];
                manaLevel = CharacterDB.characterList[1].SkillLevel[4];
                break;
            case SavesYG.PlayerSkin.Piromant:
                firstSkilltLevel = CharacterDB.characterList[2].SkillLevel[0];
                secondSkilltLevel = CharacterDB.characterList[2].SkillLevel[1];
                thirdSkilltLevel = CharacterDB.characterList[2].SkillLevel[2];
                manaLevel = CharacterDB.characterList[2].SkillLevel[4];
                break;
        }
    }

    void Update()
    {
        if (isThirdSkillUse)
        {
            timer -= Time.deltaTime;
            thunder.transform.position = dragon.transform.position;
        }
        if (timer <= 0)
        {
            starRain.SetActive(false);
            thunder.SetActive(false);
            switch (YandexGame.savesData.skin)
            {
                case SavesYG.PlayerSkin.Wanderer:
                    timer = starRainLifeTime;
                    break;
                case SavesYG.PlayerSkin.Cliric:
                    timer = thunderLifeTime;
                    break;
            }
        }
        string num;
        if (Input.anyKeyDown)
        {
            num = Input.inputString;
            switch (num)
            {
                case "1":
                    skillNow = 1;
                    GameObject.Find($"Skill {skillNow}").GetComponent<Outline>().enabled = true;
                    GameObject.Find($"Skill 2").GetComponent<Outline>().enabled = false;
                    GameObject.Find($"Skill 3").GetComponent<Outline>().enabled = false;
                    break;
                case "2":
                    skillNow = 2;
                    GameObject.Find($"Skill 1").GetComponent<Outline>().enabled = false;
                    GameObject.Find($"Skill {skillNow}").GetComponent<Outline>().enabled = true;
                    GameObject.Find($"Skill 3").GetComponent<Outline>().enabled = false;
                    break;
                case "3":
                    skillNow = 3;
                    GameObject.Find($"Skill 1").GetComponent<Outline>().enabled = false;
                    GameObject.Find($"Skill 2").GetComponent<Outline>().enabled = false;
                    GameObject.Find($"Skill {skillNow}").GetComponent<Outline>().enabled = true;
                    break;
                default:
                    break;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MousePos.z += Camera.main.nearClipPlane;
            switch (skillNow)
            {
                case 1:
                    if(mana >= 0.18f)
                    {
                        mana -= 0.18f;
                        animator.SetTrigger("Use_skill1");

                        switch (YandexGame.savesData.skin)
                        {
                            case SavesYG.PlayerSkin.Wanderer:
                                var arrow = new GameObject("Magic arrow");
                                arrow.transform.position = transform.position;
                                arrow.AddComponent<Magic_arrow>().Initialize(MousePos, damage: 20*(firstSkilltLevel * 0.5f));
                                break;
                            case SavesYG.PlayerSkin.Cliric:
                                var spirit = new GameObject("Spirit of light");
                                spirit.transform.position = transform.position;
                                spirit.AddComponent<Spirit_of_light>().Initialize(2 * firstSkilltLevel);
                                break;
                            case SavesYG.PlayerSkin.Piromant:
                                pillarOfFire.SetActive(true);
                                pillarOfFire.GetComponent<SpecialSkill>().UpDamage(3 * firstSkilltLevel);
                                break;
                        }
                    }
                    break;
                case 2:
                    if (mana >= 0.38f)
                    {
                        mana -= 0.38f;
                        animator.SetTrigger("Use_skill2");

                        switch (YandexGame.savesData.skin)
                        {
                            case SavesYG.PlayerSkin.Wanderer:
                                

                                var arrow = new GameObject("Magic arrow");
                                arrow.transform.position = transform.position;
                                arrow.AddComponent<Magic_arrow>().Initialize(MousePos, damage: 40 * (secondSkilltLevel * 0.5f), speed: 0.02f, type:"type2");
                                break;
                            case SavesYG.PlayerSkin.Cliric:

                                var arrow2 = new GameObject("Light sword");
                                arrow2.transform.position = transform.position;
                                arrow2.AddComponent<Magic_arrow>().Initialize(MousePos, damage: 35 * (secondSkilltLevel * 0.5f), speed: 0.01625f, type: "type3");
                                break;
                            case SavesYG.PlayerSkin.Piromant:
                                var arrow3 = new GameObject("Fireball piromant");
                                arrow3.transform.position = transform.position;

                                var rnd = new System.Random(951753268);
                                var randomDamage = rnd.Next(35, 70) + (3 * secondSkilltLevel);

                                arrow3.AddComponent<Magic_arrow>().Initialize(MousePos, damage: randomDamage, speed: 0.0105f, type: "type4");
                                break;
                        }

                    }
                    break;
                case 3:
                    if (mana == 1)
                    {
                        mana -= 1;
                        animator.SetTrigger("Use_skill3");
                        isThirdSkillUse = true;

                        switch (YandexGame.savesData.skin)
                        {
                            case SavesYG.PlayerSkin.Wanderer:

                                starRain.SetActive(true);
                                starRain.GetComponent<SpecialSkill>().UpDamage(3 * thirdSkilltLevel);
                                timer = starRainLifeTime;
                                break;
                            case SavesYG.PlayerSkin.Cliric:
                                thunder.SetActive(true);
                                thunder.GetComponent<SpecialSkill>().UpDamage(3 * thirdSkilltLevel);
                                timer = thunderLifeTime;
                                break;
                            case SavesYG.PlayerSkin.Piromant:
                                Vector3 targetLeft = MousePos, target = MousePos, targetright = MousePos;
                                targetLeft.x += (float)Math.Sin(targetLeft.x) * 5;
                                targetLeft.y += (float)Math.Cos(targetLeft.y) * 5;

                                targetright.x -= (float)Math.Sin(targetright.x) * 5;
                                targetright.y -= (float)Math.Cos(targetright.y) * 5;

                                var magma1 = new GameObject("Magma ball1");
                                var magma2 = new GameObject("Magma ball2");
                                var magma3 = new GameObject("Magma ball3");

                                magma1.transform.position = transform.position;
                                magma2.transform.position = transform.position;
                                magma3.transform.position = transform.position;

                                magma1.AddComponent<Magic_arrow>().Initialize(targetLeft, damage: 55 * (thirdSkilltLevel * 0.5f), speed: 0.01959f, type: "type4");
                                magma2.AddComponent<Magic_arrow>().Initialize(target, damage: 55 * (thirdSkilltLevel * 0.5f), speed: 0.01959f, type: "type4");
                                magma3.AddComponent<Magic_arrow>().Initialize(targetright, damage: 55 * (thirdSkilltLevel * 0.5f), speed: 0.01959f, type: "type4");
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        animator.SetTrigger("Idle");
        Mana_Bar.fillAmount = mana;
    }

    public void UpMana(float manaPoint)
    {
        mana += manaPoint * manaLevel;
        if (mana > 1)
            mana = 1;
    }
}
