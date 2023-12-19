using UnityEngine;

[CreateAssetMenu]
public class DragonDatabase : ScriptableObject
{
    public Dragon[] dragonsList;

    public int DragonCount
    {
        get
        {
            return dragonsList.Length;
        }
    }

    public Dragon GetDragon(int index)
    {
        return dragonsList[index];
    }
}
