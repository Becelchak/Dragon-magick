using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private bool isDestroy = false;
    [SerializeField] private Sprite Sprite_Destroyed;
    private AudioSource buildingAudioSource;
    void Start()
    {
        buildingAudioSource = GetComponent<AudioSource>();
    }

    public void Destroy()
    {
        isDestroy = true;
        GetComponent<SpriteRenderer>().sprite = Sprite_Destroyed;

        if(!buildingAudioSource.isPlaying)
        {
            var destroyClip = Resources.Load<AudioClip>("Sound/building_destroy");
            buildingAudioSource.PlayOneShot(destroyClip);
        }
    }

    public bool Getstatus()
    {
        return isDestroy;
    }
}
