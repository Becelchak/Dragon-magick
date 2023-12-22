using UnityEngine;

[System.Serializable]
public class Dragon
{
    public string name;
    public Sprite sprite;
    public int health;
    public float speedMove;
    public float speedFireball;
    public string description;
    public int priceModifire;
    public bool isUnloked;
    public int price;

    public Dragon(string name, Sprite sprite, int health, float speedMove, float speedFireball, string description, int priceModifire, bool isUnloked, int price)
    {
        this.name = name;
        this.sprite = sprite;
        this.health = health;
        this.speedMove = speedMove;
        this.speedFireball = speedFireball;
        this.description = description;
        this.priceModifire = priceModifire;
        this.isUnloked = isUnloked;
        this.price = price;
    }
}
