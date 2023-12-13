using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spirit_of_light : MonoBehaviour
{
    private float damage;
    private float speed;
    [SerializeField] private bool isExplosion = false;
    [SerializeField] public GameObject dragon;
    private Animator animator;
    private float timer;
    private float death_timer = 0;

    public void Initialize(float damageModification, float speed = 0.0425f)
    {
        dragon = GameObject.Find("Dragon");
        var rnd = new System.Random(179358246);
        damage = rnd.Next(15,35) + damageModification;
        this.speed = speed;

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(dragon.transform.position.y - transform.position.y, dragon.transform.position.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

    }

    void Start()
    {
        this.AddComponent<BoxCollider>();
        this.AddComponent<SpriteRenderer>();
        animator = GameObject.Find("Primal spirit of light").GetComponent<Animator>();

        if(name != "Primal spirit of light")
            this.AddComponent<Animator>().runtimeAnimatorController = animator.runtimeAnimatorController;

        // Set collider parameter
        var box_collider = GetComponent<BoxCollider>();
        box_collider.size = new Vector3(0.2f, 0.05f, 50);
        box_collider.isTrigger = true;

        var sprite = GetComponent<SpriteRenderer>();
        sprite.sortingLayerName = "GameObjects";
        sprite.sortingOrder = 4;

        // Change small size sprite
        transform.localScale = new Vector3(3, 4.5f, 1.1f);

    }

    void Update()
    {
        var rnd = new System.Random(179358246);

        if (isExplosion)
        {
            // Change big size sprite
            transform.localScale = new Vector3(6, 9, 2);
            death_timer += Time.deltaTime;
            if (death_timer > 0.6f)
                Destroy(gameObject);

        }
        else
        {
            if (timer < 1.2)
                timer += Time.deltaTime;
            if (timer > 1.2)
            {
                transform.position = Vector3.MoveTowards(transform.position, dragon.transform.position, speed);
                transform.position = new Vector3(
                    transform.position.x + rnd.Next(-1, 1) / 2,
                    transform.position.y + rnd.Next(-1, 1) /2, 
                    transform.position.z);
            }
        }

        if (transform.position == dragon.transform.position)
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
