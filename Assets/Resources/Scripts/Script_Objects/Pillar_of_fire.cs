using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar_of_fire : MonoBehaviour
{
    [SerializeField] private int damage = 45;
    [SerializeField] private GameObject dragon;
    private float timer = 0.665f;


    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
            transform.position = new Vector3(
            dragon.transform.position.x,
            transform.position.y,
            transform.position.z);
        else
        {
            timer = 0.6f;
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Dragon"))
        {
            var health = other.GetComponent<Dragon_HealPoint>();
            health.GetDamage(damage);
        }
    }

    public void UpDamage(int modification)
    {
        damage += modification;
    }
}
