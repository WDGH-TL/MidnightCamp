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
        Debug.Log("Interacting with an item");
        showMenu();
    }

    public void consume()
    {
        Debug.Log("Item is consumed");
        
    }

    public void closeTab()
    {
        Debug.Log("Closing Tab");
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
    }
}
