using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Player_set_character : MonoBehaviour
{
    [SerializeField] private GameObject skillsCanvas;
    [SerializeField] private CharacterDatabase characterDB;
    private Animator animator;
    private SavesYG.PlayerSkin character;

    void Start()
    {
        animator = GetComponent<Animator>();

        var skill1 = skillsCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        var skill2 = skillsCanvas.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
        var skill3 = skillsCanvas.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>();

        character = YandexGame.savesData.skin;

        switch (character)
        {
            case SavesYG.PlayerSkin.Wanderer:
                skill1.sprite = characterDB.characterList[0].skill1;
                skill2.sprite = characterDB.characterList[0].skill2;
                skill3.sprite = characterDB.characterList[0].skill3;
                break;
            case SavesYG.PlayerSkin.Cliric:
                skill1.sprite = characterDB.characterList[1].skill1;
                skill2.sprite = characterDB.characterList[1].skill2;
                skill3.sprite = characterDB.characterList[1].skill3;
                break;
            case SavesYG.PlayerSkin.Piromant:
                skill1.sprite = characterDB.characterList[2].skill1;
                skill2.sprite = characterDB.characterList[2].skill2;
                skill3.sprite = characterDB.characterList[2].skill3;
                break;
        }

        switch (character)
        {
            case SavesYG.PlayerSkin.Wanderer:
                animator.runtimeAnimatorController = Resources.Load("Animation/Wandered mage/Player_wanderer") as RuntimeAnimatorController;
                break;
            case SavesYG.PlayerSkin.Cliric:
                animator.runtimeAnimatorController = Resources.Load("Animation/Cliric mage/Player_cliric") as RuntimeAnimatorController;
                break;
            case SavesYG.PlayerSkin.Piromant:
                animator.runtimeAnimatorController = Resources.Load("Animation/Fire mage/Player_fire") as RuntimeAnimatorController;
                break;
        }
    }
}
