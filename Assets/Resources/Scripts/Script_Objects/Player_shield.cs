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


    private AudioSource spellAudioSource;

    void Start()
    {
        spellAudioSource = GetComponent<AudioSource>();

        Shield.SetActive(false);
        animator = GetComponent<Animator>();
        timer = Life_time;

        var collider = Shield.GetComponent<BoxCollider>();
        var sprite = Shield.transform;

        collider.size *= shieldLevelCharacter;
        sprite.localScale *= shieldLevelCharacter;

    }

    private void OnEnable() => YandexGame.GetDataEvent += Prepare;

    void Prepare()
    {
        switch (YandexGame.savesData.skin)
        {
            case SavesYG.PlayerSkin.Wanderer:
                shieldLevelCharacter = YandexGame.savesData.characters[0].SkillLevel[3];
                break;
            case SavesYG.PlayerSkin.Cliric:
                shieldLevelCharacter = YandexGame.savesData.characters[1].SkillLevel[3];
                break;
            case SavesYG.PlayerSkin.Piromant:
                shieldLevelCharacter = YandexGame.savesData.characters[2].SkillLevel[3];
                break;
        }
    }

    void Update()
    {
        if (YandexGame.SDKEnabled)
            Prepare();
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
                var shueldSpell = Resources.Load<AudioClip>("Sound/Spell/Shield");
                spellAudioSource.PlayOneShot(shueldSpell);
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
