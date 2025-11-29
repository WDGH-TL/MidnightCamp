using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    // *** NUEVA VARIABLE CLAVE: Array que contendrá la referencia a los 6 scripts InventorySlotUI ***
    public InventorySlotUI[] inventorySlotsUI;

    // Variables originales mantenidas
    public TextMeshProUGUI objectName;
    public Image spriteImage;
    public int inventoryIndex;
    public GameObject ItemUI;

    private void Awake()
    {
        Instance = this;
    }

    // --- NUEVO MÉTODO CLAVE: Notifica a un slot específico de la UI para que se actualice ---
    public void UpdateSlotDisplay(int slotIndex, Items itemSO, int resourceIndex)
    {
        // Solo actualiza si el índice que viene del PlayerInventory (0-35)
        // se encuentra dentro del rango de slots visibles (0-5).
        if (slotIndex >= 0 && slotIndex < inventorySlotsUI.Length)
        {
            inventorySlotsUI[slotIndex].UpdateSlotUI(itemSO, resourceIndex);
        }
    }

    // Funciones originales (su uso ahora es más para detalles o interacciones secundarias)

    public void Display(Items item)
    {
        // Esta función se puede usar para mostrar detalles del ítem seleccionado.
        // objectName.text = item.itemTemplate[inventoryIndex].name; 
    }

    public string GetProductName(Items item)
    {
        // Esta función se mantiene, pero su uso directo puede ser limitado con la nueva estructura.
        // return item.itemTemplate[inventoryIndex].name;
        return "Not Implemented for general use";
    }
}