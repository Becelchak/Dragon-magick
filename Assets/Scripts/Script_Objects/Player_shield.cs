using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shield : MonoBehaviour
{
    [SerializeField] public GameObject Shield;
    [SerializeField] public float Life_time = 1.5f;
    public float timer = 0;
    private Animator animator;
    public bool shieldActive = false;

    void Start()
    {
        Shield.SetActive(false);
        animator = GetComponent<Animator>();
        timer = Life_time;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;
        Shield.transform.position = mousePosition;
        if (Input.GetMouseButtonUp(1))
        {
            Shield.SetActive(false);
            shieldActive = false;
            animator.SetTrigger("Idle");
            timer = Life_time;
        }

        if (Input.GetMouseButton(1))
        {
            if (!shieldActive)
            {
                //Debug.Log($"{shieldActive}");
                animator.SetTrigger("Shield");
            }

            shieldActive = true;

            if (timer > 0)
            {
                Shield.SetActive(true);
                timer -= Time.deltaTime;
            }
            else if (timer <= 0)
            {
                Shield.SetActive(false);
                animator.SetTrigger("Idle");
            }
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }
}
