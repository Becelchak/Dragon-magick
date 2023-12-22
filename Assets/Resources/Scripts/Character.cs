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


    public Character(string name, Sprite sprite, RuntimeAnimatorController animator, Sprite skill1, Sprite skill2, Sprite skill3, List<int> skills,
        List<string> SkillName, bool isUnlock, string description, List<int> PriceList)
    {
        this.name = name;
        this.sprite = sprite;
        this.animator = animator;
        this.skill1 = skill1;
        this.skill2 = skill2;
        this.skill3 = skill3;
        this.SkillLevel = skills;
        this.SkillName = SkillName;
        this.isUnlock = isUnlock;
        this.description = description;
        this.PriceList = PriceList;
    }
}
