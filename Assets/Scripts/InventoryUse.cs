using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUse : MonoBehaviour
{
    public GameObject menuDesplegable;
    public GameObject menuCrafteo;
    public PlayerInventory restoration;

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
        Debug.Log("ConsumingItem");
        //restoration.ConsumeItem();
        UseItem();
    }
    public void UseItem()
    {

        int indexSlot = restoration.SelectedSlotIndex;

        if (indexSlot != -1)
        {
            Debug.Log($"Usando ítem del slot: {indexSlot}");
            restoration.ConsumeItem(indexSlot);


            restoration.SelectedSlotIndex = -1;
        }
        else
        {
            Debug.LogWarning("Ningún ítem seleccionado.");
        }
    }

}
