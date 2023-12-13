using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloude_Move : MonoBehaviour
{
    public GameObject border_right;
    public GameObject border_left;
    private float speed;

    private float timer = 0;
    void Start()
    {
        speed = Random.Range(0.6f, 2.2f);
    }

    void Update()
    {
        if (timer < 0.5)
            timer += Time.deltaTime;

        if (timer > 0.5)
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, 0);

            timer = 0;
        }

        if ((int)transform.position.x >= (int)border_right.gameObject.transform.position.x)
            transform.position = new Vector3(border_left.transform.position.x, transform.position.y, 0);

    }
}
