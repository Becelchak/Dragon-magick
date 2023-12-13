using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;


[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{
    public Character[] characterList;

    public int CharacterCount
    {
        get
        {
            return characterList.Length;
        }
    }

    public Character GetCharacter(int index)
    {
        return characterList[index];
    }
}
