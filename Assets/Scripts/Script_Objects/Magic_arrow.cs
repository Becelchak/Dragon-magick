using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magic_arrow : MonoBehaviour
{
    private double damage;
    private float speed;
    [SerializeField] private bool isExplosion = false;
    private Vector3 target;
    private Animator animator;
    private float timer;
    private float death_timer = 0;

    public void Initialize(Vector3 target, double damage = 20, float speed = 0.09f)
    {
        this.target = target;

        this.damage = damage;
        this.speed = speed;

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
        }
    }
}
