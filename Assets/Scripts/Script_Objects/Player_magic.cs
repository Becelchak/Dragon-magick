using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_magic : MonoBehaviour
{
    [SerializeField] private Image Mana_Bar;
    [SerializeField] private GameObject Skills;

    [SerializeField] private GameObject secondSkill;
    private float ThirdSkillLifetTime = 2f;
    private float timer = 0;
    private bool isThirdSkillUse = false;

    private Animator animator;
    private float mana = 1;
    private int skillNow = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = ThirdSkillLifetTime;
    }

    void Update()
    {
        if (isThirdSkillUse)
            timer -= Time.deltaTime;
        if (timer <= 0)
        {
            secondSkill.SetActive(false);
            timer = ThirdSkillLifetTime;
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
            switch (skillNow)
            {
                case 1:
                    if(mana >= 0.18f)
                    {
                        mana -= 0.18f;
                        animator.SetTrigger("Use_skill1");

                        var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        MousePos.z += Camera.main.nearClipPlane;
                        var arrow = new GameObject("Magic arrow");
                        arrow.transform.position = transform.position;
                        arrow.AddComponent<Magic_arrow>().Initialize(MousePos);
                    }
                    break;
                case 2:
                    if (mana >= 0.38f)
                    {
                        mana -= 0.38f;
                        animator.SetTrigger("Use_skill2");

                        var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        MousePos.z += Camera.main.nearClipPlane;
                        var arrow = new GameObject("Magic arrow");
                        arrow.transform.position = transform.position;
                        arrow.AddComponent<Magic_arrow>().Initialize(MousePos, damage: 40, speed: 0.02f);

                    }
                    break;
                case 3:
                    if (mana == 1)
                    {
                        //mana -= 1;
                        animator.SetTrigger("Use_skill3");

                        secondSkill.SetActive(true);
                        isThirdSkillUse = true;
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
        mana += manaPoint;
        if (mana > 1)
            mana = 1;
    }
}
