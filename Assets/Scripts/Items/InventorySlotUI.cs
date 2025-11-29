using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    // Variables del slot
    public int slotIndex; // Identifica la posición en el array del PlayerInventory (0 a 5)
    public TextMeshProUGUI objectName;
    public Image spriteImage;
    public GameObject ItemUI; // El contenedor visual (panel o imagen del ítem)

    // Método para actualizar la visualización de este slot
    public void UpdateSlotUI(Items itemSO, int resourceIndex)
    {
        // 1. Verificar si el slot está vacío (itemSO == null)
        if (itemSO == null)
        {
            // Ocultar el ítem
            ItemUI.SetActive(false);
            spriteImage.sprite = null;
            objectName.text = "";
            return;
        }

        // 2. Si hay un ítem, obtener la información
        if (resourceIndex >= 0 && resourceIndex < itemSO.itemTemplate.Length)
        {
            RESOURCES itemData = itemSO.itemTemplate[resourceIndex];

            // 3. Mostrar la información y el nombre
            ItemUI.SetActive(true); // Mostrar el contenedor del ítem
            objectName.text = itemData.name;
            spriteImage.sprite = itemData.objectSprite;
        }
    }
}