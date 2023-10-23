using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shield : MonoBehaviour
{
    [SerializeField] private GameObject Shield;

    void Start()
    {
        Shield.SetActive(false);
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;
        Shield.transform.position = mousePosition;
        if (Input.GetMouseButton(1))
            Shield.SetActive(true);
        if (Input.GetMouseButtonUp(1))
            Shield.SetActive(false);
    }
}
