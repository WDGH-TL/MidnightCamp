using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public Items[] itemInventory;
    public int[] itemIndex;
    public InventoryUI inventoryUI;
    public DropDown craftingList;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        itemInventory = new Items[5];
        itemIndex = new int[5];
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
                RESOURCES productData = itemInventory[i].itemTemplate[addedToIndex];
                craftingList.fill(productData.name);
                inventoryUI.drawNames(productData.name);
                inventoryUI.drawSprites(productData.objectSprite);
                Destroy(itemToAdd.gameObject);
                SaveInventory();
                break;
            }
        }
    }

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
            if (Hunger.instance != null)
            {
                Hunger.instance.AddHunger(itemData.hungerRestoration);
            }
            if (Thirst.instance != null)
            {
                Thirst.instance.AddThirst(itemData.thirstRestoration);
            }

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
    public void RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < itemInventory.Length)
        {
            itemInventory[slotIndex] = null;
            itemIndex[slotIndex] = 0;
            SaveInventory();
        }
    }
    public float GetHungerValue(int index)
    {
        float itemValue = 0;
        Items item = itemInventory[index];
        int productIndex = itemIndex[index];

        if (productIndex >= 0 && productIndex < item.itemTemplate.Length)
        {
            RESOURCES productData = item.itemTemplate[productIndex];

            Debug.Log("Item found (SO): " + item.name);
            Debug.Log("Product Name: " + productData.name);
            Debug.Log("Product Value: " + productData.hungerRestoration);
            Debug.Log("Product value type: " + productData.GetType());
            itemValue = productData.hungerRestoration;

        }

        return itemValue;

    }
}