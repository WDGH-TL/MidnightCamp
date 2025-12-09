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
    public AudioSource consumeSFX;
    public int selectedSlotIndex { get; set; } = -1;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        itemInventory = new Items[5];
        itemIndex = new int[5];
        consumeSFX = GetComponent<AudioSource>();
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
            return false;
        }

        Items itemSO = itemInventory[inventorySlot];
        int resourceID = itemIndex[inventorySlot];
        RESOURCES itemData = itemSO.itemTemplate[resourceID];

        if (itemData.isConsumable)
        {
            if (Hunger.instance != null)
            {
                consumeSFX.Play();
                Hunger.instance.AddHunger(itemData.hungerRestoration);
            }
            if (Thirst.instance != null)
            {
                consumeSFX.Play();
                Thirst.instance.AddThirst(itemData.thirstRestoration);
            }
            string nombreItem = GetNameItem(inventorySlot);
            int dropdownIndex = craftingList.findIndex(nombreItem);

            if (dropdownIndex != -1)
            {
                craftingList.delete(dropdownIndex);
            }
            itemInventory[inventorySlot] = null;
            itemIndex[inventorySlot] = 0;
            inventoryUI.RemoveItemFromSlot(inventorySlot);
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

            itemValue = productData.hungerRestoration;

        }

        return itemValue;

    }
    public void SaveSlotClick(int slotIndex)
    {

        this.selectedSlotIndex = slotIndex;

    }
    public string findNameItemInventory(int index)
    {
        string itemName = "";
        Items item = itemInventory[index];
        int productIndex = itemIndex[index];

        if (productIndex >= 0 && productIndex < item.itemTemplate.Length)
        {
            RESOURCES productData = item.itemTemplate[productIndex];

            itemName = productData.name;
        }
        return itemName;
    }

    public int findItemInventoryByName(string itemName)
    {
        for (int i = 0; i < itemInventory.Length; i++)
        {
            Items item = itemInventory[i];

            if (item != null)
            {
                int productIndex = itemIndex[i];

                if (productIndex >= 0 && productIndex < item.itemTemplate.Length)
                {
                    RESOURCES productData = item.itemTemplate[productIndex];

                    if (productData.name.Equals(itemName, System.StringComparison.OrdinalIgnoreCase))
                    {
                        Debug.Log($"Ítem '{itemName}' encontrado en el slot índice: {i}");
                        return i;
                    }
                }
            }
        }

        Debug.LogWarning($"Ítem '{itemName}' no encontrado en el inventario.");
        return -1;
    }
    public string whoCraft(int index)
    {
        string nameItem = "";

        Items item = itemInventory[index];
        int productIndex = itemIndex[index];

        if (productIndex >= 0 && productIndex < item.itemTemplate.Length)
        {
            RESOURCES productData = item.itemTemplate[productIndex];

            nameItem = productData.nameWithCraft;


        }

        return nameItem;

    }
    public bool AddItemToInventoryInternal(Items item, int index)
    {
        for (int i = 0; i < itemInventory.Length; i++)
        {
            if (itemInventory[i] == null)
            {
                itemInventory[i] = item;
                itemIndex[i] = index;


                RESOURCES productData = item.itemTemplate[index];
                inventoryUI.drawNames(productData.name);
                inventoryUI.drawSprites(productData.objectSprite);

                SaveInventory();
                return true;
            }
        }
        return false;
    }
    public string GetNameItem(int index)
    {

        string nameItem = "";
        Items item = itemInventory[index];


        if (item != null)
        {
            int productIndex = itemIndex[index];
            if (productIndex >= 0 && productIndex < item.itemTemplate.Length)
            {
                RESOURCES productData = item.itemTemplate[productIndex];

                nameItem = productData.name;

            }
        }


        return nameItem;

    }
}