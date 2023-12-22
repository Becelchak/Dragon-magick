using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magic_arrow : MonoBehaviour
{
    private float damage;
    private float speed;
    [SerializeField] private bool isExplosion = false;
    private Vector3 target;
    private Animator animator;
    private float timer;
    private float death_timer = 0;
    private String type;

    public void Initialize(Vector3 target, float damage = 20, float speed = 0.9f, string type = "common")
    {
        this.target = target;
        this.damage = damage;
        this.speed = speed;
        this.type = type;

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

    }

    void Start()
    {

        this.AddComponent<BoxCollider>();
        this.AddComponent<SpriteRenderer>();
        animator = GameObject.Find("Primal magic arrow").GetComponent<Animator>();

        this.AddComponent<Animator>().runtimeAnimatorController = animator.runtimeAnimatorController;

        // Set collider parameter
        var box_collider = GetComponent<BoxCollider>();
        box_collider.size = new Vector3(0.2f, 0.05f,50);
        box_collider.isTrigger = true;

        var sprite = GetComponent<SpriteRenderer>();
        sprite.sortingLayerName = "GameObjects";
        sprite.sortingOrder = 4;

        // Change small size sprite
        transform.localScale = new Vector3(6, 9, 1);

        switch (type)
        {
            case "type2":
                GetComponent<Animator>().SetTrigger("Arrow2");
                break;
            case "type3":
                GetComponent<Animator>().SetTrigger("LightSword");
                box_collider.size = new Vector3(1f, 0.03f, 50);
                break;
            case "type4":
                GetComponent<Animator>().SetTrigger("Fireball");
                box_collider.size = new Vector3(0.35f, 0.1f, 50);
                break;
            case "type5":
                GetComponent<Animator>().SetTrigger("Magma");
                box_collider.size = new Vector3(0.55f, 0.2f, 50);
                break;
            default:
                GetComponent<Animator>().SetTrigger("Arrow1");
                break;
        }

    }

    void Update()
    {
        if (isExplosion)
        {
            GetComponent<Animator>().SetTrigger("Explode");
            // Change big size sprite
            transform.localScale = new Vector3(3, 6, 1);
            death_timer += Time.deltaTime;
            if (death_timer > 1)
                Destroy(gameObject);

        }
        else
        {
            if (timer < 0.8)
                timer += Time.deltaTime;
            if (timer > 0.8)
                transform.position = Vector3.MoveTowards(transform.position, target, speed);
        }

        if (transform.position == target)
            isExplosion = true;

    }

    void OnTriggerEnter(Collider collision)
    {
        // If arrow strike dragon -> dragon get damage
        if (collision.name.Contains("Dragon"))
        {
            isExplosion = true;
            collision.GetComponent<Dragon_HealPoint>().GetDamage(damage);
            transform.position = collision.transform.position;
        }
    }
}
