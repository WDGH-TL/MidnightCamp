using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public Items[] itemInventory;
    public int[] itemIndex;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        itemInventory = new Items[10];
        itemIndex = new int[10];
    }


    public void AddItemToInventory(RaycastHit hit)
    {
        ItemTemplate itemToAdd = hit.transform.GetComponent<ItemTemplate>();
        int addedToIndex = ItemManager.Instance.inventoryIndex;

        for (int i = 0; i < itemInventory.Length; i++)
        {
            if (itemInventory[i] == null)
            {
                itemInventory[i] = itemToAdd.itemText;
                itemIndex[i] = addedToIndex;
                Destroy(itemToAdd.gameObject);
                SaveInventory();
                break;
            }
        }
    }

    // --- NUEVO MÉTODO PARA CONSUMIR ITEMS ---
    public bool ConsumeItem(int inventorySlot)
    {
        // 1. Validar que el slot sea válido y contenga un ítem
        if (inventorySlot < 0 || inventorySlot >= itemInventory.Length || itemInventory[inventorySlot] == null)
        {
            Debug.LogWarning("Slot de inventario inválido o vacío.");
            return false;
        }

        Items itemSO = itemInventory[inventorySlot];
        int resourceID = itemIndex[inventorySlot];
        RESOURCES itemData = itemSO.itemTemplate[resourceID];

        // 2. Verificar si el ítem es consumible
        if (itemData.isConsumable)
        {
            // 3. Aplicar efectos: Llamar a los Singletons de Hambre y Sed
            if (Hunger.instance != null)
            {
                Hunger.instance.AddHunger(itemData.hungerRestoration);
            }
            if (Thirst.instance != null)
            {
                // Usamos la nueva función AddThirst en el script Thirst.cs
                Thirst.instance.AddThirst(itemData.thirstRestoration);
            }

            // 4. Eliminar el ítem del inventario
            itemInventory[inventorySlot] = null;
            itemIndex[inventorySlot] = 0;

            SaveInventory();
            Debug.Log($"Consumido: {itemData.name}. Restauró Hambre y Sed.");
            return true;
        }

        Debug.Log("Este ítem no es consumible.");
        return false;
    }

    public void SaveInventory()
    {
        List<string> inventoryEntries = new List<string>();

        for (int i = 0; i < itemInventory.Length; i++)
        {
            if (itemInventory[i] != null)
            {
                int productIndex = itemIndex[i];

                if (productIndex >= 0 && productIndex < itemInventory[i].itemTemplate.Length)
                {
                    RESOURCES productData = itemInventory[i].itemTemplate[productIndex];
                }
            }
        }

        string serializedInventory = string.Join("|", inventoryEntries);

        PlayerPrefs.SetString("PlayerInventoryData", serializedInventory);
        PlayerPrefs.Save();
    }
}