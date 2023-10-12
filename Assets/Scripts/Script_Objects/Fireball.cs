using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed = 0.1f;
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

        if (timer < 2)
            timer += Time.deltaTime;
        if (timer > 2)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);

    }

    void OnTriggerEnter(Collider collision)
    {
        isExplosion = true;
    }
    void OnTriggerStay(Collider collision)
    {
        isExplosion = true;
    }
}
