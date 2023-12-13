using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed = 0.05f;
    [SerializeField] private bool isExplosion = false;
    private GameObject target;
    private Animator animator;
    private float timer;
    private float death_timer = 0;
    void Start()
    {

        this.AddComponent<SphereCollider>();
        this.AddComponent<SpriteRenderer>();
        animator = GameObject.Find("Primal fireball").GetComponent<Animator>();

        this.AddComponent<Animator>().runtimeAnimatorController = animator.runtimeAnimatorController;

        var box_collider = GetComponent<SphereCollider>();
        box_collider.radius = 0.4f;
        box_collider.isTrigger = true;

        var sprite = GetComponent<SpriteRenderer>();
        sprite.sortingLayerName = "GameObjects";
        sprite.sortingOrder = 4;
    }

    public  void Initialize(GameObject target)
    {
        this.target = target;

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

    }
    void Update()
    {
        if (isExplosion)
        {
            GetComponent<Animator>().SetTrigger("Explode");
            death_timer += Time.deltaTime;
            if (death_timer > 1)
                Destroy(gameObject);

        }
        else
        {
            if (timer < 2)
                timer += Time.deltaTime;
            if (timer > 2)
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        // If fireball strike building -> destroy building
        if (collision.name.Contains("Village"))
        {
            isExplosion = true;
            collision.GetComponent<Building>().Destroy();
        }
        // If player catch fireball -> up his mana
        else if (collision.name.Contains("Magic_shield"))
        {
            isExplosion = true;
            var player = GameObject.Find("Player");
            player.GetComponent<Player_magic>().UpMana(0.2f);
        }
    }
    void OnTriggerStay(Collider collision)
    {
        // If fireball strike building -> destroy building
        if (collision.name.Contains("Village"))
        {
            isExplosion = true;
            collision.GetComponent<Building>().Destroy();
        }
            
    }
}
