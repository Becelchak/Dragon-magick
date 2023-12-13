using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] private List<AudioClip> musicClipList;
    private AudioSource audioSource;
    private int counter = 0;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (counter > musicClipList.Count)
                counter = 0;
            audioSource.PlayOneShot(musicClipList[counter]);
            counter++;
        }
    }
}
