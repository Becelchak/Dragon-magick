using System;
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

    private AudioSource spellAudioSource;
    void Start()
    {
        animator = GetComponent<Animator>();
        spellAudioSource = GetComponent<AudioSource>();
        dragon = GameObject.Find("Dragon");

    }

    void Prepare()
    {
        switch (YandexGame.savesData.skin)
        {
            case SavesYG.PlayerSkin.Wanderer:
                firstSkilltLevel = YandexGame.savesData.characters[0].SkillLevel[0];
                secondSkilltLevel = YandexGame.savesData.characters[0].SkillLevel[1];
                thirdSkilltLevel = YandexGame.savesData.characters[0].SkillLevel[2];
                manaLevel = YandexGame.savesData.characters[0].SkillLevel[4];
                break;
            case SavesYG.PlayerSkin.Cliric:
                firstSkilltLevel = YandexGame.savesData.characters[1].SkillLevel[0];
                secondSkilltLevel = YandexGame.savesData.characters[1].SkillLevel[1];
                thirdSkilltLevel = YandexGame.savesData.characters[1].SkillLevel[2];
                manaLevel = YandexGame.savesData.characters[1].SkillLevel[4];
                break;
            case SavesYG.PlayerSkin.Piromant:
                firstSkilltLevel = YandexGame.savesData.characters[2].SkillLevel[0];
                secondSkilltLevel = YandexGame.savesData.characters[2].SkillLevel[1];
                thirdSkilltLevel = YandexGame.savesData.characters[2].SkillLevel[2];
                manaLevel = YandexGame.savesData.characters[2].SkillLevel[4];
                break;
        }
    }

    void Update()
    {
        if (YandexGame.SDKEnabled)
            Prepare();
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

                                var spellArrow = Resources.Load<AudioClip>("Sound/Spell/MagicSpell");
                                spellAudioSource.PlayOneShot(spellArrow);
                                break;
                            case SavesYG.PlayerSkin.Cliric:
                                var spirit = new GameObject("Spirit of light");
                                spirit.transform.position = transform.position;
                                spirit.AddComponent<Spirit_of_light>().Initialize(2 * firstSkilltLevel);

                                var spellSpirit = Resources.Load<AudioClip>("Sound/Spell/SpiritOfLight");
                                spellAudioSource.PlayOneShot(spellSpirit);
                                break;
                            case SavesYG.PlayerSkin.Piromant:
                                pillarOfFire.SetActive(true);
                                pillarOfFire.GetComponent<Pillar_of_fire>().UpDamage(3 * firstSkilltLevel);

                                var spellPillar = Resources.Load<AudioClip>("Sound/Spell/PillarOfFire");
                                spellAudioSource.PlayOneShot(spellPillar);
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
                                arrow.AddComponent<Magic_arrow>().Initialize(MousePos, damage: 40 * (secondSkilltLevel * 0.5f), speed: 0.2f, type:"type2");
                                var spellArrow = Resources.Load<AudioClip>("Sound/Spell/MagicBall");
                                spellAudioSource.PlayOneShot(spellArrow);
                                break;
                            case SavesYG.PlayerSkin.Cliric:

                                var arrow2 = new GameObject("Light sword");
                                arrow2.transform.position = transform.position;
                                arrow2.AddComponent<Magic_arrow>().Initialize(MousePos, damage: 35 * (secondSkilltLevel * 0.5f), speed: 0.1625f, type: "type3");

                                var spellSword = Resources.Load<AudioClip>("Sound/Spell/SunBlade");
                                spellAudioSource.PlayOneShot(spellSword);
                                break;
                            case SavesYG.PlayerSkin.Piromant:
                                var arrow3 = new GameObject("Fireball piromant");
                                arrow3.transform.position = transform.position;

                                var rnd = new System.Random(951753268);
                                var randomDamage = rnd.Next(35, 70) + (3 * secondSkilltLevel);

                                arrow3.AddComponent<Magic_arrow>().Initialize(MousePos, damage: randomDamage, speed: 0.105f, type: "type4");

                                var spellFire = Resources.Load<AudioClip>("Sound/Spell/FireBall");
                                spellAudioSource.PlayOneShot(spellFire);
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

                                var spellStarRain = Resources.Load<AudioClip>("Sound/Spell/StarRain");
                                spellAudioSource.PlayOneShot(spellStarRain);
                                break;
                            case SavesYG.PlayerSkin.Cliric:
                                thunder.SetActive(true);
                                thunder.GetComponent<SpecialSkill>().UpDamage(3 * thirdSkilltLevel);
                                timer = thunderLifeTime;

                                var spellThunder = Resources.Load<AudioClip>("Sound/Spell/Thunder");
                                spellAudioSource.PlayOneShot(spellThunder);
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

                                magma1.AddComponent<Magic_arrow>().Initialize(targetLeft, damage: 55 * (thirdSkilltLevel * 0.5f), speed: 0.1959f, type: "type4");
                                magma2.AddComponent<Magic_arrow>().Initialize(target, damage: 55 * (thirdSkilltLevel * 0.5f), speed: 0.1959f, type: "type4");
                                magma3.AddComponent<Magic_arrow>().Initialize(targetright, damage: 55 * (thirdSkilltLevel * 0.5f), speed: 0.1959f, type: "type4");

                                var spellMagma = Resources.Load<AudioClip>("Sound/Spell/MagmaBall");
                                spellAudioSource.PlayOneShot(spellMagma);
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
