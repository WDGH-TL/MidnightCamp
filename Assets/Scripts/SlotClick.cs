using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlotClick : MonoBehaviour
{
    public int slotIndex;

    private Button myButton;
    public TextMeshProUGUI tmp;
    public TextMeshProUGUI nameToGet;
    private string itemName;
    private int itemIndex;
    public PlayerInventory restoration;


    void Start()
    {
        myButton = GetComponent<Button>();

        myButton.onClick.AddListener(OnSlotClicked);
    }

    void OnSlotClicked()
    {
        itemName = tmp.text;
        itemIndex = slotIndex;
        Debug.Log(slotIndex);
        restoration.SaveSlotClick(slotIndex);
        nameToGet.text = itemName;
    }

    public string getNameItemSlot()
    {
        return itemName;
    }

    public int getIndexItemSlot()
    {
        return itemIndex;
    }
}