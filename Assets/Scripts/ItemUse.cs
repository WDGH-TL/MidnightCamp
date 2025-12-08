using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.VolumeComponent;

public class ItemUse : MonoBehaviour
{
    public PlayerInventory inventory;
    public AssignPrefab assign;
    public DropDown dropDown;
    public InventoryUI inventoryUI;
    public Transform playerHands;
    public Transform playerTransform;
    public GameObject itemPrefab;
    public GameObject grabbed;
    public float spawnDistance = 1f;
    public Transform spawnPosition;
    

    public void useItem()
    {
        Debug.Log("Using item");
        int slotIndex = inventory.selectedSlotIndex;
        string itemName = inventory.findNameItemInventory(slotIndex);
        GameObject itemPrefab = assign.assignedPrefab(itemName);
        if (grabbed != null)
        {
            Destroy(grabbed);
        }

        if (itemPrefab != null)
        {

            Vector3 targetPosition = spawnPosition.position + spawnPosition.forward * spawnDistance;
            Quaternion targetRotation = playerTransform.rotation;

            GameObject nuevoItem = Instantiate(itemPrefab, targetPosition, targetRotation);
            //nuevoItem.transform.localScale = Vector3.one;

            nuevoItem.transform.SetParent(playerHands);
            nuevoItem.SetActive(true);
            Rigidbody rb = nuevoItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            }
            grabbed = nuevoItem;

            Debug.Log($"Se ha creado el ítem: {nuevoItem.name}");
        }
        else
        {
            Debug.LogError("¡El Prefab del ítem no está asignado en el Inspector!");
        }
    }
    public void soltarDesdeInventario()
    {
        int slotIndex = inventory.selectedSlotIndex;
        string nameItem = inventory.findNameItemInventory(slotIndex);
        GameObject itemPrefab = assign.assignedPrefab(nameItem);

        Vector3 targetPosition = playerTransform.position + playerTransform.forward * spawnDistance;
        Quaternion targetRotation = playerTransform.rotation;

        GameObject itemSoltado = Instantiate(itemPrefab, targetPosition, targetRotation);
        //itemSoltado.transform.localScale = Vector3.one;

        itemSoltado.SetActive(true);
        Rigidbody rb = itemSoltado.GetComponent<Rigidbody>();
        rb = null;

        itemSoltado.transform.SetParent(null);


        inventory.RemoveItem(slotIndex);
        int dropdownIndex = dropDown.findIndex(nameItem);
        dropDown.delete(dropdownIndex);

        inventoryUI.RemoveItemFromSlot(slotIndex);

        if (grabbed != null)
        {
            Destroy(grabbed);
        }
    }
}
