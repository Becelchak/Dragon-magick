using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_HealPoint : MonoBehaviour
{
    [SerializeField] private double HealtPoint = 100;
    private bool isDead = false;
    private CanvasGroup GameEndUI;
    void Start()
    {
        GameEndUI = GameObject.Find("Win Menu").GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if(HealtPoint == 0)
            isDead = true;
        if (!isDead) return;
        GameEndUI.alpha = 1;
        GameEndUI.blocksRaycasts = true;
        GameEndUI.interactable = true;
    }

    public void GetDamage(double damage)
    {
        HealtPoint -= damage;
        if(HealtPoint < 0)
            HealtPoint = 0;
        Debug.Log($"Now healt_point dragon = {HealtPoint}");
    }

    public bool Dead()
    {
        return isDead;
    }
}
