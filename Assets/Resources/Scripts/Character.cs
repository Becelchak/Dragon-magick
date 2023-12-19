using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string name;
    public Sprite sprite;
    public RuntimeAnimatorController animator;
    public Sprite skill1;
    public Sprite skill2;
    public Sprite skill3;
    public List<int> SkillLevel;
    public List<string> SkillName;
    public bool isUnlock;
    public string description;
    public List<int> PriceList;


    public Character(List<int> skills)
    {
        this.SkillLevel = skills;
    }
}
