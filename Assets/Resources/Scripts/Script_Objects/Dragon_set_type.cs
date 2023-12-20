using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Dragon_set_type : MonoBehaviour
{
    [SerializeField] private DragonDatabase dragonDB;
    private Animator animator;
    private SavesYG.DragonType dragon;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() => YandexGame.GetDataEvent += Prepare;

    void Prepare()
    {
        dragon = YandexGame.savesData.enemy;

        switch (dragon)
        {
            case SavesYG.DragonType.Vivern:
                animator.SetTrigger("Vivern");
                break;
            case SavesYG.DragonType.SwampDragon:
                animator.SetTrigger("Swamp");
                break;
            case SavesYG.DragonType.MountainDragon:
                animator.SetTrigger("Mountain");
                break;
        }
    }

    void Update()
    {
        if (YandexGame.SDKEnabled)
            Prepare();
    }
}
