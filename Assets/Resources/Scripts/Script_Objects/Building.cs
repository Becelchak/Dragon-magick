using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private bool isDestroy = false;
    [SerializeField] private Sprite Sprite_Destroyed;
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Destroy()
    {
        isDestroy = true;
        GetComponent<SpriteRenderer>().sprite = Sprite_Destroyed;
    }

    public bool Getstatus()
    {
        return isDestroy;
    }
}
