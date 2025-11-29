using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public static PlayerInventory Instance;
    public Items[] itemInventory; // El array lógico de 36 espacios
    public int[] itemIndex;       // El array de índices de recursos


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        itemInventory = new Items[6];
        itemIndex = new int[6];
    }


    public void AddItemToInventory(RaycastHit hit)
    {
        ItemTemplate itemToAdd = hit.transform.GetComponent<ItemTemplate>();
        int addedToIndex = ItemManager.Instance.inventoryIndex; // Índice del recurso en el Scriptable Object

        // Itera desde el índice 0 (izquierda) buscando el primer slot libre
        for (int i = 0; i < itemInventory.Length; i++)
        {
            if (itemInventory[i] == null)
            {
                // 1. Añadir el ítem al inventario lógico en la posición 'i'
                itemInventory[i] = itemToAdd.itemText;
                itemIndex[i] = addedToIndex;

                // *** SINCRONIZACIÓN CLAVE AL AÑADIR ***
                // Se notifica al ItemManager, usando el índice 'i' para que sepa qué slot visual actualizar.
                ItemManager.Instance.UpdateSlotDisplay(i, itemToAdd.itemText, addedToIndex);

                Destroy(itemToAdd.gameObject);
                SaveInventory();
                break; // El bucle se rompe para asegurar que solo se añada un ítem
            }
        }
    }

    // Método para consumir un ítem y actualizar la UI
    public bool ConsumeItem(int inventorySlot)
    {
        if (inventorySlot < 0 || inventorySlot >= itemInventory.Length || itemInventory[inventorySlot] == null)
        {
            Debug.LogWarning("Slot de inventario inválido o vacío.");
            return false;
        }

        Items itemSO = itemInventory[inventorySlot];
        int resourceID = itemIndex[inventorySlot];
        RESOURCES itemData = itemSO.itemTemplate[resourceID];

        if (itemData.isConsumable)
        {
            // (Aplicar efectos de consumo a Hunger.cs y Thirst.cs...)

            // Eliminar el ítem del inventario lógico
            itemInventory[inventorySlot] = null;
            itemIndex[inventorySlot] = 0;

            // *** SINCRONIZACIÓN CLAVE AL CONSUMIR ***
            // Se notifica al ItemManager, pasando 'null' para que limpie el slot visual.
            ItemManager.Instance.UpdateSlotDisplay(inventorySlot, null, 0);

            SaveInventory();
            return true;
        }
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