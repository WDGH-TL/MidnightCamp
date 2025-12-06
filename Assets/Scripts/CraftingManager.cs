using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public int slotIndex;
    public PlayerInventory inventory;
    public DropDown dropDown;
    public Items scriptableObjects;

    public void crafting()
    {
        int indexA = inventory.selectedSlotIndex;
        string textB = dropDown.selectedDropDown;
        Debug.Log("Begin Crafting" + indexA);
        Debug.Log("Begin Crafting" + textB);
    }
}
