using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string name;
    public Sprite sprite;
    public AnimatorController animator;
    public Sprite skill1;
    public Sprite skill2;
    public Sprite skill3;
    public List<int> SkillLevel;
    public List<string> SkillName;
}
