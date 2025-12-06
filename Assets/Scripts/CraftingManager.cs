using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public int slotIndex;
    public PlayerInventory inventory;
    public DropDown dropDown;
    public Items scriptableObjects;
    public InventoryUI inventoryUI;
    public int scriptableResult;

    public void crafting()
    {
        int indexA = inventory.selectedSlotIndex;
        string slotSelectedName = inventory.findNameItemInventory(indexA);
        string itemnSelected = dropDown.selectedDropDown;

        int indexB = inventory.findItemInventoryByName(itemnSelected);

        string itemB = inventory.whoCraft(indexA);
        if (itemB == itemnSelected)
        {
            inventory.RemoveItem(indexA);
            inventory.RemoveItem(indexB);
            int dropdownIndexA = dropDown.findIndex(slotSelectedName);
            dropDown.delete(dropdownIndexA);
            int dropdownIndexB = dropDown.findIndex(itemnSelected);
            dropDown.delete(dropdownIndexB);

            inventoryUI.RemoveItemFromSlot(indexA);
            inventoryUI.RemoveItemFromSlot(indexB);


            inventory.AddItemToInventoryInternal(scriptableObjects, scriptableResult);

        }
        else
        {

            // messageCraft.text = "esto no se puede craftear...";
        }
    }
}
