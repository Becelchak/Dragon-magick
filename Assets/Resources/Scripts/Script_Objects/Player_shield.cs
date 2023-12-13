using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using YG;

public class Player_shield : MonoBehaviour
{
    [SerializeField] public GameObject Shield;
    [SerializeField] private float Life_time = 1.5f;
    [SerializeField] private CharacterDatabase CharacterDB;
    private int shieldLevelCharacter = 1;
    public float timer = 0;
    private Animator animator;
    public bool shieldActive = false;

    void Start()
    {
        switch (YandexGame.savesData.skin)
        {
            case SavesYG.PlayerSkin.Wanderer:
                shieldLevelCharacter = CharacterDB.characterList[0].SkillLevel[3];
                break;
            case SavesYG.PlayerSkin.Cliric:
                shieldLevelCharacter = CharacterDB.characterList[1].SkillLevel[3];
                break;
            case SavesYG.PlayerSkin.Piromant:
                shieldLevelCharacter = CharacterDB.characterList[2].SkillLevel[3];
                break;
        }
        Shield.SetActive(false);
        animator = GetComponent<Animator>();
        timer = Life_time;

        var collider = Shield.GetComponent<BoxCollider>();
        var sprite = Shield.transform;

        collider.size *= shieldLevelCharacter;
        sprite.localScale *= shieldLevelCharacter;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;
        Shield.transform.position = mousePosition;
        if (Input.GetMouseButtonUp(1))
        {
            Shield.SetActive(false);
            shieldActive = false;
            animator.SetTrigger("Idle");
            timer = Life_time;
        }

        if (Input.GetMouseButton(1))
        {
            if (!shieldActive)
            {
                animator.SetTrigger("Shield");
            }

            shieldActive = true;

            if (timer > 0)
            {
                Shield.SetActive(true);
                timer -= Time.deltaTime;
            }
            else if (timer <= 0)
            {
                Shield.SetActive(false);
                animator.SetTrigger("Idle");
            }
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }
}
