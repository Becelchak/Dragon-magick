using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using YG;
using static Fireball;
using static UnityEngine.GraphicsBuffer;

public class Dragon_move : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private GameObject endPointStart;
    [SerializeField] private GameObject rightPoint;
    [SerializeField] private GameObject leftPoint;
    [SerializeField] private GameObject topPoint;
    [SerializeField] private GameObject deathPoint;

    [SerializeField] private float speed = 1;

    private bool isCameInVillage = false;
    private bool isAttackFly = false;
    private float timer = 0;
    private int direction = 0;
    private string turn = "Left";

    [Header("Attack")]
    [SerializeField] private float attackTime = 3;
    private float attackRandom = 0;
    private float attackTimer = 0;
    private bool isAttack = false;
    private float coldownAttack = 5f;
    private float coldownTimer = 0;
    [SerializeField] private Animator animator;
    private GameObject targetForAttack;
    private AudioSource dragonAudioSource;
    private AudioClip attackClip;

    [Header("player and village status")] 
    [SerializeField] private GameObject player;

    private Player_shield shieldCLass;
    private Dragon_HealPoint Health;


    void Start()
    {
        animator = GetComponent<Animator>();
        shieldCLass = player.GetComponent<Player_shield>();
        Random.InitState(25696);
        Health = GetComponent<Dragon_HealPoint>();
        dragonAudioSource = GetComponent<AudioSource>();
        attackClip = Resources.Load<AudioClip>("Sound/dragon_attack");

    }

    private void OnEnable() => YandexGame.GetDataEvent += Prepare;

    void Prepare()
    {
        speed = YandexGame.savesData.NowDragon.speedMove;
    }

    void Update()
    {

        if(YandexGame.SDKEnabled)
            Prepare();
        // if Dead -> stop all end move to death point
        if (GetComponent<Dragon_HealPoint>().Dead())
        {
            GetComponent<SpriteRenderer>().sortingOrder = 5;
            if (timer < 0.3)
                timer += Time.deltaTime;
            if (timer > 0.3)
            {
                transform.position = Vector3.MoveTowards(transform.position, deathPoint.transform.position, speed);
                timer = 0;
            }
            animator.SetTrigger("Dead");
            return;
        }
        // Check direction rotate dragon
        transform.rotation = turn switch
        {
            "Left" => new Quaternion(0, 0, 0, 0),
            "Right" => new Quaternion(0, 180, 0, 0),
            _ => transform.rotation
        };
        // When dragon only start fly -> he fly to the start point
        if (!isCameInVillage)
        {
            if (timer < 0.3)
                timer += Time.deltaTime;
            if (timer > 0.3)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPointStart.transform.position, speed);
                timer = 0;
            }
        }

        // When dragon came to start point -> activate attack fly with other logic 
        if (transform.position == endPointStart.transform.position && !isCameInVillage)
        {
            isCameInVillage = true;
            isAttackFly = true;
            Debug.Log("Start combat");
        }

        if (isAttackFly)
            Attack_moving();

    }

    private void Attack_moving()
    {
        // Random choose direction point
        if (direction == 0)
            direction = Random.Range(2, 2);
        // Random modificator for dragon speed every point
        var mod_speed = Random.Range(0.1f, 3.5f);

        //Attack module, when timers and random numbers determine the behavior of the dragon
        if (!isAttack)
        {
            coldownTimer += Time.deltaTime;

        }
        // If attack - wait some second
        if (coldownTimer >= coldownAttack)
        {
            // Random time for attack
            if (!isAttack)
            {
                attackRandom = Random.Range(2, 5);
                coldownTimer = 0;
            }
            if(attackRandom is 4 or 2)
                Attack();
        }
        // IF attack - don't move
        if (isAttack)
        {
            AttackEnding();
            return;
        }
        // Fly module between points
        //Debug.Log($"direction {direction}");
        switch (direction)
        {
            case 1:
                turn = "Right";
                if (timer < 0.3)
                    timer += Time.deltaTime;
                if (timer > 0.3)
                {
                    transform.position = Vector3.MoveTowards(transform.position, rightPoint.transform.position,
                        speed * direction);
                    timer = 0;
                }

                if (transform.position == rightPoint.transform.position)
                {
                    direction = Random.Range(2, 3);
                    Debug.Log("End right");
                }

                break;
            case 2:
                turn = "Left";
                if (timer < 0.3)
                    timer += Time.deltaTime;
                if (timer > 0.3)
                {
                    transform.position = Vector3.MoveTowards(transform.position, leftPoint.transform.position,
                        speed * direction);
                    timer = 0;
                }

                if (transform.position == leftPoint.transform.position)
                {
                    direction = 3;
                    turn = "Right";
                    Debug.Log("End left");
                }
                break;
            case 3:
                turn = "Top";
                if (timer < 0.3)
                    timer += Time.deltaTime;
                if (timer > 0.3)
                {
                    transform.position = Vector3.MoveTowards(transform.position, topPoint.transform.position,
                        speed * direction);
                    timer = 0;
                }

                if (transform.position == topPoint.transform.position)
                {
                    direction = Random.Range(1, 3);
                    Debug.Log("End Top");
                }
                break;
        }
    }

    private void Attack()
    {
        // Start animation attack
        if (!isAttack && !Health.Dead())
        {
            dragonAudioSource.PlayOneShot(attackClip);

            isAttack = true;
            animator.SetTrigger("Attack");

            var target_name = FindAttackTarget();
            var fireball = new GameObject($"Fireball on {target_name}");
            fireball.transform.position = transform.position;
            fireball.AddComponent<Fireball>().Initialize(targetForAttack);
        }
    }

    private void AttackEnding()
    {
        // Count time attack animation (TODO something better)
        if (isAttack)
            attackTimer += Time.deltaTime;
        // End animation attack
        if (attackTimer >= attackTime && isAttack)
        {
            isAttack = false;
            animator.SetTrigger("Fly");
            attackTimer = 0;
            coldownTimer = 0;
        }
    }

    private string FindAttackTarget()
    {
        var target_rnd = Random.Range(1, 6);
        var target_name = "player";
        if (player.GetComponent<Village_heal_point>().VillageStatus()) 
            return target_name;
        else if (target_rnd < 6)
        {
            target_name = $"Village_building {target_rnd}";
            targetForAttack = GameObject.Find(target_name);
            if (targetForAttack.GetComponent<Building>().Getstatus())
                FindAttackTarget();

        }
        return target_name;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Magic_shield") return;
        shieldCLass.Shield.SetActive(false);
        shieldCLass.shieldActive = false;
        animator.SetTrigger("Idle");
        shieldCLass.timer = 0;
    }
}
