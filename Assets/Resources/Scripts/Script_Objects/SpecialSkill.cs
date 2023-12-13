using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSkill : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    private float timer = 1f;

    void OnTriggerStay(Collider collision)
    {
        // If dragon enter the zone -> dragon get damage
        timer -= Time.deltaTime;
        if (collision.name.Contains("Dragon") && timer <= 0)
        {
            collision.GetComponent<Dragon_HealPoint>().GetDamage(damage);
            timer = 1f;
        }
    }

    public void UpDamage(int modification)
    {
        damage += modification;
    }
}
