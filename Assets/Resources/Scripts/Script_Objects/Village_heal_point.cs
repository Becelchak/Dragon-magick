using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Village_heal_point : MonoBehaviour
{
    [SerializeField]
    private List<Building> village;
    private CanvasGroup GameEndUI;
    private bool IsVillageDestroyed = false;
    [SerializeField] private AudioSource mainAudioSource;
    void Start()
    {
        GameEndUI = GameObject.Find("Dead Menu").GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if(IsVillageDestroyed) return;
        foreach (var structure in village.Where(structure => structure.Getstatus()))
        {
            village.Remove(structure);
        }

        if (village.Count != 0) return;

        var defeatMusic = Resources.Load("Music/defeat") as AudioClip;
        mainAudioSource.PlayOneShot(defeatMusic);
        GameEndUI.alpha = 1;
        GameEndUI.interactable = true;
        GameEndUI.blocksRaycasts = true;
        IsVillageDestroyed = true;

    }

    public bool VillageStatus()
    {
        return IsVillageDestroyed;
    }
}
