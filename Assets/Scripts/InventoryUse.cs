using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUse : MonoBehaviour
{
    public GameObject menuDesplegable;
    public GameObject menuCrafteo;
    public PlayerInventory restoration;
    public CraftingManager callCrafting;

    public void Start()
    {
        menuDesplegable.SetActive(false);
        menuCrafteo.SetActive(false);
    }

    public void interact()
    {
        showMenu();
    }

    public void closeTab()
    {
        HideMenu();
    }
    public void showMenu()
    {
        if (menuDesplegable != null)
        {
            
            menuDesplegable.SetActive(true);
        }
    }

    public void craft()
    {
        callCrafting.crafting();

    }


    public void HideMenu()
    {
        if (menuDesplegable != null)
        {
            menuDesplegable.SetActive(false);
        }
    }
    public void showMenuCraft()
    {

        if (menuCrafteo != null)
        {

            menuCrafteo.SetActive(true);
        }

    }

    public void AddHungerOrThirst()
    {
        UseItem();
    }
    public void UseItem()
    {

        int indexSlot = restoration.selectedSlotIndex;

        if (indexSlot != -1)
        {
            restoration.ConsumeItem(indexSlot);


            restoration.selectedSlotIndex = -1;
        }
    }

}
