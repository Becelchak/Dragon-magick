using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using static Fireball;
using static UnityEngine.GraphicsBuffer;

public class Dragon_move : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private GameObject End_point_for_start;
    [SerializeField] private GameObject Right_point;
    [SerializeField] private GameObject Left_point;
    [SerializeField] private GameObject Top_point;

    [SerializeField] private float Speed = 1;

    private bool is_came_in_village = false;
    private bool is_attack_fly = false;
    private float timer = 0;
    private int direction = 0;
    private string turn = "Left";

    [Header("Attack")]
    [SerializeField] private float Attack_time = 3;
    private float attack_rnd = 0;
    private float attack_timer = 0;
    private bool is_attack = false;
    private float coldown_attack = 5f;
    private float coldown_timer = 0;
    [SerializeField] private Animator animator;
    private GameObject target_for_attack;


    void Start()
    {
        animator = GetComponent<Animator>();
        Random.InitState(25696);
    }

    void Update()
    {
        // Check direction rotate dragon
        transform.rotation = turn switch
        {
            "Left" => new Quaternion(0, 0, 0, 0),
            "Right" => new Quaternion(0, 180, 0, 0),
            _ => transform.rotation
        };
        // When dragon only start fly -> he fly to the start point
        if (!is_came_in_village)
        {
            if (timer < 0.3)
                timer += Time.deltaTime;
            if (timer > 0.3)
            {
                transform.position = Vector3.MoveTowards(transform.position, End_point_for_start.transform.position, Speed);
                timer = 0;
            }
        }

        // When dragon came to start point -> activate attack fly with other logic 
        if (transform.position == End_point_for_start.transform.position && !is_came_in_village)
        {
            is_came_in_village = true;
            is_attack_fly = true;
            Debug.Log("Start combat");
        }

        if (is_attack_fly)
            Attack_moving();

    }

    private void Attack_moving()
    {
        // Random choose direction point
        if (direction == 0)
            direction = Random.Range(1, 3);
        // Random modificator for dragon speed every point
        var mod_speed = Random.Range(0.1f, 3.5f);

        //Attack module, when timers and random numbers determine the behavior of the dragon
        if (!is_attack)
        {
            coldown_timer += Time.deltaTime;
            Debug.Log($"cooldown {coldown_timer}");

        }
        // If attack - wait some second
        if (coldown_timer >= coldown_attack)
        {
            // Random time for attack
            if (!is_attack)
            {
                attack_rnd = Random.Range(2, 2);
                coldown_timer = 0;
            }
            if(attack_rnd is 4 or 2)
                Attack();
            Debug.Log($"attack {attack_rnd}");
        }
        // IF attack - don't move
        if (is_attack)
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
                    transform.position = Vector3.MoveTowards(transform.position, Right_point.transform.position,
                        Speed * direction);
                    timer = 0;
                }

                if (transform.position == Right_point.transform.position)
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
                    transform.position = Vector3.MoveTowards(transform.position, Left_point.transform.position,
                        Speed * direction);
                    timer = 0;
                }

                if (transform.position == Left_point.transform.position)
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
                    transform.position = Vector3.MoveTowards(transform.position, Top_point.transform.position,
                        Speed * direction);
                    timer = 0;
                }

                if (transform.position == Top_point.transform.position)
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
        if (!is_attack)
        {
            is_attack = true;
            animator.SetTrigger("Attack");

            var target_name = FindAttackTarget();
            var fireball = new GameObject($"Fireball on {target_name}");
            fireball.transform.position = transform.position;
            fireball.AddComponent<Fireball>().Initialize(target_for_attack);
            Debug.Log("Create fire");
        }
    }

    private void AttackEnding()
    {
        // Count time attack animation (TODO something better)
        if (is_attack)
            attack_timer += Time.deltaTime;
        // End animation attack
        if (attack_timer >= Attack_time && is_attack)
        {
            is_attack = false;
            animator.SetTrigger("Fly");
            attack_timer = 0;
            coldown_timer = 0;
            Debug.Log("End attack");
        }
    }

    private string FindAttackTarget()
    {
        var target_rnd = Random.Range(1, 6);
        var target_name = "Player";
        if (target_rnd < 6)
        {
            target_name = $"Village_building {target_rnd}";
            target_for_attack = GameObject.Find(target_name);
           
        }
        return target_name;
    }
}
