using UnityEngine;

[System.Serializable]
public struct RESOURCES
{
    public string name;
    public Sprite objectSprite;
    public bool isConsumable; // Variable para saber si el ítem es apto para consumo.

    // --- NUEVAS VARIABLES PARA CONSUMIBLES ---
    [Header("Consumable Effects")]
    public float hungerRestoration; // Cantidad de puntos de hambre que restaura.
    public float thirstRestoration; // Cantidad de puntos de sed que restaura.

    // Nota: Las variables de crafteo (ingredientes, etc.) usualmente van en un ScriptableObject de "Recipe"
}