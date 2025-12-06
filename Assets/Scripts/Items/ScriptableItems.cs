using UnityEngine;

[System.Serializable]
public struct RESOURCES
{
    public string name;
    public Sprite objectSprite;
    public bool isConsumable;
    public bool isCraftable;
    public string nameWithCraft;

    [Header("Consumable Effects")]
    public float hungerRestoration; 
    public float thirstRestoration; 

}