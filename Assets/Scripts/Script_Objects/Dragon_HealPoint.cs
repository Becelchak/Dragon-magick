using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_HealPoint : MonoBehaviour
{
    [SerializeField] private double Healt_Point = 100;
    private bool isDead = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(Healt_Point == 0)
            isDead = true;
    }

    public void GetDamage(double damage)
    {
        Healt_Point -= damage;
        if(Healt_Point < 0)
            Healt_Point = 0;
        Debug.Log($"Now healt_point dragon = {Healt_Point}");
    }

    public bool Dead()
    {
        return isDead;
    }
}
